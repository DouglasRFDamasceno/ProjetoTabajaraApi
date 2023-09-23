﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ProjetoTabajaraApi.Data.Dtos.Attendance;
using ProjetoTabajaraApi.Data.Dtos.Student;
using ProjetoTabajaraApi.Models;
using ProjetoTabajaraApi.Services;

namespace ProjetoTabajaraApi.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    [EnableCors("MyPolicy")]
    public class AttendanceController: ControllerBase
    {
        public AttendanceService _attendanceService;

        public AttendanceController(AttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }

        [HttpPost("createOrUpdate")]
        [Authorize]
        public bool CreateOrUpdateAttendance(CreateAttendanceDto attendanceDto)
        {
            bool isOk = _attendanceService.CreateOrUpdateAttendance(attendanceDto);
            return isOk;
        }

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult UpdateStudent(int id, [FromBody] UpdateAttendanceDto attendanceDto)
        {
            var student = _attendanceService.UpdateAttendance(id, attendanceDto);

            if (student == null) return NotFound();

            return NoContent();
        }

        [HttpGet("{id}")]
        [Authorize]
        public IActionResult GetAttendance(int id)
        {
            var attendanceDto = _attendanceService.GetAttendance(id);

            return Ok(attendanceDto);
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetAttendances(DateTime startDate, DateTime finalDate)
        {
            var attendanceDto = _attendanceService.GetAttendances(startDate, finalDate);

            return Ok(attendanceDto);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult DeleteAttendance(int id)
        {
            var deletedAttendance = _attendanceService.DeleteAttendance(id);

            if (!deletedAttendance) return NotFound();

            return NoContent();
        }

    }
}
