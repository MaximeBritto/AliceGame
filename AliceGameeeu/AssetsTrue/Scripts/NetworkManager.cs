using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public static NetworkManager Instance;
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }
    
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connecté au serveur Photon");
        PhotonNetwork.JoinLobby();
    }
    
    public override void OnJoinedLobby()
    {
        Debug.Log("Connecté au lobby");
        // Rejoindre ou créer une room
        PhotonNetwork.JoinRandomRoom();
    }
    
    public override void OnJoinRandomRoomFailed(object[] codeAndMsg)
    {
        Debug.Log("Aucune room disponible, création d'une nouvelle room");
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 10 });
    }
    
    public override void OnJoinedRoom()
    {
        Debug.Log("Room rejointe");
        // Spawn du joueur
        Vector3 randomPosition = new Vector3(Random.Range(-5f, 5f), 2f, Random.Range(-5f, 5f));
        PhotonNetwork.Instantiate("Player", randomPosition, Quaternion.identity);
    }
} 