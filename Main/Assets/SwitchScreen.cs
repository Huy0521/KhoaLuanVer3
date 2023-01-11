using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchScreen : MonoBehaviour
{
    public List<GameObject> listScreen;

    void Start()
    {
        
    }
    public void ShowMainScreen()
    {
        for(int i=0;i<listScreen.Count;i++)
        {
            listScreen[i].SetActive(false);
            if (listScreen[i].name.Equals("MainScreen"))
            {
                listScreen[i].SetActive(true);
            }
        }
    }
    public void ShowIfScreen()
    {
        for (int i = 0; i < listScreen.Count; i++)
        {
            listScreen[i].SetActive(false);
            if (listScreen[i].name.Equals("IfScreen"))
            {
                listScreen[i].SetActive(true);
            }
        }
    }
    public void ShowLoopScreen()
    {
        for (int i = 0; i < listScreen.Count; i++)
        {
            listScreen[i].SetActive(false);
            if (listScreen[i].name.Equals("LoopScreen(Clone)"))
            {
                listScreen[i].SetActive(true);
            }
        }
    }
}
