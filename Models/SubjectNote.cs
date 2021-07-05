using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EasyCSIT.Models
{
    public class SubjectNote
    {
  


   
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter your name")]
        public string Name { get; set; }
        [Display(Name = "What's on your mind?")]
        [Required(ErrorMessage = "Please enter what's on your mind")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Please upload file")]
        public string FileContent { get; set; }

        public int Counter { get; set; }


    }


}
