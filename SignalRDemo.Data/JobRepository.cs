using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SignalRDemo.Data
{
    public class JobRepository
    {
        private string _connectionString;
        public JobRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Job> GetJobs()
        {
            using (JobContext context = new JobContext(_connectionString))
            {
                return context
                    .Jobs
                    .Include(j => j.Working)
                    .ThenInclude(w => w.User)
                    .Where(j => j.Working.Done != true)
                    .ToList();
            }
        }

        public Job AddJob(Job job)
        {
            using (JobContext context = new JobContext(_connectionString))
            {
                context.Jobs.Add(job);
                context.SaveChanges();
                return job;
            }
        }

        public void AddWorking(Working working, int jobId)
        {
            using (JobContext context = new JobContext(_connectionString))
            {
                context.Workings.Add(working);
                context.Jobs.FirstOrDefault(j => j.Id == jobId).Working = working;
                context.SaveChanges();
            }
        }

        public void Finished(int jobId)
        {
            using (JobContext context = new JobContext(_connectionString))
            {
                Job job = context.Jobs.Include(j => j.Working).FirstOrDefault(j => j.Id == jobId);
                job.Working.Done = true;
                context.SaveChanges();
            }
        }
    }
}
