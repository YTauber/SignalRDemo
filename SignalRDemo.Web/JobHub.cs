using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using SignalRDemo.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRDemo.Web
{
    public class JobHub : Hub
    {
        private string _connectionString;
        public JobHub(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }

        public void AddJob(string title)
        {
            Job j = new Job { Title = title };
            JobRepository repo = new JobRepository(_connectionString);
            repo.AddJob(j);
            Clients.All.SendAsync("NewJob", j);
        }

        public void AddWorking(int jobId)
        {
            UserRepository repo = new UserRepository(_connectionString);
            JobRepository repoJ = new JobRepository(_connectionString);

            Working w = new Working { UserId = repo.GetUserByEmail(Context.User.Identity.Name).Id };
            repoJ.AddWorking(w, jobId);
            Clients.Others.SendAsync("NewWorking", new {Name = repo.GetUserByEmail(Context.User.Identity.Name).Name, JobId = jobId } );
        }

        public void Finished(int jobId)
        {
            JobRepository repo = new JobRepository(_connectionString);
            repo.Finished(jobId);
            Clients.All.SendAsync("FinishedJob", new { JobId = jobId });
        }

    }
}
