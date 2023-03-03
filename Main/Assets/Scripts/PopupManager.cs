using BaseClass;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Loaibai { tuantu, vonglap, renhanh, phuongthuc };
public enum Character { astronaut, cat };
public class PopupManager : MonoBehaviour
{
    [HideInInspector] public static PopupManager instance = null;
    [Header("GameObject")]
    public GameObject canvas;//Canvas tổng để Instantiate các GameObject vào
    public GameObject currentMap;//Map hiện tại đang chơi
    public List<GameObject> listCurrentTopic;//Toàn bộ màn chơi mà chủ đề hiện tại đang có
    [HideInInspector] public GameObject menu;//Màn menu lúc mới vào game
    [HideInInspector] public GameObject mapToReload;//Lưu map hiện tại để reload khi thua
    [Header("Scripts")]
    public PanelFinish panel_Finish;//Popup kết thúc màn chơi 
    public Panel_DieuKhien userPlay;//Bảng điều khiển để Instantiate
    [HideInInspector] public Panel_DieuKhien currentDashboard;//Bảng điều khiển hiện tại trên màn hình
    [HideInInspector] public PlayerController playerController;//Lưu nhân vật hiện tại trên Sceen 
    [SerializeField] private Notification notification;
    [Header("Bool")]
    [HideInInspector] public bool notificationIsOn;//Check hiện đang có thông báo đang được hiển thị ko
    [HideInInspector] public bool goFromCutScene;//Check lần đầu chơi đi từ CutScene ra
    [Header("Other")]
    public TextAsset manchoi;//File Json chứa dữ liệu của các màn chơi
    [HideInInspector] public ListMap listmap;//Chứa dữ liệu lấy ra từ file Json
    public Loaibai loaibai;//Lưu chủ đề được chọn
    public Map currentLevel;//Thông tin của màn chơi hiện tại
    [HideInInspector] public Character character;//Check nhân vật được người chơi chọn
    public float timeRemaining;


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
        //Ko hủy khi tải các scene, GameObject luôn tồn tại
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        //Lưu thông tin màn chơi từ Json
        listmap = JsonUtility.FromJson<ListMap>(manchoi.text);
        //Chỉ định FPS tốt nhất game có thể đạt được
        Application.targetFrameRate = 300;
    }
    //Hàm hiển thị thông báo
    public void ShowNotification(GameObject canvas, string message,float time, Sprite sprite)
    {
        if (!notificationIsOn)
        {
            Notification gb = Instantiate(notification, canvas.transform);
            gb.titel.text = message;
            notificationIsOn = true;
            gb.SetNotiTime(time,sprite);
        }
    }
}
