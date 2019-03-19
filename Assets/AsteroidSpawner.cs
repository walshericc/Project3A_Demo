using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [Header("Settings")]
    public float smallAstertoidSpawnChance;
    public float largeAsteroidSpawnChance;
    public float horizontalSpawnChance;
    public float speed = 2f;
    public float resetTime = 3f;

    [Header("References")]
    public GameObject AsteroidContainer;
    public GameObject ExplosionPrefab;
    public List<Sprite> LargeAsteroids;
    public List<Sprite> MediumAsteroids;
    public List<Sprite> SmallAsteroids;
    
    private void OnEnable()
    {
        ShipCollisionHandler.OnPlayerDestroyed += ResetAsteroids;
        GameManager.OnGameOver += ClearAsteroids;
    }

    private void OnDisable()
    {
        ShipCollisionHandler.OnPlayerDestroyed -= ResetAsteroids;
        GameManager.OnGameOver -= ClearAsteroids;
    }

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnAsteroid", 1f, 1f);
    }

    private void ClearAsteroids()
    {
        CancelInvoke("ResetAsteroids");

        foreach (var asteroid in GetComponentsInChildren<Asteroid>())
        {
            Destroy(asteroid.gameObject);
        }
    }

    private void ResetAsteroids()
    {
        ClearAsteroids();

        InvokeRepeating("SpawnAsteroid", 3f, 1f);
    }

    private void SpawnAsteroid()
    {
        // Decide if it should be small, medium, or large
        Sprite asteroidSprite;
        if (Random.value <= smallAstertoidSpawnChance)
            asteroidSprite = SmallAsteroids[Random.Range(0, SmallAsteroids.Count)];
        else if (Random.value <= largeAsteroidSpawnChance)
            asteroidSprite = LargeAsteroids[Random.Range(0, LargeAsteroids.Count)];
        else
            asteroidSprite = MediumAsteroids[Random.Range(0, MediumAsteroids.Count)];

        // Decide if it is moving horizontally or vertically and determine an appropriate spawn position
        Vector3 spawnPos;
        Vector3 direction;
        if (Random.value <= horizontalSpawnChance)
        {
            if (Random.value <= 0.5f)
            {
                spawnPos = new Vector3(0, Random.Range(0, Screen.height), 10);
                direction = new Vector3(1, Random.Range(-1f, 1f));
            }
            else
            {
                spawnPos = new Vector3(Screen.width, Random.Range(0, Screen.height), 10);
                direction = new Vector3(-1, Random.Range(-1f, 1f));
            }
           
        }  
        else
        {
            if (Random.value <= 0.5f)
            {
                spawnPos = new Vector3(Random.Range(0, Screen.width), 0, 10);
                direction = new Vector3(Random.Range(-1f, 1f), 1);
            }
            else
            {
                spawnPos = new Vector3(Random.Range(0, Screen.width), Screen.height, 10);
                direction = new Vector3(Random.Range(-1f, 1f), -1);
            }
        }
          
        spawnPos = Camera.main.ScreenToWorldPoint(spawnPos);

        // Create our object in the world
        Debug.Log("New asteroid spawned");
        var newAsteroid = new GameObject();
        newAsteroid.name = "Asteroid";
        newAsteroid.transform.position = spawnPos;
        newAsteroid.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
        newAsteroid.transform.SetParent(AsteroidContainer.transform);
        
        // Set our object's sprite and collider
        var spriteRenderer = newAsteroid.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = asteroidSprite;
        newAsteroid.AddComponent<CircleCollider2D>();
        var asteroid = newAsteroid.AddComponent<Asteroid>();
        asteroid.ExplosionPrefab = ExplosionPrefab;

        // Get our object moving
        var rb = newAsteroid.AddComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.velocity = direction * speed;
    }
}
