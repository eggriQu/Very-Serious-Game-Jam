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

        StartCoroutine(FadeIn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void PlayGame()
    {
        SceneManager.LoadScene("Level");
    }

    public void PauseGame()
    {
        pauseUI.SetActive(true);
        isPaused = true;
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        pauseUI.SetActive(false);
        isPaused = false;
        Time.timeScale = 1;
    }

    public void PlayButton()
    {
        StartCoroutine(PlayRoutine());
    }

    public void QuitButton()
    {
        StartCoroutine(QuitToMenu());
    }

    private IEnumerator QuitToMenu()
    {
        Time.timeScale = 1;
        fadeAnim.Play("FadeOut");
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("MainMenu");
    }

    public IEnumerator LevelComplete()
    {
        StartCoroutine(FadeOut());
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("End Credits");
    }

    private IEnumerator PlayRoutine()
    {
        StartCoroutine(FadeOut());
        yield return new WaitForSeconds(0.5f);
        PlayGame();
    }

    IEnumerator FadeIn()
    {
        fadeAnim.Play("FadeIn");
        yield return new WaitForSeconds(0.5f);
        fadeAnim.Play("Transparent");
    }

    IEnumerator FadeOut()
    {
        fadeAnim.Play("FadeOut");
        yield return new WaitForSeconds(0.5f);
        fadeAnim.Play("Opaque");
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
