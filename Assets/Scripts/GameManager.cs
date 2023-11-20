using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    CardController cc;
    StartPlay sp;
    EndScreen es;

    private void Awake()
    {
        cc = FindObjectOfType<CardController>();
        sp = FindObjectOfType<StartPlay>();
        es = FindObjectOfType<EndScreen>();
    }
    void Start()
    {
        sp.gameObject.SetActive(true);
        cc.gameObject.SetActive(false);
        es.gameObject.SetActive(false);
    }

    void Update()
    {
        if (cc.isGameComplete)
        {
            LoadEndScreen();
        }
    }

   void LoadEndScreen()
    {
        StartCoroutine(LoadingEndScreen());
    }

    IEnumerator LoadingEndScreen()
    {
        yield return new WaitForSeconds(0.5f);
        cc.gameObject.SetActive(false);
        sp.gameObject.SetActive(false);
        es.gameObject.SetActive(true);
        es.ScoreCalculator();
    }

    public void StartGame()
    {
        cc.gameObject.SetActive(true);
        sp.gameObject.SetActive(false);
        es.gameObject.SetActive(false);
        cc.isPlayer1Active = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
