<<<<<<< Updated upstream
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
=======
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Video;
using System.Collections;
>>>>>>> Stashed changes

public class PostDisplay : MonoBehaviour
{
    [Header("UI Components")]
    public TextMeshProUGUI captionText;
    public TextMeshProUGUI descriptionText;
<<<<<<< Updated upstream
    public RawImage mediaDisplay;
=======
    public RawImage imageDisplay;
    public RawImage videoDisplay;
    public VideoPlayer videoPlayer;
    public Button playButton;
    public Button pauseButton;
    public Slider videoTimeline;
>>>>>>> Stashed changes
    public TextMeshProUGUI hashtagsText;
    public TextMeshProUGUI referenceText;
    public TextMeshProUGUI privacyBadge;
    public TextMeshProUGUI postDateText;

<<<<<<< Updated upstream
    public void Initialize(PostData data)
    {
=======
    private RenderTexture currentRenderTexture;
    private bool isDraggingTimeline = false;

    void Start()
    {
        playButton.onClick.AddListener(PlayVideo);
        pauseButton.onClick.AddListener(PauseVideo);
        videoTimeline.onValueChanged.AddListener(OnTimelineChanged);
    }

    void Update()
    {
        if (videoPlayer.isPlaying && !isDraggingTimeline)
        {
            videoTimeline.value = (float)(videoPlayer.time / videoPlayer.length);
        }
    }

    public void Initialize(PostData data, RenderTexture renderTexture)
    {
        // Hide all media components initially
        imageDisplay.gameObject.SetActive(false);
        videoDisplay.gameObject.SetActive(false);
        videoPlayer.gameObject.SetActive(false);
        playButton.gameObject.SetActive(false);
        pauseButton.gameObject.SetActive(false);
        videoTimeline.gameObject.SetActive(false);

        // Set text fields
>>>>>>> Stashed changes
        captionText.text = data.caption;
        descriptionText.text = data.description;
        hashtagsText.text = data.hashtags;
        referenceText.text = data.reference;
        privacyBadge.text = data.privacy;
        postDateText.text = data.postDate.ToString("MMM dd, yyyy - HH:mm");

<<<<<<< Updated upstream
        mediaDisplay.texture = data.media ?? Texture2D.blackTexture;
        mediaDisplay.gameObject.SetActive(data.media != null);
=======
        // Display image if available
        if (data.image != null)
        {
            imageDisplay.gameObject.SetActive(true);
            imageDisplay.texture = data.image;
        }
        // Display video if available
        else if (!string.IsNullOrEmpty(data.videoPath))
        {
            currentRenderTexture = renderTexture;
            videoDisplay.gameObject.SetActive(true);
            playButton.gameObject.SetActive(false);
            pauseButton.gameObject.SetActive(true);
            videoTimeline.gameObject.SetActive(true);

            // Set VideoPlayer properties before enabling it
            videoPlayer.source = VideoSource.Url;
            videoPlayer.url = data.videoPath;
            videoPlayer.targetTexture = currentRenderTexture;
            videoDisplay.texture = currentRenderTexture;

            // Enable the VideoPlayer before preparing it
            StartCoroutine(EnableAndPrepareVideoPlayer());
        }
    }

    private IEnumerator EnableAndPrepareVideoPlayer()
    {
        videoDisplay.gameObject.SetActive(true);
        videoPlayer.gameObject.SetActive(true);
        videoPlayer.Prepare();
        videoPlayer.prepareCompleted += OnVideoPlayerPrepared;
        yield return null;
    }

    private void OnVideoPlayerPrepared(VideoPlayer source)
    {
        videoPlayer.prepareCompleted -= OnVideoPlayerPrepared; // Unsubscribe from the event
        videoPlayer.gameObject.SetActive(true);
        videoPlayer.Play();
    }

    private void PlayVideo()
    {
        if (videoPlayer.isPrepared)
        {
            playButton.gameObject.SetActive(false);
            pauseButton.gameObject.SetActive(true);
            videoPlayer.Play();
        }
    }

    private void PauseVideo()
    {
        if (videoPlayer.isPlaying)
        {
            playButton.gameObject.SetActive(true);
            pauseButton.gameObject.SetActive(false);
            videoPlayer.Pause();
        }
    }

    private void OnTimelineChanged(float value)
    {
        if (videoPlayer.isPrepared)
        {
            isDraggingTimeline = true;
            videoPlayer.time = value * videoPlayer.length;
            isDraggingTimeline = false;
        }
>>>>>>> Stashed changes
    }
}