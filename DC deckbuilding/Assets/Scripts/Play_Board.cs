using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Play_Board : MonoBehaviour
{
    List<Card> MainDeck = new List<Card>();
    private static List<Card> LineUp = new List<Card>();
    List<Card> Kicks;
    List<Card> Weaknesses;

    //Used for max amount of cards allowed in LineUp
    const int Line_Up_Max_Size = 5;
    //Used for to determine how far cards in the line up should be placed
    private const float Line_Up_Max_Length = 10; // MIGHT NOT BE USED
    private const float Line_Up_Increment = 1.5f; 


    //Used to tell the start position of the line up to place cards in
    private Vector3 LineUpStartPos;
    private Vector3 KickPilePos;
    private Vector3 MainDeckPos;

    //TEMP: Test Card
    public GameObject TestCard;

    // Start is called before the first frame update
    void Start()
    {
        MainDeckPos = transform.Find("MainDeck").position;
        LineUpStartPos = transform.Find("LineUp Start").position;
        KickPilePos = transform.Find("Kick").position;

        //TEMP: For testing if the game system works
        for (int i = 0; i < 10; i++)
        {
            //TEMP: IF statement to stop error that says that the card is null
            if (TestCard != null)
            {
                Card c = Instantiate(TestCard, MainDeckPos, Quaternion.identity).GetComponent<Card>();
                MainDeck.Add(c);
            }

        }

        //Take cards from Main Deck and put them line up
        AddCardsToLineUp();
    }

    public void AddCardsToLineUp()
    {
        //Check for if there are no more cards in the main deck
        int Amount_to_add = Line_Up_Max_Size - LineUp.Count;

        if (Amount_to_add > MainDeck.Count)
        {
            //Might need to move else where
            //TODO: GameOver Method not complete yet
            GameOver();
            return;
        }

        List<Card> sublist = MainDeck.GetRange(0, Amount_to_add);
        //Flip cards now because they are visible
        foreach (Card c in sublist)
            c.getCardLogic().SetFaceUp();

        LineUp.AddRange(sublist);
        MainDeck.RemoveRange(0, Amount_to_add);

        //Position cards in the line up
        for (int i = 0; i < LineUp.Count; i++)
        {
            Vector3 newCardPos = LineUpStartPos;
            newCardPos.x += i * Line_Up_Increment;
            LineUp[i].gameObject.transform.position = newCardPos;
            LineUp[i].getCardLogic().setSnapBackPos();
        }

    }

    //TODO: Gets Called when the game in over
    void GameOver()
    {
        print("Not Enough Cards in Main Deck");
    }

    public static void RemoveFromLineUp(Card c) {
        LineUp.Remove(c);
        c.getCardLogic().RemoveFromLineUp();
    }

    //GIZMOS
    void OnDrawGizmos()
    {
        // Draw a semitransparent blue cube at the transforms position
        Gizmos.color = new Color(1, 1, 0, 0.5f);
        Gizmos.DrawCube(transform.Find("MainDeck").transform.position, new Vector3(1, 2, 1));

        Gizmos.color = new Color(1, 0.5f, 1, 0.5f);
        Gizmos.DrawCube(transform.Find("Kick").transform.position, new Vector3(1, 2, 1));

        Gizmos.color = new Color(0.6f, 0, 1, 0.5f);
        Gizmos.DrawCube(transform.Find("LineUp Start").transform.position, new Vector3(1, 2, 1));
    }

}
