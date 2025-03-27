using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CustomAnimator : MonoBehaviour
{
    [SerializeField] private UnityEvent _onEnd;
    [SerializeField] private KeyFrame[] _keyFrames;
    [SerializeField] private float _fps = 60f;
    [SerializeField] private bool _loop = true;
    private SpriteRenderer _spriteRenderer;
    private Image _image;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _image = GetComponent<Image>();
    }

    public void Play()
    {
        StartCoroutine(Animation());
    }

    private IEnumerator Animation()
    {
        float frameRate = 1 / _fps;

        foreach (var key in _keyFrames)
        {
            if(_spriteRenderer != null) _spriteRenderer.sprite = key.sprite;
            if (_image != null) _image.sprite = key.sprite;
            
            yield return new WaitForSeconds(frameRate * key.frameCount);
        }
        
        _onEnd.Invoke();
        
        if(_loop) StartCoroutine(Animation());
    }

    public void Set(KeyFrame[] keyFrame, float fps)
    {
        _keyFrames = keyFrame;
        _fps = fps;
    }
}
