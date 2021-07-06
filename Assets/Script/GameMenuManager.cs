using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuManager : MonoBehaviour
{
    public GameObject menuPanel;
    public GameObject ensiklopediaPanel;

    public GameObject SoundOnButton;
    public GameObject SoundOffButton;

    public static bool menuIsActive;


    // Start is called before the first frame update
    void Start()
    {
        menuIsActive = false;

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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Menu();
        }
    }

    public void Menu()
    {
        menuPanel.SetActive(true);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        menuIsActive = true;
    }
    public void Lanjut()
    {
        menuPanel.SetActive(false);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        menuIsActive = false;
    }

    public void Kembali()
    {
        SceneManager.LoadScene(0);
    }

    public void Ensiklopedia()
    {
        ensiklopediaPanel.SetActive(true);
    }
    public void CloseEnsiklopedia()
    {
        ensiklopediaPanel.SetActive(false);
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
