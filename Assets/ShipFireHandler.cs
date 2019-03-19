using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipFireHandler : MonoBehaviour
{
    public delegate void ProjectileFired();
    public static event ProjectileFired OnProjectileFired;

    [Header("References")]
    public GameObject ProjectileContainer;

    [System.Serializable]
    public class Weapon
    {
        public Sprite image;
        public float speed;
        public float fireRate;
    }

    public Weapon CurrentWeapon;

    private float timeSinceFired = 0f;
    private bool justFired;

    private void Update()
    {
        if (justFired)
        {
            timeSinceFired += Time.deltaTime;
            if (timeSinceFired >= CurrentWeapon.fireRate)
            {
                justFired = false;
            }
        }
        

        if (Input.GetButtonDown("Fire"))
        {
            InvokeRepeating("Fire", 0f, CurrentWeapon.fireRate);
        }
         
        if (Input.GetButtonUp("Fire"))
        {
            CancelInvoke("Fire");
        }
    }
    
    private void Fire()
    {
        if (justFired)
            return;

        OnProjectileFired?.Invoke();

        var projectile = new GameObject();
        projectile.name = "Projectile";
        projectile.transform.position = gameObject.transform.position;
        projectile.transform.rotation = gameObject.transform.rotation;
        projectile.transform.SetParent(ProjectileContainer.transform);

        var spriteRenderer = projectile.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = CurrentWeapon.image;
        var coll = projectile.AddComponent<BoxCollider2D>();
        coll.isTrigger = true;
        projectile.AddComponent<DestroyWhenOffScreen>();
        projectile.AddComponent<Projectile>();

        var rb = projectile.AddComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.velocity = transform.up * CurrentWeapon.speed;

        justFired = true;
        timeSinceFired = 0f;
    }
}
