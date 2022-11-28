namespace BusinessLayer.Utilities;

public class TimeFormatter
{

    private int _amount;
    private string _timeFormat;

    public string FormatTime(DateTime time)
    {
        FindUnitOfTime(time);

        var timeDifference = _amount > 1
            ? $"{_amount} {_timeFormat}s ago"
            : $"{_amount} {_timeFormat} ago";

        return timeDifference;
    }

    private void FindUnitOfTime(DateTime time)
    {
        var dateDifference = DateTime.Now - time;
        if (dateDifference.Seconds > 0)
        {
            _amount = dateDifference.Seconds;
            _timeFormat = "second";
        }

        if (dateDifference.Minutes > 0)
        {
            _amount = dateDifference.Minutes;
            _timeFormat = "minute";
        }

        if (dateDifference.Hours > 0)
        {
            _amount = dateDifference.Hours;
            _timeFormat = "hour";
        }

        if (dateDifference.Days > 0)
        {
            _amount = dateDifference.Days;
            _timeFormat = "day";
        }
    }
}