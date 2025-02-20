using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Data.Models.SkinTestModel
{
    public class SkinTestCreateRequest
    {
        [Required]   
        public string SkinTestName { get; set; }

        public bool Status {  get; set; }

        public List<SkinTypeQuestionRequest> SkinTypeQuestions { get; set; } = new List<SkinTypeQuestionRequest>();
    }
}
