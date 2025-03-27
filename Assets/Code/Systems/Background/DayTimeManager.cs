using System;
using UnityEngine;

public class DayTimeManager : Singleton<DayTimeManager>
{
    public event Action OnDayTimeChanged;
    
    public DayTime dayTime;

    private void OnEnable()
    {
        TimeManager.instance.OnHourChanged += OnHourChanged;
        TimeManager.instance.OnMinuteChanged += OnMinuteChanged;
    }
    
    private void OnDisable()
    {
        TimeManager.instance.OnHourChanged -= OnHourChanged;
        TimeManager.instance.OnMinuteChanged -= OnMinuteChanged;
    }

    private void Start()
    {
        SetDayTime();
    }

    private void SetDayTime()
    {
        int currHour = TimeManager.instance.hour;
        int currMinute = TimeManager.instance.minute;
        
        if(currHour > 6 && currHour < 12) dayTime = DayTime.Morning;
        else if (currHour > 12 && currHour < 18) dayTime = DayTime.Evening;
        else dayTime = DayTime.Night;

        if (currMinute < 15)
        {
            if(currHour == 6) dayTime = DayTime.Sunrise;
            else if(currHour == 18) dayTime = DayTime.Sunset;
        }
        
        OnDayTimeChanged?.Invoke();
    }

    private void OnHourChanged()
    {
        DayTime previousDayTime = dayTime;
        
        int currHour = TimeManager.instance.hour;
        
        if(currHour > 6 && currHour < 12) dayTime = DayTime.Morning;
        else if (currHour > 12 && currHour < 18) dayTime = DayTime.Evening;
        else dayTime = DayTime.Night;
        
        if(previousDayTime != dayTime) OnDayTimeChanged?.Invoke();
    }

    private void OnMinuteChanged()
    {
        DayTime previousDayTime = dayTime;
        
        int currHour = TimeManager.instance.hour;
        int currMinute = TimeManager.instance.minute;

        if (currMinute == 15)
        {
            if(currHour == 6) dayTime = DayTime.Sunrise;
            else if(currHour == 18) dayTime = DayTime.Sunset;
        }
        
        if(previousDayTime != dayTime) OnDayTimeChanged?.Invoke();
    }
}