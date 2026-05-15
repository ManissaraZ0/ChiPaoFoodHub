using Microsoft.EntityFrameworkCore;
using FoodHubLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodHubLogic;

public static class StatusConstants
{
    public const string RoleClient = "client";
    public const string RoleManager = "manager";

    public const string TicketActive = "Active";
    public const string TicketUsed = "Used";
    public const string TicketExpired = "Expired";
}

public class DomainLogic
{
    private readonly string connectionString;

    public DomainLogic(string connectionString)
    {
        this.connectionString = connectionString;
    }

    // ==========================================
    // Helper Methods
    // ==========================================
    private void ValidateRole(User user, string expectedRole, string message)
    {
        if (user.Role != expectedRole)
            throw new Exception($"{message}: User role must be '{expectedRole}'.");
    }

    // ==========================================
    // Admin / General User Logic
    // ==========================================

    public List<UserRsp> GetAllUsers()
    {
        using var context = new FoodhubContext(connectionString);

        return context.Users
            .Select(u => new UserRsp
            {
                Id = u.Id,
                Username = u.Username,
                Email = u.Email,
                Role = u.Role,
                CreatedAt = u.CreatedAt
            })
            .OrderBy(u => u.Id) // เรียงตาม ID หรือจะเปลี่ยนเป็น u.CreatedAt ก็ได้
            .ToList();
    }

    // ==========================================
    // 1. Functional Requirement for User/Client
    // ==========================================

    // 1.d Browse Promotion
    public List<Promotion> BrowseActivePromotions(int? restaurantId = null)
    {
        using var context = new FoodhubContext(connectionString);

        var query = context.Promotions
            .Include(p => p.Restaurant)
            .Where(p => p.StartDate <= DateTime.Now && p.EndDate >= DateTime.Now);

        if (restaurantId.HasValue)
        {
            query = query.Where(p => p.RestaurantId == restaurantId.Value);
        }

        return query.OrderByDescending(p => p.StartDate).ToList();
    }

    // 1.a Buy Promotion Ticket
    public PromotionTicket BuyPromotionTicket(int userId, int promotionId)
    {
        using var context = new FoodhubContext(connectionString);
        context.Database.BeginTransaction();

        var user = context.Users.SingleOrDefault(u => u.Id == userId);
        if (user == null) throw new Exception("User not found.");

        var promotion = context.Promotions.SingleOrDefault(p => p.Id == promotionId);
        if (promotion == null) throw new Exception("Promotion not found.");

        if (promotion.EndDate < DateTime.Now)
            throw new Exception("This promotion has expired.");

        // Check Quota
        var currentTicketsCount = context.PromotionTickets.Count(t => t.PromotionId == promotionId);
        if (currentTicketsCount >= promotion.TotalQuota)
            throw new Exception("Promotion quota is full.");

        // Create Ticket
        var ticket = new PromotionTicket
        {
            UserId = userId,
            PromotionId = promotionId,
            Status = StatusConstants.TicketActive,
            PurchaseDate = DateTime.Now,
            UsedDate = null
        };

        context.PromotionTickets.Add(ticket);
        context.SaveChanges();
        context.Database.CommitTransaction();

        return ticket;
    }

    // 1.b Reviewer Restaurant (Write Review)
    public Review SubmitReview(int userId, int restaurantId, int rating, string comment)
    {
        if (rating < 1 || rating > 5)
            throw new ArgumentException("Rating must be between 1 and 5.");

        using var context = new FoodhubContext(connectionString);

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

        return review;
    }

    // 1.c Read Review
    public List<Review> GetRestaurantReviews(int restaurantId)
    {
        using var context = new FoodhubContext(connectionString);

        return context.Reviews
            .Include(r => r.User) // ดึงข้อมูลคนรีวิวมาด้วย
            .Where(r => r.RestaurantId == restaurantId)
            .OrderByDescending(r => r.CreatedAt)
            .ToList();
    }

    // ==========================================
    // 2. Functional Requirement for Restaurant Manager
    // ==========================================

    // 2.a Add Promotion Ticket
    public Promotion AddPromotion(int managerId, int restaurantId, Promotion newPromotion)
    {
        using var context = new FoodhubContext(connectionString);

        var user = context.Users.SingleOrDefault(u => u.Id == managerId);
        ValidateRole(user, StatusConstants.RoleManager, "Only managers can add promotions");

        var restaurant = context.Restaurants.SingleOrDefault(r => r.Id == restaurantId);
        if (restaurant == null || restaurant.ManagerId != managerId)
            throw new Exception("You are not authorized to add promotions for this restaurant.");

        if (newPromotion.StartDate >= newPromotion.EndDate)
            throw new ArgumentException("Start date must be before end date.");

        // Map and Save
        newPromotion.RestaurantId = restaurantId;

        context.Promotions.Add(newPromotion);
        context.SaveChanges();

        return newPromotion;
    }

    // 2.b Receive Promotion Tickets (Validate & Change Status)
    public PromotionTicket ValidatePromotionTicket(int managerId, int ticketId)
    {
        using var context = new FoodhubContext(connectionString);
        context.Database.BeginTransaction();

        // 1. ดึงข้อมูลตั๋ว พร้อมข้อมูลโปรโมชันและร้านอาหารเพื่อเช็คสิทธิ์
        var ticket = context.PromotionTickets
            .Include(t => t.Promotion)
            .ThenInclude(p => p.Restaurant)
            .SingleOrDefault(t => t.Id == ticketId);

        if (ticket == null)
            throw new Exception("Ticket not found.");

        // 2. ตรวจสอบว่าคนที่กดยืนยัน เป็น Manager ของร้านอาหารเจ้าของโปรโมชันนี้จริงหรือไม่
        if (ticket.Promotion.Restaurant.ManagerId != managerId)
            throw new Exception("Unauthorized: You do not manage the restaurant for this ticket.");

        // 3. ตรวจสอบสถานะตั๋ว
        if (ticket.Status == StatusConstants.TicketUsed)
            throw new Exception("This ticket has already been used.");

        if (ticket.Status == StatusConstants.TicketExpired || ticket.Promotion.EndDate < DateTime.Now)
            throw new Exception("This ticket has expired.");

        // 4. อัปเดตสถานะ (Change promotion ticket status)
        ticket.Status = StatusConstants.TicketUsed;
        ticket.UsedDate = DateTime.Now;

        context.SaveChanges();
        context.Database.CommitTransaction();

        return ticket;
    }

    // (Optional) Get tickets for a restaurant to display to the manager
    public List<PromotionTicket> GetTicketsForRestaurant(int managerId, int restaurantId)
    {
        using var context = new FoodhubContext(connectionString);

        var restaurant = context.Restaurants.SingleOrDefault(r => r.Id == restaurantId);
        if (restaurant == null || restaurant.ManagerId != managerId)
            throw new Exception("Unauthorized to view these tickets.");

        return context.PromotionTickets
            .Include(t => t.Promotion)
            .Include(t => t.User)
            .Where(t => t.Promotion.RestaurantId == restaurantId)
            .OrderByDescending(t => t.PurchaseDate)
            .ToList();
    }

    // ==========================================
    // Customer Logic Extensions
    // ==========================================

    public List<RestaurantRecommendationRsp> GetRecommendedRestaurants()
    {
        using var context = new FoodhubContext(connectionString);
        return context.Restaurants
            .Select(r => new RestaurantRecommendationRsp
            {
                RestaurantId = r.Id,
                Name = r.Name,
                Category = r.Category,
                // หาค่าเฉลี่ยเรตติ้ง ถ้ายัังไม่มีรีวิวให้เป็น 0
                OverallRating = r.Reviews.Any() ? r.Reviews.Average(rev => rev.Rating) : 0
            })
            .OrderByDescending(r => r.OverallRating)
            .ToList();
    }

    public CustomerProfileRsp GetCustomerProfile(int userId)
    {
        using var context = new FoodhubContext(connectionString);
        var user = context.Users
            .Include(u => u.PromotionTickets)
            .ThenInclude(t => t.Promotion)
            .SingleOrDefault(u => u.Id == userId);

        if (user == null) throw new Exception("User not found.");

        return new CustomerProfileRsp
        {
            Username = user.Username,
            Email = user.Email,
            TotalCollectedPromotions = user.PromotionTickets.Count,
            // เลือกเฉพาะตั๋วที่ยัง Active และ วันที่สิ้นสุดโปรโมชันยังไม่เลยเวลาปัจจุบัน
            ActivePromotions = user.PromotionTickets
                .Where(t => t.Status == StatusConstants.TicketActive && t.Promotion.EndDate >= DateTime.Now)
                .Select(t => new CustomerActivePromotionRsp
                {
                    Title = t.Promotion.Title,
                    EndDate = t.Promotion.EndDate
                }).ToList()
        };
    }

    public RestaurantDetailRsp GetRestaurantDetail(int restaurantId)
    {
        using var context = new FoodhubContext(connectionString);
        var res = context.Restaurants
            .Include(r => r.Reviews)
            .SingleOrDefault(r => r.Id == restaurantId);

        if (res == null) throw new Exception("Restaurant not found.");

        return new RestaurantDetailRsp
        {
            Name = res.Name,
            Category = res.Category,
            OverallRating = res.Reviews.Any() ? res.Reviews.Average(rev => rev.Rating) : 0
        };
    }

    // ==========================================
    // Manager Logic Extensions
    // ==========================================

    public List<ManagerPromotionSummaryRsp> GetManagerPromotionSummaries(int restaurantId)
    {
        using var context = new FoodhubContext(connectionString);
        return context.Promotions
            .Where(p => p.RestaurantId == restaurantId)
            .Select(p => new ManagerPromotionSummaryRsp
            {
                Title = p.Title,
                Price = p.Price,
                Conditions = p.Conditions,
                // คำนวณโควต้าที่เหลือ: โควต้าทั้งหมด - จำนวนตั๋วที่ถูกซื้อไปแล้ว
                RemainingQuota = p.TotalQuota - context.PromotionTickets.Count(t => t.PromotionId == p.Id)
            })
            .ToList();
    }

    public List<ManagerTicketDetailRsp> GetManagerTicketDetails(int restaurantId)
    {
        using var context = new FoodhubContext(connectionString);
        return context.PromotionTickets
            .Include(t => t.Promotion)
            .Where(t => t.Promotion.RestaurantId == restaurantId)
            .Select(t => new ManagerTicketDetailRsp
            {
                TicketId = t.Id,
                UserId = t.UserId,
                PromotionTitle = t.Promotion.Title,
                Conditions = t.Promotion.Conditions
            })
            .ToList();
    }

    public List<ManagerReviewDetailRsp> GetManagerReviewDetails(int restaurantId)
    {
        using var context = new FoodhubContext(connectionString);
        return context.Reviews
            .Include(r => r.User)
            .Where(r => r.RestaurantId == restaurantId)
            .Select(r => new ManagerReviewDetailRsp
            {
                Username = r.User.Username,
                Comment = r.Comment,
                Rating = r.Rating
            })
            .OrderByDescending(r => r.Rating) // เรียงจากคะแนนมากไปน้อย (หรือเรียงตาม CreatedAt ก็ได้)
            .ToList();
    }
}