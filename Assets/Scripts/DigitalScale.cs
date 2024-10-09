using UnityEngine;
using TMPro;

public class DigitalScale : MonoBehaviour
{
    public TextMeshProUGUI totalWeightText;
    public ScaleObject[] allScaleObjectsOnScale;
    private int weight = 16;

    public void Update()
    {
        int newWeight = 0;
        foreach (ScaleObject scaleObject in allScaleObjectsOnScale)
        {
            if (scaleObject.gameObject.activeSelf)
            {
                newWeight += scaleObject.weight;
            }
        }

        weight = newWeight;
        totalWeightText.text = weight.ToString();
    }

    public int GetWeight()
    {
        return weight;
    }
}
