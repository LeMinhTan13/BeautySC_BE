using System;
using System.Collections.Generic;

namespace BeautySCProject.Data.Entities;

public partial class SkinTypeAnswer
{
    public int SkinTypeAnswerId { get; set; }

    public string Description { get; set; } = null!;

    public int SkinTypeQuestionId { get; set; }

    public int SkinTypeId { get; set; }

    public virtual SkinType SkinType { get; set; } = null!;

    public virtual SkinTypeQuestion SkinTypeQuestion { get; set; } = null!;
}
