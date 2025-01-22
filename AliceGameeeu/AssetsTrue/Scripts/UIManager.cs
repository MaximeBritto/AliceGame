using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    
    [Header("HUD")]
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI playersAliveText;
    public TextMeshProUGUI staminaText;
    public Image staminaBar;
    
    [Header("Menus")]
    public GameObject mainMenu;
    public GameObject gameOverMenu;
    public GameObject winScreen;
    
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    
    public void UpdateTimer(float currentTime)
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(currentTime / 60);
            int seconds = Mathf.FloorToInt(currentTime % 60);
            timerText.text = $"{minutes:00}:{seconds:00}";
        }
    }
    
    public void UpdatePlayersAlive(int alive, int total)
    {
        if (playersAliveText != null)
        {
            playersAliveText.text = $"Joueurs: {alive}/{total}";
        }
    }
    
    public void UpdateStamina(float current, float max)
    {
        if (staminaBar != null)
            staminaBar.fillAmount = current / max;
            
        if (staminaText != null)
            staminaText.text = $"{Mathf.CeilToInt(current)}/{Mathf.CeilToInt(max)}";
    }
    
    public void ShowGameOver()
    {
        gameOverMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    
    public void ShowWinScreen()
    {
        winScreen.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
} 