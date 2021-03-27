using System;

namespace ViralLinks
{
    public static class HelperMethods
    {
        public static string GetTimeAgo(this DateTime time)
        {
            // if(teTime.Now)
            //     return "0m ago";
            var timeSpan = TimeSpan.FromTicks((DateTime.Now - time).Ticks);
            var days = timeSpan.Days;
            var hours = timeSpan.Hours;
            var minutes = timeSpan.Minutes;
            // check for years
            if(days >= 365)
                return $"{(int)Math.Floor(days / 365.0)}years ago";
            // check for months
            if(days >= 30)
                return $"{(int)Math.Floor(days / 30.0)} months ago";
            // check for weeks
            if(days >= 7)
                return $"{(int)Math.Floor(days/ 7.0)} weeks ago";
            if(days > 0)
                return $"{days}d ago";
            // check for hours
            if(hours >= 1)
                return $"{hours}h ago";
            // zero minute
            if(minutes == 0)
                return $"now";
            return $"{minutes}m ago";
        }

        public static string GetTodaysDate(this DateTime time)
        {
            var day = time.Date.Day;
            var day_string = day == 1 ? "1st" : day == 2 ? "2nd" : day == 3 ? "3rd" : $"{day}th";
            var month = time.Date.Month;
            var year = time.Date.Year;
            var month_string = month.GetMonth();
            return $"{day_string} {month_string} {year}";
        }

        public static string GetMonth(this int month)
        {
            switch(month)
            {
                case 1:return "Jan";
                case 2:return "Feb";
                case 3:return "Mar";
                case 4:return "Apr";
                case 5:return "May";
                case 6:return "Jun";
                case 7:return "July";
                case 8:return "Aug";
                case 9:return "Sept";
                case 10:return "Oct";
                case 11:return "Nov";
                case 12:return "Dec";
                default: return string.Empty;
            }
        }

        public static string GetPostsCount(this int count)
        {
            if(count > 999)
                return Math.Round(count / 1000.0,1).ToString() + "k";
            return count.ToString();
        }
    }
}