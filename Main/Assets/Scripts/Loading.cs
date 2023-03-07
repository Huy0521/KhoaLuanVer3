using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Loading : MonoBehaviour
{
    [SerializeField] Slider loadingSlider;
    [SerializeField] GameObject gb;
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
