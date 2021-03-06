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

        public static string GetPostsCount(this int count)
        {
            if(count > 999)
                return Math.Round(count / 1000.0,1).ToString() + "k";
            return count.ToString();
        }
    }
}