using Microsoft.AspNetCore.Mvc;

namespace AksHealthTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MachineController : ControllerBase
    {
        [HttpGet(Name = "GetMachine")]
        public string Get()
        {
            return System.Environment.MachineName;
        }
    }
}