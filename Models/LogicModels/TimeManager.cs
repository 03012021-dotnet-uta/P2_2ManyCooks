using System;

namespace Models.LogicModels
{
    class TimeManager
    {
        public static DateTime GetTimeNow()
        {
            var timeUtc = DateTime.UtcNow;
            TimeZoneInfo easternZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            DateTime easternTime = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, easternZone);
            return easternTime;
        }
    }
}