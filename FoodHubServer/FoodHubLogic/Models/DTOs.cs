using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodHubLogic.Models
{
    public class UserRsp
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class RestaurantRecommendationRsp
    {
        public int RestaurantId { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Address { get; set; }
        public double OverallRating { get; set; }
    }

    public class CustomerProfileRsp
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public int TotalCollectedPromotions { get; set; }
        public List<CustomerActivePromotionRsp> ActivePromotions { get; set; }
    }

    public class CustomerActivePromotionRsp
    {
        public int TicketId { get; set; }
        public string Title { get; set; }
        public DateTime EndDate { get; set; }
    }

    // ==========================================
    // Manager DTOs
    // ==========================================
    public class ManagerPromotionSummaryRsp
    {
        public string Title { get; set; }
        public decimal Price { get; set; }
        public DateTime StartDate {  get; set; }
        public DateTime EndDate { get; set; }
        public string Conditions { get; set; }
        public int RemainingQuota { get; set; }
    }

    public class ManagerTicketDetailRsp
    {
        public int TicketId { get; set; }
        public int UserId { get; set; }
        public string PromotionTitle { get; set; }
        public string Conditions { get; set; }
        public string Status { get; set; } // เพิ่มฟิลด์สถานะเข้ามา
    }

    public class ManagerReviewDetailRsp
    {
        public string Username { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
    }

    // ข้อมูลที่ Manager ต้องกรอกเวลาสร้าง Promotion
    public class AddPromotionReq
    {
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Conditions { get; set; }
        public int TotalQuota { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    // ข้อมูลที่จะส่งกลับไปให้ Manager ดูหลังจากสร้างเสร็จ (ไม่มี Navigation Properties กวนใจ)
    public class PromotionBasicRsp
    {
        public int Id { get; set; }
        public int RestaurantId { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Conditions { get; set; }
        public int TotalQuota { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class ManagerRestaurantListRsp
    {
        public int RestaurantId { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Address { get; set; }
    }
}
