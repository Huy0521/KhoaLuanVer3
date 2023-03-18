using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PanelArenaEnd : MonoBehaviour
{
    [SerializeField] TMP_Text des;
    void Start()
    {
        if(PopupManager.Instance.currentDashboard.myTime > PopupManager.Instance.currentDashboard.otherPlayerTime)
        {
            des.text = "Đối thủ đã dành chiến thắng với thời gian thi đấu ít hơn bạn!";
        }
        else 
        {
            des.text = "Bạn đã dành chiến thắng với thời gian thi đấu ít hơn đối thủ!";
        }
        
    }
    public void Close_Click()
    {
        PopupManager.Instance.currentDashboard.close_Click();
    }
}
