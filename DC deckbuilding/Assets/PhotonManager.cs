using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Pun;
using Photon.Realtime;

public class PhotonManager : MonoBehaviourPunCallbacks
{

    public string versionName = "1";

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();

    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();

        Debug.Log("Connected to Master" + PhotonNetwork.CloudRegion) ;

    }

    public override void OnJoinedLobby() {
        Debug.Log("Joined Lobby");
        //Change from "Room" if you want to rename the room 
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions { MaxPlayers = 4 }, TypedLobby.Default);
    }

    private void OnDisconnectedFromPhoton()
    {
        Debug.Log("Disconnected");
    }

    public override void OnJoinedRoom()
    {

        //Test for shuffling decks and what not
        if (PhotonNetwork.IsMasterClient == true)
        {
            PhotonNetwork.Instantiate("Test_Card", new Vector3(), Quaternion.identity);
        }


      
    }

    // Start is called before the first frame update


}
