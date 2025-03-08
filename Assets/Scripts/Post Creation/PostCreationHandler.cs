<<<<<<< Updated upstream
/*using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PostCreationHandler : MonoBehaviour
{
    [Header("Form Components")]
    [SerializeField] private GameObject postCreationPanel;
    [SerializeField] private Dropdown privacyDropdown;
    [SerializeField] private InputField captionInput;
    [SerializeField] private InputField descriptionInput;
    [SerializeField] private RawImage mediaPreview;
    [SerializeField] private InputField hashtagsInput;
    [SerializeField] private InputField referenceInput;
    [SerializeField] private Button submitButton;
    [SerializeField] private Button mediaUploadButton;

    [Header("Post Settings")]
    [SerializeField] private GameObject postPrefab;
    [SerializeField] private Transform feedContentParent;

    private List<PostData> posts = new List<PostData>();
    private Texture2D selectedMedia;

    private void Start()
    {
        postCreationPanel.SetActive(false);
        mediaPreview.gameObject.SetActive(false);
        
        submitButton.onClick.AddListener(OnSubmitPost);
        mediaUploadButton.onClick.AddListener(OnMediaUploadClicked);
        
        InitializePrivacyDropdown();
        LoadPosts();
    }

    private void InitializePrivacyDropdown()
    {
        privacyDropdown.ClearOptions();
        privacyDropdown.AddOptions(new List<string> { "Public", "Friends", "Private" });
    }

    public void ToggleCreationPanel(bool state)
    {
        postCreationPanel.SetActive(state);
        if(!state) ClearForm();
    }

    private void ClearForm()
    {
        privacyDropdown.value = 0;
        captionInput.text = "";
        descriptionInput.text = "";
        hashtagsInput.text = "";
        referenceInput.text = "";
        selectedMedia = null;
        mediaPreview.gameObject.SetActive(false);
    }

    public void OnMediaUploadClicked()
    {
        Texture2D loadedMedia = Resources.Load<Texture2D>("Media/sample_image");
        if(loadedMedia != null)
        {
            selectedMedia = loadedMedia;
            mediaPreview.texture = selectedMedia;
            mediaPreview.gameObject.SetActive(true);
        }
    }

    private void OnSubmitPost()
    {
        if(string.IsNullOrEmpty(captionInput.text))
        {
            Debug.LogError("Caption is required!");
            return;
        }

        PostData newPost = new PostData(
            privacyDropdown.options[privacyDropdown.value].text,
            captionInput.text,
            descriptionInput.text,
            selectedMedia,
            ParseHashtags(hashtagsInput.text),
            referenceInput.text
        );

        AddPostToFeed(newPost);
        SavePost(newPost);
        ToggleCreationPanel(false);
    }

    private string ParseHashtags(string rawInput)
    {
        return rawInput.Replace(" ", "").Replace("#", " #");
    }

    private void AddPostToFeed(PostData postData)
    {
        GameObject newPost = Instantiate(postPrefab, feedContentParent);
        PostDisplay display = newPost.GetComponent<PostDisplay>();
        if(display != null)
        {
            display.Initialize(postData);
        }
        else
        {
            Debug.LogError("Post prefab missing PostDisplay component!");
        }
    }

    private void SavePost(PostData post)
    {
        posts.Add(post);
        SaveToPlayerPrefs();
    }

    private void SaveToPlayerPrefs()
    {
        string jsonData = JsonUtility.ToJson(new Serialization<PostData>(posts));
        PlayerPrefs.SetString("Posts", jsonData);
        PlayerPrefs.Save();
    }

    public void LoadPosts()
    {
        if(PlayerPrefs.HasKey("Posts"))
        {
            string jsonData = PlayerPrefs.GetString("Posts");
            posts = JsonUtility.FromJson<Serialization<PostData>>(jsonData).ToList();
            
            foreach(PostData post in posts)
            {
                AddPostToFeed(post);
            }
        }
    }

    [System.Serializable]
    private class Serialization<T>
    {
        [SerializeField] 
        public List<T> data;

        public Serialization(List<T> data) => this.data = data;
        public List<T> ToList() => data;
    }
}*/
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class PostCreationHandler : MonoBehaviour
=======
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Video;
using NativeGalleryNamespace;
using UnityEngine.Android;
using UnityEngine.EventSystems;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class PostCreationHandler : MonoBehaviour, IDropHandler
>>>>>>> Stashed changes
{
    [Header("UI References")]
    public GameObject formPanel;
    public TMP_Dropdown privacyDropdown;
    public TMP_InputField captionInput;
    public TMP_InputField descriptionInput;
    public RawImage mediaPreview;
    public TMP_InputField hashtagsInput;
    public TMP_InputField referenceInput;
    public Button submitButton;
<<<<<<< Updated upstream
=======
    public TMP_Text mediaErrorText;
>>>>>>> Stashed changes

    [Header("Post Settings")]
    public GameObject postPrefab;
    public Transform feedContent;
<<<<<<< Updated upstream

    [Header("Media Settings")]
    public Texture2D defaultMedia;

    private Texture2D selectedMedia;
    private List<PostData> allPosts = new List<PostData>();
=======
    public RenderTexture videoRenderTextureTemplate;

    [Header("Media Settings")]
    public Texture2D defaultMedia;
    public VideoPlayer videoPreview;

    [Header("Drag & Drop")]
    public GameObject dragDropOverlay;

    private Texture2D selectedImage;
    private string selectedVideoPath;
    private List<PostData> allPosts = new List<PostData>();
    private bool isPosting = false;
>>>>>>> Stashed changes

    void Start()
    {
        formPanel.SetActive(false);
<<<<<<< Updated upstream
        submitButton.onClick.AddListener(HandlePostCreation);
        ClearForm();
    }

=======
        submitButton.onClick.RemoveAllListeners(); // Ensure no duplicate listeners
        submitButton.onClick.AddListener(HandlePostCreation);
        
        #if UNITY_ANDROID
        RequestStoragePermission();
        #endif
    }

    #if UNITY_ANDROID
    private void RequestStoragePermission()
    {
        if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageRead))
        {
            Permission.RequestUserPermission(Permission.ExternalStorageRead);
        }
    }
    #endif

>>>>>>> Stashed changes
    public void OpenForm()
    {
        formPanel.SetActive(true);
        ClearForm();
    }

    private void ClearForm()
    {
        privacyDropdown.value = 0;
        captionInput.text = "";
        descriptionInput.text = "";
        hashtagsInput.text = "";
        referenceInput.text = "";
<<<<<<< Updated upstream
        selectedMedia = null;
        mediaPreview.texture = defaultMedia;
    }

    public void HandleMediaUpload(Texture2D media)
    {
        selectedMedia = media;
        mediaPreview.texture = media;
=======
        
        selectedImage = null;
        selectedVideoPath = null;
        
        mediaPreview.texture = defaultMedia;
        mediaPreview.gameObject.SetActive(true);
        
        videoPreview.Stop();
        videoPreview.gameObject.SetActive(false);
        mediaErrorText.text = "";
    }

    public void OnUploadButtonClick()
    {
        NativeGallery.GetMixedMediaFromGallery((path) =>
        {
            if (string.IsNullOrEmpty(path)) return;

            string extension = Path.GetExtension(path).ToLower();
            if (extension == ".png" || extension == ".jpg" || extension == ".jpeg")
            {
                LoadImage(path);
            }
            else if (extension == ".mp4")
            {
                LoadVideo(path);
            }
            else
            {
                mediaErrorText.text = "Unsupported file type!";
            }
        }, NativeGallery.MediaType.Image | NativeGallery.MediaType.Video, "Select Media");
    }

    private void LoadImage(string path)
    {
        Texture2D texture = NativeGallery.LoadImageAtPath(path, 2048, false);
        if (texture == null)
        {
            mediaErrorText.text = "Failed to load image!";
            return;
        }

        selectedImage = texture;
        selectedVideoPath = null;
        mediaPreview.texture = texture;
        mediaErrorText.text = "";
    }

    private void LoadVideo(string path)
    {
        selectedVideoPath = path;
        selectedImage = null;
        mediaPreview.gameObject.SetActive(false);
        
        videoPreview.gameObject.SetActive(true);
        videoPreview.url = path;
        videoPreview.Prepare();
        videoPreview.prepareCompleted += (source) =>
        {
            videoPreview.Play();
        };
        mediaErrorText.text = "Video selected!";
>>>>>>> Stashed changes
    }

    public void HandlePostCreation()
    {
<<<<<<< Updated upstream
        PostData newPost = new PostData
        {
            privacy = privacyDropdown.options[privacyDropdown.value].text,
            caption = captionInput.text,
            description = descriptionInput.text,
            media = selectedMedia,
            hashtags = hashtagsInput.text,
            reference = referenceInput.text,
            postDate = DateTime.Now
        };

        allPosts.Add(newPost);
        InstantiatePost(newPost);
        formPanel.SetActive(false);
        ClearForm();
=======
        if (isPosting) return;
        isPosting = true;

        try
        {
            PostData newPost = new PostData(
                captionInput.text,
                descriptionInput.text,
                hashtagsInput.text,
                referenceInput.text,
                privacyDropdown.options[privacyDropdown.value].text,
                DateTime.Now,
                selectedImage != null ? CopyTexture(selectedImage) : null,
                selectedVideoPath
            );

            allPosts.Add(newPost);
            InstantiatePost(newPost);
        }
        finally
        {
            formPanel.SetActive(false);
            isPosting = false;
        }
    }

    private Texture2D CopyTexture(Texture2D source)
    {
        Texture2D copy = new Texture2D(source.width, source.height);
        copy.SetPixels(source.GetPixels());
        copy.Apply();
        return copy;
>>>>>>> Stashed changes
    }

    private void InstantiatePost(PostData postData)
    {
        GameObject newPost = Instantiate(postPrefab, feedContent);
<<<<<<< Updated upstream
        newPost.GetComponent<PostDisplay>().Initialize(postData);
    }

    // Simulate media selection for prototype
    public void OnMediaUploadClick()
    {
        // For testing: Load a sample texture from Resources
        Texture2D sampleTexture = Resources.Load<Texture2D>("SampleImage");
        if(sampleTexture != null)
        {
            HandleMediaUpload(sampleTexture);
        }
    }

    // For future database implementation
=======
        PostDisplay display = newPost.GetComponent<PostDisplay>();
        
        RenderTexture rt = new RenderTexture(videoRenderTextureTemplate);
        display.Initialize(postData, rt);
    }

    public void OnDrop(PointerEventData eventData)
    {
        dragDropOverlay.SetActive(false);

        #if UNITY_EDITOR
        string[] paths = DragAndDrop.paths;
        #else
        string[] paths = null;
        #endif

        if (paths == null || paths.Length == 0) return;

        string path = paths[0];
        string extension = Path.GetExtension(path).ToLower();
        if (extension == ".png" || extension == ".jpg" || extension == ".jpeg")
        {
            LoadImage(path);
        }
        else if (extension == ".mp4")
        {
            LoadVideo(path);
        }
    }

    public void OnDragEnter(PointerEventData eventData)
    {
        dragDropOverlay.SetActive(true);
    }

    public void OnDragExit(PointerEventData eventData)
    {
        dragDropOverlay.SetActive(false);
    }

>>>>>>> Stashed changes
    public void SavePosts()
    {
        string jsonData = JsonUtility.ToJson(new Serialization<PostData>(allPosts));
        PlayerPrefs.SetString("SavedPosts", jsonData);
    }

    public void LoadPosts()
    {
<<<<<<< Updated upstream
        if(PlayerPrefs.HasKey("SavedPosts"))
        {
            string jsonData = PlayerPrefs.GetString("SavedPosts");
            allPosts = JsonUtility.FromJson<Serialization<PostData>>(jsonData).ToList();
            
            foreach(PostData post in allPosts)
            {
                InstantiatePost(post);
            }
=======
        if (!PlayerPrefs.HasKey("SavedPosts")) return;
        
        string jsonData = PlayerPrefs.GetString("SavedPosts");
        allPosts = JsonUtility.FromJson<Serialization<PostData>>(jsonData).ToList();
        
        foreach (PostData post in allPosts)
        {
            InstantiatePost(post);
        }
    }

    private void OnDestroy()
    {
        if (selectedImage != null)
        {
            Destroy(selectedImage);
>>>>>>> Stashed changes
        }
    }
}