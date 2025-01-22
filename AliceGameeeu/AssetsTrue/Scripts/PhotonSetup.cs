using UnityEngine;
using Photon.Pun;

public class PhotonSetup : MonoBehaviour
{
    [SerializeField]
    private string appIdPUN = "VOTRE_APP_ID_ICI"; // Remplacez par votre App ID Photon

    void Awake()
    {
        PhotonNetwork.PhotonServerSettings.AppSettings.AppIdPun = appIdPUN;
        PhotonNetwork.ConnectUsingSettings();
    }
} 