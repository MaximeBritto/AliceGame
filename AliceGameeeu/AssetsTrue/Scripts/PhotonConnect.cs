using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhotonConnect : MonoBehaviourPunCallbacks
{
    void Start()
    {
        Debug.Log("Connexion au serveur Photon...");
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connecté au serveur Photon!");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Lobby rejoint!");
        // Si vous voulez rejoindre une room automatiquement
        // PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomRoomFailed(object[] codeAndMsg)
    {
        Debug.Log("Aucune room disponible, création d'une nouvelle room...");
        PhotonNetwork.CreateRoom(null, new Photon.Realtime.RoomOptions { MaxPlayers = 10 });
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Room rejointe!");
        if (PhotonNetwork.IsMasterClient)
        {
            // Chargez la scène de jeu si vous êtes le master client
            // PhotonNetwork.LoadLevel("GameScene");
        }
    }
} 