using AdpLabs.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AdpLabs.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly TaskDomainService _TaskDomainService;
        public TaskController(TaskDomainService taskDomainService)
        {
            _TaskDomainService = taskDomainService;
        }

        // GET: api/<TaskController>
        /// <summary>
        /// Calculate the result of operation with the datas returned by https://interview.adpeai.com/api/v1/get-task
        /// </summary>
        /// <returns>Return a ID and result of calculate.Please, copy and save this information to use in the Post method</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TaskModel))]        
        public async Task<IActionResult> GetAsync()
        {
            var response = await _TaskDomainService.GetAsync();
            return Ok(response);
        }

        // POST api/<TaskController>
        /// <summary>
        /// Validate the result
        /// </summary>
        /// <param name="taskRequest">Place here the return of Get method</param>
        /// <returns>A string with the lidation result</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable, Type = typeof(string))]
        public async Task<IActionResult> PostAsync([FromBody] TaskModel taskRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest("Incorrect value in result; No ID specified;Value is invalid");

            var response = await _TaskDomainService.SubmitAsync(taskRequest);

            return Ok(response);
        }


    }
}
