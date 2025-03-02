/*using System;
using UnityEngine;

[System.Serializable]
public class PostData
{
    public string privacyLevel;
    public string caption;
    public string description;
    public Texture2D media;
    public string hashtags;
    public string reference;
    public DateTime timestamp;

    public PostData(string privacy, string caption, string description, 
                   Texture2D media, string hashtags, string reference)
    {
        this.privacyLevel = privacy;
        this.caption = caption;
        this.description = description;
        this.media = media;
        this.hashtags = hashtags;
        this.reference = reference;
        this.timestamp = DateTime.Now;
    }
}*/
using UnityEngine;
using System;
using System.Collections.Generic;

[System.Serializable]
public class PostData
{
    public string privacy;
    public string caption;
    public string description;
    public Texture2D media;
    public string hashtags;
    public string reference;
    public DateTime postDate;
}

[System.Serializable]
public class Serialization<T>
{
    public List<T> list;
    public Serialization(List<T> list) => this.list = list;
    public List<T> ToList() => list;
}