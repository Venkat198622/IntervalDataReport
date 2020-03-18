using IntervalReport.Models;
using System.Collections.Generic;
namespace IntervalReport.DataAccessLayer
{
    /// <summary>
    /// This interface represent database logic contract
    /// </summary>
    public interface IIntervalRepository
    {
        /// <summary>
        /// this method is used to get the data from database
        /// </summary>
        /// <returns></returns>
        IEnumerable<IntervalResponse> GetIntervalDataByHourly();
    }
}
