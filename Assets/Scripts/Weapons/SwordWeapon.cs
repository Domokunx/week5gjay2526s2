using UnityEngine;

public class SwordWeapon : WeaponBase
{
    public override WeaponType WeaponType => WeaponType.Sword;

    public override void Attack(Player attacker)
    {
        Debug.Log("Sword attack placeholder");
    }
}
