using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Loading : MonoBehaviour
{
    [SerializeField] Slider loadingSlider;
    void Update()
    {
        if(loadingSlider.value<100)
        {
            loadingSlider.value = loadingSlider.value + 0.2f;
        }
        else if(loadingSlider.value==100)
        {
            gameObject.SetActive(false);
        }
      

    }
}
