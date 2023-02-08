﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class panel_hangmuc : MonoBehaviour
{
    [SerializeField] private Button btn_Vonglap;
    [SerializeField] private Button btn_Renhanh;
    [SerializeField] private Button btn_Tuantu;
    [SerializeField] private Image vonglapLock;
    [SerializeField] private Image renhanhLock;
    [SerializeField] private Image tuantuLock;
    [SerializeField] private Button btn_Back;
    [SerializeField] private selectLevel_Controller selectLevel; 
    int tuantuStar;
    int renhanhStar;
    int vonglapStar;
    void Start()
    {
      
        for(int i=0;i<PopupManager.Instance.listmap.tuantu.Length;i++)
        {
          tuantuStar = tuantuStar + PopupManager.Instance.listmap.tuantu[i].star;
        }
        for (int i = 0; i < PopupManager.Instance.listmap.vonglap.Length; i++)
        {
            vonglapStar = vonglapStar + PopupManager.Instance.listmap.vonglap[i].star;
        }
        for (int i = 0; i < PopupManager.Instance.listmap.renhanh.Length; i++)
        {
            renhanhStar = renhanhStar + PopupManager.Instance.listmap.renhanh[i].star;
        }
        if(vonglapStar<12)
        {
            btn_Renhanh.image.color = new Color(0.2735849f, 0.2735849f, 0.2735849f);
            renhanhLock.gameObject.SetActive(true);
        }
        else
        {
            btn_Renhanh.image.color = new Color(1f, 1f,1f);
            renhanhLock.gameObject.SetActive(false);
        }
        if (tuantuStar < 12)
        {
            btn_Vonglap.image.color = new Color(0.2735849f, 0.2735849f, 0.2735849f);
            vonglapLock.gameObject.SetActive(true);
        }
        else
        {
            btn_Vonglap.image.color = new Color(1, 1, 1);
            renhanhLock.gameObject.SetActive(false);
        }
        btn_Vonglap.gameObject.LeanMoveLocal(new Vector2(btn_Vonglap.transform.localPosition.x+ Random.Range(-10, 10), btn_Vonglap.transform.localPosition.y + 30), Random.Range(1f, 1.6f)).setLoopPingPong();
        btn_Renhanh.gameObject.LeanMoveLocal(new Vector2(btn_Renhanh.transform.localPosition.x+ Random.Range(-10, 10), btn_Renhanh.transform.localPosition.y + 30), Random.Range(1f, 1.6f)).setLoopPingPong();
        btn_Tuantu.gameObject.LeanMoveLocal(new Vector2(btn_Tuantu.transform.localPosition.x+ Random.Range(-10, 10), btn_Tuantu.transform.localPosition.y + 30), Random.Range(1f, 1.6f)).setLoopPingPong();
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
        Instantiate(selectLevel,PopupManager.Instance.canvas.transform);
        Destroy(gameObject);
    }
    void vonglap_Click()
    {
        if(tuantuStar<12)
        {
            PopupManager.Instance.ShowNotification(gameObject, "Hãy hoàn thành thử thành ở hành tinh trước đó để mở khóa!");       
        }
        else
        {
            AudioManager.Instance.PlaySound(Sound.Button);
            PopupManager.Instance.loaibai = Loaibai.vonglap;
            Instantiate(selectLevel, PopupManager.Instance.canvas.transform);
            Destroy(gameObject);
        }
  
    }
    void renhanh_Click()
    {
        if(vonglapStar<12)
        {
            PopupManager.Instance.ShowNotification(gameObject, "Hãy hoàn thành thử thành ở hành tinh trước đó để mở khóa!");
        }
        else
        {
            AudioManager.Instance.PlaySound(Sound.Button);
            PopupManager.Instance.loaibai = Loaibai.renhanh;
            Instantiate(selectLevel, PopupManager.Instance.canvas.transform);
            Destroy(gameObject);
        }
    }
}
