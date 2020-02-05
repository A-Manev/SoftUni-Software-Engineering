using System;
using System.Collections.Generic;

public class DateModifier
{
    public long CalculateDifferenceBetweenTwoDatas(List<DateTime> datas)
    {
        TimeSpan result = datas[0] - datas[1];

        string data = Data(result);

        string[] currentDays = data.Split(".");

        long days = long.Parse(currentDays[0]);

        return Math.Abs(days);
    }

    public string Data(TimeSpan result)
    {
        string date = result.ToString();

        date = date.Replace("/", "");
        date = date.Replace(":", "");
        date = date.Replace(" ", "");
        date = date.Replace("AM", "");
        date = date.Replace("PM", "");
        return date;
    }
}

