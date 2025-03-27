using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundImageManager : Singleton<BackgroundImageManager>
{
    private const float crossfadeDuration = 10f;
    
    [SerializeField] private Image _bgImage;
    [SerializeField] private Image _fadeImage;
    
    [Header("Backgrounds")]
    public Sprite morningBg;
    public Sprite eveningBg;
    public Sprite nightBg;
    public Sprite sunriseBg;
    public Sprite sunsetBg;
    private Sprite _desiredSprite;
    
    [Header("Crossfade")]
    [SerializeField] private bool _crossfading;
    
    private void OnEnable()
    {
        DayTimeManager.Instance.OnDayTimeChanged += SetBackgroundImage;
    }
    
    private void OnDisable()
    {
        DayTimeManager.Instance.OnDayTimeChanged -= SetBackgroundImage;
    }

    private void SetBackgroundImage()
    {
        DayTime dayTime = DayTimeManager.Instance.dayTime;
        
        _desiredSprite = dayTime switch
        {
            DayTime.Morning => morningBg,
            DayTime.Evening => eveningBg,
            DayTime.Night => nightBg,
            DayTime.Sunrise => sunriseBg,
            DayTime.Sunset => sunsetBg,
            _ => _desiredSprite
        };
    }

    private void Update()
    {
        if (_desiredSprite != _bgImage.sprite) StartCrossfade();
    }
    
    private void StartCrossfade()
    {
        if(_crossfading) return;
        _crossfading = true;
        
        _fadeImage.sprite = _bgImage.sprite; 
        _bgImage.sprite = _desiredSprite;
        _bgImage.DOFade(0, crossfadeDuration).From(1).SetEase(Ease.InOutSine);
        _fadeImage.DOFade(1, crossfadeDuration).From(0).SetEase(Ease.InOutSine);
        
        Invoke(nameof(ResetCrossfade), crossfadeDuration);
    }

    private void ResetCrossfade()
    {
        _crossfading = false;
    }
}