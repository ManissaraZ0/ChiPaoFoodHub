using Microsoft.EntityFrameworkCore;
using FoodHubLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodHubLogic
{
    public class DomainLogic
    {
        private readonly string connectionString;

        // 1. แก้ไขชื่อ Constructor ให้ตรงกับชื่อ Class ใหม่
        public DomainLogic(string connectionString)
        {
            this.connectionString = connectionString;
        }

        // --- Validation Helpers ---
        private void ValidateRole(User user, string expectedRole, string message)
        {
            if (!string.Equals(user.Role, expectedRole, StringComparison.OrdinalIgnoreCase))
                throw new Exception($"{message}: User role must be '{expectedRole}'.");
        }

        private void ValidateTicketStatus(PromotionTicket ticket, string status, string message)
        {
            if (!string.Equals(ticket.Status, status, StringComparison.OrdinalIgnoreCase))
                throw new Exception($"{message}: Ticket status must be '{status}'.");
        }

        // --- Restaurant & Promotion Management (Manager) ---
        public void CreatePromotion(int managerId, int restaurantId, Promotion newPromotion)
        {
            using var context = new FoodhubContext(connectionString);
            context.Database.BeginTransaction();

            var manager = context.Users.Single(u => u.Id == managerId);
            ValidateRole(manager, "manager", "Cannot create promotion");

            var restaurant = context.Restaurants.Single(r => r.Id == restaurantId);
            if (restaurant.ManagerId != managerId)
                throw new Exception("Cannot create promotion: Manager does not own this restaurant.");

            newPromotion.RestaurantId = restaurantId;
            context.Promotions.Add(newPromotion);

            context.SaveChanges();
            context.Database.CommitTransaction();
        }

        public List<Promotion> GetActivePromotions(int restaurantId)
        {
            using var context = new FoodhubContext(connectionString);
            var now = DateTime.Now;

            // ดึงโปรโมชันที่ยังไม่หมดเวลา
            return context.Promotions
                .Where(p => p.RestaurantId == restaurantId && p.StartDate <= now && p.EndDate >= now)
                .OrderBy(p => p.EndDate)
                .ToList();
        }

        // --- Ticket Operations (Client) ---
        public PromotionTicket PurchaseTicket(int userId, int promotionId)
        {
            using var context = new FoodhubContext(connectionString);
            context.Database.BeginTransaction();

            var user = context.Users.Single(u => u.Id == userId);
            ValidateRole(user, "client", "Cannot purchase ticket");

            var promotion = context.Promotions.Single(p => p.Id == promotionId);
            var now = DateTime.Now;

            if (now < promotion.StartDate || now > promotion.EndDate)
                throw new Exception("Cannot purchase ticket: Promotion is not active.");

            // เช็ค Quota
            var currentTicketCount = context.PromotionTickets.Count(t => t.PromotionId == promotionId);
            if (currentTicketCount >= promotion.TotalQuota)
                throw new Exception("Cannot purchase ticket: Promotion quota has been reached.");

            var ticket = new PromotionTicket
            {
                UserId = userId,
                PromotionId = promotionId,
                Status = "Active",
                PurchaseDate = now
            };

            context.PromotionTickets.Add(ticket);
            context.SaveChanges();
            context.Database.CommitTransaction();

            return ticket;
        }

        public void UseTicket(int userId, int ticketId)
        {
            using var context = new FoodhubContext(connectionString);
            context.Database.BeginTransaction();

            var ticket = context.PromotionTickets
                .Include(t => t.Promotion)
                .Single(t => t.Id == ticketId);

            if (ticket.UserId != userId)
                throw new Exception("Cannot use ticket: Ticket does not belong to this user.");

            ValidateTicketStatus(ticket, "Active", "Cannot use ticket");

            var now = DateTime.Now;
            if (now > ticket.Promotion.EndDate)
            {
                // ถ้าหมดอายุแล้วให้เปลี่ยน Status เป็น Expired
                ticket.Status = "Expired";
                context.SaveChanges();
                context.Database.CommitTransaction();
                throw new Exception("Cannot use ticket: Promotion has expired.");
            }

            ticket.Status = "Used";
            ticket.UsedDate = now;

            context.SaveChanges();
            context.Database.CommitTransaction();
        }

        public List<PromotionTicket> GetMyTickets(int userId)
        {
            using var context = new FoodhubContext(connectionString);

            return context.PromotionTickets
                .Include(t => t.Promotion)
                .ThenInclude(p => p.Restaurant)
                .Where(t => t.UserId == userId)
                .OrderByDescending(t => t.PurchaseDate)
                .ToList();
        }

        // --- Review Operations (Client) ---
        public void AddReview(int userId, int restaurantId, int rating, string comment)
        {
            if (rating < 1 || rating > 5)
                throw new Exception("Invalid rating: Rating must be between 1 and 5.");

            using var context = new FoodhubContext(connectionString);
            context.Database.BeginTransaction();

            var user = context.Users.Single(u => u.Id == userId);
            ValidateRole(user, "client", "Cannot add review");

            // (Optional Business Logic): ตรวจสอบว่าเคยซื้อตั๋วร้านนี้ หรือเคยใช้บริการหรือไม่
            var hasUsedTicketForRestaurant = context.PromotionTickets
                .Include(t => t.Promotion)
                .Any(t => t.UserId == userId && t.Promotion.RestaurantId == restaurantId && t.Status == "Used");

            if (!hasUsedTicketForRestaurant)
                throw new Exception("Cannot add review: You must have used a promotion ticket at this restaurant first.");

            var review = new Review
            {
                UserId = userId,
                RestaurantId = restaurantId,
                Rating = rating,
                Comment = comment,
                CreatedAt = DateTime.Now
            };

            context.Reviews.Add(review);
            context.SaveChanges();
            context.Database.CommitTransaction();
        }

        public List<Review> GetRestaurantReviews(int restaurantId)
        {
            using var context = new FoodhubContext(connectionString);

            return context.Reviews
                .Include(r => r.User) // เพื่อเอา Username ไปแสดงผล
                .Where(r => r.RestaurantId == restaurantId)
                .OrderByDescending(r => r.CreatedAt)
                .ToList();
        }
    }
}