using UnityEngine;
using Photon.Pun;
using TMPro;

public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager Instance;
    
    [Header("Game Settings")]
    public float gameDuration = 300f; // 5 minutes
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI playersAliveText;
    
    private float currentTime;
    private bool gameStarted = false;
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            StartGame();
        }
    }
    
    void Update()
    {
        if (!gameStarted) return;
        
        currentTime -= Time.deltaTime;
        UpdateUI();
        
        if (currentTime <= 0)
        {
            EndGame();
        }
    }
    
    void StartGame()
    {
        currentTime = gameDuration;
        gameStarted = true;
        photonView.RPC("SyncGameStart", RpcTarget.All);
    }
    
    [PunRPC]
    void SyncGameStart()
    {
        gameStarted = true;
        currentTime = gameDuration;
    }
    
    void EndGame()
    {
        gameStarted = false;
        // Logique de fin de partie
    }
    
    void UpdateUI()
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(currentTime / 60);
            int seconds = Mathf.FloorToInt(currentTime % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
} 