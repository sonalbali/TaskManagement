using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Task_Managment_System.Data;
using Task_Managment_System.Migrations;
using Task_Managment_System.Model.Entities;
namespace Task_Managment_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly TaskDBContext _TaskDBContext;
        public TaskController(TaskDBContext _TaskDBContext)
        {

            this._TaskDBContext = _TaskDBContext;
        }

        [HttpGet]
        public IActionResult GetAllTasks()
        {
            var TaskList = _TaskDBContext.TaskItems.ToList();

            return Ok(TaskList);
        }

        //[HttpGet]
        //[Route("{Username}")]
        //public IActionResult getTask(string Username)
        //{
        //    var TaskList = _TaskDBContext.TaskItems.FirstOrDefault(u=>u.Assign_To == Username);         


        //    return TaskList == null ? NotFound() : Ok(TaskList);
        //}

        [HttpGet]
        [Route("{UserId}")]
        public IActionResult getTask(int UserId)
        {
            var TaskList = _TaskDBContext.TaskItems.Where(u => u.UserId == UserId).ToList();
            return TaskList.Any() ? Ok(TaskList) : NotFound();
        }


        [HttpGet("search/{id}")]        
        public IActionResult getTaskbyId(int id)
        {
            var TaskList = _TaskDBContext.TaskItems.Find(id); 
            return TaskList == null ? NotFound() : Ok(TaskList);
        }

        [HttpPost]
        public IActionResult createTask(TaskItem _taskItem)
        {    
            //var currentUsername = User.Identity.UserName;            

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_taskItem != null && !string.IsNullOrEmpty(_taskItem.Assign_To))
            {
                //Fetch UserId by UserName(Assign_To) from the Registration table
                var user = _TaskDBContext.Users
                                         .Where(u => u.Name ==_taskItem.Assign_To)
                                         .FirstOrDefault();

                if (user != null)
                {
                    // Set the UserId in TaskItem
                    _taskItem.UserId = user.UserId;
                    _taskItem.Assign_To = user.Name;
                }
                else
                {
                    // Handle case where the user with the specified UserName is not found
                    return NotFound("User not found");
                }
            }

            _TaskDBContext.TaskItems.Add(_taskItem);
             _TaskDBContext.SaveChangesAsync();
            return CreatedAtAction(nameof(getTaskbyId), new { id = _taskItem.Task_Id }, _taskItem);

        }
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateTask(int id, [FromBody] TaskItem taskItem)
        {            
            var existingTask = await _TaskDBContext.TaskItems.FirstOrDefaultAsync(t => t.Task_Id == id);

            if (existingTask == null)
            {
                return NotFound("Task not found.");
            }

            if (existingTask.Assign_To!= taskItem.Assign_To)
            {
                //Fetch UserId by UserName(Assign_To) from the Registration table
                var user = _TaskDBContext.Users
                                         .Where(u => u.Name == taskItem.Assign_To)
                                         .FirstOrDefault();
                if (user != null)
                {
                    // Set the UserId in TaskItem
                    taskItem.UserId = user.UserId;
                    taskItem.Assign_To = user.Name;
                }

            }
            // Update fields manually
            existingTask.Title = taskItem.Title;
            existingTask.Description = taskItem.Description;
            existingTask.Status = taskItem.Status;
            existingTask.DueDate = taskItem.DueDate;
            existingTask.Assign_To = taskItem.Assign_To;
            existingTask.UserId = taskItem.UserId;

            try
            {
                await _TaskDBContext.SaveChangesAsync();
                return Ok(existingTask);
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the task.");
            }
        }

        [HttpDelete()]
        [Route("{taskId}")]
        public IActionResult deleteTask(int taskId)
        {

            var Task = _TaskDBContext.TaskItems.Find(taskId);

            if (Task is null)
            {
                return NotFound();
            }
            else
            {
                _TaskDBContext.TaskItems.Remove(Task);
                _TaskDBContext.SaveChanges();
                return Ok();
            }

        }

    }
}
