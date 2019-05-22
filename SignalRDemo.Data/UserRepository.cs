using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SignalRDemo.Data
{
    public class UserRepository
    {
        private string _connectionString;

        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public User AddUser(User user)
        {
            using (JobContext context = new JobContext(_connectionString))
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                context.Users.Add(user);
                context.SaveChanges();
                return user;
            }
        }

        public User GetUserByEmail(string email)
        {
            using (JobContext context = new JobContext(_connectionString))
            {
                return context.Users.FirstOrDefault(u => u.Email == email);
            }
        }

        public User GetUserById(int id)
        {
            using (JobContext context = new JobContext(_connectionString))
            {
                return context.Users.FirstOrDefault(u => u.Id == id);
            }
        }

        public User Verify(string email, string password)
        {
            using (JobContext context = new JobContext(_connectionString))
            {
                User user = context.Users.FirstOrDefault(u => u.Email == email);

                if (user == null)
                {
                    return null;
                }

                return BCrypt.Net.BCrypt.Verify(password, user.Password) ? user : null;
            }
        }
    }
}
