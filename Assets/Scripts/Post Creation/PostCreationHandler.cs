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

    [Header("Post Settings")]
    public GameObject postPrefab;
    public Transform feedContent;

    [Header("Media Settings")]
    public Texture2D defaultMedia;

    private Texture2D selectedMedia;
    private List<PostData> allPosts = new List<PostData>();

    void Start()
    {
        formPanel.SetActive(false);
        submitButton.onClick.AddListener(HandlePostCreation);
        ClearForm();
    }

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
        selectedMedia = null;
        mediaPreview.texture = defaultMedia;
    }

    public void HandleMediaUpload(Texture2D media)
    {
        selectedMedia = media;
        mediaPreview.texture = media;
    }

    public void HandlePostCreation()
    {
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
    }

    private void InstantiatePost(PostData postData)
    {
        GameObject newPost = Instantiate(postPrefab, feedContent);
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
    public void SavePosts()
    {
        string jsonData = JsonUtility.ToJson(new Serialization<PostData>(allPosts));
        PlayerPrefs.SetString("SavedPosts", jsonData);
    }

    public void LoadPosts()
    {
        if(PlayerPrefs.HasKey("SavedPosts"))
        {
            string jsonData = PlayerPrefs.GetString("SavedPosts");
            allPosts = JsonUtility.FromJson<Serialization<PostData>>(jsonData).ToList();
            
            foreach(PostData post in allPosts)
            {
                InstantiatePost(post);
            }
        }
    }
}