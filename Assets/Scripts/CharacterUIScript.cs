using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class CharacterSelectUI : MonoBehaviour
{
    public GameObject descriptionPanel;
    public TMP_Text nameText;
    public TMP_Text descriptionText;

    public CharacterInfo[] characters;

    // Assign in index order (Character1_Arrow, Character2_Arrow, etc.)
    public GameObject[] characterArrows;

    private int currentSelectedIndex = -1;

    private string sceneName = "SampleScene";

    private const string Character1ArrowName = "Character1_Arrow";
    private const string Character2ArrowName = "Character2_Arrow";

    private void Start()
    {
        TryAutoBindArrows();

        if (characters != null && characters.Length > 0)
            ShowCharacter(0);
    }

    private void TryAutoBindArrows()
    {
        bool hasAssignedArrow = false;
        if (characterArrows != null)
        {
            for (int i = 0; i < characterArrows.Length; i++)
            {
                if (characterArrows[i] != null)
                {
                    hasAssignedArrow = true;
                    break;
                }
            }
        }

        if (hasAssignedArrow)
            return;

        characterArrows = new GameObject[2];
        characterArrows[0] = GameObject.Find(Character1ArrowName);
        characterArrows[1] = GameObject.Find(Character2ArrowName);
    }

    private void Update()
    {
        if (characters == null || characters.Length == 0)
            return;

        var keyboard = Keyboard.current;
        if (keyboard == null)
            return;

        if (keyboard.rightArrowKey.wasPressedThisFrame || keyboard.dKey.wasPressedThisFrame)
        {
            int nextIndex = GetCycledIndex(1);
            ShowCharacter(nextIndex);
        }
        else if (keyboard.leftArrowKey.wasPressedThisFrame || keyboard.aKey.wasPressedThisFrame)
        {
            int nextIndex = GetCycledIndex(-1);
            ShowCharacter(nextIndex);
        }
    }

    private int GetCycledIndex(int direction)
    {
        if (currentSelectedIndex < 0)
            return direction > 0 ? 0 : characters.Length - 1;

        int nextIndex = currentSelectedIndex + direction;

        if (nextIndex >= characters.Length)
            nextIndex = 0;
        else if (nextIndex < 0)
            nextIndex = characters.Length - 1;

        return nextIndex;
    }

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

        if (characterArrows != null)
        {
            for (int i = 0; i < characterArrows.Length; i++)
            {
                if (characterArrows[i] != null)
                    characterArrows[i].SetActive(i == index);
            }
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
