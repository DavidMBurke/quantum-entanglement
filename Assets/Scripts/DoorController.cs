using Unity.VisualScripting;
using UnityEngine;


public class DoorController : MonoBehaviour
{
    public Sprite openSprite;
    public Sprite closedSprite;

    [SerializeField] private bool isOpen = false;
    public SpriteRenderer spriteRenderer;
    public endscreen endscreen;

    public bool IsOpen
    {
        get { return isOpen; }
        set
        {
            isOpen = value;
            endscreen.checkOpen();
            UpdateSprite();
        }
    }

    public void ToggleDoor()
    {
        IsOpen = !IsOpen;
    }

    private void UpdateSprite()
    {
        spriteRenderer.sprite = isOpen ? openSprite : closedSprite;
    }

    private void OnValidate()
    {
        UpdateSprite();
    }
}
