using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
public class menu_Controller : MonoBehaviourPunCallbacks
{
    [SerializeField] private selectLevel_Controller popup_SelectLV;
    [SerializeField] private panel_hangmuc panelHangmuc;
    [SerializeField] private PanelSetting panelSetting;
    [SerializeField] private Button btn_NewGame;
    [SerializeField] private Button btn_Medal;
    [SerializeField] private Button btn_Exit;
    [SerializeField] private Button btn_Setting;
    [SerializeField] private Button btn_ArenaZone; 
    [SerializeField] private GameObject medalPanel;
    [SerializeField] private GameObject Menu;
    [SerializeField] private GameObject firstLevel;
    [SerializeField] private List<GameObject> listTuantu;//List cơ bản khi đi từ Cutscene vào
    [SerializeField] private GameObject panelLoading;
    [SerializeField] private GameObject panelIpName;
    [SerializeField] private Button btn_Fb;
    [SerializeField] private Button btn_Gm;
    private void Start()
    {
        PopupManager.Instance.isArena = false;
        LeanTween.moveLocalX(btn_NewGame.gameObject, 10, 0.3f).setEaseInOutBounce().setOnComplete(() => { LeanTween.moveLocalX(btn_Setting.gameObject, 10, 0.3f).setEaseInOutBounce().setOnComplete(() => { LeanTween.moveLocalX(btn_Medal.gameObject, 10, 0.3f).setEaseInOutBounce().setOnComplete(()=> { LeanTween.moveLocalX(btn_ArenaZone.gameObject, 10, 0.3f).setEaseInOutBounce().setOnComplete(()=> { LeanTween.moveLocalX(btn_Exit.gameObject, 10, 0.3f).setEaseInOutBounce(); }); }); });});
        Menu = gameObject.transform.GetChild(0).gameObject;
        PopupManager.Instance.canvas = gameObject.transform.parent.gameObject;
        PopupManager.Instance.menu = Menu;
        SetUp();
        AudioManager.Instance.PlaySound(Sound.Start);
        //Check đi từ cut scene vào
        if (PopupManager.Instance.goFromCutScene==true)
        {
            PopupManager.Instance.currentLevel = PopupManager.Instance.listmap.tuantu[0];
            PopupManager.Instance.listCurrentTopic = listTuantu;
            PopupManager.Instance.currentMap = Instantiate(firstLevel);
            PopupManager.Instance.currentDashboard = Instantiate(PopupManager.Instance.userPlay, PopupManager.Instance.canvas.transform);
            PopupManager.Instance.mapToReload = firstLevel;
            panelLoading.SetActive(false);
        }

    }
    private void SetUp()
    {
        btn_Exit.onClick.AddListener(exit_Click);
        //btn_NewGame.GetComponent<CustomButton>().Click = newgame_Click;
        btn_NewGame.onClick.AddListener(newgame_Click);
        btn_Medal.onClick.AddListener(medal_Click);
        btn_Setting.onClick.AddListener(setting_Click);
        btn_ArenaZone.onClick.AddListener(Arena_Click);
        btn_Fb.onClick.AddListener(Fb_Click);
        btn_Gm.onClick.AddListener(Gmail_Click);
    }
    //Vào chọn chủ đề
    private void newgame_Click()
    {
        AudioManager.Instance.PlaySound(Sound.Button);
        if (PopupManager.Instance.listmap.tuantu[0].star > 0)
        {
            Instantiate(panelHangmuc, PopupManager.Instance.canvas.transform);
            Menu.SetActive(false);
        }
        else
        {
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
        AudioManager.Instance.PlaySound(Sound.Button);
        panelSetting.gameObject.SetActive(true);
    }
    private void Arena_Click()
    {
        AudioManager.Instance.PlaySound(Sound.Button);
        panelIpName.SetActive(true);
    }
    private void Fb_Click()
    {
        Application.OpenURL("");
    }
    private void Gmail_Click()
    {
        string t = "mailto:thankhuya4@gmail.com?subject=PhanhoiveGame";
        Application.OpenURL(t);
    }
}
