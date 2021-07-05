using EasyCSIT.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EasyCSIT.Controllers.Note
{
    public class NoteController : Controller
    {

        private readonly ILogger<CrudController> _logger;
        private IConfiguration _iConfig;
        private IWebHostEnvironment _env;
        private EasyCSITDB context;
        public NoteController(ILogger<CrudController> logger, IConfiguration iConfig, IWebHostEnvironment env, EasyCSITDB _context)
        {
            _logger = logger;
            _iConfig = iConfig;
            _env = env;
            context = _context;
        }
        public IActionResult Create()
        {
          
            return View(new FileDetails());
        }
        [HttpPost]
        public IActionResult Create(FileDetails student)
        {

            FileDetails studentRow = new FileDetails();

           string pathToFolder = Path.Combine(_env.WebRootPath, "images", Request.Form.Files["FileContent"].FileName);

            var fileStream = new FileStream(pathToFolder, FileMode.Create);
            Request.Form.Files["FileContent"].CopyTo(fileStream);
   
            studentRow.Id = student.Id;
            studentRow.Name = student.Name;
            studentRow.Description = student.Description;
          
         
            studentRow.FileDate = DateTime.Now;
            studentRow.FileContent = Path.Combine(Request.Form.Files["FileContent"].FileName);
            context.Add(studentRow);
            context.Entry(studentRow).State = EntityState.Added;
            context.SaveChanges();
            return RedirectToAction("filedetails");
        }
    [HttpGet]
        public IActionResult FileDetails()
        {
            List<FileDetails> students = context.FileDetails.Select(student => new FileDetails()
            {
                Id = student.Id,
                Name = student.Name,
                Description=student.Description,
                FileContent = student.FileContent,
            
                FileDate=student.FileDate
               

               

            }
             ).ToList();
            return View(students);
        }

        public IActionResult Edit(int Id)
        {
            FileDetails student = context.FileDetails.Where(x => x.Id == Id).Select(x => new FileDetails()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                FileContent = x.FileContent,
                    FileDate = x.FileDate

            }).FirstOrDefault();

            return View(student);
        }

        [HttpPost]
        public IActionResult Edit(FileDetails student)
        {
            FileDetails studentRow = new FileDetails();
            string pathToFolder = Path.Combine(_env.WebRootPath, "images", Request.Form.Files["FileContent"].FileName);

            var fileStream = new FileStream(pathToFolder, FileMode.Create);
            Request.Form.Files["FileContent"].CopyTo(fileStream);

            studentRow.Id = student.Id;
            studentRow.Name = student.Name;
            studentRow.Description = student.Description;
            studentRow.FileDate = DateTime.Now;
          

            studentRow.FileContent = Path.Combine(Request.Form.Files["FileContent"].FileName);

            context.Attach(studentRow);
            context.Entry(studentRow).State = EntityState.Modified;
            context.SaveChanges();
            return RedirectToAction("filedetails");

        }
/*
        public IActionResult Delete(int Id)
        {
            FileDetails student = context.FileDetails.Where(x => x.Id == Id).Select(x => new FileDetails()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                FileContent = x.FileContent,
                FileDate = x.FileDate


            }).FirstOrDefault();

            return View(student);
        }

        [HttpPost]
        public IActionResult Delete(FileDetails student)
        {

            FileDetails studentRow = new FileDetails();
            studentRow.Id = student.Id;
            studentRow.Name = student.Name;
            studentRow.Description = student.Description;
            studentRow.FileContent = student.FileContent;
            studentRow.FileDate = student.FileDate;

            context.FileDetails.Remove(studentRow);
            context.Entry(studentRow).State = EntityState.Deleted;
            context.SaveChanges();
            return RedirectToAction("filedetails");
        }
        */



        [HttpGet]
        public IActionResult Delete(FileDetails student)
        {
            FileDetails studentRow = new FileDetails();
            studentRow = context.FileDetails.Where(x => x.Id == student.Id).FirstOrDefault();
            context.FileDetails.Remove(studentRow);
            context.SaveChanges();
            return RedirectToAction("filedetails");
        }



     


    }




}
