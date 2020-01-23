using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Play_Board : MonoBehaviour
{

    List<Card> MainDeck;
    List<Card> LineUp;
    List<Card> Kicks;
    List<Card> Weaknesses;

    int LineUpMaxSize = 5;

    void AddCardsToLineUp(){
        //Check for if there are no more cards in the main deck
        int Amount_to_add = LineUpMaxSize - LineUp.Count;

        if (Amount_to_add > MainDeck.Count) {
            //Might need to move else where
            GameOver();
        }

        List<Card> sublist = MainDeck.GetRange(0, Amount_to_add);
        LineUp.AddRange(sublist);
        MainDeck.RemoveRange(0, Amount_to_add);

    }

    void GameOver() {

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
