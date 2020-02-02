using System.Collections;   
using System.Collections.Generic;
using UnityEngine;

public class Card_Logic : MonoBehaviour
{
    //For the card preview so it doesn't change when you drag and hover multiple cards
    private static bool hoveringCard;

    private bool isDragging;
    //For dragging the card around
    private float startMousePosX;    
    private float startMousePosY;

    //Original Position for when after you drag the card and let go in at wrong position it snap backs to the original position
    private Vector3 snapBackPos;

    private bool FaceDown = true;
    private bool isInLineUp = true;

    //Checking if a card is owned or not
    private Player PlayerOwned;
    private bool isOwned;

    //For buying so it doesnt snapback but goes to discard instead
    private bool isInPurchaseArea;

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
            transform.position = new Vector3(mousePos.x - startMousePosX, mousePos.y - startMousePosY, 0);
        }
        
    }

    void OnMouseDown()
    {

        if (Input.GetMouseButtonDown(0)) {

            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos.z = Camera.main.nearClipPlane;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            startMousePosX = mousePos.x - transform.position.x;
            startMousePosY = mousePos.y - transform.position.y;

            isDragging = true;

        }
    }

    void OnMouseUp()
    {
        isDragging = false;

        //TODO: Need to make it so it only snaps back in some cases when in hand and when on the line up otherwise dont snap back
        //TODO: Currently you can move cards in the discard pile and it can snap back so make that you cant move cards in the discard pile
        if (!FaceDown) {
            transform.position = snapBackPos;
        }
    }

    void OnMouseOver()
    {
        //If your mouse hovers over the GameObject with the script attached, output this message
        if (!FaceDown && !hoveringCard) {
            CardPreview.ChangePreview(front_Sprite);
            hoveringCard = true;
        }
    }

    void OnMouseExit()
    {
        //The mouse is no longer hovering over the GameObject so output this message each frame
        CardPreview.ChangePreview(back_Sprite);
        hoveringCard = false;
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

    public void setPlayerOwned(Player player) {
        PlayerOwned = player;
        isOwned = true;
        transform.parent = player.transform;

    }

    public bool getOwned() {
        return isOwned;
    }

    public bool IsInPurchaseArea()
    {
        return isInPurchaseArea;
    }

    public bool IsInLineUp()
    {
        return isInLineUp;
    }

    public Player getPlayerOwned() {
        return PlayerOwned;
    }

    public void setIsInPurchaseArea(bool area) {
        isInPurchaseArea = area;
    }

    //Sets SnapBackpos to the current position
    public void setSnapBackPos() {
        snapBackPos = transform.position;

    }
    public void RemoveFromLineUp() {
        isInLineUp = false;
    }
}
