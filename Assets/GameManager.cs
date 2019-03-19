using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public delegate void ScoreChanged();
    public static event ScoreChanged OnScoreChanged;

    public delegate void LivesChanged();
    public static event LivesChanged OnLivesChanged;

    public delegate void GameOver();
    public static event GameOver OnGameOver;

    public static GameManager Instance;
    
    [Header("Settings")]
    public int startingLives = 3;
    public int startingScore = 0;
    public int startingTimeInSeconds = 60;
    public int scorePerSecond = 10;
    public int asteroidValue = 25;
    public float respawnDelay = 2f;

    [Header("References")]
    public GameObject Player;
    public GameObject ExplosionPrefab;

    public int CurrentLives { get; private set; }
    public int CurrentScore { get; private set; }
    public float CurrentTime { get; private set; }


    private bool hasEnded;

    private void OnEnable()
    {
        ShipCollisionHandler.OnPlayerDestroyed += PlayerDestroyed;
        Asteroid.OnAsteroidHit += AsteroidHit;
    }

    private void OnDisable()
    {
        ShipCollisionHandler.OnPlayerDestroyed -= PlayerDestroyed;
        Asteroid.OnAsteroidHit -= AsteroidHit;
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            if (Instance != this)
                Destroy(this.gameObject);
    }

    private void Start()
    {
        CurrentTime = startingTimeInSeconds;
        UpdateLives(startingLives);
        UpdateScore(startingScore);

        InvokeRepeating("GainScoreForTime", 1f, 1f);
    }

    private void Update()
    {
        if (!hasEnded)
            CurrentTime -= Time.deltaTime;
        if (CurrentTime <= 0 && !hasEnded)
            EndGame();

        CurrentTime -= Time.deltaTime;
        if (CurrentTime <= 0)
            EndGame();
    }
    
    private void AsteroidHit()
    {
        UpdateScore(asteroidValue);
    }

    private void GainScoreForTime()
    {
        UpdateScore(scorePerSecond);
    }

    private void PlayerDestroyed()
    {
        UpdateLives(-1);
        var explosionPrefab = Instantiate(ExplosionPrefab, Player.transform.position, Player.transform.rotation);
        explosionPrefab.GetComponent<ParticleSystem>().Play();

        if (CurrentLives <= 0 && !hasEnded)
            EndGame();
        else
            StartCoroutine(RespawnPlayer());
    }

    private IEnumerator RespawnPlayer()
    {
        Player.SetActive(false);

        yield return new WaitForSeconds(respawnDelay);

        Player.SetActive(true);
        Player.transform.position = new Vector3(0, 0, 0);
        Player.transform.rotation = Quaternion.identity;
        Player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        Player.GetComponent<Rigidbody2D>().angularVelocity = 0f;
    }

    private void EndGame()
    {
        hasEnded = true;
        CancelInvoke("GainScoreForTime");
        Destroy(Player);
        Debug.Log("Game Over");
        OnGameOver?.Invoke();
    }

    private void UpdateLives(int delta)
    {
        CurrentLives += delta;
        OnLivesChanged?.Invoke();
    }

    private void UpdateScore(int delta)
    {
        CurrentScore += delta;
        OnScoreChanged?.Invoke();
    }
}
