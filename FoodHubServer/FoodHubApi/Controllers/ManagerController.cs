using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

    // 4. Manager's Restaurants List (สำหรับหน้าแรกหลังจาก Manager Login)
    // การใช้งาน: GET /Manager/v1/restaurants?managerId=5
    [HttpGet("restaurants")]
    public List<ManagerRestaurantListRsp> GetMyRestaurants([FromQuery, Required] int managerId)
    {
        var domain = new DomainLogic(MyConfig.ConnStr);
        return domain.GetRestaurantsByManagerId(managerId);
    }

    // 5. Delete Promotion
    // การใช้งาน: DELETE /Manager/v1/restaurants/1/promotions/99?managerId=5
    [HttpDelete("restaurants/{restaurantId}/promotions/{promotionId}")]
    public IActionResult DeletePromotion(int restaurantId, int promotionId, [FromQuery, Required] int managerId)
    {
        var domain = new DomainLogic(MyConfig.ConnStr);

        // เราสามารถใช้ try-catch ในระดับ Controller หรือ Middleware จัดการ Exception ได้
        // ในที่นี้สมมติว่าถ้า Error จะถูกโยนกลับไปตามปกติ 
        domain.DeletePromotion(managerId, restaurantId, promotionId);

        // การ Delete สำเร็จ ปกติจะ Return เป็น 200 OK พร้อมข้อความ หรือ 204 No Content
        return Ok(new { message = "Promotion deleted successfully." });
    }
}