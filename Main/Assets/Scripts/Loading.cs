using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Loading : MonoBehaviour
{
    [SerializeField] Slider loadingSlider;
    [SerializeField] GameObject gb;
    private void Start()
    {
        if(PopupManager.Instance.goFromCutScene==true)
        {
            gb.SetActive(true);
            gameObject.SetActive(false);
        }
        else
        {
            gb.SetActive(false);
            gameObject.SetActive(true);
        }
    }

    void Update()
    {
        if(loadingSlider.value<100)
        {
            loadingSlider.value = loadingSlider.value + 22*Time.deltaTime;
        }
        else if(loadingSlider.value==100)
        {
            gb.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
