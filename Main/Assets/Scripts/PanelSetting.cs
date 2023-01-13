using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PanelSetting : MonoBehaviour
{
    [SerializeField] private Slider sliderMusic;
    [SerializeField] private Slider sliderEffect;
    void Start()
    {
        sliderMusic.onValueChanged.AddListener(val => AudioManager.Instance.ChangeMusicVolume(val));
        sliderEffect.onValueChanged.AddListener(val => AudioManager.Instance.ChangeEffectVolume(val));
    }
    public void Close_click()
    {
        gameObject.SetActive(false);
    }
}
