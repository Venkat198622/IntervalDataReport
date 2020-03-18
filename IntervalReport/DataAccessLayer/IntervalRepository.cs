using IntervalReport.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
namespace IntervalReport.DataAccessLayer
{
    public class IntervalRepository : IIntervalRepository
    {
        /// <summary>
        /// this method used to retreive the interval data from database
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IntervalResponse> GetIntervalDataByHourly()
        {
            List<IntervalResponse> responseList = new List<IntervalResponse>();
            try
            {
                using (SqlDataReader reader = DataAccess.ExecuteQuery("usp_intervaldata_select", commandType: System.Data.CommandType.StoredProcedure))
                {
                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            IntervalResponse response = new IntervalResponse();
                            response.DeliveryPoint = Convert.ToInt64(reader["DeliveryPoint"]);
                            response.Date = Convert.ToDateTime(reader["Date"]).ToString("dd/MM/yyyy");
                            response.TimeSlot = Convert.ToInt32(reader["TimeSlot"]);
                            response.SlotVal = Convert.ToDecimal(reader["SlotVal"]);
                            responseList.Add(response);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorDetails errorDetails = new ErrorDetails();
                errorDetails.methodname = Convert.ToString("GetIntervalDataByHourly ") + System.Environment.NewLine;
                errorDetails.errormessage = ex.Message;
                var trace = new System.Diagnostics.StackTrace(ex);
                errorDetails.stacktrace = Convert.ToString(trace);
                //Log the exception here
            }

            return responseList;
        }
    }
}
