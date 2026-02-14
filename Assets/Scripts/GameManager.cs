using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private Transform playerSpawnPoint;    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        } else if (Instance != this)
        {
            Destroy(gameObject);
        }
        
        SpawnPlayer(SelectionManager.Instance.playerPrefabs[SelectionManager.Instance.selectedPlayer]);
    }

    public void SpawnPlayer(GameObject playerPrefab)
    {
        Instantiate(playerPrefab, playerSpawnPoint.position, Quaternion.identity);
    }
}
