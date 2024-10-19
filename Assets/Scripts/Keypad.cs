using UnityEngine;
using TMPro;  // TextMeshPro namespace

public class KeypadController : MonoBehaviour
{
    public TMP_Text displayText; // Reference to the TMP_Text component for the display
    public bool isLocked = true;
    public string correctCode = "";
    private string enteredCode = "";
    public int maxCodeLength = 4;

    public DoorController doorController;
    public GameObject lockView;
    public GameObject roomView;

    void Start()
    {
        displayText.text = "";
    }

    public void OnKeyClicked(string keyValue)
    {
        if (enteredCode.Length < maxCodeLength && keyValue != "enter")
        {
            enteredCode += keyValue;
            displayText.text = enteredCode;
        }
        if (keyValue == "clear") {
            enteredCode = "";
            displayText.text = "";
        }
        if (keyValue == "enter" && enteredCode == correctCode)
        {
            isLocked = false;
            doorController.IsOpen = !isLocked;
            roomView.SetActive(true);
            lockView.SetActive(false);
        }
    }


}
