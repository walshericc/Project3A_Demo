using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [Header("References")]
    public Text LivesText;
    public Text ScoreText;
    public Text TimerText;

    private void OnEnable()
    {
        GameManager.OnLivesChanged += UpdateLivesDisplay;
        GameManager.OnScoreChanged += UpdateScoreDisplay;
    }

    private void OnDisable()
    {
        GameManager.OnLivesChanged -= UpdateLivesDisplay;
        GameManager.OnScoreChanged -= UpdateScoreDisplay;
    }

    private void Update()
    {
        TimerText.text = "Time: " + GameManager.Instance.CurrentTime.ToString("F2") + " / " + GameManager.Instance.startingTimeInSeconds.ToString("F2");
    }

    private void UpdateLivesDisplay()
    {
        LivesText.text = "Lives: " + GameManager.Instance.CurrentLives + " / " + GameManager.Instance.startingLives;
    }

    private void UpdateScoreDisplay()
    {
        ScoreText.text = "Score: " + GameManager.Instance.CurrentScore;
    }
}
