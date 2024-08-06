using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public event EventHandler<HealthEventArgs> OnTakeDamage;
    public event EventHandler<HealthEventArgs> OnDeath;

    [SerializeField] private bool isPlayer = false;
    [SerializeField] private int health = 50;

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
        if (OnTakeDamage != null)
        {
            OnTakeDamage.Invoke(this, new HealthEventArgs(damagePosition, damage, isPlayer));
        }
        else
        {
            Debug.LogWarning("No subscribers for OnTakeDamage event.");
        }

        audioPlayer.PlayExplosionClip(isPlayer);

        health -= damage;
        if (health <= 0)
        {
            if (OnDeath != null)
            {
                OnDeath.Invoke(this, new HealthEventArgs(damagePosition, damage, isPlayer));
            }
            else
            {
                Debug.LogWarning("No subscribers for OnDeath event.");
            }
            Destroy(gameObject);
        }
    }
}
