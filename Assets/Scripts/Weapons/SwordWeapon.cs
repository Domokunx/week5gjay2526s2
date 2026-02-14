using UnityEngine;

public class SwordWeapon : WeaponBase
{
    public LayerMask enemyMask;
    public override WeaponType WeaponType => WeaponType.Sword;

    public float damage = 50f;

    private Collider2D[] _results;
    public override void Attack(Player attacker)
    {
        var size = Physics2D.OverlapCircleNonAlloc(attacker.shootPoint.position, 2f, _results, enemyMask);

        foreach (var hit in _results)
        {
            if (hit.CompareTag("Enemy"))
            {
                hit.GetComponent<Enemy>().TakeDamage(damage);                
            }
        }
    }
}
