using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Http;

namespace EasyCSIT.Models
{
    [Table("Student")]
    public class StudentModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int StudentId { get; set; }
        [Required(ErrorMessage = "Please enter your name")]
        [DataType(DataType.Text, ErrorMessage = "Please correct name")]
        [DisplayName("Full Name")]
        public string StudentName { get; set; }

        [Required(ErrorMessage = "Please select your semester")]
        public string Semester { get; set; }
        [Required(ErrorMessage = "Please enter your roll no")]
        [DisplayName("Symbol Number")]
        public int StudentRoll { get; set; }
        [Required(ErrorMessage = "Please enter your address")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Please enter your batch")]
     

        public string Batch { get; set; }
        [Required(ErrorMessage = "Please enter your contact no.")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Please correct phone no.")]
        [DisplayName("Mobile No.")]
        public string ContactNumber { get; set; }
        [Required(ErrorMessage = "Please enter your email")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Please correct email address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please upload your photo")]
        [DataType(DataType.ImageUrl, ErrorMessage = "Please correct name")]
        [DisplayName("Photo")]
        public string StudentPhoto { get; set; }
        [Required(ErrorMessage = "Please enter your Nationality")]
        public string Nationality { get; set; }

        [Required(ErrorMessage = "Please enter your Date of birth")]
        [DataType(DataType.Date, ErrorMessage = "Please enter correct DOB")]
        [DisplayName("Date of Birth")] 
        public DateTime DOB { get; set; }

        [Required(ErrorMessage = "Please select your Gender")]

        public string Sex { get; set; }
        [Required(ErrorMessage = "Please  enter your Father's name")]
        [DisplayName("Father's Name")]
        public string FatherName { get; set; }
        [Required(ErrorMessage = "Please enter your Mother's name")]
        [DisplayName("Mother's Name")]
        public string MotherName { get; set; }
        [Required(ErrorMessage = "Please enter your Registration number")]

        [DisplayName("Registration No.")]
        public string RegistrationNumber { get; set; }
        [Required(ErrorMessage = "Please enter your college's name")]
        [DisplayName("College")]
        public string   CollegeName { get; set; }
        [Required(ErrorMessage = "Please enter your age")]
        public int Age { get; set; }

    }
    public class ClassModel
    {
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public string ClassTeacher { get; set; }
    }
    public class SubjectModel
    {
        [Required(ErrorMessage = "Please select your semester")]
        public string Semester { get; set; }
        [Required(ErrorMessage = "Please enter subject code")]
        [DisplayName("Subject Code")]
        public string SubjectId { get; set; }
        [Required(ErrorMessage = "Please enter subject name")]

        public string SubjectName { get; set; }
    }

    public class MarkModel
    {
        [Required(ErrorMessage = "Please select your semester")]
        public string Semester { get; set; }
        public int MarkId { get; set; }
        [DisplayName("Name")]
        [Required(ErrorMessage = "Please select your name")]
        public int StudentId { get; set; }
        [DisplayName("Theory Mark")]
        [Range(0, 60, ErrorMessage = "Theory Marks must  between 0 and 60")]
        [Required(ErrorMessage = "Please enter the marks")]
        public double TheoryMark { get; set; }
        [DisplayName("Practical Mark")]
        [Range(0, 20, ErrorMessage = "Practical Marks must  between 0.0 and 20.0")]
        [Required(ErrorMessage = "Please enter the marks")]

        public double PracticalMark { get; set; }

        [Range(0, 60, ErrorMessage = "Internal Marks must  between 0.0 and 60.0")]
        [DisplayName("Internal Mark")]
        [Required(ErrorMessage = "Please enter the marks")]
        public double InternalMark { get; set; }




        [Required(ErrorMessage = "Please select the subject")]
        [DisplayName("Subject")]
        public string SubjectId { get; set; }
        [Required(ErrorMessage = "Please select your name")]
        public string StudentName { get; set; }
        public string SubjectName { get; set; }
        public int StudentRoll { get; set; }
        public string RegistrationNumber { get; set; }
        public string CollegeName { get; set; }

    }


    public class ExamModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ExamId { get; set; }
        [Required(ErrorMessage = "Please Select Exam Type")]

        [DisplayName("Exam Type")]
        public string ExamName { get; set; }
        [DisplayName("Name")]
        [Required(ErrorMessage = "Please select your name")]
        public string StudentId { get; set; }
    }
    public class Exam
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ExamId { get; set; }
        [Required(ErrorMessage = "Please Select Exam Type")]

        [DisplayName("Exam Type")]
        public string ExamName { get; set; }
        [DisplayName("Name")]
        [Required(ErrorMessage = "Please select your name")]
        public string StudentId { get; set; }
    }


    public class MarksModel
    {

        [Required(ErrorMessage = "Please select your semester")]
        public string Semester { get; set; }
        public int MarkId { get; set; }
        [Required(ErrorMessage = "Please select your name")]
        public int StudentId { get; set; }
        [DisplayName("Theory Mark")]
        [Required(ErrorMessage = "Please enter the marks")]
        public double TheoryMark { get; set; }
        [DisplayName("Practical Mark")]
        [Required(ErrorMessage = "Please enter the marks")]
        public double PracticalMark { get; set; }
        [DisplayName("Internal Mark")]
        [Required(ErrorMessage = "Please enter the marks")]
        public double InternalMark { get; set; }

        [Required(ErrorMessage = "Please select the subject")]
        public string SubjectId { get; set; }
        [Required(ErrorMessage = "Please select your name")]
        public string StudentName { get; set; }
        public string SubjectName { get; set; }
        public int StudentRoll { get; set; }
        public string RegistrationNumber { get; set; }
        public string CollegeName { get; set; }
        public string Batch { get; set; }
      
    }
    public class Contact
    {
        public int Id { get; set; }

        public string Name { get; set; }


        public string Email { get; set; }

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
    public class Subscribe
    {
        public int Id { get; set; }




        public string Email { get; set; }


        public DateTime SubscribeDate { get; set; }




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





}



