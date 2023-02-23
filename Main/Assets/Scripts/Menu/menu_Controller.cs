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
    [SerializeField] private Button btn_NewGame;
    [SerializeField] private Button btn_Medal;
    [SerializeField] private Button btn_Exit;
    [SerializeField] private Button btn_Setting;
    [SerializeField] private GameObject medalPanel;
    [SerializeField] private GameObject Menu;
    [SerializeField] private GameObject firstLevel;
    [SerializeField] private List<GameObject> listTuantu;//List cơ bản khi đi từ Cutscene vào
    [SerializeField] private GameObject panelLoading;
    private void Start()
    {
        Menu = gameObject.transform.GetChild(0).gameObject;
        PopupManager.Instance.canvas = gameObject.transform.parent.gameObject;
        PopupManager.Instance.menu = Menu;
        SetUp();
        AudioManager.Instance.PlaySound(Sound.Start);
        //Check đi từ cut scene vào
        if (PopupManager.Instance.goFromCutScene)
        {
            PopupManager.Instance.currentLevel = PopupManager.Instance.listmap.tuantu[0];
            PopupManager.Instance.listCurrentTopic = listTuantu;
            PopupManager.Instance.currentMap = Instantiate(firstLevel);
            PopupManager.Instance.currentDashboard = Instantiate(PopupManager.Instance.userPlay, PopupManager.Instance.canvas.transform);
            panelLoading.SetActive(false);
        }
        
    }
    private void SetUp()
    {
        btn_Exit.onClick.AddListener(exit_Click);
        btn_NewGame.onClick.AddListener(newgame_Click);
        btn_Medal.onClick.AddListener(medal_Click);
        btn_Setting.onClick.AddListener(setting_Click);
    }    
    //Vào chọn chủ đề
    private void newgame_Click()
    {
        if (PopupManager.Instance.listmap.tuantu[0].star > 0)
        {
            Instantiate(panelHangmuc, PopupManager.Instance.canvas.transform);
            Menu.SetActive(false);
        }
        else
        {
            AudioManager.Instance.PlaySound(Sound.Button);
            SceneManager.LoadScene("CutScene");
        }
    }
    //Mở panel thành tích
    private void medal_Click()
    {
        AudioManager.Instance.PlaySound(Sound.Button);
        Instantiate(medalPanel, PopupManager.Instance.canvas.transform);
        Menu.SetActive(false);
    }
    //Thoát game
    private void exit_Click()
    {
        AudioManager.Instance.PlaySound(Sound.Button);
        Application.Quit();
    }
    //Mở panel cài đặt
   private void setting_Click()
    {
        panelSetting.gameObject.SetActive(true);
    }

}
