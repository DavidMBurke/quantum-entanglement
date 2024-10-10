using UnityEngine;

public class Key : MonoBehaviour
{
    public string keyValue; // Set this in the Inspector for each key (e.g., "0", "1", "2", etc.)
    private KeypadController keypadController;

    void Start()
    {
        // Find and reference the KeypadController in the scene
        keypadController = FindObjectOfType<KeypadController>();
    }

    void OnMouseDown()
    {
        // Notify the KeypadController of the clicked key
        if (keypadController != null)
        {
            keypadController.OnKeyClicked(keyValue);
        }
    }
}
