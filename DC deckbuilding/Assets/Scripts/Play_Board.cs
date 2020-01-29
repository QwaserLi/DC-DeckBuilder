using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Play_Board : MonoBehaviour
{


    List<Card> MainDeck = new List<Card>();
    List<Card> LineUp = new List<Card>();
    List<Card> Kicks;
    List<Card> Weaknesses;

    //Used for max amount of cards allowed in LineUp
    const int Line_Up_Max_Size = 5;
    //Used for to determine how far cards in the line up should be placed
    private const float Line_Up_Max_Length = 10; // MIGHT NOT BE USED
    private const float Line_Up_Increment = 1.75f; 


    //Used to tell the start position of the line up to place cards in
    private Vector3 LineUpStartPos = new Vector3(-3.5f, 1.5f, 0);

    //TEMP: Test Card
    public GameObject TestCard;

    void AddCardsToLineUp(){
        //Check for if there are no more cards in the main deck
        int Amount_to_add = Line_Up_Max_Size - LineUp.Count;

        if (Amount_to_add > MainDeck.Count) {
            //Might need to move else where
            GameOver();
        }

        List<Card> sublist = MainDeck.GetRange(0, Amount_to_add);
        //Flip cards now because they are visible
        foreach (Card c in sublist)
            c.gameObject.GetComponent<Card_Logic>().FlipCard();

        LineUp.AddRange(sublist);
        MainDeck.RemoveRange(0, Amount_to_add);

    }

    void GameOver() {

    }

    // Start is called before the first frame update
    void Start()
    {
        
        //TEMP: For testing if the game system works
        for (int i = 0; i < 10; i++)
        {
            //TEMP: IF statement to stop error that says that the card is null
            if (TestCard != null)
            {
                Card c = Instantiate(TestCard, new Vector3(3.5f, 4.5f, 0), Quaternion.identity).GetComponent<Card>();
                MainDeck.Add(c);
            }

        }

        //Take cards from Main Deck and put them line up
        AddCardsToLineUp();

        for (int i = 0; i < LineUp.Count; i++) {
            Vector3 newCardPos = LineUpStartPos;
            newCardPos.x += i * Line_Up_Increment;
            LineUp[i].gameObject.transform.position = newCardPos;
        }
        
    }

}
