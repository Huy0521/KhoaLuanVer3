using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using BaseClass;
using UnityEditor;

public class PanelFinish : MonoBehaviour
{
    [SerializeField] private Image titleSprite;
    [SerializeField] private Image star1;
    [SerializeField] private Image star2;
    [SerializeField] private Image star3;
    [SerializeField] private Sprite win;
    [SerializeField] private Sprite lose;
    [SerializeField] private TMP_Text txt1;
    [SerializeField] private Button btnContinue;
    [SerializeField] private Button btnBack;
    [SerializeField] private Button btnReplay;
    [SerializeField] private panel_hangmuc panelHangmuc;
    int numberStar = 3;
    private void Start()
    {
        btnContinue.onClick.AddListener(ContinuePlay_click);
        //btnBack.onClick.AddListener(Back_click);
        btnReplay.onClick.AddListener(Replay_click);
    }
    //Truyền dữ liệu vào PopUp kết thúc game
    public void configView(bool result)
    {
        txt1.text = "Hoàn thành màn chơi dưới " + PopupManager.Instance.currentLevel.time + " giây";
        if (result == true)
        {
            AudioManager.Instance.PlaySound(Sound.Win);
            titleSprite.sprite = win;
        }
        else
        {
            AudioManager.Instance.PlaySound(Sound.Lose);
            titleSprite.sprite = lose;
            star2.color = new Color(0.273f, 0.273f, 0.273f);
            numberStar = numberStar - 3;
        }
        if (PopupManager.Instance.currentLevel.time < PopupManager.Instance.timeRemaining)
        {
            star1.color = new Color(0.273f, 0.273f, 0.273f);
            numberStar = numberStar - 1;
        }
        Debug.Log("ssss: "+PopupManager.Instance.playerController.futurePosition.Count);
        if (PopupManager.Instance.currentLevel.sobuoc_an < PopupManager.Instance.playerController.futurePosition.Count)
        {
            star2.color = new Color(0.273f, 0.273f, 0.273f);
            numberStar = numberStar - 1;
        }
        switch (PopupManager.Instance.loaibai)
        {
            case Loaibai.tuantu:
                for (int i = 0; i < PopupManager.Instance.listmap.tuantu.Length; i++)
                {
                    if (PopupManager.Instance.currentLevel.level == PopupManager.Instance.listmap.tuantu[i].level)
                    {
                        PopupManager.Instance.listmap.tuantu[i].star = numberStar;
                        PopupManager.Instance.listmap.tuantu[i].playertime = (int)PopupManager.Instance.timeRemaining;
                    }
                }
                break;
            case Loaibai.vonglap:
                for (int i = 0; i < PopupManager.Instance.listmap.vonglap.Length; i++)
                {
                    if (PopupManager.Instance.currentLevel.level == PopupManager.Instance.listmap.vonglap[i].level)
                    {
                        PopupManager.Instance.listmap.vonglap[i].star = numberStar;
                        PopupManager.Instance.listmap.vonglap[i].playertime = (int)PopupManager.Instance.timeRemaining;
                    }
                }
                break;
            case Loaibai.renhanh:
                for (int i = 0; i < PopupManager.Instance.listmap.renhanh.Length; i++)
                {
                    if (PopupManager.Instance.currentLevel.level == PopupManager.Instance.listmap.renhanh[i].level)
                    {
                        PopupManager.Instance.listmap.renhanh[i].star = numberStar;
                        PopupManager.Instance.listmap.renhanh[i].playertime = (int)PopupManager.Instance.timeRemaining;
                    }
                }
                break;
        }
        string tojson = JsonUtility.ToJson(PopupManager.Instance.listmap);
        SaveFile(tojson);
    }
    //Lưu file 
    public void SaveFile(string data)
    {
        string destination = "Assets/Resources/Json/Manchoi.json";
        StreamWriter write = new StreamWriter(destination);
        write.Write(data);
        write.Close();
        Resources.Load(destination);//tải lại file trong resource để cập nhật giá trị mới

    }
    //Chơi lại bàn chơi
    private void Replay_click()
    {
        AudioManager.Instance.PlaySound(Sound.Button);
        Destroy(PopupManager.Instance.currentMap);
        Destroy(PopupManager.Instance.currentDashboard.gameObject);
        Destroy(gameObject);
        PopupManager.Instance.currentMap = Instantiate(PopupManager.Instance.mapToReload);
        PopupManager.Instance.currentDashboard = Instantiate(PopupManager.Instance.userPlay, PopupManager.Instance.canvas.transform);
        GameController.Instance.ResetGameController();

    }
    //Chơi bàn chơi tiếp theo
    private void ContinuePlay_click()
    {
        AudioManager.Instance.PlaySound(Sound.Button);
        int level = int.Parse(PopupManager.Instance.currentLevel.level);
        Destroy(PopupManager.Instance.currentMap);
        Destroy(PopupManager.Instance.currentDashboard.gameObject);
        if (level < 5)
        {
            //Truyền dữ liệu thông tin màn chơi
            switch (PopupManager.Instance.loaibai)
            {
                case Loaibai.tuantu:
                    PopupManager.Instance.currentLevel = PopupManager.Instance.listmap.tuantu[level];
                    break;
                case Loaibai.vonglap:
                    PopupManager.Instance.currentLevel = PopupManager.Instance.listmap.vonglap[level];
                    break;
                case Loaibai.renhanh:
                    PopupManager.Instance.currentLevel = PopupManager.Instance.listmap.renhanh[level];
                    break;
            }
            //Gán các giá trị của màn chơi tiếp theo
            PopupManager.Instance.currentMap = Instantiate(PopupManager.Instance.listCurrentTopic[level]);
            PopupManager.Instance.mapToReload = PopupManager.Instance.listCurrentTopic[level];
            PopupManager.Instance.currentDashboard = Instantiate(PopupManager.Instance.userPlay, PopupManager.Instance.canvas.transform);
        }
        else
        {
            Destroy(PopupManager.Instance.currentMap);
            Destroy(PopupManager.Instance.currentDashboard.gameObject);
            Instantiate(panelHangmuc, PopupManager.Instance.canvas.transform);
        }

        Destroy(gameObject);

    }
    private void Back_click()
    {

    }
}
