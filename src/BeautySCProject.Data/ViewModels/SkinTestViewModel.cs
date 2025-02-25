using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Data.ViewModels
{
    public class SkinTestViewModel
    {
        public int SkinTestId { get; set; }
        public string? SkinTestName { get; set; }
        public bool Status { get; set; }
        public List<SkinTypeQuestionViewModel> SkinTypeQuestions { get; set; } = new List<SkinTypeQuestionViewModel>(); 
    }

    public class SkinTypeQuestionViewModel
    {
        public int SkinTypeQuestionId { get; set; }
        public string? Description { get; set; }
        public List<SkinTypeAnswerViewModel> SkinTypeAnswers { get; set; } = new List<SkinTypeAnswerViewModel>();
    }

    public class SkinTypeAnswerViewModel
    {
        public int SkinTypeAnswerId { get; set; }
        public string? Description { get; set; }
        public int SkinTypeId { get; set; }
    }
    public class SkinTestResultViewModel
    {
        public int SkinTypeId { get; set; }
        public string SkinTypeName { get; set; }
    }
    public class SkinTestGetAllViewModel
    {
        public int SkinTestId { get; set; }
        public string SkinTestName { get; set; }
    }
}
