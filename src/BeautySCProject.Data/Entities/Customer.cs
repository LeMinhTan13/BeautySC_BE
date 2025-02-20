using System;
using System.Collections.Generic;

namespace BeautySCProject.Data.Entities;

public partial class Customer
{
    public int CustomerId { get; set; }

    public int AccountId { get; set; }

    public string FullName { get; set; } = null!;

    public DateOnly? Birthday { get; set; }

    public string? PhoneNumber { get; set; }

    public bool ConfirmedEmail { get; set; }

    public string? Image { get; set; }

    public bool? Status { get; set; }

    public int? SkinTypeId { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<ShippingAddress> ShippingAddresses { get; set; } = new List<ShippingAddress>();

    public virtual SkinType? SkinType { get; set; }
}
