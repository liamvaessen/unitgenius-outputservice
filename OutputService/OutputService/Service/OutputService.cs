using Newtonsoft.Json;
using OutputService.Model;
using OutputService.Service.Abstraction;

namespace OutputService.Service
{
    /// <summary>
    /// Represents the output service that handles generation requests and provides output data.
    /// </summary>
    public class OutputService : IOutputService
    {
        private List<GenerationRequest> _outputGenerationRequests;
        private MessageReceiverHelper _completedMessageReceiver;

        /// <summary>
        /// Initializes a new instance of the <see cref="OutputService"/> class.
        /// </summary>
        public OutputService(IConfiguration configuration)
        {
            // Initialize the list of generation requests.
            _outputGenerationRequests = new List<GenerationRequest>();

            string rabbitmqHost = configuration["RABBITMQ_HOST"] ?? throw new ArgumentNullException("RABBITMQ_HOST");
            int rabbitmqPort = int.TryParse(configuration["RABBITMQ_PORT"], out int port) ? port : 5672; // default RabbitMQ port
            string rabbitmqUser = configuration["RABBITMQ_USERNAME"] ?? throw new ArgumentNullException("RABBITMQ_USERNAME");
            string rabbitmqPass = configuration["RABBITMQ_PASSWORD"] ?? throw new ArgumentNullException("RABBITMQ_PASSWORD");
            string rabbitmqVhost = configuration["RABBITMQ_VHOST"] ?? "/"; // default RabbitMQ virtual host

            // Initialize the message receiver helper. Interacts with the generationRequests_Completed queue.
            // This queue receives messages from the generation service when a generation request is completed.
            // The receiver runs continuously and listens for new messages.
            _completedMessageReceiver = new MessageReceiverHelper(
                callback: message =>
                {
                    // Deserialize the received message and add the generation request to the list.
                    GenerationRequest request = JsonConvert.DeserializeObject<GenerationRequest>(message);

                    // Add the generation request to the list of requests for retrieval when asked.
                    _outputGenerationRequests.Add(request);
                },
                queueName: "generationRequests_Completed",
                hostName: rabbitmqHost,
                hostPort: rabbitmqPort,
                hostUsername: rabbitmqUser,
                hostPassword: rabbitmqPass,
                virtualHost: rabbitmqVhost);
        }

        /// <summary>
        /// Gets all the generation requests stored in the output service.
        /// </summary>
        /// <returns>A list of <see cref="GenerationRequest"/> objects.</returns>
        public List<GenerationRequest> GetAllGenerationRequests() => _outputGenerationRequests;

        /// <summary>
        /// Gets the output data for the specified generation request.
        /// </summary>
        /// <param name="requestBody">The request body containing the generation request ID.</param>
        /// <returns>An <see cref="OutputHttpResponseBody"/> object containing the output data.</returns>
        public OutputHttpResponseBody GetOutput(OutputHttpRequestBody requestBody)
        {
            // Find the generation request with the specified ID.
            GenerationRequest foundGenerationRequest = _outputGenerationRequests.FirstOrDefault(request => request.RequestId == requestBody.RequestId);

            // Return the output data if the generation request is found.
            return new OutputHttpResponseBody
            {
                RequestId = foundGenerationRequest.RequestId,
                UserId = foundGenerationRequest.UserId,
                Result = foundGenerationRequest.Result
            };
        }
    }
}
