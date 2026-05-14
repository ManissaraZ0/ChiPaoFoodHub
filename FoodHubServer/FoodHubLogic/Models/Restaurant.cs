using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FoodHubLogic.Models;

[Table("restaurants")]
public partial class Restaurant
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name", TypeName = "character varying")]
    public string Name { get; set; } = null!;

    [Column("manager_id")]
    public int ManagerId { get; set; }

    [Column("address")]
    public string Address { get; set; } = null!;

    [Column("category")]
    [StringLength(50)]
    public string Category { get; set; } = null!;

    [Column("created_at", TypeName = "timestamp without time zone")]
    public DateTime CreatedAt { get; set; }

    [ForeignKey("ManagerId")]
    [InverseProperty("Restaurants")]
    public virtual User Manager { get; set; } = null!;

    [InverseProperty("Restaurant")]
    public virtual ICollection<Promotion> Promotions { get; set; } = new List<Promotion>();

    [InverseProperty("Restaurant")]
    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}
