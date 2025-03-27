using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class JsonSerializer : ISerializer
{
    private readonly JsonSerializerSettings settings;
    
    public JsonSerializer()
    {
        settings = new JsonSerializerSettings
        {
            Formatting = Formatting.Indented,
        };
    }
    
    public string Serialize<T>(T obj)
    {
        return JsonConvert.SerializeObject(obj, settings);
    }

    public T Deserialize<T>(string json)
    {
        return JsonConvert.DeserializeObject<T>(json, settings);
    }
}