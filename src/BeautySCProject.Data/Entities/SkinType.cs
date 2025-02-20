using System;
using System.Collections.Generic;

namespace BeautySCProject.Data.Entities;

public partial class SkinType
{
    public int SkinTypeId { get; set; }

    public string SkinTypeName { get; set; } = null!;

    public int Priority { get; set; }

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

    public virtual ICollection<ProductSkinType> ProductSkinTypes { get; set; } = new List<ProductSkinType>();

    public virtual ICollection<Routine> Routines { get; set; } = new List<Routine>();

    public virtual ICollection<SkinTypeAnswer> SkinTypeAnswers { get; set; } = new List<SkinTypeAnswer>();
}
