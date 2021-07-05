using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyCSIT.Controllers
{
 
    public class MyProfileController : Controller
    {
        private readonly ILogger<MyProfileController> _logger;

        private IConfiguration _iConfig;
        private IWebHostEnvironment _env;
        private EasyCSITDB context;
        public MyProfileController(ILogger<MyProfileController> logger, IConfiguration iConfig, IWebHostEnvironment env, EasyCSITDB _context)
        {
            _logger = logger;
            _iConfig = iConfig;
            _env = env;
            context = _context;
        }
        public IActionResult MyProfileIndex(Contact student)
        {
            return View();
        }
        [HttpPost]
        public IActionResult MyProfileIndex(PortfolioContact student)
        {
            PortfolioContact studentRow = new PortfolioContact();
            studentRow.Id = student.Id;
            studentRow.Name = student.Name;
            studentRow.Email = student.Email;
            studentRow.Subject = student.Subject;
            studentRow.ContactDate = DateTime.Now;
            studentRow.Message = student.Message;
            context.Add(studentRow);
            context.Entry(studentRow).State = EntityState.Added;
            context.SaveChanges();
        
        

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com");
                client.Authenticate("armantiwari1123@gmail.com", "Armantiwari1123@*");
                var bodyBuilder = new BodyBuilder
                {
                    HtmlBody = $"<p><b>Dear {student.Name}</b></p>" +
                    $"<p>How can I help you?</p>" +
                     $"<p>I am <b>Arjun Shrestha</b> From Easy CSIT.com.I am a programmer,designer,web developer,artist and nature lover.I have received your details.</p>" +
                   $"<p>Here is your submitted details</p>" +
                    $"<p>Name:{student.Name}</p><p>Email:{student.Email}</p><p>Subject:{student.Subject}</p><p>Message:{student.Message}</p>",
          

                    TextBody = "{student.Id}\r\n{student.Name}\r\n{student.Email}\r\n{student.Subject}\r\n{student.Message}"

                };
                var message = new MimeMessage
                {
                    Body = bodyBuilder.ToMessageBody()

                };
                //arjunshrestha817@gmail.com is written for displaying purpose only
                message.From.Add(new MailboxAddress("No-reply my site","arjunshrestha817@gmail.com"));
                message.To.Add(new MailboxAddress(student.Email));
                message.Subject = "Message sent successfully!!!";
                client.Send(message);
                client.Disconnect(true);
            }
            TempData["Message"] = "Thank you for your query.We will contact you soon!!!";
            return RedirectToAction("MyProfileIndex");


        }
    }
}
