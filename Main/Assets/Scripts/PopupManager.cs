using BaseClass;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum Loaibai { tuantu,vonglap,renhanh };
public class PopupManager : MonoBehaviour
{
    public static PopupManager instance = null;
    public Canvas canvas;
    public PanelFinish panel_Finish;
    public TextAsset manchoi;
    [HideInInspector] public PlayerController playerController;
    public GameObject menu;
    public ListMap listmap;
    public Loaibai loaibai;
    public Panel_DieuKhien userPlay;
    public Map currentLevel;
    public GameObject currentMap;
    public float timeRemaining;
    public List<GameObject> listCurrentTopic;
    public bool notificationIsOn;
    public Panel_DieuKhien currentDashboard;
    [SerializeField] private Notification notification;
    public static PopupManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new PopupManager();
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
    private void Start()
    {
        listmap = JsonUtility.FromJson<ListMap>(manchoi.text);
        Application.targetFrameRate = 300;
    }
    public void ShowNotification(GameObject canvas,string message)
    {
        if(!notificationIsOn)
        {
            Notification gb = Instantiate(notification, canvas.transform);
            gb.titel.text = message;
            notificationIsOn = true;
        }
    }
}
