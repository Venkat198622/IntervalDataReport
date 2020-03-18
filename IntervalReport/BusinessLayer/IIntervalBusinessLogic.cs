using IntervalReport.Models;
using System.Collections.Generic;
namespace IntervalReport.BusinessLayer
{
    /// <summary>
    /// This interface represent business  logic contract
    /// </summary>
    public interface IIntervalBusinessLogic
    {
        /// <summary>
        /// this interface method used to retreive the interval data from database
        /// </summary>
        /// <returns></returns>
        IEnumerable<IntervalResponse> GetIntervalDataByHourly();
    }
}
