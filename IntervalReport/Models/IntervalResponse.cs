namespace IntervalReport.Models
{
    public class IntervalResponse
    {
        public long DeliveryPoint { get; set; }
        public string Date { get; set; }
        public int TimeSlot { get; set; }
        public decimal SlotVal { get; set; }
    }
}
