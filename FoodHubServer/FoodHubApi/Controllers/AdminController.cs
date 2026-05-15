using Microsoft.AspNetCore.Mvc;
using FoodHubLogic;
using FoodHubLogic.Models;
using System.Collections.Generic;

namespace FoodHubApi.Controllers;

[Route("admin/v1/")]
[ApiController]
public class AdminController : ControllerBase
{
    // ดึงข้อมูล User ทั้งหมดในระบบ
    [HttpGet("users")]
    public List<UserRsp> GetAllUsers()
    {
        var domain = new DomainLogic(MyConfig.ConnStr);
        return domain.GetAllUsers();
    }
}