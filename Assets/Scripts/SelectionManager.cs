using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public static SelectionManager Instance;
    public GameObject[] playerPrefabs;
    public int selectedPlayer;
    
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
        
        DontDestroyOnLoad(this);
    }

    public void SelectPlayer(int playerID)
    {
        selectedPlayer = playerID;
    }
}
