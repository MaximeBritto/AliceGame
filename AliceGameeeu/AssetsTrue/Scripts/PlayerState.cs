using UnityEngine;
using Photon.Pun;

public class PlayerState : MonoBehaviourPunCallbacks
{
    [Header("État du Joueur")]
    public bool isAlive = true;
    public bool isTagged = false;
    public float health = 100f;
    
    [Header("UI Elements")]
    public GameObject taggedIndicator;
    public GameObject healthBar;
    
    private PlayerController playerController;
    
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        UpdateVisuals();
    }
    
    [PunRPC]
    public void SetTagged(bool tagged)
    {
        isTagged = tagged;
        UpdateVisuals();
        
        if (isTagged && photonView.IsMine)
        {
            // Logique quand le joueur est tagué
            GameManager.Instance.OnPlayerTagged();
        }
    }
    
    void UpdateVisuals()
    {
        if (taggedIndicator != null)
            taggedIndicator.SetActive(isTagged);
    }
    
    public void TakeDamage(float damage)
    {
        if (!photonView.IsMine) return;
        
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }
    
    void Die()
    {
        isAlive = false;
        photonView.RPC("AnnouncePlayerDeath", RpcTarget.All);
    }
    
    [PunRPC]
    void AnnouncePlayerDeath()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.OnPlayerDeath();
    }
} 