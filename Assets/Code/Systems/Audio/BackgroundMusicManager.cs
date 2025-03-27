using System;
using UnityEngine;

public class BackgroundMusicManager : Singleton<BackgroundMusicManager>
{
    [Header("Tracks")]
    public string sunriseTrackName;
    public string morningTrackName;
    public string eveningTrackName;
    public string sunsetTrackName;
    public string nightTrackName;

    private void OnEnable()
    {
        DayTimeManager.Instance.OnDayTimeChanged += PlayTrack;
    }

    private void OnDisable()
    {
        DayTimeManager.Instance.OnDayTimeChanged -= PlayTrack;
    }

    private void PlayTrack()
    {
        DayTime dayTime = DayTimeManager.Instance.dayTime;
        
        switch (dayTime)
        {
            case DayTime.Sunrise:
                MusicManager.Instance.Set(sunriseTrackName);
                break;
            case DayTime.Morning:
                MusicManager.Instance.Set(morningTrackName);
                break;
            case DayTime.Evening:
                MusicManager.Instance.Set(eveningTrackName);
                break;
            case DayTime.Sunset:
                MusicManager.Instance.Set(sunsetTrackName);
                break;
            case DayTime.Night:
                MusicManager.Instance.Set(nightTrackName);
                break;
        }
        
        MusicManager.Instance.Play();
    }
}