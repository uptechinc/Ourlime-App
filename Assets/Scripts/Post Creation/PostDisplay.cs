/*using UnityEngine;
using UnityEngine.UI;
using System;

public class PostDisplay : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] private Text privacyBadge;
    [SerializeField] private Text captionText;
    [SerializeField] private Text descriptionText;
    [SerializeField] private RawImage mediaDisplay;
    [SerializeField] private Text hashtagsText;
    [SerializeField] private Text referenceText;
    [SerializeField] private Text timestampText;

    public void Initialize(PostData postData)
    {
        privacyBadge.text = postData.privacyLevel;
        captionText.text = postData.caption;
        descriptionText.text = postData.description;
        hashtagsText.text = FormatHashtags(postData.hashtags);
        referenceText.text = postData.reference;
        timestampText.text = postData.timestamp.ToString("MMM dd yyyy - h:mm tt");

        if(postData.media != null)
        {
            mediaDisplay.texture = postData.media;
            mediaDisplay.gameObject.SetActive(true);
            
            AspectRatioFitter arf = mediaDisplay.GetComponent<AspectRatioFitter>();
            if(arf != null)
            {
                arf.aspectRatio = (float)postData.media.width / postData.media.height;
            }
        }
        else
        {
            mediaDisplay.gameObject.SetActive(false);
        }
    }

    private string FormatHashtags(string rawHashtags)
    {
        string formatted = "";
        string[] tags = rawHashtags.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        foreach(string tag in tags)
        {
            formatted += $"<color=#00B4FF>#{tag.Trim()}</color> ";
        }
        return formatted;
    }
}*/
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PostDisplay : MonoBehaviour
{
    [Header("UI Components")]
    public TextMeshProUGUI captionText;
    public TextMeshProUGUI descriptionText;
    public RawImage mediaDisplay;
    public TextMeshProUGUI hashtagsText;
    public TextMeshProUGUI referenceText;
    public TextMeshProUGUI privacyBadge;
    public TextMeshProUGUI postDateText;

    public void Initialize(PostData data)
    {
        captionText.text = data.caption;
        descriptionText.text = data.description;
        hashtagsText.text = data.hashtags;
        referenceText.text = data.reference;
        privacyBadge.text = data.privacy;
        postDateText.text = data.postDate.ToString("MMM dd, yyyy - HH:mm");

        mediaDisplay.texture = data.media ?? Texture2D.blackTexture;
        mediaDisplay.gameObject.SetActive(data.media != null);
    }
}