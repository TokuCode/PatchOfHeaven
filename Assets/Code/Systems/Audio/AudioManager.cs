using System.Collections.Generic;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>, IBind<AudioData>
{
    [field: SerializeField] public SerializableGuid Id { get; set; } = SerializableGuid.NewGuid();
    private AudioData _audioData;
    
    private Bus _masterBus;
    private Bus _sfxBus;
    private Bus _musicBus;
    
    private List<EventInstance> _eventInstances;
    private List<StudioEventEmitter> _emitters;
    private EventInstance _musicEventInstance;
    
    [Header("Volume Settings")]
    public float MasterVolume { get => _audioData.masterVolume; set => _audioData.masterVolume = Mathf.Clamp01(value); }
    public float MusicVolume { get => _audioData.musicVolume; set => _audioData.musicVolume = Mathf.Clamp01(value); }
    public float SFXVolume { get => _audioData.sfxVolume; set => _audioData.sfxVolume = Mathf.Clamp01(value); }

    private void Awake()
    {
        _eventInstances = new List<EventInstance>();
        _emitters = new List<StudioEventEmitter>();
        
        _masterBus = RuntimeManager.GetBus("bus:/");
        _sfxBus = RuntimeManager.GetBus("bus:/SFX");
        _musicBus = RuntimeManager.GetBus("bus:/Music");
        
        InitializeMusic();
        
        UpdateVolume();
    }

    private void InitializeMusic()
    {
        Sound sound = SoundLibrary.GetSound(SoundCategory.Music);
        if(sound != null)
        {
            _musicEventInstance = RuntimeManager.CreateInstance(sound.sound);
            _musicEventInstance.start();
        }
    }
    
    public void PlayMusic(string musicKey)
    {
        _musicEventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        _musicEventInstance.release();
        
        var sound = SoundLibrary.GetSound(musicKey);
        if(sound != null)
        {
            _musicEventInstance = RuntimeManager.CreateInstance(sound.sound);
            _musicEventInstance.start();
        }
    }
    
    private void SetMusicParameter(string parameterName, float parameterValue)
    {
        _musicEventInstance.setParameterByName(parameterName, parameterValue);
    }

    private void PlayOneShot(string soundKey)
    {
        var sound = SoundLibrary.GetSound(soundKey);
        if(sound != null) RuntimeManager.PlayOneShot(sound.sound);
    }
    
    public void UpdateVolume()
    {
        _masterBus.setVolume(_audioData.masterVolume);
        _sfxBus.setVolume(_audioData.sfxVolume);
        _musicBus.setVolume(_audioData.musicVolume);
    }
    
    private EventInstance? CreateInstance(string soundKey)
    {
        var sound = SoundLibrary.GetSound(soundKey);
        if (sound == null) return null;
        EventInstance eventInstance = RuntimeManager.CreateInstance(sound.sound);
        _eventInstances.Add(eventInstance);
        return eventInstance;
    }

    private StudioEventEmitter InitializeEventEmitter(string soundKey, GameObject emitterGO)
    {
        var sound = SoundLibrary.GetSound(soundKey);
        if (sound == null) return null;
        StudioEventEmitter emitter = emitterGO.GetComponent<StudioEventEmitter>();
        if(emitter == null) return null;
        emitter.EventReference = sound.sound;
        _emitters.Add(emitter);
        return emitter;
    }

    private void CleanUp()
    {
        _musicEventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        _musicEventInstance.release();
        
        foreach (var instance in _eventInstances)
        {
            instance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            instance.release();
        }
        
        foreach (var emitter in _emitters)
        {
            emitter.Stop();
        }
    }

    public void OnDestroy()
    {
        CleanUp();
    }

    public void Bind(AudioData data)
    {
        _audioData = data;
        
        UpdateVolume();
    }
}
