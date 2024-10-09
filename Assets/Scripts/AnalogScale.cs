using UnityEngine;

public class AnalogScale : MonoBehaviour
{
    public DigitalScale digitalScale;
    public ScaleObject[] allScaleObjectsOnScale;
    public GameObject leftScale;
    public GameObject rightScale;
    public GameObject smileyA;
    public GameObject smileyB;

    private int leftWeight = 0;
    private int rightWeight = 0;
    private float leftHeight = 0;
    private float rightHeight = 0;

    const float SCALE = 25;
    const float OFFSET = (float)-0.45;
    const float SCALE_OBJECTS_OFFSET = (float)-0.65;

    void Update()
    {
        // calculate left weight
        leftWeight = digitalScale.GetWeight();

        // calculate right weight
        int newRightWeight = 0;
        foreach (ScaleObject scaleObject in allScaleObjectsOnScale)
        {
            if (scaleObject.gameObject.activeSelf)
            {
                newRightWeight += scaleObject.weight;
            }
        }

        rightWeight = newRightWeight;

        // calculate heights of scales and objects on right scale
        if (leftWeight > rightWeight)
        {
            leftHeight = -(leftWeight - rightWeight) / SCALE + OFFSET;
            rightHeight = (leftWeight - rightWeight) / SCALE + OFFSET;
        }
        else
        {
            leftHeight = (rightWeight - leftWeight) / SCALE + OFFSET;
            rightHeight = -(rightWeight - leftWeight) / SCALE + OFFSET;
        }

        // set left and right scale heights
        leftScale.transform.localPosition = new Vector3(leftScale.transform.localPosition.x, leftHeight, leftScale.transform.localPosition.z);
        rightScale.transform.localPosition = new Vector3(rightScale.transform.localPosition.x, rightHeight, rightScale.transform.localPosition.z);

        // set object heights on right scale
        foreach (ScaleObject scaleObject in allScaleObjectsOnScale)
        {
            if (scaleObject.gameObject.activeSelf)
            {
                scaleObject.transform.localPosition = new Vector3(scaleObject.transform.localPosition.x, rightHeight + SCALE_OBJECTS_OFFSET, scaleObject.transform.localPosition.z);
            }
        }

        // active or deactivate smiley
        if (leftWeight == rightWeight && !smileyA.activeSelf)
        {
            smileyA.SetActive(true);
            smileyB.SetActive(true);
        }
        else if (leftWeight != rightWeight && smileyA.activeSelf)
        {
            smileyA.SetActive(false);
            smileyB.SetActive(false);
        }
    }
}
