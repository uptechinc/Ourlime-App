/*using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PostManager : MonoBehaviour
{
    public GameObject postPrefab; // Assign in Inspector
    public Transform postContainer; // Assign the UI container where posts will be added

    public Dropdown visibilityDropdown;
    public InputField captionInput;
    public InputField descriptionInput;
    public InputField hashtagsInput;
    public InputField referenceInput;
    public Image mediaPreview; // Shows uploaded image (modify for videos)
    public GameObject postForm; // The form panel

    private List<Post> posts = new List<Post>(); // Simulating a database

    public void CreatePost()
    {
        // Get values from the form
        string visibility = visibilityDropdown.options[visibilityDropdown.value].text;
        string caption = captionInput.text;
        string description = descriptionInput.text;
        string[] hashtags = hashtagsInput.text.Split(','); // Convert to array
        string reference = referenceInput.text;
        Sprite media = mediaPreview.sprite;

        // Create a new post object
        Post newPost = new Post(visibility, caption, description, hashtags, reference, media);
        posts.Add(newPost);

        // Display the post in UI
        DisplayPost(newPost);

        // Clear the form
        ClearForm();
        postForm.SetActive(false);
    }

    private void DisplayPost(Post post)
    {
        GameObject newPostUI = Instantiate(postPrefab, postContainer);
        newPostUI.transform.Find("CaptionText").GetComponent<Text>().text = post.caption;
        newPostUI.transform.Find("DescriptionText").GetComponent<Text>().text = post.description;
        newPostUI.transform.Find("VisibilityText").GetComponent<Text>().text = post.visibility;
        newPostUI.transform.Find("ReferenceText").GetComponent<Text>().text = post.reference;
        newPostUI.transform.Find("HashtagsText").GetComponent<Text>().text = string.Join(", ", post.hashtags);

        if (post.media != null)
        {
            newPostUI.transform.Find("MediaImage").GetComponent<Image>().sprite = post.media;
        }
    }

    private void ClearForm()
    {
        captionInput.text = "";
        descriptionInput.text = "";
        hashtagsInput.text = "";
        referenceInput.text = "";
        mediaPreview.sprite = null;
    }
}
*/