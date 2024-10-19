using Unity.VisualScripting;
using UnityEngine;


public class DoorController : MonoBehaviour
{
    public Sprite openSprite;
    public Sprite closedSprite;

    [SerializeField] private bool isOpen = false;
    public SpriteRenderer spriteRenderer;

    public bool IsOpen
    {
        get { return isOpen; }
        set
        {
            isOpen = value;
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
