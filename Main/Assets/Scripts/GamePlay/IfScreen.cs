using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfScreen : MonoBehaviour
{
    [SerializeField] private Panel_DieuKhien panelDieukhien;
    public List<GameObject> listpostionIf;
    public List<GameObject> listpostiondoIf;
    public List<GameObject> listBtnIf;
    public List<GameObject> listBtndoIf;
    public GameObject doZone;
    public GameObject ifZone;
    public int vitriIf = 0;
    public int vitridoIf = 0;
    public int posInLooplist;
    public GameObject currentBtn;
    private void OnEnable()
    {
        GameController.Instance.chooseBtn = SpecialBtn.ifElse;
        panelDieukhien.btn_No.gameObject.SetActive(true);
    }
    private void OnDisable()
    {
        GameController.Instance.chooseBtn = SpecialBtn.none;
        panelDieukhien.btn_No.gameObject.SetActive(false);
    }
    public void ShowIfScreen()
    {
        for (int i = 0; i < GameController.Instance.listScreenAdd.Count; i++)
        {
            GameController.Instance.listScreenAdd[i].SetActive(false);
        }
        gameObject.SetActive(true);
        panelDieukhien.posOfList = posInLooplist;

    }
}
