using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using BaseClass;
public class Panel_DieuKhien : MonoBehaviour
{
    public List<GameObject> Listposition;
    public List<GameObject> ListpostionLoop;
    public List<GameObject> ListpostionIf;
    public List<GameObject> ListpostiondoIf;
    private int vitri;
    private int vitriLoop;
    private int vitriIf;
    private int vitridoIf;
    [SerializeField] private Button btn_Left;
    [SerializeField] private Button btn_Right;
    [SerializeField] private Button btn_Up;
    [SerializeField] private Button btn_Down;
    [SerializeField] private Button btn_Loop;
    [SerializeField] private Button btn_If;
    [SerializeField] private Button btn_Yes;
    [SerializeField] private Button btn_No;
    [SerializeField] private Button btn_Play;
    [SerializeField] private Button btn_Delete;
    [SerializeField] private Button btn_Close;
    [SerializeField] private GameObject Zone;
    [SerializeField] private GameObject Zone2;
    [SerializeField] private GameObject ifZone;
    [SerializeField] private GameObject doZone;
    [SerializeField] private GameObject postisionForbtn;
    [SerializeField] private RectTransform btnZone;
    [SerializeField] private SwitchScreen switchScreen;
    [SerializeField] private Button BtnloopScreen;
    [SerializeField] private Button BtnIfScreen;
    [SerializeField] private GameObject IfScreen;
    [SerializeField] private GameObject loopScreen;
    [SerializeField] private GameObject contentForScreen;
    [SerializeField] private GameObject panelAllsetting;
    [SerializeField] private GameObject panelSelectlevel;
    [SerializeField] private TMP_Text description;
    [SerializeField] private CountdownTimer Time;
    [SerializeField] private CustomMask customMask;
    [SerializeField] private RectTransform mainScreen;
    [SerializeField] private RectTransform leftGameSceen;
    [SerializeField] private Image background;
    [SerializeField] private Sprite lavaEnvironment;
    [SerializeField] private Sprite earthEnvironment;
    [SerializeField] private Sprite spaceEnvironment;
    private int clickState = 0;
    private void Start()
    {
        switch (PopupManager.Instance.loaibai)
        {
            case Loaibai.tuantu:
                background.sprite = spaceEnvironment;
                break;
            case Loaibai.vonglap:
                background.sprite = lavaEnvironment;
                break;
            case Loaibai.renhanh:
                background.sprite = earthEnvironment;
                break;
        }
        if (PopupManager.Instance.listmap.tuantu[0].star < 1)
        {
            customMask.gameObject.SetActive(true);
            customMask.GetComponent<Canvas>().sortingLayerName = "Ground";
            description.text = "Sau khi bị hút vào hố đen phi hành gia đang lạc ở một hành tinh xa lạ. Hãy giúp anh ấy trở về nhà nhé!";
        }
        else
        {
            customMask.gameObject.SetActive(false);
        }
        GameController.Instance.listButton.Clear();
        GameController.Instance.chooseBtn = SpecialBtn.none;

        for (int i = 0; i < PopupManager.Instance.currentLevel.sobuoc; i++)
        {
            GameObject gb = Instantiate(postisionForbtn.gameObject, Zone.transform);
            Listposition.Add(gb);
        }
        for (int i = 0; i < 8; i++)
        {
            GameObject gb = Instantiate(postisionForbtn.gameObject, ifZone.transform);
            ListpostionIf.Add(gb);
        }
        for (int i = 0; i < 8; i++)
        {
            GameObject gb = Instantiate(postisionForbtn.gameObject, doZone.transform);
            ListpostiondoIf.Add(gb);
        }
        vitri = 0;
        vitriLoop = 0;
        if (PopupManager.Instance.loaibai == Loaibai.renhanh)
        {
            for (int i = 0; i < PopupManager.Instance.currentLevel.buocAo.Length; i++)
            {
                switch (PopupManager.Instance.currentLevel.buocAo[i])
                {
                    case "up":
                        move(btn_Up);
                        break;
                    case "down":
                        move(btn_Down);
                        break;
                    case "left":
                        move(btn_Left);
                        break;
                    case "right":
                        move(btn_Right);
                        break;
                    case "if":
                        Button btn = Instantiate(BtnIfScreen, switchScreen.transform);
                        move(btn_If);
                        switchScreen.listScreen.Add(IfScreen);
                        btn.onClick.AddListener(switchScreen.ShowIfScreen);
                        break;
                }
            }
        }
        Setup();
    }
    private void Setup()
    {
        btn_Left.onClick.AddListener(left_Click);
        btn_Right.onClick.AddListener(right_Click);
        btn_Up.onClick.AddListener(up_Click);
        btn_Down.onClick.AddListener(down_Click);
        btn_Play.onClick.AddListener(play_Click);
        btn_Delete.onClick.AddListener(delete_Click);
        btn_Loop.onClick.AddListener(loop_Click);
        btn_If.onClick.AddListener(if_Click);
        btn_No.onClick.AddListener(no_Click);
        btn_Yes.onClick.AddListener(yes_Click);
        btn_Close.onClick.AddListener(close_Click);
        switch (PopupManager.Instance.loaibai)
        {
            case Loaibai.tuantu:
                btn_Loop.gameObject.SetActive(false);
                btn_If.gameObject.SetActive(false);
                break;
            case Loaibai.vonglap:
                btn_Loop.gameObject.SetActive(true);
                break;
            case Loaibai.renhanh:
                btn_If.gameObject.SetActive(false);
                btn_Loop.gameObject.SetActive(false);
                break;
        }
        btn_Yes.gameObject.SetActive(false);
        btn_No.gameObject.SetActive(false);
    }
    private void move(Button btn)
    {
        if (vitri < Listposition.Count && GameController.Instance.chooseBtn == SpecialBtn.none)
        {

            GameObject gb = Instantiate(btn.gameObject, Listposition[vitri].transform);
            //gb.transform.SetPositionAndRotation(Listposition[vitri].transform.position, Listposition[vitri].transform.rotation);
            RectTransform rectTtransform = gb.transform.GetComponent<RectTransform>();
            rectTtransform.anchorMin = Vector2.zero;
            rectTtransform.anchorMax = Vector2.one;
            rectTtransform.offsetMax = Vector2.zero;
            rectTtransform.offsetMin = Vector2.zero;
            GameController.Instance.listButton.Add(gb);
            vitri++;
        }
        if (vitriLoop < ListpostionLoop.Count && GameController.Instance.chooseBtn == SpecialBtn.loop)
        {
            GameObject gb = Instantiate(btn.gameObject, ListpostionLoop[vitriLoop].transform);
            RectTransform rectTtransform = gb.transform.GetComponent<RectTransform>();
            rectTtransform.anchorMin = Vector2.zero;
            rectTtransform.anchorMax = Vector2.one;
            rectTtransform.offsetMax = Vector2.zero;
            rectTtransform.offsetMin = Vector2.zero;
            GameController.Instance.listBtnFor.Add(gb);
            vitriLoop++;
        }
        if (vitriIf < ListpostionIf.Count && GameController.Instance.chooseBtn == SpecialBtn.ifElse)
        {
            GameObject gb = Instantiate(btn.gameObject, ListpostionIf[vitriIf].transform);
            RectTransform rectTtransform = gb.transform.GetComponent<RectTransform>();
            rectTtransform.anchorMin = Vector2.zero;
            rectTtransform.anchorMax = Vector2.one;
            rectTtransform.offsetMax = Vector2.zero;
            rectTtransform.offsetMin = Vector2.zero;
            GameController.Instance.listBtnIf.Add(gb);
            vitriIf++;
        }
        if (vitridoIf < ListpostiondoIf.Count && GameController.Instance.chooseBtn == SpecialBtn.doIf)
        {
            GameObject gb = Instantiate(btn.gameObject, ListpostiondoIf[vitridoIf].transform);
            RectTransform rectTtransform = gb.transform.GetComponent<RectTransform>();
            rectTtransform.anchorMin = Vector2.zero;
            rectTtransform.anchorMax = Vector2.one;
            rectTtransform.offsetMax = Vector2.zero;
            rectTtransform.offsetMin = Vector2.zero;
            GameController.Instance.listBtndoIf.Add(gb);
            vitridoIf++;
        }
    }
    private void delete_Click()
    {
        AudioManager.Instance.PlaySound(Sound.Button);
        if (GameController.Instance.listButton.Count > 0)
        {
            if (GameController.Instance.chooseBtn == SpecialBtn.none)
            {
                if (GameController.Instance.listButton[GameController.Instance.listButton.Count - 1].name.Equals("btn_Loop(Clone)"))
                {
                    btn_Loop.gameObject.SetActive(true);
                }
                Destroy(GameController.Instance.listButton[GameController.Instance.listButton.Count - 1]);
                GameController.Instance.listButton.RemoveAt(GameController.Instance.listButton.Count - 1);
                vitri--;
            }
        }
        if (GameController.Instance.listBtnFor.Count > 0)
        {
            if (GameController.Instance.chooseBtn == SpecialBtn.loop)
            {
                Destroy(GameController.Instance.listBtnFor[GameController.Instance.listBtnFor.Count - 1]);
                GameController.Instance.listBtnFor.RemoveAt(GameController.Instance.listBtnFor.Count - 1);
                vitriLoop--;
            }
        }
        if (GameController.Instance.listBtnIf.Count > 0)
        {
            if (GameController.Instance.chooseBtn == SpecialBtn.ifElse)
            {
                Destroy(GameController.Instance.listBtnIf[GameController.Instance.listBtnIf.Count - 1]);
                GameController.Instance.listBtnIf.RemoveAt(GameController.Instance.listBtnIf.Count - 1);
                vitriIf--;
            }
        }
        if (GameController.Instance.listBtndoIf.Count > 0)
        {
            if (GameController.Instance.chooseBtn == SpecialBtn.doIf)
            {
                Destroy(GameController.Instance.listBtndoIf[GameController.Instance.listBtndoIf.Count - 1]);
                GameController.Instance.listBtndoIf.RemoveAt(GameController.Instance.listBtndoIf.Count - 1);
                vitridoIf--;
            }
        }
    }
    private void loop_Click()
    {
        GameObject gb = Instantiate(btn_Loop.gameObject, Listposition[vitri].transform);
        gb.transform.SetPositionAndRotation(Listposition[vitri].transform.position, Listposition[vitri].transform.rotation);
        GameController.Instance.listButton.Add(gb);
        vitri++;
        btn_Loop.gameObject.SetActive(false);
        Button lp = Instantiate(BtnloopScreen, switchScreen.transform);// Add button tắt bật screen đó
        lp.onClick.AddListener(switchScreen.ShowLoopScreen);
        gb = Instantiate(loopScreen, contentForScreen.transform);//add screen đó lên
        switchScreen.listScreen.Add(gb);
        Zone2 = gb.transform.GetChild(1).gameObject;
        for (int i = 0; i < 9; i++)
        {
            gb = Instantiate(postisionForbtn.gameObject, Zone2.transform);
            ListpostionLoop.Add(gb);
        }
    }
    private void if_Click()
    {
        AudioManager.Instance.PlaySound(Sound.Button);
        GameObject gb = Instantiate(btn_If.gameObject, Listposition[vitri].transform);
        gb.transform.SetPositionAndRotation(Listposition[vitri].transform.position, Listposition[vitri].transform.rotation);
        GameController.Instance.listButton.Add(gb);
        vitri++;
        btn_If.gameObject.SetActive(false);
    }
    private void yes_Click()
    {
        move(btn_Yes);
    }
    private void no_Click()
    {
        move(btn_No);
    }
    private void left_Click()
    {
        AudioManager.Instance.PlaySound(Sound.Button);
        move(btn_Left);
    }
    private void right_Click()
    {
        AudioManager.Instance.PlaySound(Sound.Button);
        move(btn_Right);
    }
    private void up_Click()
    {
        AudioManager.Instance.PlaySound(Sound.Button);
        move(btn_Up);
    }
    private void down_Click()
    {
        AudioManager.Instance.PlaySound(Sound.Button);
        move(btn_Down);
    }
    private void play_Click()
    {
        AudioManager.Instance.PlaySound(Sound.Button);
        PopupManager.Instance.playerController.playCharacter();
        Time.stopTime();
    }
    private void close_Click()
    {
        AudioManager.Instance.PlaySound(Sound.Button);
        Destroy(gameObject);
        Destroy(PopupManager.Instance.currentMap);
        Instantiate(panelSelectlevel, PopupManager.Instance.canvas.transform);

    }
    public void panelIf_click()
    {
        GameController.Instance.chooseBtn = SpecialBtn.ifElse;
        btn_Yes.gameObject.SetActive(true);
        btn_No.gameObject.SetActive(true);
    }
    public void paneldoIfclick()
    {
        GameController.Instance.chooseBtn = SpecialBtn.doIf;
        btn_Yes.gameObject.SetActive(false);
        btn_No.gameObject.SetActive(false);
    }
    public void allSetting_Click()
    {
        AudioManager.Instance.PlaySound(Sound.Button);
        if (panelAllsetting.activeSelf)
        {
            panelAllsetting.SetActive(false);
        }
        else
        {
            panelAllsetting.SetActive(true);
        }
    }
    public void Tutorial_click()
    {
        clickState++;
        switch (clickState)
        {
            case 1:
                customMask.target = btnZone;
                description.text = "BẢNG ĐIỀU KHIỂN: Dùng để gửi tín hiệu di chuyển cho phi hành gia.";
                break;
            case 2:
                customMask.target = mainScreen;
                description.text = "MÀN THÔNG TIN: hiển thị các tín hiệu được gửi.";
                break;
            case 3:
                customMask.target = btn_Play.GetComponent<RectTransform>();
                description.text = "Gửi tín hiệu cho phi hành gia thực hiện các tín hiệu đã được gửi trong màn hình trên.";
                break;
            case 4:
                customMask.target = btn_Delete.GetComponent<RectTransform>();
                description.text = "Xóa các tín hiệu gửi sai. Lệnh sẽ thực hiện xóa từ tín hiệu gần nhất.";
                break;
            case 5:
                customMask.target = leftGameSceen;
                description.text = "Màn hình hiển thị vị trí của phi hành gia.";
                break;
            case 6:
                description.text = "Xem chừng bạn đã sẵn sàng. Hãy bắt đầu thôi nào!";
                break;
            case 7:
                customMask.gameObject.SetActive(false);
                break;
        }
    }
}
