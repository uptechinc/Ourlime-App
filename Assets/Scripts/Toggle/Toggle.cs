using UnityEngine;

public class Toggle : MonoBehaviour
{
    // Reference to the GameObject containing the menu buttons
    public GameObject menu;

    // Method to toggle the menu's visibility
    public void ToggleMenu()
    {
        // Toggle the active state of the menu
        menu.SetActive(!menu.activeSelf);
    }
}
