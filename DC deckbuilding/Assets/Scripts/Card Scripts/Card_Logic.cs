using System.Collections;   
using System.Collections.Generic;
using UnityEngine;

public class Card_Logic : MonoBehaviour
{
    private bool isDragging;
    private float startPosX;
        
    private float startPosY;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isDragging)
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
}
