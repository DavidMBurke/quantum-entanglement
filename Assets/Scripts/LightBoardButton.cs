using System.Collections.Generic;
using UnityEngine;

public class LightBoardButton : MonoBehaviour
{

    public GameObject Button;
    public bool isLit = false;
    public Sprite LitButton;
    public Sprite UnlitButton;
    private SpriteRenderer spriteRenderer;


    private void Start()
    {
        spriteRenderer = Button.GetComponent<SpriteRenderer>();
        UpdateButtonSprite();
    }

    private void OnMouseDown()
    {
        isLit = !isLit;
        UpdateButtonSprite();
    }

    private void UpdateButtonSprite()
    {
        if (isLit)
        {
            spriteRenderer.sprite = LitButton;
        }
        else
        {
            spriteRenderer.sprite = UnlitButton;
        }
    }
}
