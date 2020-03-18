namespace IntervalReport.Models
{
    /// <summary>
    /// this class used store the error details.
    /// </summary>
    public class ErrorDetails
    {
        /// <summary>
        /// error message when the excepion occured
        /// </summary>
        public string errormessage { get; set; }
        /// <summary>
        /// method name where the excepion occured
        /// </summary>
        public string methodname { get; set; }
        /// <summary>
        /// stack details when the excepion occured
        /// </summary>
        public string stacktrace { get; set; }
    }
}
