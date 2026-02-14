using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroSceneScript : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    // Call this to go to the Character Selection scene
    public void GoToCharacterSelection()
    {
        SceneManager.LoadScene("CharacterSelectionScene");
    }
}
