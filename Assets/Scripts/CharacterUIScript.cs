using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CharacterSelectUI : MonoBehaviour
{
    public GameObject descriptionPanel;
    public TMP_Text nameText;
    public TMP_Text descriptionText;

    public CharacterInfo[] characters;

    public void ShowCharacter(int index)
    {
        CharacterInfo c = characters[index];

        nameText.text = c.characterName;
        descriptionText.text = c.description;

        descriptionPanel.SetActive(true);
    }

    public void HidePanel()
    {
        descriptionPanel.SetActive(false);
    }
}
