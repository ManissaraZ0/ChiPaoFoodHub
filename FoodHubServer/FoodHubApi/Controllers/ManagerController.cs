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
    public PromotionBasicRsp AddPromotion(int restaurantId, [FromQuery] int managerId, [FromBody] AddPromotionReq req)
    {
        var domain = new DomainLogic(MyConfig.ConnStr);
        return domain.AddPromotion(managerId, restaurantId, req);
    }

    // 2.b Receive Promotion Tickets (Get/View Tickets for Restaurant)
    [HttpGet("restaurants/{restaurantId}/tickets")]
    public List<ManagerTicketDetailRsp> GetRestaurantTickets(int restaurantId, [FromQuery] int managerId, [FromQuery] string status = null)
    {
        var domain = new DomainLogic(MyConfig.ConnStr);
        return domain.GetTicketsForRestaurant(managerId, restaurantId, status);
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