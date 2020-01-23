using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PlayState {Start, P1Turn, P2Turn, P3Turn, P4Turn, End}

public class PlaySystem : MonoBehaviour
{
    public PlayState state;
    public int AmtPlayers; 


    // Start is called before the first frame update
    void Start()
    {
        state = PlayState.Start;
        SetupGame();
    }

    void SetupGame() {
        //Choose Amt of players But set to 4 for now
        AmtPlayers = 4;
        state = PlayState.P1Turn;
    }

    // Update is called once per frame
    void Update()
    {
        //switch (state) {
        //    case PlayState.P1Turn:
        //        break;
        //    case PlayState.P2Turn:
        //        break;
        //    case PlayState.P3Turn:
        //        break;
        //    case PlayState.P4Turn:
        //        break;
        //    default:
        //        break;
        //}
    }

    void OnMouseUp()
    {
        nextPlayerTurn();
    }

    void nextPlayerTurn() {
        if (state == PlayState.P1Turn) {
                state = PlayState.P2Turn;
        } else if (state == PlayState.P2Turn) {
            if (AmtPlayers == 2) {
                state = PlayState.P1Turn;
            }
            else {
                state = PlayState.P3Turn;
            }
        } else if (state == PlayState.P3Turn) {
            if (AmtPlayers == 3)
            {
                state = PlayState.P1Turn;
            }
            else
            {
                state = PlayState.P3Turn;
            }
        } else if (state == PlayState.P4Turn) {
            state = PlayState.P1Turn;
        }

    }
}
