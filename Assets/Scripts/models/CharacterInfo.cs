using UnityEngine;
using System;

[System.Serializable]
public class CharacterInfo
{
    public string characterName;
    [TextArea(3, 6)]
    public string description;
    public WeaponType startingWeapon = WeaponType.Sword;

    [Header("Stat Placeholders")]
    public int baseHealth = 100;
    public int baseAttack = 10;
    public float baseMoveSpeed = 5f;

    public WeaponType GetResolvedStartingWeapon()
    {
        if (string.Equals(characterName, "Gemu", StringComparison.OrdinalIgnoreCase))
            return WeaponType.Sword;

        if (string.Equals(characterName, "Garu", StringComparison.OrdinalIgnoreCase))
            return WeaponType.Guns;

        return startingWeapon;
    }
}
