using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PanelArenaEnd : MonoBehaviour
{
    [SerializeField] TMP_Text des;
    public int checkIsMine = 2;
    void Start()
    {
        if(checkIsMine == 2)
        {
            if (PopupManager.Instance.currentDashboard.myTime > PopupManager.Instance.currentDashboard.otherPlayerTime)
            {
                des.text = "Đối thủ đã dành chiến thắng với thời gian thi đấu ít hơn bạn!";
            }
            else
            {
                des.text = "Bạn đã dành chiến thắng với thời gian thi đấu ít hơn đối thủ!";
            }
        }
        else
        {
            if(checkIsMine==1)
            {
                des.text = "Bạn đã va vào tường. Đối thủ của bạn là người chiến thắng!";
            }
            else
            {
                des.text = "Đối thủ của bạn đã va vào tường. Bạn là người chiến thắng!";
            }
        }
    }
    public void Close_Click()
    {
        PopupManager.Instance.currentDashboard.close_Click();
    }
}
