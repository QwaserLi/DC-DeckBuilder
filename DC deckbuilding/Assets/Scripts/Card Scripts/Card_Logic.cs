using System.Collections;   
using System.Collections.Generic;
using UnityEngine;

public class Card_Logic : MonoBehaviour
{
    private bool isDragging;
    private float startPosX;    
    private float startPosY;
    private bool FaceDown = true;

    //TEMP: Put here for now as image of the cards need to Change 
    public Sprite front_Sprite;
    public Sprite back_Sprite;

    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDragging && !FaceDown)
        {
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos.z = Camera.main.nearClipPlane;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            transform.position = new Vector3(mousePos.x - startPosX, mousePos.y - startPosY, 0);
        }
        
    }

    void OnMouseDown()
    {

        if (Input.GetMouseButtonDown(0)) {

            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos.z = Camera.main.nearClipPlane;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            startPosX = mousePos.x - transform.localPosition.x;
            startPosY = mousePos.y - transform.localPosition.y;

            isDragging = true;

        }
    }

    void OnMouseUp()
    {
        isDragging = false;
    }

    public void FlipCard() {
        FaceDown = !FaceDown;
        //TEMP for card facing down
        if (FaceDown)
        {
            spriteRenderer.sprite = back_Sprite;
        }
        else {
            spriteRenderer.sprite = front_Sprite;
        }
    }
}
