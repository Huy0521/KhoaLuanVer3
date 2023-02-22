using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum SpecialBtn { loop, ifElse, none, doIf }
public class GameController : MonoBehaviour
{
    public static GameController instance = null;
    public List<GameObject> listButton;
    public List<string> listBtnMain;        
    public List<GameObject> listBtnIf;
    public List<string> listShadedIf;
    public List<GameObject> listBtndoIf;
    public List<GameObject> listScreenAdd;
    public bool run = false;
    public bool shadedRun = false;
    public SpecialBtn chooseBtn = SpecialBtn.none;
    public static GameController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameController();
            }
            return instance;
        }
    }
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }
    public void ResetGameController()
    {
        listButton.Clear();
        listBtnMain.Clear();
        listBtnIf.Clear();
        listShadedIf.Clear();
        listBtndoIf.Clear();
        run = false;
        shadedRun = false;
        chooseBtn = SpecialBtn.none;
    }
}
