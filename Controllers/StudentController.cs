using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


using EasyCSIT.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace EasyCSIT.Controllers
{
    public class StudentController : Controller
    {



        private EasyCSITDB context;
        public StudentController(EasyCSITDB _context)
        {
            context = _context;
        }
        public IActionResult Create()
        {
            return View(new StudentModel());
        }
        [HttpPost]
        public IActionResult Create(StudentModel student)
        {
            Student studentRow = new Student();
            studentRow.StudentId = student.StudentId;
            studentRow.StudentName = student.StudentName;
            studentRow.Address = student.Address;
            studentRow.Batch = student.Batch;
            studentRow.ContactNumber = student.ContactNumber;
            context.Add(studentRow);
            //  context.Entry(studentRow).State = EntityState.Added;
            context.SaveChanges();
            return RedirectToAction("students");
        }



        public IActionResult Details(int studentId)
        {
            StudentModel student = context.Students.Where(x => x.StudentId == studentId).Select(x => new StudentModel()
            {
                StudentId = x.StudentId,
                StudentName = x.StudentName,
                Address = x.Address,
                Batch = x.Batch,
                ContactNumber = x.ContactNumber


            }).FirstOrDefault();

            return View(student);
        }






        public IActionResult Edit(int studentId)
        {
            StudentModel student = context.Students.Where(x => x.StudentId == studentId).Select(x => new StudentModel()
            {
                StudentId = x.StudentId,
                StudentName = x.StudentName,
                Address = x.Address,
                Batch = x.Batch,
                ContactNumber = x.ContactNumber


            }).FirstOrDefault();

            return View(student);
        }

        [HttpPost]
        public IActionResult Edit(StudentModel student)
        {
            Student studentRow = new Student();
            studentRow.StudentId = student.StudentId;
            studentRow.StudentName = student.StudentName;
            studentRow.Address = student.Address;
            studentRow.Batch = student.Batch;
            studentRow.ContactNumber = student.ContactNumber;
            context.Attach(studentRow);
            context.Entry(studentRow).State = EntityState.Modified;
            context.SaveChanges();
            return RedirectToAction("students");
        }

        public IActionResult Delete(int StudentID)
        {
            StudentModel student = context.Students.Where(x => x.StudentId == StudentID).Select(x => new StudentModel()
            {
                StudentId = x.StudentId,
                StudentName = x.StudentName,
                Address = x.Address,
                Batch = x.Batch,
                ContactNumber = x.ContactNumber


            }).FirstOrDefault();

            return View(student);
        }



        [HttpPost]
        public IActionResult Delete(StudentModel student)
        {


            Student studentRow = new Student();
            studentRow = context.Students.Where(x => x.StudentId == student.StudentId).FirstOrDefault();
            context.Students.Remove(studentRow);
            context.SaveChanges();
            return RedirectToAction("students");

         /*   var studentRow = context.Students.Find(student.StudentId);
            context.Students.Remove(studentRow);
            context.Entry(studentRow).State = EntityState.Deleted;
            context.SaveChanges();
            return RedirectToAction("students");*/
        }
        public IActionResult Students()
        {

            List<StudentModel> students = context.Students.Select(student => new StudentModel()
            {
                StudentId = student.StudentId,
                StudentName = student.StudentName,
                Address = student.Address,
                Batch = student.Batch,
                ContactNumber = student.ContactNumber

            }
            ).ToList();
            return View(students);
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
