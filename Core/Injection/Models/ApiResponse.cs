namespace Core.Injection.Models
{
    /// <summary>
    /// Standardised API response wrapper used by all api actions that return data. The Data
    /// property will contain the results of the operation. The MetaData property will contain
    /// information about the result of the API operation (success, failure, etc). If any errors
    /// occur the information will be detailed within the property
    /// </summary>
    /// <typeparam name="T">The type of the API response</typeparam>
    public class ApiResponse<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiResponse{T}"/> class.
        /// </summary>
        /// <param name="data">Response Data</param>
        /// <param name="code">Response Code</param>
        /// <param name="message">Response message</param>
        /// <param name="detail">Additional detail for the related message</param>
        public ApiResponse(T data, string code = "R00", string message = "Operation successful.", string detail = null)
        {
            Data = data;
            MetaData = new ResponseMetaData { Code = code, Message = message, Detail = detail };
        }

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>The data.</value>
        public T Data { get; set; }

        /// <summary>
        /// Gets or sets the meta data.
        /// </summary>
        /// <value>The meta data.</value>
        public ResponseMetaData MetaData { get; set; }
    }
}