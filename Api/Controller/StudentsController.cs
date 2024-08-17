using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Api.Data;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Api.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    
    public class StudentsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StudentsController(AppDbContext context)
        {
            _context=context;
            
        }

        [HttpGet]
        public async Task<IEnumerable<Student>> GetStudents()
        {
            var students = await _context.Students.AsNoTracking().ToListAsync();

            return students;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Student student)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _context.AddAsync(student);

            var result = await _context.SaveChangesAsync();
            if(result>0)
            {
                return Ok();
            }

            else{
                return BadRequest();
            }

        }
        [HttpGet("{id:int}")]
        public async  Task<ActionResult<Student>> GetStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if(student is null)
                return NotFound();
            else
                return Ok(student);
        }

       
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var student = await _context.Students.SingleOrDefaultAsync(x=>x.Id==id);

            if(student is null)
            {

                return NotFound();
            }
            _context.Remove(student);

            var result =await _context.SaveChangesAsync();
            if(result > 0){

                return Ok();
            }

            
            return BadRequest("Unable to Delete student");
        }

           [HttpPut("{id:int}")]
        // api/students/1
        public async Task<IActionResult> EditStudent(int id, Student student)
        {
            var studentFromDb = await _context.Students.FindAsync(id);

            if(studentFromDb is null)
            {
                return BadRequest("Student Not found");
            }

            studentFromDb.Name = student.Name;
            studentFromDb.Address = student.Address;
            studentFromDb.Email = student.Email;
            student.PhoneNumber = student.PhoneNumber;

            var result = await _context.SaveChangesAsync();

            if(result > 0)
            {
                return Ok();
            }

            return BadRequest();


        }



        

    }
}