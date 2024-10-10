using UnityEngine;
using TMPro;  // TextMeshPro namespace

public class KeypadController : MonoBehaviour
{
    public TMP_Text displayText; // Reference to the TMP_Text component for the display
    private string enteredCode = "";
    public int maxCodeLength = 4;

    void Start()
    {
        // Initialize the display
        displayText.text = "";
    }

    // Function to update the display when a key is clicked
    public void OnKeyClicked(string keyValue)
    {
        if (enteredCode.Length < maxCodeLength)
        {
            enteredCode += keyValue;
            displayText.text = enteredCode;  // Update the display
        }
        if (keyValue == "clear") {
            enteredCode = "";
            displayText.text = enteredCode;
        }
    }

    // Function to clear the display
    public void ClearDisplay()
    {
        enteredCode = "";
        displayText.text = "";  // Reset the display
    }
}
