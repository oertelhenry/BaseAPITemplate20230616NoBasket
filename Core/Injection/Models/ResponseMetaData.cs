namespace Core.Injection.Models
{
    public class ResponseMetaData
    {
        /// <summary>
        /// ResultCode describes the result of the request
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// The Detail field contains a brief description of the status of the request.
        /// </summary>
        public string Detail { get; set; }

        /// <summary>
        /// The Message section gives a generic overview of the error that occurred
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// The Name field gives a high-level indication of the result
        /// </summary>
        public string Name { get; set; }
    }
}