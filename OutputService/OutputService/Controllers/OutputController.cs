using Microsoft.AspNetCore.Authorization;
using OutputService.Model;
using OutputService.Service;
using OutputService.Service.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace OutputService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OutputController : ControllerBase
    {
        private IOutputService OutputService { get; }
        public OutputController(IOutputService outputService)
        {
            OutputService = outputService;
        }

        [HttpGet]
        public ActionResult<List<GenerationRequest>> GetAll() =>
            OutputService.GetAllGenerationRequests();

        [HttpPost("/retrieve")]
        [Authorize]
        public ActionResult<OutputHttpResponseBody> RetrieveOutput(OutputHttpRequestBody requestBody)
        {
            return OutputService.GetOutput(requestBody);
        }
    }
}
