using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class SoundLibrary
{
    private static Dictionary<SerializableGuid, Sound> sounds;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
    private static void Init()
    {
        sounds = new Dictionary<SerializableGuid, Sound>();
        var soundSOs = Resources.LoadAll<SoundCollectionSO>("");
        
        foreach (var soundSO in soundSOs)
        foreach (var sound in soundSO.sounds)
        {
            sounds.Add(sound.Id, sound);
        }
    }

    public static Sound? GetSound(string name)
    {
        return sounds.Values.SingleOrDefault(sound => sound.Name == name);
    }
    
    public static Sound? GetSound(SerializableGuid id)
    {
        if(!sounds.TryGetValue(id, out Sound sound)) return null;
        return sound;
    }

    public static Sound? GetSound(SoundCategory category)
    {
        return sounds.Values.FirstOrDefault(s => s.category == category);
    }

    public static Sound[] GetAllSoundsInCategory(SoundCategory category)
    {
        return sounds.Values.Where(s => s.category == category).ToArray();
    }
}
