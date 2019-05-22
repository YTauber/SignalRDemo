using System;
using System.Collections.Generic;

namespace SignalRDemo.Data
{
    public class Job
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Working Working { get; set; }
    }

    public class Working
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public bool Done { get; set; }
    }

    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public List<Working> Workings { get; set; }
    }

}
