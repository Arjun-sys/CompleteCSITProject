using EasyCSIT.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyCSIT.Controllers
{
     public class MarkController : Controller
        {
        private EasyCSITDB context;
            public MarkController(EasyCSITDB _context)
            {
                context = _context;
            }


            public IActionResult Add_Markslip()
            {

                var Students = context.Students.Select(student => new StudentModel()
                {
                    StudentId = student.StudentId,
                    StudentName = student.StudentName,
                    Address = student.Address,
                    Batch = student.Batch,
                    ContactNumber = student.ContactNumber

                }
                ).ToList();
                ViewBag.Students = new SelectList(Students, "StudentId", "StudentName");



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
            markRow.PracticalMark = mark.PracticalMark;
            markRow.InternalMark = mark.InternalMark;
            markRow.TheoryMark = mark.TheoryMark;
                markRow.StudentId = mark.StudentId;

                context.Add(markRow);
                context.SaveChanges();
                return RedirectToAction("marks");
            }



            public IActionResult Marks_Details(int markId)
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






            public IActionResult Edit_Marks(int markId)
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




            public IActionResult Marks()
            {

                List<MarkModel> marks = (from M in context.Marks    //yessai name deko aalias gareko
                                         join S in context.Students on M.StudentId equals S.StudentId
                                         join Su in context.Subject on M.SubjectId equals Su.SubjectId
                                         select new MarkModel()
                                         {
                                             MarkId = M.MarkId,
                                             StudentId = M.StudentId,
                                             TheoryMark = M.TheoryMark,
                                             PracticalMark=M.PracticalMark,
                                             InternalMark=M.InternalMark,
                                             SubjectId = M.SubjectId,
                                             StudentName = S.StudentName,
                                             SubjectName = Su.SubjectName


                                         }
                ).ToList();
                return View(marks);
            }
            public IActionResult StudentMarks(int studentId)
            {
                List<MarksModel> marks = context.MarksModel.FromSqlRaw("SpGetStudentMarks " + studentId.ToString()).ToList();


                return View(marks);
            }



        }
    }