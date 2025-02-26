using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.IO;

public class FeedsManager : MonoBehaviour
{
    public GameObject postPrefab; // Assign your post template prefab
    public Transform contentPanel; // Assign the ScrollView's content panel
    public TMP_InputField captionInput; // Assign the caption input field
    public TMP_InputField descriptionInput; // Assign the description input field
    public TMP_InputField hashtagsInput; // Assign the hashtags input field
    public Button submitButton; // Assign the submit button
    public Button uploadImageButton; // Assign the image upload button
    public Button uploadVideoButton; // Assign the video upload button
    public Image postImagePreview; // Assign an Image component for preview (optional)
    public TextMeshProUGUI videoPreviewText; // Assign a TextMeshProUGUI component for video preview (optional)

    private List<PostData> posts = new List<PostData>(); // Local storage for posts
    private Texture2D uploadedImage; // For image uploads
    private string uploadedVideoPath; // For video uploads

    [System.Serializable]
    public class PostData
    {
        public string caption;
        public string description;
        public string hashtags;
        public Texture2D image; // For image uploads
        public string videoPath; // For video uploads
    }

    private void Start()
    {
        // Add listeners to buttons
        submitButton.onClick.AddListener(CreatePost);
        uploadImageButton.onClick.AddListener(UploadImage);
        uploadVideoButton.onClick.AddListener(UploadVideo);

        // Load posts (simulate loading from a database)
        LoadPosts();
    }

    private void UploadImage()
    {
        // Simulate image upload (replace with actual file browser logic)
        string path = UnityEditor.EditorUtility.OpenFilePanel("Select Image", "", "png,jpg,jpeg");
        if (path.Length != 0)
        {
            byte[] imageData = File.ReadAllBytes(path);
            uploadedImage = new Texture2D(2, 2);
            uploadedImage.LoadImage(imageData);
            postImagePreview.sprite = Sprite.Create(uploadedImage, new Rect(0, 0, uploadedImage.width, uploadedImage.height), Vector2.zero);
        }
    }

    private void UploadVideo()
    {
        // Simulate video upload (replace with actual file browser logic)
        string path = UnityEditor.EditorUtility.OpenFilePanel("Select Video", "", "mp4,mov,avi");
        if (path.Length != 0)
        {
            uploadedVideoPath = path;
            videoPreviewText.text = "Video Selected: " + Path.GetFileName(path);
        }
    }

    private void CreatePost()
    {
        // Create a new post data object
        PostData newPost = new PostData
        {
            caption = captionInput.text,
            description = descriptionInput.text,
            hashtags = hashtagsInput.text,
            image = uploadedImage,
            videoPath = uploadedVideoPath
        };

        // Add the post to local storage
        posts.Add(newPost);

        // Display the post in the UI
        DisplayPost(newPost);

        // Clear input fields
        captionInput.text = "";
        descriptionInput.text = "";
        hashtagsInput.text = "";
        postImagePreview.sprite = null;
        videoPreviewText.text = "";
        uploadedImage = null;
        uploadedVideoPath = null;
    }

    private void DisplayPost(PostData post)
    {
        // Instantiate a new post from the template
        GameObject newPost = Instantiate(postPrefab, contentPanel);

        // Get references to the post's UI elements
        TextMeshProUGUI captionText = newPost.transform.Find("Caption").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI descriptionText = newPost.transform.Find("Description").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI hashtagsText = newPost.transform.Find("Hashtags").GetComponent<TextMeshProUGUI>();
        Image postImage = newPost.transform.Find("Image").GetComponent<Image>();
        TextMeshProUGUI postVideoText = newPost.transform.Find("VideoText").GetComponent<TextMeshProUGUI>();

        // Populate the post with data
        captionText.text = post.caption;
        descriptionText.text = post.description;
        hashtagsText.text = post.hashtags;

        // Set the image (if available)
        if (post.image != null)
        {
            postImage.sprite = Sprite.Create(post.image, new Rect(0, 0, post.image.width, post.image.height), Vector2.zero);
            postImage.gameObject.SetActive(true); // Show the image element
        }
        else
        {
            postImage.gameObject.SetActive(false); // Hide the image element
        }

        // Set the video text (if available)
        if (!string.IsNullOrEmpty(post.videoPath))
        {
            postVideoText.text = "Video: " + Path.GetFileName(post.videoPath);
            postVideoText.gameObject.SetActive(true); // Show the video text element
        }
        else
        {
            postVideoText.gameObject.SetActive(false); // Hide the video text element
        }
    }

    private void LoadPosts()
    {
        // Simulate loading posts from a database
        foreach (PostData post in posts)
        {
            DisplayPost(post);
        }
    }
}