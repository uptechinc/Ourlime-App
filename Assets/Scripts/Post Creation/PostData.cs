<<<<<<< Updated upstream
/*using System;
=======
using System;
>>>>>>> Stashed changes
using UnityEngine;

[System.Serializable]
public class PostData
{
<<<<<<< Updated upstream
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
=======
    public string caption;
    public string description;
    public string hashtags;
    public string reference;
    public string privacy;
    public DateTime postDate;
    public Texture2D image;
    public string videoPath;

    public PostData(string caption, string description, string hashtags, string reference, string privacy, DateTime postDate, Texture2D image, string videoPath)
    {
        this.caption = caption;
        this.description = description;
        this.hashtags = hashtags;
        this.reference = reference;
        this.privacy = privacy;
        this.postDate = postDate;
        this.image = image;
        this.videoPath = videoPath;
    }
>>>>>>> Stashed changes
}