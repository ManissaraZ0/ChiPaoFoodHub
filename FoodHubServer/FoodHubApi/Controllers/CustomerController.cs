using System.Collections.Generic;
using FoodHubLogic;
using FoodHubLogic.Models;
using Microsoft.AspNetCore.Mvc;

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
}

// Request Models สำหรับ Customer
public class SubmitReviewReq
{
    public int UserId { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; }
}