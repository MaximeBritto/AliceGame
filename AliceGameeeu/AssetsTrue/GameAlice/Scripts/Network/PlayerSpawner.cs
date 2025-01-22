using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    private Vector3[] spawnPoints;

    void Awake()
    {
        // Récupérer tous les points de spawn
        Transform spawnPointsParent = transform.Find("SpawnPoints");
        if (spawnPointsParent != null)
        {
            spawnPoints = new Vector3[spawnPointsParent.childCount];
            for (int i = 0; i < spawnPointsParent.childCount; i++)
            {
                spawnPoints[i] = spawnPointsParent.GetChild(i).position;
            }
        }
    }
} 