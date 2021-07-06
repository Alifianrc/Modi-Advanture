using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public GameObject SoundOnButton;
    public GameObject SoundOffButton;

    public GameObject tantanganPanel;
    public GameObject highScorePanel;

    public GameObject ensiklopediaPanel;

    public GameObject CreditPanel;

    public GameObject TutorialPanel;

    public GameObject QuitPanel;

    public GameObject loadingPanel;
    public Slider slider;

    public GameObject SedangLock;
    public GameObject SusahLock;

    public Text HighScoreMudah;
    public Text HighScoreSedang;
    public Text HighScoreSusah;

    // Start is called before the first frame update
    void Start()
    {
        // Load game data
        TheData data = SaveData.LoadData();

        // Load level
        if (data.GetSedang())
        {
            SedangLock.SetActive(false);
        }
        if (data.GetSusah())
        {
            SusahLock.SetActive(false);
        }

        // Load HighScore
        HighScoreMudah.text = "" + data.GetScoreMudah();
        HighScoreSedang.text = "" + data.GetScoreSedang();
        HighScoreSusah.text = "" + data.GetScoreSusah();

        if (AudioManager.soundIsOn)
        {
            SoundOffButton.SetActive(true);
            SoundOnButton.SetActive(false);
        }
        else
        {
            SoundOffButton.SetActive(false);
            SoundOnButton.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Menjelajah()
    {
        GameManager.GameMode = 0;
        GameManager.GameLevel = 0;
        StartCoroutine(LoadAsynchronously());
    }
    public void Tantangan()
    {
        tantanganPanel.SetActive(true);
    }
    public void Kembali()
    {
        tantanganPanel.SetActive(false);
    }
    public void Mudah()
    {
        GameManager.GameMode = 1;
        GameManager.GameLevel = 1;
        StartCoroutine(LoadAsynchronously());
    }
    public void Sedang()
    {
        GameManager.GameMode = 1;
        GameManager.GameLevel = 2;
        StartCoroutine(LoadAsynchronously());
    }
    public void Sulit()
    {
        GameManager.GameMode = 1;
        GameManager.GameLevel = 3;
        StartCoroutine(LoadAsynchronously());
    }
    public void OpenHighScore()
    {
        highScorePanel.SetActive(true);
    }
    public void CloseHighScore()
    {
        highScorePanel.SetActive(false);
    }
    IEnumerator LoadAsynchronously()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(1);

        loadingPanel.SetActive(true);

        while (!operation.isDone)
        {
            slider.value = Mathf.Clamp01(operation.progress / .9f); ;

            yield return null;
        }
    }

    public void Ensiklopedia()
    {
        ensiklopediaPanel.SetActive(true);
    }
    public void CloseEnsiklopedia()
    {
        ensiklopediaPanel.SetActive(false);
    }

    public void CreditOpen()
    {
        CreditPanel.SetActive(true);
    }
    public void CreditClose()
    {
        CreditPanel.SetActive(false);
    }

    public void OpenTutorial()
    {
        TutorialPanel.SetActive(true);
    }
    public void CloseTutorial()
    {
        TutorialPanel.SetActive(false);
    }

    public void QuitButton()
    {
        QuitPanel.SetActive(true);
    }
    public void UnQuitButton()
    {
        QuitPanel.SetActive(false);
    }

    public void CloseApk()
    {
        Application.Quit();
    }

    public void SoundTurnOn()
    {
        SoundOffButton.SetActive(true);
        SoundOnButton.SetActive(false);
        AudioManager.soundIsOn = true;
    }

    public void SoundTurnOff()
    {
        SoundOffButton.SetActive(false);
        SoundOnButton.SetActive(true);
        AudioManager.soundIsOn = false;
    }
}
