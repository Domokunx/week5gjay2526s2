using UnityEngine;

public class SwordWeapon : WeaponBase
{
    public LayerMask enemyMask;
    public override WeaponType WeaponType => WeaponType.Sword;

    public float damage = 50f;

    public override void Attack(Player attacker)
    {
        var results = Physics2D.OverlapCircleAll(attacker.shootPoint.position, 2f, enemyMask);

        foreach (var hit in results)
        {
            if (hit.CompareTag("Enemy"))
            {
                hit.GetComponent<Enemy>().TakeDamage(damage);                
            }
        }
    }
}
