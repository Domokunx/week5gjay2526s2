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
        if (characters == null || index < 0 || index >= characters.Length)
            return;

        CharacterInfo c = characters[index];

        nameText.text = c.characterName;
        descriptionText.text = c.description;
        descriptionPanel.SetActive(true);

        SelectCharacter(index);
    }

    public void SelectCharacter(int index)
    {
        if (index < 0 || (characters != null && index >= characters.Length))
            return;

        // Disable previous outline
        if (characterUIObjects != null &&
            currentSelectedIndex >= 0 &&
            currentSelectedIndex < characterUIObjects.Length)
        {
            Outline oldOutline = characterUIObjects[currentSelectedIndex].GetComponent<Outline>();
            if (oldOutline != null)
                oldOutline.enabled = false;
        }

        // Enable new outline
        if (characterUIObjects != null && index < characterUIObjects.Length)
        {
            Outline newOutline = characterUIObjects[index].GetComponent<Outline>();
            if (newOutline != null)
                newOutline.enabled = true;
        }

        currentSelectedIndex = index;
    }

    public void HidePanel()
    {
        descriptionPanel.SetActive(false);
    }
    
    // Upon confirming the character
    public void StartGame()
    {
        if (characters != null && characters.Length > 0)
        {
            int selectedIndex = currentSelectedIndex < 0 ? 0 : currentSelectedIndex;
            selectedIndex = Mathf.Clamp(selectedIndex, 0, characters.Length - 1);
            SelectedCharacterState.SetSelection(characters[selectedIndex]);
        }
        else
        {
            SelectedCharacterState.SetSelection(null);
        }

        SceneManager.LoadScene(sceneName);
    }
}
