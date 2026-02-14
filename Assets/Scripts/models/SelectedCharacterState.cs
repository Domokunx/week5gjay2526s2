public static class SelectedCharacterState
{
    public static bool HasSelection { get; private set; }
    public static string CharacterName { get; private set; } = "Gemu";
    public static WeaponType StartingWeapon { get; private set; } = WeaponType.Sword;

    public static void SetSelection(CharacterInfo characterInfo)
    {
        if (characterInfo == null)
        {
            HasSelection = false;
            CharacterName = "Gemu";
            StartingWeapon = WeaponType.Sword;
            return;
        }

        HasSelection = true;
        CharacterName = characterInfo.characterName;
        StartingWeapon = characterInfo.GetResolvedStartingWeapon();
    }
}
