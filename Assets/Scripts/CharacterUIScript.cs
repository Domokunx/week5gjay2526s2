using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterSelectUI : MonoBehaviour
{
    public GameObject descriptionPanel;
    public TMP_Text nameText;
    public TMP_Text descriptionText;

    public CharacterInfo[] characters;

    // Assign the UI objects for each character (must have Outline component)
    public GameObject[] characterUIObjects;

    private int currentSelectedIndex = -1;

    private string sceneName = "SampleScene";

    public void ShowCharacter(int index)
    {
        CharacterInfo c = characters[index];

        nameText.text = c.characterName;
        descriptionText.text = c.description;
        descriptionPanel.SetActive(true);

        SelectCharacter(index);
    }

    public void SelectCharacter(int index)
    {
        // Disable previous outline
        if (currentSelectedIndex >= 0 && currentSelectedIndex < characterUIObjects.Length)
        {
            Outline oldOutline = characterUIObjects[currentSelectedIndex].GetComponent<Outline>();
            if (oldOutline != null)
                oldOutline.enabled = false;
        }

        // Enable new outline
        Outline newOutline = characterUIObjects[index].GetComponent<Outline>();
        if (newOutline != null)
            newOutline.enabled = true;

        currentSelectedIndex = index;
    }

    public void HidePanel()
    {
        descriptionPanel.SetActive(false);
    }
    
    // Upon confirming the character
    public void StartGame()
    {
        SceneManager.LoadScene(sceneName);
    }
}
