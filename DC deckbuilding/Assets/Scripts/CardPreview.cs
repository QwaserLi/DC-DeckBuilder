using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPreview : MonoBehaviour
{
    static SpriteRenderer spriteRenderer;


    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public static void ChangePreview(Sprite sprite) {
        spriteRenderer.sprite = sprite;
    }
}
