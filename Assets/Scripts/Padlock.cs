using UnityEngine;
using TMPro;  // TextMeshPro namespace
using System.Collections;

public class Padlock : MonoBehaviour
{
    public TMP_Text key1text;
    public TMP_Text key2text;
    public TMP_Text key3text;
    public TMP_Text key4text;
    public bool isLocked = true;
    public string correctCode = "";
    private string enteredCode = "0000";
    private int arrow1num = 0;
    private int arrow2num = 0;
    private int arrow3num = 0;
    private int arrow4num = 0;
    public GameObject upArrow1;
    public GameObject upArrow2;
    public GameObject upArrow3;
    public GameObject upArrow4;
    public GameObject downArrow1;
    public GameObject downArrow2;
    public GameObject downArrow3;
    public GameObject downArrow4;
    public GameObject padlockView;
    public GameObject doorView;

    public DoorController doorController;

    void Start()
    {
        updateDisplay();
    }

    public void OnButtonClicked(GameObject clickedButton)
    {
        print(clickedButton.name);
        if (clickedButton == upArrow1) { arrow1num += 11; arrow1num %= 10; }
        else if (clickedButton == downArrow1) { arrow1num += 9; arrow1num %= 10; }
        else if (clickedButton == upArrow2) { arrow2num += 11; arrow2num %= 10; }
        else if (clickedButton == downArrow2) { arrow2num += 9; arrow2num %= 10; }
        else if (clickedButton == upArrow3) { arrow3num += 11; arrow3num %= 10; }
        else if (clickedButton == downArrow3) { arrow3num += 9; arrow3num %= 10; }
        else if (clickedButton == upArrow4) { arrow4num += 11; arrow4num %= 10; }
        else if (clickedButton == downArrow4) { arrow4num += 9; arrow4num %= 10; }

        updateDisplay();
    }


    private void updateDisplay()
    {
        key1text.text = arrow1num.ToString();
        key2text.text = arrow2num.ToString();
        key3text.text = arrow3num.ToString();
        key4text.text = arrow4num.ToString();
        enteredCode = arrow1num.ToString() + arrow2num.ToString() + arrow3num.ToString() + arrow4num.ToString();
        if (enteredCode == correctCode)
        {
            padlockView.SetActive(false);
            doorView.SetActive(true);
            doorController.IsOpen = true;
        }
    }

}
