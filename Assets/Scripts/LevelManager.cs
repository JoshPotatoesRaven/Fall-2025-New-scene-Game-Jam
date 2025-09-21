using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public GameObject gameEndCanvas;
    public GameObject mainCanvas;

    void Awake()
    {
        Time.timeScale = 1f;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Start()
    {
        GameObject canvasHolder = GameObject.Find("CanvasHolder");
        if (canvasHolder == null)
        {
            var canvases = GameObject.FindGameObjectsWithTag("Canvas");
            if (canvases.Length > 0)
                canvasHolder = canvases[0];
        }

        if (canvasHolder == null)
        {
            return;
        }

        Transform youDiedT = canvasHolder.transform.Find("YouDied");
        Transform mainT = canvasHolder.transform.Find("Canvas");
        if (youDiedT == null || mainT == null)
        {
            return;
        }

        gameEndCanvas = youDiedT.gameObject;
        mainCanvas = mainT.gameObject;

        gameEndCanvas.SetActive(false);
        mainCanvas.SetActive(true);

        Button restartButton = gameEndCanvas.transform.Find("RestartButton")?.GetComponent<Button>();
        if (restartButton == null)
        {
            return;
        }

        restartButton.onClick.RemoveAllListeners();
        restartButton.onClick.AddListener(() =>
        {
            Time.timeScale = 1f;
            gameEndCanvas.SetActive(false);
            mainCanvas.SetActive(true);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        });
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Time.timeScale = 1f;
    }

    public void GameEnd()
    {
        Time.timeScale = 0f;
        if (mainCanvas != null) mainCanvas.SetActive(false);
        if (gameEndCanvas != null) gameEndCanvas.SetActive(true);
    }
}
