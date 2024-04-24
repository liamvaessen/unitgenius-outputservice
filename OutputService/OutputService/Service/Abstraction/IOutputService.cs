using OutputService.Model;

namespace OutputService.Service.Abstraction
{
    /// <summary>
    /// Represents the interface for the output service.
    /// </summary>
    public interface IOutputService
    {
        /// <summary>
        /// Retrieves all existing output generation requests.
        /// </summary>
        /// <returns>A list of GenerationRequest objects.</returns>
        List<GenerationRequest> GetAllGenerationRequests();

        /// <summary>
        /// Retrieves the output for the specified request.
        /// </summary>
        /// <param name="requestBody">The request body containing the necessary information.</param>
        /// <returns>An OutputHttpResponseBody object representing the output.</returns>
        OutputHttpResponseBody GetOutput(OutputHttpRequestBody requestBody);
    }
}
