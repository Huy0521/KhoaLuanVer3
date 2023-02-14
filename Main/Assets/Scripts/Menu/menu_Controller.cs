using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class menu_Controller : MonoBehaviour
{
    [SerializeField] private selectLevel_Controller popup_SelectLV;
    [SerializeField] private panel_hangmuc panelHangmuc;
    [SerializeField] private PanelSetting panelSetting;
    [SerializeField] private GameObject medalPanel;
    [SerializeField] private Button btn_NewGame;
    [SerializeField] private Button btn_Medal;
    [SerializeField] private Button btn_Exit;
    [SerializeField] private Button btn_Setting;
    [SerializeField] private GameObject Menu;
    void Start()
    {
        btn_Exit.onClick.AddListener(exit_Click);
        btn_NewGame.onClick.AddListener(newgame_Click); 
        btn_Medal.onClick.AddListener(medal_Click);
        btn_Setting.onClick.AddListener(setting_Click);
        AudioManager.Instance.PlaySound(Sound.Start);
        if (PopupManager.Instance.goFromCutScene)
        {
          /*  PopupManager.Instance.currentLevel = PopupManager.Instance.listmap.tuantu[0];
            PopupManager.Instance.currentMap = Instantiate(level);
            Destroy(panelSelectlevel);
            PopupManager.Instance.currentDashboard = Instantiate(PopupManager.Instance.userPlay, PopupManager.Instance.canvas.transform);*/
        }
        
    }
    void newgame_Click()
    {
        /*Instantiate(panelHangmuc.gameObject, PopupManager.Instance.canvas.transform);
        Menu.SetActive(false);*/
        AudioManager.Instance.PlaySound(Sound.Button); 
        SceneManager.LoadScene("CutScene");
    }
    void medal_Click()
    {
        AudioManager.Instance.PlaySound(Sound.Button);
        Instantiate(medalPanel, PopupManager.Instance.canvas.transform);
        Menu.SetActive(false);
    }    
    void exit_Click()
    {
        AudioManager.Instance.PlaySound(Sound.Button);
        Application.Quit();
    }
    void setting_Click()
    {
        panelSetting.gameObject.SetActive(true);
    }

}
