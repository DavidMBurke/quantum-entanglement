using UnityEngine;

public class ScaleObject : MonoBehaviour
{
    public int weight;
    public ScaleObject objectToActivate;
    public ScaleObject objectToDeactivate;

    private void OnMouseDown()
    {
        if (objectToActivate != null)
        {
            objectToActivate.gameObject.SetActive(true);
        }

        if (objectToDeactivate != null)
        {
            objectToDeactivate.gameObject.SetActive(false);
        }
    }
}

