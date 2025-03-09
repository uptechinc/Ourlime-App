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
    public TMP_Text mediaErrorText;

    [Header("Post Settings")]
    public GameObject postPrefab;
    public Transform feedContent;
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

    void Start()
    {
        formPanel.SetActive(false);
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
    }

    public void HandlePostCreation()
    {
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
    }

    private void InstantiatePost(PostData postData)
    {
        GameObject newPost = Instantiate(postPrefab, feedContent);
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

    public void SavePosts()
    {
        string jsonData = JsonUtility.ToJson(new Serialization<PostData>(allPosts));
        PlayerPrefs.SetString("SavedPosts", jsonData);
    }

    public void LoadPosts()
    {
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
        }
    }
}