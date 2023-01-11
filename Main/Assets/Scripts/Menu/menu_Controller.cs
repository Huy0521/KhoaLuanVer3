using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class menu_Controller : MonoBehaviour
{
    [SerializeField] private selectLevel_Controller popup_SelectLV;
    [SerializeField] private panel_hangmuc panelHangmuc;
    [SerializeField] private GameObject medalPanel;
    [SerializeField] private Button btn_NewGame;
    [SerializeField] private Button btn_Medal;
    [SerializeField] private Button btn_Exit;
    [SerializeField] private GameObject Menu;
    void Start()
    {
        btn_Exit.onClick.AddListener(exit_Click);
        btn_NewGame.onClick.AddListener(newgame_Click); 
        btn_Medal.onClick.AddListener(medal_Click);
        AudioManager.Instance.PlaySound(Sound.Start);
    }
    void newgame_Click()
    {
        Instantiate(panelHangmuc.gameObject, PopupManager.Instance.canvas.transform);
        Menu.SetActive(false);
        AudioManager.Instance.PlaySound(Sound.Button);
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

}
