using UnityEngine;

public class ArrowClickHandler : MonoBehaviour
{

    public GameObject objectToActivate;
    public GameObject objectToDeactivate;
    public GameObject secondaryObjectToActivate;

    private void OnMouseDown()
    {
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true);
        }

        if (objectToDeactivate != null)
        {
            objectToDeactivate.SetActive(false);
        }

        if (secondaryObjectToActivate != null)
        {
            secondaryObjectToActivate.SetActive(true);
        }
    }
}
