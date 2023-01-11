using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum SpecialBtn { loop, ifElse, none, doIf }
public class GameController : MonoBehaviour
{
    public static GameController instance = null;
    public List<GameObject> listButton;
    public List<string> listBtnMain;
    public List<GameObject> listBtnFor;
    public List<GameObject> listBtnIf;
    public List<string> listShadedIf;
    public List<GameObject> listBtndoIf;
    public bool run = false;
    public bool shadedRun = false;
    public SpecialBtn chooseBtn = SpecialBtn.none;
    public int loopNumber = 0;
    public static GameController Instance
    {
        get
        {
            if(instance==null)
            {
                instance = new GameController();
            }
            return instance;
        }
    }
    private void Awake()
    {
        if(instance!=null&&instance!=this)
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
}
