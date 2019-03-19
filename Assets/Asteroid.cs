using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public delegate void AsteroidHit();
    public static event AsteroidHit OnAsteroidHit;

    public GameObject ExplosionPrefab;

    void Start()
    {
        Invoke("SelfDestruct", 10f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Projectile>() == null)
            return;

        // Give the player points and destroy both objects
        OnAsteroidHit?.Invoke();

        var explosionPrefab = Instantiate(ExplosionPrefab, gameObject.transform.position, gameObject.transform.rotation);
        explosionPrefab.GetComponent<ParticleSystem>().Play();

        Destroy(collision.gameObject);
        SelfDestruct();
    }

    private void SelfDestruct()
    {
        Destroy(this.gameObject);
    }
    


}
