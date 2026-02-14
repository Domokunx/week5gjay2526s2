using UnityEngine;

public class GunWeapon : WeaponBase
{
    public override WeaponType WeaponType => WeaponType.Guns;
    public LayerMask enemyMask;
    public LayerMask groundMask;
    
    public float damage = 30f;
    public override void Attack(Player attacker)
    {
        RaycastHit2D hit = Physics2D.Raycast(
            attacker.isFacingRight ? attacker.shootPointRight.position : attacker.shootPointLeft.position, 
            attacker.isFacingRight ? Vector2.right : Vector2.left,
            Mathf.Infinity,
            enemyMask | groundMask);

        if (hit && hit.collider.CompareTag("Enemy"))
        {
            hit.collider.GetComponent<Enemy>()?.TakeDamage(damage);
        }
        
    }
}
