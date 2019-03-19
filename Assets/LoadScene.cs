using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    [Header("Settings")]
    public string nextScene = "";

    private void OnEnable()
    {
        GameManager.OnGameOver += OnChangeScene;
    }

    private void OnDisable()
    {
        GameManager.OnGameOver -= OnChangeScene;
    }

    public void OnChangeScene()
    {
        SceneManager.LoadScene(nextScene);
    }
}
