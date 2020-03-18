using IntervalReport.BusinessLayer;
using IntervalReport.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
namespace IntervalReport.Controllers
{
    public class IntervalController : Controller
    {
        /// <summary>
        /// this private filed used to hold the business logic data
        /// </summary>
        IIntervalBusinessLogic _intervalBusinessLogic;
        /// <summary>
        /// this constructor used for DI to business logic interface 
        /// </summary>
        /// <param name="intervalBusinessLogic"></param>
        public IntervalController(IIntervalBusinessLogic intervalBusinessLogic)
        {
            _intervalBusinessLogic = intervalBusinessLogic;
        }
        /// <summary>
        /// this method used to retreive the interval data from businsess layer and display thed data
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            var response = _intervalBusinessLogic.GetIntervalDataByHourly();
            if (response != null)
            {
                return View(response);
            }
            return View(new List<IntervalResponse>());
        }
        /// <summary>
        /// this method is used to redired error page
        /// </summary>
        /// <returns></returns>
        public IActionResult Error()
        {
            return View();
        }
    }
}