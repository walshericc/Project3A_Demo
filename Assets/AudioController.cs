using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [Header("References")]
    public AudioSource sfxSource;
    public AudioSource playerMovementSource;
    public AudioClip projectileSFX;
    public AudioClip shipMovingSFX;
    public AudioClip explosionSFX;
    
    private void OnEnable()
    {
        ShipFireHandler.OnProjectileFired += PlayProjectileSFX;
        ShipMovement.OnShipMoved += PlayShipMovingSFX;
        ShipCollisionHandler.OnPlayerDestroyed += PlayExplosionSFX;
        Asteroid.OnAsteroidHit += PlayExplosionSFX;
    }

    private void OnDisable()
    {
        ShipFireHandler.OnProjectileFired -= PlayProjectileSFX;
        ShipMovement.OnShipMoved -= PlayShipMovingSFX;
        ShipCollisionHandler.OnPlayerDestroyed -= PlayExplosionSFX;
        Asteroid.OnAsteroidHit -= PlayExplosionSFX;
    }

    private void PlayProjectileSFX()
    {
        sfxSource.PlayOneShot(projectileSFX);
    }

    private void PlayShipMovingSFX()
    {
        if (!playerMovementSource.isPlaying && playerMovementSource != null)
        {
            playerMovementSource.Play();
        }   
    }

    private void PlayExplosionSFX()
    {
        sfxSource.PlayOneShot(explosionSFX);
    }


}
