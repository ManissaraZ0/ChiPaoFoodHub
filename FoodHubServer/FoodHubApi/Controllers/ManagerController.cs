using System.Collections.Generic;
using FoodHubLogic;
using FoodHubLogic.Models;
using Microsoft.AspNetCore.Mvc;

namespace FoodHubApi.Controllers;

[Route("[controller]/v1/")]
[ApiController]
public class ManagerController : ControllerBase
{
    // 2.a Add Promotion Ticket
    [HttpPost("restaurants/{restaurantId}/promotions")]
    public Promotion AddPromotion(int restaurantId, [FromQuery] int managerId, [FromBody] Promotion newPromotion)
    {
        var domain = new DomainLogic(MyConfig.ConnStr);
        var promotion = domain.AddPromotion(managerId, restaurantId, newPromotion);

        // Disable back-link
        if (promotion.Restaurant != null)
        {
            promotion.Restaurant.Promotions = null;
        }

        return promotion;
    }

    // 2.b Receive Promotion Tickets (Get/View Tickets for Restaurant)
    [HttpGet("restaurants/{restaurantId}/tickets")]
    public List<PromotionTicket> GetRestaurantTickets(int restaurantId, [FromQuery] int managerId)
    {
        var domain = new DomainLogic(MyConfig.ConnStr);
        var tickets = domain.GetTicketsForRestaurant(managerId, restaurantId);

        // Disable back-link
        foreach (var t in tickets)
        {
            if (t.Promotion != null)
            {
                t.Promotion.PromotionTickets = null;
                if (t.Promotion.Restaurant != null) t.Promotion.Restaurant.Promotions = null;
            }
            if (t.User != null) t.User.PromotionTickets = null;
        }

        return tickets;
    }

    // 2.b Receive Promotion Tickets (Validate & Change Status)
    // ใช้ HttpPatch หรือ HttpPost ก็ได้ ในตัวอย่างนี้ใช้ Post แบบ Custom Action
    [HttpPost("tickets/{ticketId}:validate")]
    public PromotionTicket ValidateTicket(int ticketId, [FromQuery] int managerId)
    {
        var domain = new DomainLogic(MyConfig.ConnStr);
        var ticket = domain.ValidatePromotionTicket(managerId, ticketId);

        // Disable back-link
        if (ticket.Promotion != null)
        {
            ticket.Promotion.PromotionTickets = null;
            if (ticket.Promotion.Restaurant != null) ticket.Promotion.Restaurant.Promotions = null;
        }
        if (ticket.User != null) ticket.User.PromotionTickets = null;

        return ticket;
    }

    // 1. Manager Promotion List (แสดง List โปรโมชันพร้อม Remaining Quota)
    [HttpGet("restaurants/{restaurantId}/promotions/summary")]
    public List<ManagerPromotionSummaryRsp> GetPromotionSummaries(int restaurantId)
    {
        var domain = new DomainLogic(MyConfig.ConnStr);
        return domain.GetManagerPromotionSummaries(restaurantId);
    }

    // 2. Manager Ticket List (ตรวจสอบ List Ticket ของร้าน)
    [HttpGet("restaurants/{restaurantId}/tickets-details")]
    public List<ManagerTicketDetailRsp> GetTicketDetails(int restaurantId)
    {
        var domain = new DomainLogic(MyConfig.ConnStr);
        return domain.GetManagerTicketDetails(restaurantId);
    }

    // 3. Manager Review List (ตรวจสอบ List Review ของร้าน)
    [HttpGet("restaurants/{restaurantId}/reviews-details")]
    public List<ManagerReviewDetailRsp> GetReviewDetails(int restaurantId)
    {
        var domain = new DomainLogic(MyConfig.ConnStr);
        return domain.GetManagerReviewDetails(restaurantId);
    }
}