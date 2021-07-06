using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnsiklopediaController : MonoBehaviour
{
    public GameObject[] deskribsiTumbuhan;

    int currentArray = 0;

    public void DeskribsiSwitch(int a)
    {
        if (a != currentArray)
        {
            deskribsiTumbuhan[currentArray].SetActive(false);
            deskribsiTumbuhan[a].SetActive(true);
            currentArray = a;
        }
    }
}
