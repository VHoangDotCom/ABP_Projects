using Hangfire;
using HangFire_AspNET.Models;
using HangFire_AspNET.Services;
using Microsoft.AspNetCore.Mvc;

namespace HangFire_AspNET.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DriverController : ControllerBase
    {
        //You can access this to track and manage your Hangfire Dashboard: https://localhost:7210/hangfire/
        private static List<Driver> drivers = new List<Driver>();
        private readonly ILogger<DriverController> _logger;

        public DriverController(ILogger<DriverController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IActionResult AddDriver(Driver driver)
        {
            if(ModelState.IsValid)
            {
                drivers.Add(driver);
                var jobId = BackgroundJob.Enqueue<IServiceManagement>(x => x.SendMail());
                return CreatedAtAction("GetDriver", new { driver.Id }, driver);
            }
            return BadRequest();
        }

        [HttpGet]
        public IActionResult GetDriver(Guid Id)
        {
            var driver = drivers.FirstOrDefault(x => x.Id == Id);

            if (driver == null)
                return NotFound();

            return Ok(driver);
        }

        [HttpDelete]
        public IActionResult DeleteDriver(Guid Id)
        {
            var driver = drivers.FirstOrDefault(y => y.Id == Id);

            if(driver == null)
                return NotFound();

            driver.Status = 0;

            RecurringJob.AddOrUpdate<IServiceManagement>(x => x.UpdateDatabase(), Cron.Minutely);

            return NoContent();
        }

    }
}
