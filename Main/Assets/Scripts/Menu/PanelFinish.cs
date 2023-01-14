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
    [SerializeField] Image titleSprite;
    [SerializeField] Image star1;
    [SerializeField] Image star2;
    [SerializeField] Image star3;
    [SerializeField] Sprite win;
    [SerializeField] Sprite lose;
    [SerializeField] TMP_Text txt1;
    int numberStar = 3;
    public void configView(bool result)
    {
        txt1.text = "Hoàn thành màn chơi dưới " + PopupManager.Instance.currentLevel.time + " giây";
        if (result==true)
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
        if(PopupManager.Instance.currentLevel.time<PopupManager.Instance.timeRemaining)
        {
            star1.color = new Color(0.273f, 0.273f, 0.273f);
            numberStar = numberStar - 1;
        }
        if (PopupManager.Instance.currentLevel.sobuoc_an < PopupManager.Instance.playerController.futurePosition.Count)
        {
            star2.color = new Color(0.273f, 0.273f, 0.273f);
            numberStar = numberStar - 1;
        }
        if (PopupManager.Instance.loaibai == Loaibai.tuantu)
        {
            for (int i = 0; i < PopupManager.Instance.listmap.tuantu.Length; i++)
            {
                if (PopupManager.Instance.currentLevel.level == PopupManager.Instance.listmap.tuantu[i].level)
                {
                    PopupManager.Instance.listmap.tuantu[i].star = numberStar;
                    PopupManager.Instance.listmap.tuantu[i].playertime = (int)PopupManager.Instance.timeRemaining;
                }
            } 
            string tojson = JsonUtility.ToJson(PopupManager.Instance.listmap);
            SaveFile(tojson);
        }
    }
    public void SaveFile(string data)
    {
        string destination = "Assets/Resources/Json/Manchoi.json";
        StreamWriter write = new StreamWriter(destination);
        write.Write(data);
        write.Close();
        Resources.Load(destination);
    }
}
