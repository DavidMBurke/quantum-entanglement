using UnityEngine;
using TMPro;

public class DigitalScale : MonoBehaviour
{
    public TextMeshProUGUI totalWeightText;
    public ScaleObject[] allScaleObjectsOnScale;
    public void Update()
    {
        int totalWeight = 0;
        foreach (ScaleObject scaleObject in allScaleObjectsOnScale)
        {
            if (scaleObject.gameObject.activeSelf)
            {
                totalWeight += scaleObject.weight;
            }
        }
        totalWeightText.text = totalWeight.ToString();
    }
}
