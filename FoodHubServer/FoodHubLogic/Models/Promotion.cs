using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FoodHubLogic.Models;

[Table("promotions")]
public partial class Promotion
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("restaurant_id")]
    public int RestaurantId { get; set; }

    [Column("title", TypeName = "character varying")]
    public string Title { get; set; } = null!;

    [Column("price")]
    [Precision(10, 2)]
    public decimal Price { get; set; }

    [Column("conditions")]
    public string Conditions { get; set; } = null!;

    [Column("total_quota")]
    public int TotalQuota { get; set; }

    [Column("start_date", TypeName = "timestamp without time zone")]
    public DateTime StartDate { get; set; }

    [Column("end_date", TypeName = "timestamp without time zone")]
    public DateTime EndDate { get; set; }

    [InverseProperty("Promotion")]
    public virtual ICollection<PromotionTicket> PromotionTickets { get; set; } = new List<PromotionTicket>();

    [ForeignKey("RestaurantId")]
    [InverseProperty("Promotions")]
    public virtual Restaurant Restaurant { get; set; } = null!;
}
