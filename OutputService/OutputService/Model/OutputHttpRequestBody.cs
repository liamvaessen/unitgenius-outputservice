using System;
using System.Diagnostics.CodeAnalysis;

namespace OutputService.Model
{
    /// <summary>
    /// Represents the HTTP request body for output data.
    /// </summary>
    public class OutputHttpRequestBody
    {
        /// <summary>
        /// Gets or sets the unique identifier for the request.
        /// </summary>
        [AllowNull]
        public Guid? RequestId { get; set; }
    }
}
