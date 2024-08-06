using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public event EventHandler<HealthEventArgs> OnTakeDamage;
    public event EventHandler<HealthEventArgs> OnDeath;

    [SerializeField] bool isPlayer = false;
    [SerializeField] int health = 50;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();
        if (damageDealer != null)
        {
            TakeDamage(damageDealer, collision.transform.position);
            damageDealer.Hit();
        }
    }

    private void TakeDamage(DamageDealer damageDealer, Vector2 damagePosition)
    {
        OnTakeDamage?.Invoke(this, new HealthEventArgs(damagePosition, isPlayer));

        health -= damageDealer.GetDamage();
        if (health <= 0)
        {
            Destroy(gameObject);
            OnDeath?.Invoke(this, new HealthEventArgs(damagePosition, isPlayer));
        }
    }
}
