using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Card_Type { Starter, Villain, SuperVillian, Equipment, Location, Hero, SuperPower, Weakness }

public class Card : MonoBehaviour
{

    //Might be used to keep track of which cards are which
    public int Card_id;

    public string Card_Name;
    public int Victory_Points;
    public int Card_Cost;

    private Card_Logic logic;
    
    //For Image of the card
    //public Sprite front_Sprite;
    //public Sprite back_Sprite;

    //All cards will have Power too make it to calucalate power and cards that don't have power will just have 0
    public int power;

    Card_Type card_Type;

    private void Awake()
    {
        logic = GetComponent<Card_Logic>();

    }

    private void Start()
    {
        //TEMP:TEST CARD FOR NOW
        Card_Name = "Aquamans trident";
        Victory_Points = 1;
        Card_Cost = 3;
        power = 2;

    }

    public Card_Logic getCardLogic() {
        return logic;
    }

}
