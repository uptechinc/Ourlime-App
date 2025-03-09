using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Video;
using System.Collections;

public class PostDisplay : MonoBehaviour
{
    [Header("UI Components")]
    public TextMeshProUGUI captionText;
    public TextMeshProUGUI descriptionText;
    public RawImage imageDisplay;
    public RawImage videoDisplay;
    public VideoPlayer videoPlayer;
    public Button playButton;
    public Button pauseButton;
    public Slider videoTimeline;
    public TextMeshProUGUI hashtagsText;
    public TextMeshProUGUI referenceText;
    public TextMeshProUGUI privacyBadge;
    public TextMeshProUGUI postDateText;

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
        captionText.text = data.caption;
        descriptionText.text = data.description;
        hashtagsText.text = data.hashtags;
        referenceText.text = data.reference;
        privacyBadge.text = data.privacy;
        postDateText.text = data.postDate.ToString("MMM dd, yyyy - HH:mm");

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
    }
}