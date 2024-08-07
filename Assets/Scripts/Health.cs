using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public event EventHandler<HealthEventArgs> OnTakeDamage;
    public event EventHandler<HealthEventArgs> OnDeath;

    [SerializeField] private bool isPlayer = false;
    [SerializeField] private int health = 50;
    [SerializeField] private int scoreValue = 50;

    private AudioPlayer audioPlayer;

    private void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

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
        int damage = damageDealer.GetDamage();
        OnTakeDamage?.Invoke(this, new HealthEventArgs(damagePosition, scoreValue, damage, isPlayer));
        audioPlayer.PlayExplosionClip(isPlayer);

        health -= damage;
        if (health <= 0)
        {
            Die(damagePosition, damage);
        }
    }

    private void Die(Vector2 damagePosition, int damage)
    {
        OnDeath?.Invoke(this, new HealthEventArgs(damagePosition, scoreValue, damage, isPlayer));
        Destroy(gameObject);
    }

    public int GetHealth()
    {
        return health;
    }
}
