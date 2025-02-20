using System;
using System.Collections.Generic;

namespace BeautySCProject.Data.Entities;

public partial class SkinTest
{
    public int SkinTestId { get; set; }

    public string SkinTestName { get; set; } = null!;

    public bool Status { get; set; }

    public virtual ICollection<SkinTypeQuestion> SkinTypeQuestions { get; set; } = new List<SkinTypeQuestion>();
}
