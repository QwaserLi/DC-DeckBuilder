using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private List<Card> Hand;
    List<Card> Deck;
    List<Card> Discard_Pile;

    //List of Locations that the player has played
    List<Card> Locations;

    //PLAYER_ABILITY: Need someway to store and code player ability and keep track of it during turns

    //Used for keeping track of remaining power each turn
    int CurrentPowerTotal;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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


    void DrawCards(int Amount) {
        if (Amount > Deck.Count)
        {
            //Draw Remaining cards in deck then shuffle discard and draw the remaining

        }
        List<Card> sublist = Deck.GetRange(0, Amount);
        Hand.AddRange(sublist);
        Deck.RemoveRange(0, Amount);
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
}
