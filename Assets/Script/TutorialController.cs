using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    public GameObject nextButton, previousButton;

    public GameObject[] ThePanel;

    int currentPanel;
    bool nextPanelMove;
    bool previousPanelMove;

    void Start()
    {
        currentPanel = 0;
        CheckButton();
    }

    
    void Update()
    {
        if (nextPanelMove)
        {
            if(ThePanel[currentPanel].transform.position.x > transform.position.x)
            {
                foreach (GameObject a in ThePanel)
                {
                    a.transform.position = new Vector2(a.transform.position.x - 1800 * Time.deltaTime, a.transform.position.y);
                }
            }
            else
            {
                nextPanelMove = false;
            }
        }
        else if (previousPanelMove && !nextPanelMove)
        {
            if (ThePanel[currentPanel].transform.position.x < transform.position.x)
            {
                foreach (GameObject a in ThePanel)
                {
                    a.transform.position = new Vector2(a.transform.position.x + 1800 * Time.deltaTime, a.transform.position.y);
                }
            }
            else
            {
                nextPanelMove = false;
            }
        }
    }

    public void NextPanel()
    {
        currentPanel++;
        nextPanelMove = true;
        CheckButton();
    }

    public void PerviousPanel()
    {
        currentPanel--;
        previousPanelMove = true;
        CheckButton();
    }

    public void CheckButton()
    {
        if (currentPanel == 7)
        {
            nextButton.SetActive(false);
        }
        else
        {
            nextButton.SetActive(true);
        }

        if (currentPanel == 0)
        {
            previousButton.SetActive(false);
        }
        else
        {
            previousButton.SetActive(true);
        }
    }
}
