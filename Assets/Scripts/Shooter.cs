using System.Collections;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float projectileLifetime = 5f;
    [SerializeField] private float firingRate = 0.2f; // Time between shots

    public bool isFiring = false;
    private Coroutine firingCoroutine;

    void Update()
    {
        if (isFiring && firingCoroutine == null)
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        else if (!isFiring && firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
    }

    private IEnumerator FireContinuously()
    {
        while (isFiring)
        {
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, moveSpeed);
            Destroy(projectile, projectileLifetime);
            yield return new WaitForSeconds(firingRate); // Control the firing rate
        }
    }
}
