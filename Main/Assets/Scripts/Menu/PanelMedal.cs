using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PanelMedal : MonoBehaviour
{
    [SerializeField] Button btn_Back;
    [SerializeField] List<ItemMedal> listMedal;
   
    void Start()
    {
        CheckMedal();
        btn_Back.onClick.AddListener(() =>
        {
            PopupManager.Instance.menu.SetActive(true);
            Destroy(gameObject);
        });
    }
    void CheckMedal()
    {
        int playstar = 0;
        int tongstar = 0;
        int time = 0;
        int playertime = 0;
        for (int i = 0; i < PopupManager.Instance.listmap.tuantu.Length; i++)
        {
            playstar = playstar + PopupManager.Instance.listmap.tuantu[i].star;
            tongstar = tongstar + 3;
            time = time + PopupManager.Instance.listmap.tuantu[i].time;
            playertime = playertime + PopupManager.Instance.listmap.tuantu[i].playertime;
        }
        for (int i = 0; i < PopupManager.Instance.listmap.vonglap.Length; i++)
        {
            playstar = playstar + PopupManager.Instance.listmap.vonglap[i].star;
            tongstar = tongstar + 3;
            time = time + PopupManager.Instance.listmap.vonglap[i].time;
            playertime = playertime + PopupManager.Instance.listmap.vonglap[i].playertime;
        }
        for (int i = 0; i < PopupManager.Instance.listmap.renhanh.Length; i++)
        {
            playstar = playstar + PopupManager.Instance.listmap.renhanh[i].star;
            tongstar = tongstar + 3;
            time = time + PopupManager.Instance.listmap.renhanh[i].time;
            playertime = playertime + PopupManager.Instance.listmap.renhanh[i].playertime;
        }
        if(playstar==tongstar)
        {
            listMedal[0].configView("Phi hành gia hoàn hảo", "Ghi nhận thành tích hoàn hảo của bạn khi vượt qua tất cả các mà chơi với số sao tuyệt đối.");
        }
        if(time==playertime)
        {
            listMedal[2].configView("Phi hành gia siêu tốc","Ghi nhận tư duy thần tốc của bạn khi hoàn thành bài chơi ít hơn thời gian quy định");
        }
    }
}
