using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum SpecialBtn { loop, ifElse, none, doIf }
public class GameController : MonoBehaviour
{
    public static GameController instance = null;
    public List<GameObject> listButton = new List<GameObject>();
    public List<GameObject> listBtnMain = new List<GameObject>();
    public List<GameObject> listScreenAdd = new List<GameObject>();
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
        for (int i=0;i<listScreenAdd.Count;i++)
        {
            Destroy(listScreenAdd[i].GetComponent<LoopScreen>().currentBtn);
            Destroy(listScreenAdd[i]);
        }
        listScreenAdd.Clear();

        run = false;
        shadedRun = false;
        chooseBtn = SpecialBtn.none;
    }
}
