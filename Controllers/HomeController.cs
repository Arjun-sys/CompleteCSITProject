using EasyCSIT.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MimeKit;
using MailKit.Net.Smtp;

namespace EasyCSIT.Controllers
{
    //dispay home/index page(main pain)only after login
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private IConfiguration _iConfig;
        private IWebHostEnvironment _env;
        private EasyCSITDB context;
        public HomeController(ILogger<HomeController> logger, IConfiguration iConfig, IWebHostEnvironment env, EasyCSITDB _context)
        {
            _logger = logger;
            _iConfig = iConfig;
            _env = env;
            context = _context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(Contact student)
        {
            
        Contact studentRow = new Contact();
            studentRow.Id = student.Id;
            studentRow.Name = student.Name;
            studentRow.Email = student.Email;
            studentRow.ContactDate = DateTime.Now;
            studentRow.Message = student.Message;
            context.Add(studentRow);
            context.Entry(studentRow).State = EntityState.Added;
            context.SaveChanges();
          

        
            using(var client=new SmtpClient())
            {
                client.Connect("smtp.gmail.com");
                client.Authenticate("armantiwari1123@gmail.com", "Armantiwari1123@*");
                var bodyBuilder = new BodyBuilder
                {
                    HtmlBody=$"<p><b>Dear {student.Name}</b></p>" +
                    $"<p>How are you?</p>"+
                     $"<p>This is <b>Arjun Shrestha</b> From Easy CSIT.com.We have received your details.</p>"+
                   $"<p>Here is your submitted details</p>" +
                    $"<p>Name:{student.Name}</p><p>Message:{student.Message}</p><p>Email:{student.Email}</p>",
                    TextBody = "{student.Id}\r\n{student.Name}\r\n{student.Message}\r\n{student.Email}"

                };
                var message = new MimeMessage
                {
                    Body=bodyBuilder.ToMessageBody()

                };
                message.From.Add(new MailboxAddress("EasyCSIT.com", "armantiwari1123@gmail.com"));
                message.To.Add(new MailboxAddress(student.Email));
                message.Subject = "Data Submitted Successfuly!!";
                client.Send(message);
                client.Disconnect(true);
            }
            TempData["Message"] = "Thank you for your query.We will contact you soon!!!";
            return RedirectToAction("Index");
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
