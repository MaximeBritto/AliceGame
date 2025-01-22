using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager Instance;
    
    [Header("Game Settings")]
    public float roundDuration = 300f; // 5 minutes
    public int minPlayersToStart = 2;
    
    private bool gameStarted = false;
    private float currentTime;
    
    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    
    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount >= minPlayersToStart && !gameStarted)
        {
            StartGame();
        }
    }
    
    void StartGame()
    {
        if (!PhotonNetwork.IsMasterClient) return;
        
        gameStarted = true;
        currentTime = roundDuration;
        photonView.RPC("SyncGameStart", RpcTarget.All);
    }
    
    [PunRPC]
    void SyncGameStart()
    {
        gameStarted = true;
        currentTime = roundDuration;
        UIManager.Instance.UpdateTimer(currentTime);
    }
    
    void Update()
    {
        if (!gameStarted) return;
        
        currentTime -= Time.deltaTime;
        UIManager.Instance.UpdateTimer(currentTime);
        
        if (currentTime <= 0)
        {
            EndGame();
        }
    }
    
    void EndGame()
    {
        gameStarted = false;
        // Logique de fin de partie
    }
} 