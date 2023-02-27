using BaseClass;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PanelMedal : MonoBehaviour
{
    [SerializeField] Button btn_Back;
    [SerializeField] List<ItemMedal> listMedal;
    [SerializeField] private List<Map> listAllMap;
    private void Start()
    {
        for (int i = 0; i < PopupManager.Instance.listmap.tuantu.Length; i++)
        {
            listAllMap.Add(PopupManager.Instance.listmap.tuantu[i]);
        }
        for (int i = 0; i < PopupManager.Instance.listmap.vonglap.Length; i++)
        {
            listAllMap.Add(PopupManager.Instance.listmap.vonglap[i]);
        }
        for (int i = 0; i < PopupManager.Instance.listmap.renhanh.Length; i++)
        {
            listAllMap.Add(PopupManager.Instance.listmap.renhanh[i]);
        }
        CheckMedal();
        btn_Back.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySound(Sound.Button);
            PopupManager.Instance.menu.SetActive(true);
            Destroy(gameObject);
        });
    }
    private void CheckMedal()
    {
        int playstar = 0;
        int tongstar = 0;
        int time = 0;
        int playertime = 0;
        int check2sao = 0;
        for (int i = 0; i < listAllMap.Count; i++)
        {
            playstar = playstar + listAllMap[i].star;
            tongstar = tongstar + 3;
            time = time + listAllMap[i].time;
            playertime = playertime + listAllMap[i].playertime;
            if (listAllMap[i].star >= 2)
            {
                check2sao++;
            }
        }
        if (playstar == tongstar)
        {
            listMedal[0].configView("Phi hành gia hoàn hảo", "Ghi nhận thành tích hoàn hảo của bạn khi vượt qua tất cả các mà chơi với số sao tuyệt đối.");
        }
        if (time == playertime)
        {
            listMedal[2].configView("Phi hành gia siêu tốc", "Ghi nhận tư duy thần tốc của bạn khi hoàn thành bài chơi ít hơn thời gian quy định.");
        }
        if (check2sao == listAllMap.Count)
        {
            listMedal[1].configView("Phi hành cao cấp", "Ghi nhận sự thông hiểu của bạn đối với các thuật toán được đưa ra.");
        }
        if (playstar > 0)
        {
            listMedal[9].configView("Mọi câu truyện đều có khởi đầu", "Đánh dấu lần đầu tiên bạn hoàn thành 1 bài chơi.");
        }
    }
}
