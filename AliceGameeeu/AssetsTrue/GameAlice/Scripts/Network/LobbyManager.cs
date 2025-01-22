using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [Header("UI")]
    public Button createRoomButton;
    public Button joinRoomButton;
    public TMP_InputField roomNameInput;
    public Transform roomListContent;
    public GameObject roomListItemPrefab;

    void Start()
    {
        createRoomButton.onClick.AddListener(CreateRoom);
        joinRoomButton.onClick.AddListener(JoinRandomRoom);
    }

    void CreateRoom()
    {
        string roomName = string.IsNullOrEmpty(roomNameInput.text) 
            ? "Room " + Random.Range(1000, 10000) 
            : roomNameInput.text;

        RoomOptions roomOptions = new RoomOptions()
        {
            MaxPlayers = 10,
            IsVisible = true,
            IsOpen = true
        };

        PhotonNetwork.CreateRoom(roomName, roomOptions);
    }

    void JoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Game");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        // Mettre à jour la liste des rooms disponibles
        foreach (Transform child in roomListContent)
            Destroy(child.gameObject);

        foreach (RoomInfo room in roomList)
        {
            if (room.IsOpen && room.IsVisible)
            {
                Instantiate(roomListItemPrefab, roomListContent)
                    .GetComponent<RoomListItem>().SetUp(room);
            }
        }
    }
} 