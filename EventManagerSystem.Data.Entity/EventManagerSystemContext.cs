using EventManagerSystem.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagerSystem.Data.Entity
{
    public class EventManagerSystemContext :DbContext
    {
        public EventManagerSystemContext() : base ("name=StudentManagerSystemDB")
        {

        } 

        public DbSet<User> Users { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Homework> Homeworks { get; set; }
        public DbSet<Classwork> Classworks { get; set; }
        public DbSet<PostPublications> PostPublications { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<WeeklyClasses> WeeklyClasses { get; set; }

    }
}

