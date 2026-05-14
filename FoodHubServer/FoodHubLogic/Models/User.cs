using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FoodHubLogic.Models;

[Table("users")]
[Index("Email", Name = "users_email_key", IsUnique = true)]
public partial class User
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("username")]
    [StringLength(100)]
    public string Username { get; set; } = null!;

    [Column("email")]
    [StringLength(150)]
    public string Email { get; set; } = null!;

    [Column("password_hash", TypeName = "character varying")]
    public string PasswordHash { get; set; } = null!;

    /// <summary>
    /// client, manager
    /// </summary>
    [Column("role", TypeName = "character varying")]
    public string Role { get; set; } = null!;

    [Column("created_at", TypeName = "timestamp without time zone")]
    public DateTime CreatedAt { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<PromotionTicket> PromotionTickets { get; set; } = new List<PromotionTicket>();

    [InverseProperty("Manager")]
    public virtual ICollection<Restaurant> Restaurants { get; set; } = new List<Restaurant>();

    [InverseProperty("User")]
    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}
