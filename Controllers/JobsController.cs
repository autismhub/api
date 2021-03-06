﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Dtos;
using API.Models;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    // This class controls the different endpoints of the route and what they do
    [Route("api/jobs")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private readonly IJobRepo _repository;
        private readonly IMapper _mapper;

        public JobsController(IJobRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET api/jobs
        [HttpGet]
        public ActionResult <IEnumerable<JobsReadDto>> GetAllJobs()
        {
            IEnumerable<Job> jobItems = _repository.GetAllJobs();

            return Ok(_mapper.Map<IEnumerable<JobsReadDto>>(jobItems));
        }

        // GET api/jobs/{id}
        [HttpGet("{id}", Name="GetJobById")]
        public ActionResult <JobReadByIdDto> GetJobById(int id)
        {
            Job jobItem = _repository.GetJobById(id);

            if(jobItem != null)
            {
                return Ok(_mapper.Map<JobReadByIdDto>(jobItem));
            }
            return NotFound();
        }

        // POST api/jobs
        [HttpPost]
        public ActionResult <JobReadByIdDto> AddJob(JobCreateDto jobAddDto)
        {
            Job jobModel = _mapper.Map<Job>(jobAddDto);
            _repository.CreateJob(jobModel);
            _repository.SaveChanges();

            JobReadByIdDto jobReadByIdDto = _mapper.Map<JobReadByIdDto>(jobModel);

            return CreatedAtRoute(nameof(GetJobById), new {Id = jobReadByIdDto.Id}, jobReadByIdDto);
        }

        // PUT api/jobs/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateJob(int id, JobUpdateDto jobUpdateDto)
        {
            Job jobModelFromRepo = _repository.GetJobById(id);
            if(jobModelFromRepo == null)
            {
                return NotFound();
            }
            _mapper.Map(jobUpdateDto, jobModelFromRepo);

            _repository.UpdateJob(jobModelFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }

        // PATCH api/jobs/{id}
        [HttpPatch("{id}")]
        public ActionResult PartialJobUpdate(int id, JsonPatchDocument<JobUpdateDto> patchDoc)
        {
            Job jobModelFromRepo = _repository.GetJobById(id);
            if (jobModelFromRepo == null)
            {
                return NotFound();
            }
            JobUpdateDto jobToPatch = _mapper.Map<JobUpdateDto>(jobModelFromRepo);
            patchDoc.ApplyTo(jobToPatch, ModelState);
            if(!TryValidateModel(jobToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(jobToPatch, jobModelFromRepo);

            _repository.UpdateJob(jobModelFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }

        // DELETE api/jobs/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteJob(int id)
        {
            Job jobModelFromRepo = _repository.GetJobById(id);
            if (jobModelFromRepo == null)
            {
                return NotFound();
            }
            _repository.DeleteJob(jobModelFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }
    }
}
