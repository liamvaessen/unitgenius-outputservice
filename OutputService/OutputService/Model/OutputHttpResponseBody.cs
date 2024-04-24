using System;
using System.Diagnostics.CodeAnalysis;

namespace OutputService.Model
{
    /// <summary>
    /// Represents the HTTP response body for the output service.
    /// </summary>
    public class OutputHttpResponseBody
    {
        /// <summary>
        /// Gets or sets the unique identifier for the request.
        /// </summary>
        public Guid RequestId { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the user.
        /// </summary>
        public Guid UserId { get; set; }
        
        /// <summary>
        /// Gets or sets the generation type.
        /// Will be converted to GenerationType enum: 0 = UnitTest, 1 = Documentation.
        /// </summary>
        public int GenerationType { get; set; }

        /// <summary>
        /// Gets or sets the status of the output.
        /// Will be converted to Status enum: 0 = Requested, 1 = Completed, 2 = Failed.
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// Gets or sets the result of the output.
        /// </summary>
        [AllowNull]
        public string? Result { get; set; }
    }
}
