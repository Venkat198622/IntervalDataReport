using IntervalReport.DataAccessLayer;
using IntervalReport.Models;
using System.Collections.Generic;
namespace IntervalReport.BusinessLayer
{
    /// <summary>
    /// This class  represent business logic  
    /// </summary>
    public class IntervalBusinessLogic : IIntervalBusinessLogic
    {
        /// <summary>
        /// this private filed used to hold the business logic data
        /// </summary>
        IIntervalRepository _intervalRepository;
        /// <summary>
        /// this constructor used for DI to data access  interface 
        /// </summary>
        /// <param name="intervalRepository"></param>
        public IntervalBusinessLogic(IIntervalRepository intervalRepository)
        {
            _intervalRepository = intervalRepository;
        }
        /// <summary>
        /// this method is used to get the data from data base logic
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IntervalResponse> GetIntervalDataByHourly()
        {
            return _intervalRepository.GetIntervalDataByHourly();
        }
    }
}
