// c:\Users\user\Unity Projects\Ourlime Application\OurlimeApp\Assets\Script\New Folder\NewMonoBehaviourScript.cs
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; // Required for EventSystem
using System.Collections; // Required for IEnumerator
using UnityEngine.Networking; // Required for UnityWebRequest

public class NewMonoBehaviourScript : MonoBehaviour
{
    public Button myButton; // Reference to the button
    public Image iconImage; // Reference to the image for the PNG
    public Text buttonText;  // Reference to the text component

    void Start()
    {
        // Set the button's onClick event
        myButton.onClick.AddListener(OnButtonClick);
        
        // Set the text for the button
        buttonText.text = "Select Photos / Videos";
        
        // Load the PNG image
        StartCoroutine(LoadYourPNG("file:///Users/user/Downloads/image.png")); // Update with your PNG file path
    }

    void OnButtonClick()
    {
        // Handle button click
        Debug.Log("Button clicked!");
    }

    private IEnumerator LoadYourPNG(string filePath)
    {
        // Load the PNG file from the specified path
        using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(filePath))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error loading PNG: " + www.error);
            }
            else
            {
                // Create a texture from the loaded PNG
                Texture2D texture = DownloadHandlerTexture.GetContent(www);
                // Convert the texture to a sprite
                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                // Assign the sprite to the iconImage
                iconImage.sprite = sprite;
            }
        }
    }
}