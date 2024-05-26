using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameTimeStamp
{
    public int year;
    public enum Season
    {
        Spring,
        Summer,
        Fall,
        Winter
    }

    public enum DayOfTheWeek
    {
        //for 2023 (Jan.1, 2023 == Sun)
        Sat,
        Sun,
        Mon,
        Tue,
        Wen,
        Thur,
        Fri
    }

    public Season season;
    public int day;
    public int hour;
    public int minute;

    //Constructor to set up the calss
    public GameTimeStamp(int year, Season season, int day, int hour, int minute)
    {
        this.year = year;
        this.season = season;
        this.day = day;
        this.hour = hour;
        this.minute = minute;
    }

    //create a new instance of a GameTimestamp from the existing one
    public GameTimeStamp(GameTimeStamp timeStamp)
    {
        this.year = timeStamp.year;
        this.season = timeStamp.season;
        this.day = timeStamp.day;
        this.hour = timeStamp.hour;
        this.minute = timeStamp.minute;
    }

    public void UpdataClock()
    {
        minute++;

        //increment of hour
        if(minute >= 60)
        {
            minute = 0;
            hour++;

        }

        //increment of day
        if(hour >= 24)
        {
            hour = 0;
            day++;
        }

        //season change
        if(day > 30)
        {
            day = 1;

            if(season == Season.Winter)
            {
                season = Season.Spring;
                year++;

            }
            else
            {
                //enum increnment
                season++;
            }
        }
    }

    /// <summary>
    /// Get the day of the week by convert curr date to the days passed in a year
    /// </summary>
    /// <returns></returns>
    public DayOfTheWeek GetDayOfTheWeek()
    {
        int daysPassed = YearsToDays(year) + SeasonsToDays(season) + day;

        // divide by 7 and get the reminder
        int dayOfWeekIndex = daysPassed % 7;

        return (DayOfTheWeek)dayOfWeekIndex;

    }
    //-------------------Convert time
    public static int HoursToMinutes(int hour)
    {
        return hour * 60;
    }

    public static int DaysToHours(int day)
    {
        return day * 24;
    }

    public static int SeasonsToDays(Season season)
    {
        int seasonIndex = (int)season;
        return seasonIndex * 30;
    }

    public static int YearsToDays(int year)
    {
        return year * 4 * 30;
    }

    //--------------------------------------for watered status duration-----------------------------------------------
    //calculate the difference between 2 timestamps
    public static int CompareTimestamps(GameTimeStamp timeStamp1, GameTimeStamp timeStamp2)
    {
        //convert timestamps to hours
        int timestamp1Hours = DaysToHours(YearsToDays(timeStamp1.year)) + DaysToHours(SeasonsToDays(timeStamp1.season)) 
                              + DaysToHours(timeStamp1.day) + timeStamp1.hour;

        int timestamp2Hours = DaysToHours(YearsToDays(timeStamp2.year)) + DaysToHours(SeasonsToDays(timeStamp2.season))
                              + DaysToHours(timeStamp2.day) + timeStamp2.hour;

        int difference = timestamp2Hours - timestamp1Hours;

        return Mathf.Abs(difference);
    }

}
