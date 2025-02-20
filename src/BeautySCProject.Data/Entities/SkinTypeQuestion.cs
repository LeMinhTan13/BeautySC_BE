using System;
using System.Collections.Generic;

namespace BeautySCProject.Data.Entities;

public partial class SkinTypeQuestion
{
    public int SkinTypeQuestionId { get; set; }

    public string Description { get; set; } = null!;

    public int? SkinTestId { get; set; }

    public virtual SkinTest? SkinTest { get; set; }

    public virtual ICollection<SkinTypeAnswer> SkinTypeAnswers { get; set; } = new List<SkinTypeAnswer>();
}
