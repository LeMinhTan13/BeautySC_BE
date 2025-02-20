using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Data.Models.SkinTestModel
{
    public class SkinTypeQuestionRequest
    {
        [Required]
        public string Description {  get; set; }
        public List<SkinTypeAnswerRequest> skinTypeAnswers { get; set; } = new List<SkinTypeAnswerRequest>();
    }
}
