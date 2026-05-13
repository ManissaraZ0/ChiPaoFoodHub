using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FoodHubLogic.Models;

[Table("promotion_tickets")]
public partial class PromotionTicket
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("user_id")]
    public int UserId { get; set; }

    [Column("promotion_id")]
    public int PromotionId { get; set; }

    /// <summary>
    /// Active, Used, Expired
    /// </summary>
    [Column("status", TypeName = "character varying")]
    public string Status { get; set; } = null!;

    [Column("purchase_date", TypeName = "timestamp without time zone")]
    public DateTime PurchaseDate { get; set; }

    [Column("used_date", TypeName = "timestamp without time zone")]
    public DateTime? UsedDate { get; set; }

    [ForeignKey("PromotionId")]
    [InverseProperty("PromotionTickets")]
    public virtual Promotion Promotion { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("PromotionTickets")]
    public virtual User User { get; set; } = null!;
}
