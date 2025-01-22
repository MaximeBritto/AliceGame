using UnityEngine;
using Photon.Pun;

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
        // Rejoindre automatiquement le lobby
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Lobby rejoint!");
        // Ici vous pouvez charger la scène du lobby si nécessaire
        SceneController.Instance.LoadLobby();
    }
} 