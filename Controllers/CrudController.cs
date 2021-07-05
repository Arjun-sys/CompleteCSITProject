using EasyCSIT.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Syncfusion.HtmlConverter;
using Syncfusion.Pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xceed.Wpf.Toolkit;

namespace EasyCSIT.Controllers
{
    public class CrudController : Controller
    {
        private readonly ILogger<CrudController> _logger;
        private IConfiguration _iConfig;
        private IWebHostEnvironment _env;
        private EasyCSITDB context;
        private readonly IHostingEnvironment _hostingEnvironment;
        public CrudController(ILogger<CrudController> logger, IConfiguration iConfig, IWebHostEnvironment env, EasyCSITDB _context,IHostingEnvironment hostingEnvironment)
        {
            _logger = logger;
            _iConfig = iConfig;
            _env = env;
            context = _context;
            _hostingEnvironment = hostingEnvironment;
        }
       
        public IActionResult Students()
        {
            List<StudentModel> students = context.Students.Select(student => new StudentModel()
            {
                StudentId = student.StudentId,
                StudentName = student.StudentName,
                Semester = student.Semester,
                StudentRoll = student.StudentRoll,
                Address = student.Address,
                Batch = student.Batch,
                Email = student.Email,
                StudentPhoto = student.StudentPhoto,
                Sex=student.Sex,
                Nationality=student.Nationality,
                DOB=student.DOB,
                Age=student.Age,
                CollegeName=student.CollegeName,
                RegistrationNumber=student.RegistrationNumber,
                FatherName=student.FatherName,
                MotherName=student.MotherName,

                ContactNumber = student.ContactNumber

            }
             ).ToList();
            return View(students);
        }
        [HttpGet]//SEARCH BOX
        public async Task<IActionResult>Students(string stdSearch)
        {
            ViewData["GetStudent"] = stdSearch;
            
            var stdquery = from m in context.Students
                           select m;

            if (!String.IsNullOrEmpty(stdSearch))
            {
                stdquery = stdquery.Where(s => s.StudentName.Contains(stdSearch)||s.Email.Contains(stdSearch)||s.StudentRoll.ToString().Contains(stdSearch) || s.RegistrationNumber.Contains(stdSearch)||s.ContactNumber.Contains(stdSearch));
            }

            return View(await stdquery.AsNoTracking().ToListAsync());
        }




        public IActionResult Marks()
        {

            List<MarkModel> marks = (from M in context.Marks    //yessai name deko aalias gareko
                                     join S in context.Students on M.StudentId equals S.StudentId
                                     join Su in context.Subject on M.SubjectId equals Su.SubjectId
                                    
                                     select new MarkModel()
                                     {

                                         Semester = S.Semester,
                                         MarkId = M.MarkId,
                                         StudentId = M.StudentId,
                                         TheoryMark = M.TheoryMark,
                                         SubjectId = M.SubjectId,
                                         StudentName = S.StudentName,
                                         StudentRoll=S.StudentRoll,
                                         RegistrationNumber=S.RegistrationNumber,
                                         CollegeName=S.CollegeName,
                                         SubjectName = Su.SubjectName,
                                         InternalMark=M.InternalMark,
                                         PracticalMark=M.PracticalMark

                                     }
            ).ToList();
            return View(marks);
        }

        public IActionResult ExportToPDF(int StudentId)
        {
            HtmlToPdfConverter converter = new HtmlToPdfConverter();
            WebKitConverterSettings settings = new WebKitConverterSettings();
            settings.WebKitPath = Path.Combine(_hostingEnvironment.ContentRootPath,"QtBinariesWindows");
            converter.ConverterSettings = settings;
            PdfDocument document = converter.Convert("https://localhost:5001/crud/StudentMarks?studentId=StudentId");
            MemoryStream ms = new MemoryStream();
            document.Save(ms);
          
          
            
            return File(ms.ToArray(), System.Net.Mime.MediaTypeNames.Application.Pdf, "Sample.pdf");

        }
        public IActionResult StudentMarks(int studentId)
        {
            
            
            List<MarksModel> marks = context.MarksModel.FromSqlRaw("SpGetStudentMarks " + studentId.ToString()).ToList();
            return View(marks);
        }






        public IActionResult Details(int studentId)
        {
            StudentModel student = context.Students.Where(x => x.StudentId == studentId).Select(x => new StudentModel()
            {
                StudentId = x.StudentId,
                StudentName = x.StudentName,
                StudentRoll = x.StudentRoll,
                Semester = x.Semester,
                Address = x.Address,
                Batch = x.Batch,
                Email = x.Email,
                StudentPhoto = x.StudentPhoto,
                Sex = x.Sex,
                Nationality = x.Nationality,
                DOB = x.DOB,
                Age = x.Age,
                CollegeName = x.CollegeName,
                RegistrationNumber = x.RegistrationNumber,
                FatherName = x.FatherName,
                MotherName = x.MotherName,

                ContactNumber = x.ContactNumber


            }).FirstOrDefault();

            return View(student);
        }




        public IActionResult Marks_Details(int markId)
        {
            MarkModel mark = context.Marks.Where(x => x.MarkId == markId).Select(x => new MarkModel()
            {
                MarkId = x.MarkId,
                SubjectId = x.SubjectId,
                TheoryMark = x.TheoryMark,
                PracticalMark=x.PracticalMark,
                InternalMark=x.InternalMark,
                StudentId = x.StudentId


            }).FirstOrDefault();

            return View(mark);
        }
        [Authorize]
       
        [HttpGet]

        public IActionResult Create()
        {
            ViewBag.Semester = new List<string>() { "First", "Second","Third","Fourth","Fifth","Sixth","Seventh","Eight" };
            return View(new StudentModel());
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]

        public IActionResult Create(StudentModel student)
        {
            try { 
                Student studentRow = new Student();
                string pathToFolder = Path.Combine(_env.WebRootPath, "images", Request.Form.Files["StudentPhoto"].FileName);

                var fileStream = new FileStream(pathToFolder, FileMode.Create);
                Request.Form.Files["StudentPhoto"].CopyTo(fileStream);
                studentRow.StudentId = student.StudentId;
                studentRow.StudentName = student.StudentName;

                studentRow.Semester = student.Semester;
                studentRow.StudentRoll = student.StudentRoll;
                studentRow.Address = student.Address;
                studentRow.Batch = student.Batch;
                studentRow.Email = student.Email;
              
                studentRow.StudentPhoto = Path.Combine(Request.Form.Files["StudentPhoto"].FileName);

                studentRow.ContactNumber = student.ContactNumber;
            studentRow.FatherName = student.FatherName;
            studentRow.MotherName = student.MotherName;
            studentRow.Sex = student.Sex;
            studentRow.Nationality = student.Nationality;
            studentRow.RegistrationNumber = student.RegistrationNumber;
            studentRow.Age = student.Age;
            studentRow.DOB = student.DOB;
            studentRow.CollegeName = student.CollegeName;


                context.Entry(studentRow).State = EntityState.Added;
                context.SaveChanges();
                return RedirectToAction("Create");
            }
            catch (DbUpdateException ex)
            {

                return Json(new { ExceptionMessage = "This Symbol Number Or Registration Number is already registered,Thank You!!!" });
            }
            finally
            {


            }
        }







        [HttpGet]
        public IActionResult Edit(int StudentId)
        {
            ViewBag.Semester = new List<string>() { "First", "Second", "Third", "Fourth", "Fifth", "Sixth", "Seventh", "Eight" };
            StudentModel student = context.Students.Where(x => x.StudentId == StudentId).Select(x => new StudentModel()
            {

            StudentId = x.StudentId,
                StudentName = x.StudentName,
                Semester = x.Semester,
                StudentRoll = x.StudentRoll,
                Address = x.Address,
                Batch = x.Batch,
                Email = x.Email,

                StudentPhoto = x.StudentPhoto,
                Sex = x.Sex,
                Nationality = x.Nationality,
                DOB = x.DOB,
                Age = x.Age,
                CollegeName = x.CollegeName,
                RegistrationNumber = x.RegistrationNumber,
                FatherName = x.FatherName,
                MotherName = x.MotherName,
                ContactNumber = x.ContactNumber
            }).FirstOrDefault();

            return View(student);
        }

        [HttpPost]
        public IActionResult Edit(StudentModel student)
        {
            Student studentRow = new Student();
            string pathToFolder = Path.Combine(_env.WebRootPath, "images", Request.Form.Files["StudentPhoto"].FileName);

            var fileStream = new FileStream(pathToFolder, FileMode.Create);
            Request.Form.Files["StudentPhoto"].CopyTo(fileStream);
            studentRow.StudentId = student.StudentId;
            studentRow.StudentName = student.StudentName;
            studentRow.Semester = student.Semester;
            studentRow.StudentRoll = student.StudentRoll;
            studentRow.Address = student.Address;
            studentRow.Batch = student.Batch;
            studentRow.Email = student.Email;
            studentRow.StudentPhoto = Path.Combine(Request.Form.Files["StudentPhoto"].FileName);
            studentRow.ContactNumber = student.ContactNumber;
            studentRow.FatherName = student.FatherName;
            studentRow.MotherName = student.MotherName;
            studentRow.Sex = student.Sex;
            studentRow.Nationality = student.Nationality;
            studentRow.RegistrationNumber = student.RegistrationNumber;
            studentRow.Age = student.Age;
            studentRow.DOB = student.DOB;
            studentRow.CollegeName = student.CollegeName;
            context.Attach(studentRow);
            context.Entry(studentRow).State = EntityState.Modified;
            context.SaveChanges();
            return RedirectToAction("students");
        }

        public IActionResult Delete(int studentId)
        {
            StudentModel student = context.Students.Where(x => x.StudentId == studentId).Select(x => new StudentModel()
            {
                StudentId = x.StudentId,
                StudentName = x.StudentName,
                Semester = x.Semester,
                StudentRoll = x.StudentRoll,
                Address = x.Address,
                Batch = x.Batch,
                Email = x.Email,
                StudentPhoto = x.StudentPhoto,
                Sex = x.Sex,
                Nationality = x.Nationality,
                DOB = x.DOB,
                Age = x.Age,
                CollegeName = x.CollegeName,
                RegistrationNumber = x.RegistrationNumber,
                FatherName = x.FatherName,
                MotherName = x.MotherName,
                ContactNumber = x.ContactNumber

            }).FirstOrDefault();

            return View(student);
        }


        [HttpGet]
        public IActionResult Delete(Student student)
        {
            Student studentRow = new Student();
            studentRow = context.Students.Where(x => x.StudentId == student.StudentId).FirstOrDefault();
            context.Students.Remove(studentRow);
            context.SaveChanges();
            return RedirectToAction("students");
        }

        public IActionResult Add_Markslip()
        {

            var Students = context.Students.Select(student => new StudentModel()
            {
                StudentId = student.StudentId,
                StudentName = student.StudentName,
                Address = student.Address,
                Batch = student.Batch,
                StudentRoll=student.StudentRoll,
                StudentPhoto=student.StudentPhoto,
                Sex = student.Sex,
                Nationality = student.Nationality,
                DOB = student.DOB,
                Age = student.Age,
                CollegeName = student.CollegeName,
                RegistrationNumber = student.RegistrationNumber,
                FatherName = student.FatherName,
                MotherName = student.MotherName,
                ContactNumber = student.ContactNumber

            }
        ).ToList();
          

            var Subject = context.Subject.Select(subject => new SubjectModel() 
            {
                SubjectId = subject.SubjectId,
                SubjectName = subject.SubjectName


            }).ToList();
            ViewBag.Subject = new SelectList(Subject, "SubjectId", "SubjectName");
            return View(new MarkModel());


        }
        [HttpPost]
        public IActionResult Add_Markslip(MarkModel mark)
        {
            Mark markRow = new Mark();
            markRow.MarkId = mark.MarkId;
            markRow.SubjectId = mark.SubjectId;
            markRow.TheoryMark = mark.TheoryMark;
            markRow.PracticalMark = mark.PracticalMark;
            markRow.InternalMark = mark.InternalMark;
            markRow.StudentId = mark.StudentId;

            context.Add(markRow);
            context.SaveChanges();
            return RedirectToAction("marks");
        }






        public IActionResult createMarksheet()
        {

            var Students = context.Students.Select(student => new StudentModel()
            {
                StudentId = student.StudentId,
                StudentName = student.StudentName,
                Address = student.Address,
                Batch = student.Batch,
                StudentRoll = student.StudentRoll,
                StudentPhoto = student.StudentPhoto,
                Sex = student.Sex,
                Nationality = student.Nationality,
                DOB = student.DOB,
                Age = student.Age,
                CollegeName = student.CollegeName,
                RegistrationNumber = student.RegistrationNumber,
                FatherName = student.FatherName,
                MotherName = student.MotherName,
                ContactNumber = student.ContactNumber
                
            }
        ).ToList();
           
            var Subject = context.Subject.Select(subject => new SubjectModel()
            {
                SubjectId = subject.SubjectId,
                SubjectName = subject.SubjectName


            }).ToList();
           
            return View(new MarkModel());


        }
        [HttpPost]
        public IActionResult createMarksheet(MarkModel mark)
        {
          
                try
                {


                    Mark markRow = new Mark();
                    markRow.MarkId = mark.MarkId;
                markRow.SubjectId = mark.SubjectId;
                markRow.TheoryMark = mark.TheoryMark;
                markRow.PracticalMark = mark.PracticalMark;
                markRow.InternalMark = mark.InternalMark;
                markRow.StudentId = mark.StudentId;
                    context.Add(markRow);
                    context.SaveChanges();
                    return RedirectToAction("marks");
                }

                catch (DbUpdateException ex)
                {

                    return Json(new { ExceptionMessage = " Please donot enter the duplicate subject" });
                }
                finally
                {


                }

               
            
          
        }

















        public IActionResult Edit_Marks(int markId)
        {
            MarkModel mark = context.Marks.Where(x => x.MarkId == markId).Select(x => new MarkModel()
            {
                MarkId = x.MarkId,
                SubjectId = x.SubjectId,
                TheoryMark = x.TheoryMark,
                PracticalMark=x.PracticalMark,
                InternalMark=x.InternalMark,
                StudentId = x.StudentId


            }).FirstOrDefault();

            return View(mark);
        }



        [HttpPost]
        public IActionResult Edit_Marks(MarkModel mark)
        {
            Mark markRow = new Mark();
            markRow.MarkId = mark.MarkId;
            markRow.SubjectId = mark.SubjectId;
            markRow.PracticalMark = mark.PracticalMark;
            markRow.InternalMark = mark.InternalMark;
            markRow.TheoryMark = mark.TheoryMark;
            markRow.StudentId = mark.StudentId;
            context.Attach(markRow);
            context.Entry(markRow).State = EntityState.Modified;
            context.SaveChanges();
            return RedirectToAction("marks");
        }

        public IActionResult Delete_Marks(int markId)
        {
            MarkModel mark = context.Marks.Where(x => x.MarkId == markId).Select(x => new MarkModel()
            {
                MarkId = x.MarkId,
                SubjectId = x.SubjectId,
                TheoryMark = x.TheoryMark,
                PracticalMark = x.PracticalMark,
                InternalMark = x.InternalMark,
                StudentId = x.StudentId


            }).FirstOrDefault();

            return View(mark);
        }



        [HttpPost]
        public IActionResult Delete_Marks(MarkModel mark)
        {
            var markRow = context.Marks.Find(mark.MarkId);
            context.Marks.Remove(markRow);
            context.Entry(markRow).State = EntityState.Deleted;
            context.SaveChanges();
            return RedirectToAction("marks");
        }


//subject
        public IActionResult CreateSubject()
        {
            ViewBag.Semester = new List<string>() { "First", "Second", "Third", "Fourth", "Fifth", "Sixth", "Seventh", "Eight" };

            return View(new SubjectModel());
        }
        [HttpPost]
        public IActionResult CreateSubject(SubjectModel subject)
        {
           
            try
            {
                Subject subjectRow = new Subject();
                subjectRow.SubjectId = subject.SubjectId;
                subjectRow.Semester = subject.Semester;
                subjectRow.SubjectName = subject.SubjectName;
                context.Subject.Add(subjectRow);
                context.SaveChanges();
                return RedirectToAction("CreateSubject");  
             
            }

            catch (DbUpdateException ex)
            {

               

                return Json(new { ExceptionMessage = "This Subject is already registered,Thank you!!!" });
            }
            finally
            {

               
            }
            


        }



        public IActionResult SubjectIndex()
        {
            List<SubjectModel> subject = context.Subject.Select(subject => new SubjectModel()
            {
                SubjectId = subject.SubjectId,
                SubjectName = subject.SubjectName,
                Semester=subject.Semester

            }).ToList();
            return View(subject);

        }

        
        public IActionResult EditSubject(string subjectId)
        {

            SubjectModel subject = context.Subject.Where(x => x.SubjectId == subjectId).Select(x => new SubjectModel()
            {
                SubjectId = x.SubjectId,
                SubjectName = x.SubjectName,
                Semester=x.Semester

            }).FirstOrDefault();
            return View(subject);
        }

        [HttpPost]
        public IActionResult EditSubject(SubjectModel subject)
        {
            Subject subjectRow = new Subject();
            subjectRow.SubjectId = subject.SubjectId;
            subjectRow.SubjectName = subject.SubjectName;
            subjectRow.Semester = subject.Semester;
            context.Subject.Attach(subjectRow);
            context.Entry(subjectRow).State = EntityState.Modified;
            context.SaveChanges();
            return RedirectToAction("SubjectIndex");
        }

        public IActionResult DeleteSubject(string subjectId)
        {
            SubjectModel subject = context.Subject.Where(x => x.SubjectId == subjectId).Select(x => new SubjectModel()
            {
                SubjectId = x.SubjectId,
                SubjectName = x.SubjectName,
                Semester = x.Semester

            }).FirstOrDefault();
            return View(subject);
        }

        [HttpPost]
        public IActionResult DeleteSubject(SubjectModel subject)
        {
            Subject subjectRow = new Subject();
            subjectRow = context.Subject.Where(x => x.SubjectId == subject.SubjectId).FirstOrDefault();
            context.Subject.Remove(subjectRow);
            context.SaveChanges();
            return RedirectToAction("SubjectIndex");
        }

        public IActionResult CreateExam()
        {
            
                ViewBag.ExamName = new List<string>() { "First Term","Pre-Board","Board Term" };

                return View(new ExamModel());
            }
            [HttpPost]
            public IActionResult CreateExam(ExamModel exam)
            {
            var Students = context.Students.Select(student => new StudentModel()
            {
                StudentId = student.StudentId,
                StudentName = student.StudentName,
                Address = student.Address,
                Batch = student.Batch,
                StudentRoll = student.StudentRoll,
                StudentPhoto = student.StudentPhoto,
                ContactNumber = student.ContactNumber

            }
      ).ToList();
            ViewBag.Students = new SelectList(Students, "StudentId", "StudentName");

            ExamModel examRow = new ExamModel();
                 examRow.ExamId = exam.ExamId;
                 examRow.ExamName = exam.ExamName;
                 examRow.StudentId = exam.StudentId;
                 context.ExamsModel.Add(examRow);
                 context.SaveChanges();

      
            return RedirectToAction("CreateExam");










        }




    }


}



    



