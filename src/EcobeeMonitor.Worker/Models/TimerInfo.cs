namespace EcobeeMonitor.Worker.Models
{
    public class TimerInfo
    {
        public TimerScheduleStatus ScheduleStatus { get; set; }

        public bool IsPastDue { get; set; }
    }
}
