using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace CodeFirst
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public CourseLevel Level { get; set; }
        public float FullPrice { get; set; }
        public Author Author { get; set; }
        public IList<Tag> Tags { get; set; }
    }

    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<Course> Courses { get; set; }
    }

    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<Course> Courses { get; set; }
    }

    public enum CourseLevel
    {
        Beginner = 1,
        Intermediate = 2,
        Advanced = 3
    }

    public class PlutoContext : DbContext
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<Author> Authors { get; set; }
        public Tag Tags { get; set; }

        public PlutoContext() : base("name=DefaultConnection")
        {

        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var ctx = new PlutoContext();
            ctx.Courses.Add(new Course
            {
                Author = new Author
                {
                    Id = 1,
                    Name = "Tolstoy"
                },
                Description = "Big book",
                FullPrice = 10,
                Id = 1,
                Level = CourseLevel.Advanced,
                Title = "Peace and war"
            });
            ctx.SaveChanges();
        }
    }
}
