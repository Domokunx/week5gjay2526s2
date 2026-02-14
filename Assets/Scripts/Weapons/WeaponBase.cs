using UnityEngine;

public abstract class WeaponBase : MonoBehaviour, IWeapon
{
    public abstract WeaponType WeaponType { get; }
    public abstract void Attack(Player player);
}
