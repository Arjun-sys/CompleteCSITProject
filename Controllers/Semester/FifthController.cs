using EasyCSIT.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyCSIT.Controllers.Semester
{
    public class FifthController : Controller
    {
        private EasyCSITDB context;
        public FifthController(EasyCSITDB _context)
        {
            context = _context;
        }
     
        public IActionResult Students()
        {
            List<StudentModel> students = context.Students.Where(student => student.Semester =="Fifth").Select(student => new StudentModel()
            {
                StudentId = student.StudentId,
                StudentName = student.StudentName,
                Semester = student.Semester,
                StudentRoll = student.StudentRoll,
                Address = student.Address,
                Batch = student.Batch,
                Email = student.Email,
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
            return View(students);
        }


        public IActionResult Add_Markslip()
        {

            var Students = context.Students.Where(subject => subject.Semester == "Fifth").Select(student => new StudentModel()
            {
                StudentId = student.StudentId,
                StudentName = student.StudentName,
                Address = student.Address,
                Batch = student.Batch,
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
            ViewBag.Students = new SelectList(Students, "StudentId", "StudentName");



            var Subject = context.Subject.Where(subject => subject.Semester == "Fifth").Select(subject => new SubjectModel()
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

        public IActionResult Marks()
        {

            List<Mark> marks = context.Marks.Select(mark => new Mark()
            {
                MarkId = mark.MarkId,
                StudentId = mark.StudentId,
                TheoryMark = mark.TheoryMark,
                PracticalMark = mark.PracticalMark,
                InternalMark = mark.InternalMark,
                SubjectId = mark.SubjectId


            }
            ).ToList();
            return View(marks);
        }
        public IActionResult CreateSubject()
        {


            return View(new SubjectModel());
        }
        [HttpPost]
        public IActionResult CreateSubject(SubjectModel subject)
        {

            try
            {
                Subject subjectRow = new Subject();
                subjectRow.SubjectId = subject.SubjectId;
                subjectRow.Semester = "Fifth";
                subjectRow.SubjectName = subject.SubjectName;
                context.Subject.Add(subjectRow);
                context.SaveChanges();
                return RedirectToAction("CreateSubject");

            }

            catch (DbUpdateException ex)
            {


                return Json(new { ExceptionMessage = ex.Message + " Please donot enter the duplicate subject" });
            }
            finally
            {


            }



        }
    }
}




