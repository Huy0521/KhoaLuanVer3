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
    [SerializeField] private GameObject firstLevel;
    [SerializeField] private List<GameObject> listTuantu;
    private void Start()
    {
        Menu = gameObject.transform.GetChild(0).gameObject;
        PopupManager.Instance.canvas = gameObject.transform.parent.gameObject;
        PopupManager.Instance.menu = Menu;
        btn_Exit.onClick.AddListener(exit_Click);
        btn_NewGame.onClick.AddListener(newgame_Click); 
        btn_Medal.onClick.AddListener(medal_Click);
        btn_Setting.onClick.AddListener(setting_Click);
        AudioManager.Instance.PlaySound(Sound.Start);
        if (PopupManager.Instance.goFromCutScene)
        {
            PopupManager.Instance.currentLevel = PopupManager.Instance.listmap.tuantu[0];
            PopupManager.Instance.listCurrentTopic = listTuantu;
            PopupManager.Instance.currentMap = Instantiate(firstLevel);
            PopupManager.Instance.currentDashboard = Instantiate(PopupManager.Instance.userPlay, PopupManager.Instance.canvas.transform);
        }
        
    }
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
    private void medal_Click()
    {
        AudioManager.Instance.PlaySound(Sound.Button);
        Instantiate(medalPanel, PopupManager.Instance.canvas.transform);
        Menu.SetActive(false);
    }    
    private void exit_Click()
    {
        AudioManager.Instance.PlaySound(Sound.Button);
        Application.Quit();
    }
   private void setting_Click()
    {
        panelSetting.gameObject.SetActive(true);
    }

}
