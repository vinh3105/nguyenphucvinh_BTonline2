using DeviceManagementAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DeviceManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViewsController : ControllerBase
    {
        private readonly DeviceServiceContext _context;

        public ViewsController(DeviceServiceContext context)
        {
            _context = context;
        }

        // GET: api/views/service-assignments
        [HttpGet("service-assignments")]
        public async Task<IActionResult> GetServiceAssignments()
        {
            var data = await _context.ViewServiceAssignments.ToListAsync();
            return Ok(data);
        }

        // GET: api/views/used-status
        [HttpGet("used-status")]
        public async Task<IActionResult> GetUsedStatus()
        {
            var data = await _context.ViewUsedStatuses.ToListAsync();
            return Ok(data);
        }

        // GET: api/views/assignments-per-device
        [HttpGet("assignments-per-device")]
        public async Task<IActionResult> GetAssignmentsPerDevice()
        {
            var data = await _context.ViewAssignmentsPerDevices.ToListAsync();
            return Ok(data);
        }

        // OPTIONAL: Làm mới dữ liệu trong Materialized View
        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshViews()
        {
            var conn = _context.Database.GetDbConnection();
            await conn.OpenAsync();
            using var cmd = conn.CreateCommand();

            cmd.CommandText = @"
                REFRESH MATERIALIZED VIEW ""View_ServiceAssignments"";
                REFRESH MATERIALIZED VIEW ""View_UsedStatus"";
                REFRESH MATERIALIZED VIEW ""View_AssignmentsPerDevice"";
            ";
            await cmd.ExecuteNonQueryAsync();

            return Ok("Views have been refreshed.");
        }
    }
}
