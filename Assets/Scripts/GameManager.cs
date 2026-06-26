using System;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool levelStarted;
    public bool isPaused;

    [SerializeField] private GameObject pauseUI;
    [SerializeField] private Animator fadeAnim;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Level");
    }

    public void PauseGame()
    {
        pauseUI.SetActive(true);
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseUI.SetActive(false);
        isPaused = false;
    }

    public void QuitButton()
    {
        StartCoroutine(QuitToMenu());
    }

    public IEnumerator QuitToMenu()
    {
        fadeAnim.Play("FadeOut");
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene("Menu");
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
