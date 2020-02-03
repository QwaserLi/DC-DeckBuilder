using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    //Used to tell the start position of the Hand to place cards in
    Vector3 HandStartPos;
    //Used for the location of the discard pile
    Vector3 DiscardPilePos;
    Vector3 DeckPos;

    //Used for to determine how far apart cards in the Hand should be placed
    private const float Hand_Increment = 6f;

    private List<Card> Hand = new List<Card>();
    private List<Card> Deck = new List<Card>();
    private List<Card> Discard_Pile = new List<Card>();

    //List of Locations that the player has played
    List<Card> Locations;

    //PLAYER_ABILITY: Need someway to store and code player ability and keep track of it during turns

    //Used for keeping track of remaining power each turn
    int CurrentPowerTotal;

    //Test Card
    public GameObject TestCard;

    // Start is called before the first frame update
    void Start()
    {
        DeckPos = transform.Find("Deck").position;
        HandStartPos = transform.Find("Hand Start").position;
        DiscardPilePos = transform.Find("Discard").position;


        //TEMP: Fill Hand and Deck with temp cards
        Deck = new List<Card>();
        for (int i = 0; i < 10; i++)
        {
            if (TestCard != null)
            {
                Card c = Instantiate(TestCard, DeckPos, transform.rotation).GetComponent<Card>();
                c.getCardLogic().RemoveFromLineUp();
                c.getCardLogic().setPlayerOwned(this);
                addCardToDeck(c);
            }
        }


        DrawCards(5);
    }

    void addCardToDeck(Card card) {
        Deck.Add(card);
    }

    public void ShuffleDeck()
    {
        var count = Deck.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var r = Random.Range(i, count);
            var tmp = Deck[i];
            Deck[i] = Deck[r];
            Deck[r] = tmp;
        }
    }

    void putDiscardIntoDeck() {
        //Add cards from discard into deck and clear discard
        Deck.AddRange(Discard_Pile);
        Discard_Pile.Clear();
        //Flip each card over and set position
        foreach (Card c in Deck) {
            c.getCardLogic().SetFaceDown();
            c.gameObject.transform.position = DeckPos;
        }
    }


    public void DrawCards(int Amount) {
        List<Card> sublist;
        if (Deck.Count == 0)
        {
            putDiscardIntoDeck();
            ShuffleDeck();
        }
        else if (Amount > Deck.Count)
        {

            //TEMP: Make sure to shuffle deck when not enough cards to draw;
            //Draw Remaining cards in deck then shuffle discard and draw the amount of cards you still need to card
            sublist = Deck.GetRange(0, Deck.Count-1);
            Hand.AddRange(sublist);
            Amount -= Deck.Count;
            putDiscardIntoDeck();
            ShuffleDeck();

        }
        sublist = Deck.GetRange(0, Amount);
 
        //Put cards into hand and Flip cards now because they are now visible
        Hand.AddRange(sublist);
        for (int i = 0; i < Hand.Count;i++)
        {
            Hand[i].getCardLogic().SetFaceUp();
            Vector3 newCardPos = HandStartPos;

            //TODO/TEMP: Will have to change it when making it multiplayer

            if (gameObject.name.Equals("Player2") || gameObject.name.Equals("Player4")) {
                newCardPos.y += -i * (Hand_Increment / Hand.Count);

            }
            else {
                newCardPos.x += i * (Hand_Increment / Hand.Count);
            }
            Hand[i].gameObject.transform.position = newCardPos;
            Hand[i].getCardLogic().setSnapBackPos();
        }
        Deck.RemoveRange(0, Amount);
    }

    public void DiscardHand() {
        
        //Add each card in Hand to Discard pile then clear the hand
        foreach (Card c in Hand) {
            DiscardCard(c);
        }
        Hand.Clear();
    }

    //To discard single cards
    void DiscardCard(Card c) {
        Discard_Pile.Add(c);
        //Move Card
        c.gameObject.transform.position = DiscardPilePos;

        //Make it so the card rotates for the player
        c.gameObject.transform.rotation = transform.rotation;
        //TEMP: To make it so that Cards in the discard pile stay in the discard pile so we snap it back there for now
        c.getCardLogic().setSnapBackPos();
    }

    int CalculatePowerInHand() {
        int powerTotal = 0;
        foreach (Card c in Deck)
        {
            powerTotal += c.power;
        }
        return powerTotal;
    }

    int CalculateTotalVictoryPotints() {
        int victoryPointTotal = 0;
        foreach (Card c in Deck) {
            victoryPointTotal += c.Victory_Points;
        }
        return victoryPointTotal;
    }

    public List<Card> getHand() {
        return Hand;
    }

    //TODO: Potentially optimize the code to make it cleaner and run smoother

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Card") {

            Card c = collision.GetComponent<Card>();
            c.getCardLogic().setIsInPurchaseArea(true);
            purchaseCard(c);
        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Card")
        {

            Card c = collision.GetComponent<Card>();
            c.getCardLogic().setIsInPurchaseArea(true);
            purchaseCard(c);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Card")
        {
            Card c = collision.GetComponent<Card>();
            c.getCardLogic().setIsInPurchaseArea(false);
        }
    }

    private void purchaseCard(Card c) {
        //TODO: Buy card if have enough power, 
        //is in the line up 
        //and not hand 
        //Not Owned
        //TODO: Not FaceDown
        //TODO: Clean Code
        Card_Logic logic = c.getCardLogic();
        if (!Hand.Contains(c) && !logic.getOwned() && logic.IsInPurchaseArea() && logic.IsInLineUp() && PlaySystem.getCurrentPlayer() == this)
        {
            //Add to discard Pile and Remove from line Up list and set ownership to player
            //TEMP? Need If here to check if you release the mouse button then you buy it rather than just when you hover over a player
            if (Input.GetMouseButtonUp(0)) {
                c.getCardLogic().setPlayerOwned(this);
                DiscardCard(c);
                Play_Board.RemoveFromLineUp(c);
            }
        }
    }


    //GIZMOS
    void OnDrawGizmos()
    {
        // Draw a semitransparent blue cube at the transforms position
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(transform.Find("Deck").transform.position, new Vector3(1, 2, 1));

        Gizmos.color = new Color(1, 0, 1, 0.5f);
        Gizmos.DrawCube(transform.Find("Discard").transform.position, new Vector3(1, 2, 1));

        Gizmos.color = new Color(0, 0, 1, 0.5f);
        Gizmos.DrawCube(transform.Find("Hand Start").transform.position, new Vector3(1, 2, 1));
    }
}
