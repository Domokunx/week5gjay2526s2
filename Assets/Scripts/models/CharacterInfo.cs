using UnityEngine;

[System.Serializable]
public class CharacterInfo
{
    public string characterName;
    [TextArea(3, 6)]
    public string description;
    //public Sprite portrait;
}
