using System;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : Singleton<MusicManager>
{
    public List<Sound> tracks;
    [SerializeField] private int currentTrackIndex;
    public Sound currentTrack => tracks[currentTrackIndex];

    private void Start()
    {
        tracks = new List<Sound>(SoundLibrary.GetAllSoundsInCategory(SoundCategory.Music));
    }
    
    public void Set(string name)
    {
        currentTrackIndex = tracks.FindIndex(track => track.Name.Equals(name));
    }
    
    public void Next()
    {
        currentTrackIndex = (currentTrackIndex + 1) % tracks.Count;
    }
    
    public void Previous()
    {
        currentTrackIndex = (currentTrackIndex - 1 + tracks.Count) % tracks.Count;
    }

    public void Shuffle()
    {
        currentTrackIndex = UnityEngine.Random.Range(0, tracks.Count);
    }
    
    public void Play()
    {
        AudioManager.Instance.PlayMusic(currentTrack.Name);
    }
}