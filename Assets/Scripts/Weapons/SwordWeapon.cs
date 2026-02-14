using UnityEngine;

public class SwordWeapon : WeaponBase
{
    public override WeaponType WeaponType => WeaponType.Sword;

    public override void Attack()
    {
        Debug.Log("Sword attack placeholder");
    }
}
