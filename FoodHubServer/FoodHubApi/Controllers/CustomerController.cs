using System.Collections.Generic;
using FoodHubLogic;
using FoodHubLogic.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FoodHubApi.Controllers;

[Route("[controller]/v1/")]
[ApiController]
public class CustomerController : ControllerBase
{
    // 1.d Browse Promotion
    [HttpGet("promotions")]
    public List<Promotion> BrowsePromotions([FromQuery] int? restaurantId)
    {
        var domain = new DomainLogic(MyConfig.ConnStr);
        var promotions = domain.BrowseActivePromotions(restaurantId);

        // Disable back-link
        foreach (var p in promotions)
        {
            if (p.Restaurant != null)
            {
                p.Restaurant.Promotions = null; // ป้องกัน Loop กลับมาที่ Promotions
            }
        }

        return promotions;
    }

    // 1.a Buy Promotion Ticket
    [HttpPost("promotions/{promotionId}/tickets")]
    public PromotionTicket BuyPromotionTicket(int promotionId, [FromQuery] int userId)
    {
        var domain = new DomainLogic(MyConfig.ConnStr);
        var ticket = domain.BuyPromotionTicket(userId, promotionId);

        // Disable back-link (ถ้ามี Include Navigation property ใน Logic)
        if (ticket.Promotion != null) ticket.Promotion.PromotionTickets = null;
        if (ticket.User != null) ticket.User.PromotionTickets = null;

        return ticket;
    }

    // 1.c Read Review
    [HttpGet("restaurants/{restaurantId}/reviews")]
    public List<Review> GetReviews(int restaurantId)
    {
        var domain = new DomainLogic(MyConfig.ConnStr);
        var reviews = domain.GetRestaurantReviews(restaurantId);

        // Disable back-link
        foreach (var r in reviews)
        {
            if (r.User != null) r.User.Reviews = null;
            if (r.Restaurant != null) r.Restaurant.Reviews = null;
        }

        return reviews;
    }

    // 1.b Reviewer Restaurant (Write Review)
    [HttpPost("restaurants/{restaurantId}/reviews")]
    public Review SubmitReview(int restaurantId, [FromBody] SubmitReviewReq req)
    {
        var domain = new DomainLogic(MyConfig.ConnStr);
        var review = domain.SubmitReview(req.UserId, restaurantId, req.Rating, req.Comment);

        // Disable back-link
        if (review.User != null) review.User.Reviews = null;
        if (review.Restaurant != null) review.Restaurant.Reviews = null;

        return review;
    }

    // 1. Recommendation Restaurant (เรียงตาม Overall Rating จากมากไปน้อย)
    [HttpGet("restaurants/recommendations")]
    public List<RestaurantRecommendationRsp> GetRecommendedRestaurants()
    {
        var domain = new DomainLogic(MyConfig.ConnStr);
        return domain.GetRecommendedRestaurants();
    }

    // 2. หน้า Detail ของ Customer (แสดง Profile, จำนวนตั๋วรวม, และตั๋วที่ยังไม่หมดอายุ)
    [HttpGet("profile")]
    public CustomerProfileRsp GetCustomerProfile([FromQuery, Required] int userId)
    {
        var domain = new DomainLogic(MyConfig.ConnStr);
        return domain.GetCustomerProfile(userId);
    }

    // 3. หน้า Detail ของ Restaurant
    [HttpGet("restaurants/{restaurantId}/details")]
    public RestaurantRecommendationRsp GetRestaurantDetail(int restaurantId)
    {
        var domain = new DomainLogic(MyConfig.ConnStr);
        return domain.GetRestaurantDetail(restaurantId);
    }

    // 4. Search Restaurant (ค้นหาร้านอาหารจากชื่อ หรือ หมวดหมู่)
    [HttpGet("restaurants/search")]
    public List<RestaurantRecommendationRsp> GetRestaurantBySearchText([FromQuery, Required] string searchText)
    {
        var domain = new DomainLogic(MyConfig.ConnStr);
        return domain.GetRestaurantBySearchText(searchText);
    }
}

// Request Models สำหรับ Customer
public class SubmitReviewReq
{
    [Required(ErrorMessage = "UserId is required.")]
    public int UserId { get; set; }

    [Required]
    [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")] // กำหนดช่วงคะแนนได้ด้วย
    public int Rating { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Comment cannot be empty.")]
    public string Comment { get; set; }
}