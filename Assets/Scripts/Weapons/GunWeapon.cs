using UnityEngine;

public class GunWeapon : WeaponBase
{
    public override WeaponType WeaponType => WeaponType.Guns;

    public override void Attack()
    {
        Debug.Log("Gun attack placeholder");
    }
}
