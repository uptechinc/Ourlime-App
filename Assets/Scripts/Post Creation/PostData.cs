using System;
using UnityEngine;

[System.Serializable]
public class PostData
{
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
}