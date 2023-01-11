using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfScript : MonoBehaviour
{
    [SerializeField] private GameObject Zone3;
    void Start()
    {
        
    }
    public void if_click()
    {
        if(GameController.Instance.chooseBtn==SpecialBtn.none)
        {
            Zone3.SetActive(true);
            GameController.Instance.chooseBtn = SpecialBtn.ifElse;
        }
        else 
        {
            Zone3.SetActive(false);
            GameController.Instance.chooseBtn = SpecialBtn.none;
        }
    }
}
