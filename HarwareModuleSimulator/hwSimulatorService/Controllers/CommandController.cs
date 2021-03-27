using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace hwSimulatorService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommandController : ControllerBase
    {
        private readonly ILogger<CommandController> _logger;

        public CommandController(ILogger<CommandController> logger)
        {
            _logger = logger;
        }

        private static Boolean isRunning = false;

        private void TimedStop(object state)
        {
            // The state object is the Timer object.
            Timer t = (Timer) state;
            t.Dispose();
            _logger.LogInformation("The timer callback executes at: {time}", DateTimeOffset.Now);
            this.Stop();
        }

        [HttpPost]
        [Route("api/hw/stop")]
        public Boolean Stop()
        {
            isRunning = false ;
            _logger.LogInformation("Hardware stopped at: {time}", DateTimeOffset.Now);
            return isRunning;
        }

        [HttpPost]
        [Route("api/hw/start")]
        public Boolean Start(int seconds)
        {
            if ( seconds < 0 || seconds > 10){
                _logger.LogInformation("Hardware was un-able to start. Cause: Running period should be between 0 and 10. at: {time}", DateTimeOffset.Now);
                return false ;

            }
            if ( isRunning ){
                _logger.LogInformation("Hardware was un-able to start. Cause: Already running. at: {time}", DateTimeOffset.Now);
                return false ;
            }
            isRunning = true;
            _logger.LogInformation("Hardware running at: {time}", DateTimeOffset.Now);

            Timer timer = new Timer(new TimerCallback(TimedStop));
            timer.Change(seconds*1000,0);

            return isRunning;
        }

        [HttpGet]
        [Route("api/hw/status")]

        public Boolean Status()
        {
            return isRunning;
        }

    }
}
