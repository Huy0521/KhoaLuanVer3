using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class panel_hangmuc : MonoBehaviour
{
    [SerializeField] private Button btn_Vonglap;
    [SerializeField] private Button btn_Renhanh;
    [SerializeField] private Button btn_Tuantu;
    [SerializeField] private Button btn_Back;
    [SerializeField] private selectLevel_Controller selectLevel;
    void Start()
    {
        btn_Tuantu.onClick.AddListener(tuantu_Click);
        btn_Vonglap.onClick.AddListener(vonglap_Click);
        btn_Renhanh.onClick.AddListener(renhanh_Click);
        btn_Back.onClick.AddListener(()=> 
        {
            AudioManager.Instance.PlaySound(Sound.Button);
            PopupManager.Instance.menu.SetActive(true);
            Destroy(gameObject);
        });
    }
    void tuantu_Click()
    {
        AudioManager.Instance.PlaySound(Sound.Button);
        PopupManager.Instance.loaibai = Loaibai.tuantu;
        Instantiate(selectLevel.gameObject,PopupManager.Instance.canvas.transform);
        gameObject.SetActive(false);
    }
    void vonglap_Click()
    {
        AudioManager.Instance.PlaySound(Sound.Button);
        PopupManager.Instance.loaibai = Loaibai.vonglap;
        Instantiate(selectLevel.gameObject, PopupManager.Instance.canvas.transform);
        gameObject.SetActive(false);
    }
    void renhanh_Click()
    {
        AudioManager.Instance.PlaySound(Sound.Button);
        PopupManager.Instance.loaibai = Loaibai.renhanh;
        Instantiate(selectLevel.gameObject, PopupManager.Instance.canvas.transform);
        gameObject.SetActive(false);
       
    }
}
