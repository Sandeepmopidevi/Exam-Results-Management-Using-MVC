using System;

namespace ExamResultsManagement.Models
{
    public class StudentResult
    {
        public int Id { get; set; }
        public string StudentName { get; set; }
        public string Subject1 { get; set; }
        public int Marks1 { get; set; }
        public string Subject2 { get; set; }
        public int Marks2 { get; set; }
        public string Subject3 { get; set; }
        public int Marks3 { get; set; }
        public string Subject4 { get; set; }
        public int Marks4 { get; set; }
        public string Subject5 { get; set; }
        public int Marks5 { get; set; }
        public int ExamMonth { get; set; }
        public int ExamYear { get; set; }
    }
}
