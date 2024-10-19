using UnityEngine;

public class AnalogScale : MonoBehaviour
{   
    public GameObject scaleBalance;
    public DigitalScale digitalScale;
    public ScaleObject[] blocksOnScale;
    public GameObject leftPlatform;
    public GameObject rightPlatform;
    public GameObject checkA;
    public GameObject checkB;
    public GameObject lockedWireBoxA;
    public GameObject lockedWireBoxB;
    public GameObject unlockedWireBoxA;
    public GameObject unlockedWireBoxB;

    const float R = 1.9f;
    const float SCALE = 25;
    const float ORIGIN_OFFSET_X = 1.86f;
    const float ORIGIN_OFFSET_Y = -0.4f;
    const float SCALE_OBJECTS_ORIGIN_OFFSET_X = -0.68f;
    const float SCALE_OBJECTS_ORIGIN_OFFSET_Y = -0.68f;
    const float SCALE_OBJECTS_INDIVIDUAL_OFFSET_X = 0.47f;

    void Update()
    {
        // calculate left weight
        int leftWeight = digitalScale.GetWeight();

        // calculate right weight
        int rightWeight = 0;
        foreach (ScaleObject scaleObject in blocksOnScale)
        {
            if (scaleObject.gameObject.activeSelf)
            {
                rightWeight += scaleObject.weight;
            }
        }

        // calculate left and right platform heights
        float theta = leftWeight - rightWeight;
        // exaggerate 0-4 degrees differences to spread between 3-4 degrees
        if (-4 <= theta && theta <= 4 && theta != 0)
        {
            theta = theta / 4 + Mathf.Sign(theta) * 3;
        }
        float radian = theta * Mathf.Deg2Rad;
        float x = R * Mathf.Cos(radian);
        float y = R * Mathf.Sin(radian);
        float leftX = -x + ORIGIN_OFFSET_X;
        float leftY = -y + ORIGIN_OFFSET_Y;
        float rightX = x + ORIGIN_OFFSET_X;
        float rightY = y + ORIGIN_OFFSET_Y;

        // set left and right platform heights
        leftPlatform.transform.localPosition = new Vector3(leftX, leftY, leftPlatform.transform.localPosition.z);
        rightPlatform.transform.localPosition = new Vector3(rightX, rightY, rightPlatform.transform.localPosition.z);

        // set object heights on right platform
        foreach (ScaleObject scaleObject in blocksOnScale)
        {
            if (scaleObject.gameObject.activeSelf)
            {
                scaleObject.transform.localPosition = new Vector3(
                    rightX + SCALE_OBJECTS_ORIGIN_OFFSET_X + SCALE_OBJECTS_INDIVIDUAL_OFFSET_X * scaleObject.order,
                    rightY + SCALE_OBJECTS_ORIGIN_OFFSET_Y,
                    scaleObject.transform.localPosition.z
                );
            }
        }

        // set balance z rotation based on left and right platform positions
        scaleBalance.transform.rotation = Quaternion.Euler(0, 0, theta);

        // active or deactivate smiley
        if (leftWeight == rightWeight && !checkA.activeSelf)
        {
            checkA.SetActive(true);
            checkB.SetActive(true);
            unlockedWireBoxA.SetActive(true);
            unlockedWireBoxB.SetActive(true);
            lockedWireBoxA.SetActive(false);
            lockedWireBoxB.SetActive(false);
        }
        else if (leftWeight != rightWeight && checkA.activeSelf)
        {
            checkA.SetActive(false);
            checkB.SetActive(false);
        }
    }
}
