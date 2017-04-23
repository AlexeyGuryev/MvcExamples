using System;
using System.Linq;

namespace DatabaseFirst
{
    public enum Level : byte
    {
        Beginner = 1,
        Intermediate = 2,
        Advanced = 3
    }

    class Program
    {
        static void Main(string[] args)
        {
            var ctx = new PlutoDbContext();
            ctx.GetCourses().ToList().ForEach(c => Console.WriteLine(c.Title));
        }
    }
}
