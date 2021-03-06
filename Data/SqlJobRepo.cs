﻿using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data
{
    // This is the repository that uses the DbContext to communicate with the database
    public class SqlJobRepo : IJobRepo
    {
        private readonly JobContext _context;

        public SqlJobRepo(JobContext context)
        {
            _context = context;
        }

        public void CreateJob(Job job)
        {
            if (job == null)
            {
                throw new ArgumentNullException(nameof(job));
            }
            _context.Jobs.Add(job);
        }

        public void DeleteJob(Job job)
        {
            if (job == null)
            {
                throw new ArgumentNullException(nameof(job));
            }
            _context.Jobs.Remove(job);
        }

        public IEnumerable<Job> GetAllJobs()
        {
            return _context.Jobs.ToList();
        }

        public Job GetJobById(int id)
        {
            return _context.Jobs.FirstOrDefault(p => p.Id == id);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateJob(Job job)
        {
            
        }
    }
}
