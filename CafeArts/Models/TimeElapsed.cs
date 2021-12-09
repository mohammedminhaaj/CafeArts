using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CafeArts.Models
{
    public class TimeElapsed
    {
        
        public static TimeSpan CalculatedTime;
        public static string GetTime(DateTime reviewTime)
        {
            CalculatedTime = DateTime.Now - reviewTime;

            if (CalculatedTime.Days >= 1)
            {
                return "Posted on "+reviewTime.ToShortDateString();
            }
            else {
                if (CalculatedTime.Hours == 0 && CalculatedTime.Minutes == 0)
                {
                    return "Posted less than a minute ago";
                }

                else
                {
                    if (CalculatedTime.Hours > 0)
                        return CalculatedTime.Hours==1?"Posted " + CalculatedTime.Hours.ToString() + " hour ago": "Posted " + CalculatedTime.Hours.ToString() + " hours ago";
                    else
                        return CalculatedTime.Minutes==1?"Posted " + CalculatedTime.Minutes.ToString() + " minute ago": "Posted " + CalculatedTime.Minutes.ToString() + " minutes ago";
                }
            }

        }
    }
}