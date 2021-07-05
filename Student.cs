using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EasyCSIT
{
   

        [Table("Student", Schema = "dbo")]
        public class Student
        {
            // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
          
        [Key]
        public int StudentId { get; set; }
    
        public string StudentName { get; set; }

    
        public string Semester { get; set; }
    
        public int StudentRoll { get; set; }
     
        public string Address { get; set; }
   
        public string Batch { get; set; }
   
        public string ContactNumber { get; set; }
    
        public string Email { get; set; }

     
      
        public string StudentPhoto { get; set; }
    
        public string Nationality { get; set; }
       


        public DateTime DOB { get; set; }

  

        public string Sex { get; set; }
     
        public string FatherName { get; set; }
     
        public string MotherName { get; set; }
    

  
        public string RegistrationNumber { get; set; }
     
        public string CollegeName { get; set; }
   
        public int Age { get; set; }

    }
    public class Exam
    {
        public int ExamId { get; set; }
        public string ExamName { get; set; }
        public string StudentId { get; set; }
    }
    public class Class
        {
            public int ClassId { get; set; }
            public string ClassName { get; set; }
            public string ClassTeacher { get; set; }
        }
        public class Subject
        {
        public string SubjectId { get; set; }
      
        public string SubjectName { get; set; }
        public string Semester { get; set; }
        }
        [Table("Mark", Schema = "dbo")]
        public class Mark
        {
            public int MarkId { get; set; }
            public int StudentId { get; set; }
            public double TheoryMark { get; set; }
        public double PracticalMark { get; set; }
        public double InternalMark { get; set; }
        public string SubjectId { get; set; }

        }
    public class FileDetails
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter your name")]
        public string Name { get; set; }
        [Display(Name = "What's on your mind?")]
        [Required(ErrorMessage = "Please enter what's on your mind")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Please upload file")]
        [DisplayName("Upload File")]
        public string FileContent { get; set; }
        public DateTime FileDate { get; set; }
    }
    public class Contact
    {
        public int Id { get; set; }

        public string Name { get; set; }


        public string Email{ get; set; }
      
        public string Message { get; set; }
        public DateTime ContactDate { get; set; }




    }
    public class PortfolioContact
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Subject { get; set; }


        public string Email { get; set; }

        public string Message { get; set; }
        public DateTime ContactDate { get; set; }
    }
   

}
