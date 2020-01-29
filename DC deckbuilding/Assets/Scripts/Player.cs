using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    //Used to tell the start position of the Hand to place cards in
    Vector3 HandStartPos = new Vector3(-0.5f,-3,0);
    //Used for to determine how far apart cards in the Hand should be placed
    private const float Hand_Increment = 1.75f;

    private List<Card> Hand = new List<Card>();
    private List<Card> Deck = new List<Card>();
    private List<Card> Discard_Pile;

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
        //TEMP: Fill Hand and Deck with temp cards
        Deck = new List<Card>();
        for (int i = 0; i < 10; i++)
        {
            if (TestCard != null)
            {
                Card c = Instantiate(TestCard, new Vector3(-3.5f, -2, 0), Quaternion.identity).GetComponent<Card>();
                Deck.Add(c);
            }
        }


        DrawCards(4);

        for (int i = 0; i < Hand.Count; i++)
        {
            Vector3 newCardPos = HandStartPos;
            newCardPos.x += i * Hand_Increment;
            Hand[i].gameObject.transform.position = newCardPos;
        }
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
        //Flip cards now because they are now visible
        foreach (Card c in sublist)
            c.gameObject.GetComponent<Card_Logic>().FlipCard();
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
