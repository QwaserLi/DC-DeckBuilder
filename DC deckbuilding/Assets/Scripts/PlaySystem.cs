using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PlayState {Start, P1Turn, P2Turn, P3Turn, P4Turn, End}

public class PlaySystem : MonoBehaviour
{
    public PlayState state;
    public int AmtPlayers;

    public List<Player> players;
    private static Player currentPlayer;
    // Start is called before the first frame update

    private Play_Board playBoard;
        
    public Text currentPlayerText;

    void Start()
    {
        state = PlayState.Start;
        SetupGame();
    }

    void SetupGame() {
        //TEMP:Choose Amt of players But set to 4 for now
        AmtPlayers = 2;

        //TEMP: Fill up the player list
        players.Add(GameObject.Find("Player1").GetComponent<Player>());
        players.Add(GameObject.Find("Player2").GetComponent<Player>());


        //TEMP: Not nice code But current just to hide all the other players cards at the beginning of the game
        foreach (Player p in players) {
            currentPlayer = p;
            UnRenderHand();    
        }

        //Set Current player to player 1 and show hand
        state = PlayState.P1Turn;
        currentPlayer = players[0];
        RenderPlayerHand();
    }


    //TODO: To be used when implementing online functionality
    void AddPlayerToList() {

    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case PlayState.P1Turn:
                currentPlayerText.text = "Current Player: P1";
                break;
            case PlayState.P2Turn:
                currentPlayerText.text = "Current Player: P2";
                break;
            case PlayState.P3Turn:
                currentPlayerText.text = "Current Player: P3";
                break;
            case PlayState.P4Turn:
                currentPlayerText.text = "Current Player: P4";
                break;
            default:
                break;
        }
    }

    private void UnRenderHand() {
        //Unrender the current players hand 
        List<Card> playerHand = currentPlayer.getHand();
        foreach (Card c in playerHand)
        {
            //UnRenderCards
            c.gameObject.SetActive(false);
        }
    }

    private void RenderPlayerHand()
    {
        //Render the current Players Hand 
        List<Card> playerHand = currentPlayer.getHand();
        foreach(Card c in playerHand) {
            //RenderCards
            c.gameObject.SetActive(true);
        }
    }

    //Called when the end turn button is pressed to end current player turn and set up next player turn
    public void prepareNextTurn()
    {
        currentPlayer.DiscardHand();
        currentPlayer.DrawCards(5);
        UnRenderHand();
        changePlayerTurn();
        RenderPlayerHand();


    }

    private void changePlayerTurn() {
        if (state == PlayState.P1Turn) {
            state = PlayState.P2Turn;
            currentPlayer = players[1];
        } else if (state == PlayState.P2Turn) {
            if (AmtPlayers == 2) {
                state = PlayState.P1Turn;
                currentPlayer = players[0];
            }
            else {
                state = PlayState.P3Turn;
                currentPlayer = players[2];
            }
        } else if (state == PlayState.P3Turn) {
            if (AmtPlayers == 3)
            {
                state = PlayState.P1Turn;
                currentPlayer = players[0];
            }
            else
            {
                state = PlayState.P4Turn;
                currentPlayer = players[3];
            }
        } else if (state == PlayState.P4Turn) {
            state = PlayState.P1Turn;
            currentPlayer = players[0];
        }
    }

    public static Player getCurrentPlayer() {
        return currentPlayer;
    }

}
