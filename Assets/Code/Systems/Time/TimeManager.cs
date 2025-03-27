using System;
using UnityEngine;

public class TimeManager : Singleton<TimeManager> ,IBind<TimeData>
{
    [field: SerializeField] public SerializableGuid Id { get; set; } = SerializableGuid.NewGuid();
    private TimeData _timeData;
    
    [SerializeField] private float _timeScale = 1f;
    private DateTime _utcTimeOnLoad;
    private DateTime _localTimeOnLoad;
    private float _secondsSinceLastSave;
    public float SecondsSinceLastSave => _secondsSinceLastSave;

    public event Action OnNewDay;
    public event Action OnHourChanged;
    public event Action OnMinuteChanged;
    public event Action OnSecondChanged;

    private float _timer;
    public int second;
    public int minute;
    public int hour;

    private void Start()
    {
        GetTimeUTC();
    }

    private void GetTimeUTC()
    {
        _utcTimeOnLoad = DateTime.UtcNow;
        
        TimeSpan fromLastSaveToNow = new TimeSpan(_utcTimeOnLoad.Ticks - _timeData.Ticks);
        _secondsSinceLastSave = (float) fromLastSaveToNow.TotalSeconds;
        
        _localTimeOnLoad = DateTime.Now;
        
        second = _localTimeOnLoad.Second;
        minute = _localTimeOnLoad.Minute;
        hour = _localTimeOnLoad.Hour;
    }
    
    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= _timeScale)
        {
            _timer -= _timeScale;
            second++;
            bool cycleSeconds = second >= 60;
            if (cycleSeconds)
            {
                second = 0;
                minute++;
            }
            OnSecondChanged?.Invoke();

            if (cycleSeconds)
            {
                bool cycleMinutes = minute >= 60;
                if (cycleMinutes)
                {
                    minute = 0;
                    hour++;
                }
                OnMinuteChanged?.Invoke();

                if (cycleMinutes)
                {
                    bool cycleHours = hour >= 60;
                    if (cycleHours)
                    {
                        hour = 0;
                        OnNewDay?.Invoke();
                    }
                    OnHourChanged?.Invoke();
                }
            }
        }
    }
    
    public void Bind(TimeData data)
    {
        _timeData = data;
    }
}
