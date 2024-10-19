using UnityEngine;

public class ArrowButton : MonoBehaviour
{
    public Padlock padlock;

    private void OnMouseDown()
    {
        if (padlock != null)
        {
            padlock.OnButtonClicked(gameObject);
        }
    }
}