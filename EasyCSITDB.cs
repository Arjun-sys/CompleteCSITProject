using EasyCSIT.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyCSIT
{
    public class EasyCSITDB : DbContext
    {
        public EasyCSITDB(DbContextOptions<EasyCSITDB> dbContextOptions) : base(dbContextOptions)
        {

        }
        public DbSet<Claim> Claims { get; set; }
        public DbSet<StudentModel> StudentModels { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Mark> Marks { get; set; }
        public DbSet<FileDetails> FileDetails { get; set; }
        public DbSet<Subject> Subject { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Exam> Exam { get; set; }
        public DbSet<ExamModel> ExamsModel { get; set; }
        public DbSet<Contact> Contact { get; set; }
        public DbSet<PortfolioContact> PortfolioContact { get; set; }

        public DbSet<MarksModel> MarksModel { get; set; }
     

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MarksModel>().HasNoKey();
        
        }
      

        //add all the tables from the database like student table
    }



}