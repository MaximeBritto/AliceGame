using UnityEngine;
using Photon.Pun;

public class PlayerController : MonoBehaviourPunCallbacks
{
    [Header("Player Settings")]
    public float moveSpeed = 5f;
    public float sprintSpeed = 8f;
    public float jumpForce = 5f;
    public float mouseSensitivity = 2f;
    
    [Header("Game Settings")]
    public float staminaMax = 100f;
    public float currentStamina;
    public float tagRange = 2f;
    
    private CharacterController controller;
    private Camera playerCamera;
    private float verticalRotation = 0f;
    private Vector3 velocity;
    private bool isTagged = false;
    
    void Start()
    {
        if (!photonView.IsMine) return;
        
        controller = GetComponent<CharacterController>();
        playerCamera = GetComponentInChildren<Camera>();
        currentStamina = staminaMax;
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    void Update()
    {
        if (!photonView.IsMine) return;
        
        HandleMovement();
        HandleMouseLook();
        HandleSprinting();
        HandleTagging();
    }
    
    void HandleMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) && currentStamina > 0 ? sprintSpeed : moveSpeed;
        Vector3 move = transform.right * x + transform.forward * z;
        
        controller.Move(move * currentSpeed * Time.deltaTime);
        
        // Gravité simple
        velocity.y += Physics.gravity.y * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
    
    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);
        
        playerCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }
    
    void HandleSprinting()
    {
        if (Input.GetKey(KeyCode.LeftShift) && currentStamina > 0)
        {
            currentStamina -= Time.deltaTime * 20f;
        }
        else
        {
            currentStamina = Mathf.Min(staminaMax, currentStamina + Time.deltaTime * 10f);
        }
    }
    
    void HandleTagging()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;
            if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, tagRange))
            {
                PlayerController otherPlayer = hit.collider.GetComponent<PlayerController>();
                if (otherPlayer != null)
                {
                    photonView.RPC("TagPlayer", RpcTarget.All, otherPlayer.photonView.ViewID);
                }
            }
        }
    }
    
    [PunRPC]
    void TagPlayer(int viewID)
    {
        PhotonView targetView = PhotonView.Find(viewID);
        if (targetView != null)
        {
            PlayerController targetPlayer = targetView.GetComponent<PlayerController>();
            targetPlayer.isTagged = true;
        }
    }
}
 