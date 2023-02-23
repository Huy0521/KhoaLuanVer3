﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Panel_DieuKhien : MonoBehaviour
{
    [Header("List")]
    public List<GameObject> listBtnPos;//List GameObject để Instantiate button
    public List<GameObject> ListpostionIf;//List GameObject để Instantiate button cho chủ đề If
    public List<GameObject> ListpostiondoIf;//List GameObject để Instantiate button cho chủ đề doIf
    [Header("Int")]
    private int vitri;//Con trỏ xác định index trong listBtnPos
    private int vitriIf;//Con trỏ xác định index trong listpostionIf
    private int vitridoIf;//Con trỏ xác định index trong listpostiondoIf
    public int posOfList; //Con trỏ xác định index trong GameController.Instance.listScreenAdd
    private int clickState = 0;//Biến điếm dùng để check các bước trong game tutorial
    [Header("GameObject")]
    [SerializeField] private GameObject Zone;//Lấy vị trí GameObject để Instantiate số bước được đi từ json
    [SerializeField] private GameObject ifZone;
    [SerializeField] private GameObject doZone;
    [SerializeField] private GameObject postisionForbtn;//Instantiate ô trống vào để điền nút
    [SerializeField] private GameObject IfScreen;
    [SerializeField] private GameObject loopScreen;//Instantiate màn hình cho chủ đề vòng lặp
    [SerializeField] private GameObject contentForScreen;//Vị trí để Instantiate GameObject vào
    [SerializeField] private GameObject panelAllsetting;//Chứa Panel cài đặt
    [SerializeField] private GameObject panelSelectlevel;
    [Header("Button")]
    [SerializeField] private Button btn_Left;//Nút rẽ trái
    [SerializeField] private Button btn_Right;//Nút rẽ phải
    [SerializeField] private Button btn_Up;//Nút đi lên
    [SerializeField] private Button btn_Down;//Nút đi xuống
    [SerializeField] private Button btn_Loop;//Nút vòng lặp
    [SerializeField] private Button btn_If;
    [SerializeField] private Button btn_Yes;
    [SerializeField] private Button btn_No;
    [SerializeField] private Button btn_Play;//Nút chơi
    [SerializeField] private Button btn_Delete;//Nút xóa
    [SerializeField] private Button btn_Close;//Nút thoát game
    [SerializeField] private Button BtnloopScreen;//Nút bật màn vòng lặp
    [SerializeField] private Button BtnIfScreen;//Nút bật màn rẽ nhánh
    [Header("Scripts")]
    [SerializeField] private SwitchScreen switchScreen;//Hỗ trợ việc tắt bật các màn(Thừa)
    [SerializeField] private CountdownTimer Time;//Script đếm thời gian
    [SerializeField] private CustomMask customMask;//Script panel phủ để làm game tutorial
    [Header("UI")]
    [SerializeField] private RectTransform btnZone;//Khu vực các nút điều khiểu cho GamePlay dùng làm game tutorial
    [SerializeField] private RectTransform mainScreen;//Màn hình chứa các điều khiểu dc chọn cho GamePlay dùng làm gametutorial
    [SerializeField] private RectTransform leftGameSceen;//mà hình bên trái khu hiển thị màn chơi dùng làm gametutorial
    [SerializeField] private Image background;//Nền phía sau
    [SerializeField] private TMP_Text description;//Text hướng dẫn chơi
    [SerializeField] private Sprite lavaEnvironment;//Ảnh nền
    [SerializeField] private Sprite earthEnvironment;//Ảnh nền
    [SerializeField] private Sprite spaceEnvironment;//Ảnh nền
    private void Start()
    {
        //Reset giá trị khi mới bắt đầu game
        GameController.Instance.listButton.Clear();
        GameController.Instance.chooseBtn = SpecialBtn.none;
        //Đổi ảnh nền theo hành tinh được chọn
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
        //Bật hướng dẫn chơi
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
        //Instantiate các ô trống để chứa nút
        for (int i = 0; i < PopupManager.Instance.currentLevel.sobuoc; i++)
        {
            GameObject gb = Instantiate(postisionForbtn.gameObject, Zone.transform);
            listBtnPos.Add(gb);
        }
        //Instantiate các ô trống để chứa nút
        for (int i = 0; i < 8; i++)
        {
            GameObject gb = Instantiate(postisionForbtn.gameObject, ifZone.transform);
            ListpostionIf.Add(gb);
        }
        //Instantiate các ô trống để chứa nút
        for (int i = 0; i < 8; i++)
        {
            GameObject gb = Instantiate(postisionForbtn.gameObject, doZone.transform);
            ListpostiondoIf.Add(gb);
        }
        vitri = 0;
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
    //Set các giá trị cơ bản
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
        //Tắt bật các nút hỗ trợ cho từng chức năng
        switch (PopupManager.Instance.loaibai)
        {
            case Loaibai.tuantu:
                btn_Loop.gameObject.SetActive(false);
                btn_If.gameObject.SetActive(false);
                break;
            case Loaibai.vonglap:
                btn_Loop.gameObject.SetActive(true);
                btn_If.gameObject.SetActive(false);
                break;
            case Loaibai.renhanh:
                btn_If.gameObject.SetActive(false);
                btn_Loop.gameObject.SetActive(false);
                break;
        }
        btn_Yes.gameObject.SetActive(false);
        btn_No.gameObject.SetActive(false);
    }
    //Set kích cỡ cho các nút 
    void SetRectransfrom(GameObject gb)
    {
        RectTransform rectTtransform = gb.transform.GetComponent<RectTransform>();
        rectTtransform.anchorMin = Vector2.zero;
        rectTtransform.anchorMax = Vector2.one;
        rectTtransform.offsetMax = Vector2.zero;
        rectTtransform.offsetMin = Vector2.zero;
    }
    //Instantiate button bước đi
    private void move(Button btn)
    {
        switch (GameController.Instance.chooseBtn)
        {
            case SpecialBtn.none:
                if (vitri < listBtnPos.Count)
                {
                    GameObject gb = Instantiate(btn.gameObject, listBtnPos[vitri].transform);
                    SetRectransfrom(gb);
                    GameController.Instance.listButton.Add(gb);
                    vitri++;
                }
                break;
            case SpecialBtn.loop:
                LoopScreen loopScreen = GameController.Instance.listScreenAdd[posOfList].GetComponent<LoopScreen>();
                if (loopScreen.vitriloop < loopScreen.listPosLoop.Count)
                {
                    GameObject gb = Instantiate(btn.gameObject, loopScreen.listPosLoop[loopScreen.vitriloop].transform);
                    SetRectransfrom(gb);
                    loopScreen.listBtnFor.Add(gb);
                    loopScreen.vitriloop++;
                }
                break;
            case SpecialBtn.ifElse:
                if (vitriIf < ListpostionIf.Count)
                {
                    GameObject gb = Instantiate(btn.gameObject, ListpostionIf[vitriIf].transform);
                    SetRectransfrom(gb);
                    GameController.Instance.listBtnIf.Add(gb);
                    vitriIf++;
                }
                break;
            case SpecialBtn.doIf:
                if(vitridoIf < ListpostiondoIf.Count)
                {
                    GameObject gb = Instantiate(btn.gameObject, ListpostiondoIf[vitridoIf].transform);
                    SetRectransfrom(gb);
                    GameController.Instance.listBtndoIf.Add(gb);
                    vitridoIf++;
                }
                break;
        }
    }
    //Xóa nút
    private void delete_Click()
    {
        AudioManager.Instance.PlaySound(Sound.Button);
        switch (GameController.Instance.chooseBtn)
        {
            case SpecialBtn.none:
                if (GameController.Instance.listButton.Count > 0)
                {
                    Destroy(GameController.Instance.listButton[GameController.Instance.listButton.Count - 1]);
                    GameController.Instance.listButton.RemoveAt(GameController.Instance.listButton.Count - 1);
                    vitri--;
                }
                else
                {
                    PopupManager.Instance.ShowNotification(gameObject,"Hiện không còn lệnh nào để xóa!");
                }
                break;
            case SpecialBtn.loop:
                LoopScreen loopScreen = GameController.Instance.listScreenAdd[posOfList].GetComponent<LoopScreen>();
                if (loopScreen.listBtnFor.Count > 0)
                {
                    Destroy(loopScreen.listBtnFor[loopScreen.listBtnFor.Count - 1]);
                    loopScreen.listBtnFor.RemoveAt(loopScreen.listBtnFor.Count - 1);
                    loopScreen.vitriloop--;
                }
                else
                {
                    PopupManager.Instance.ShowNotification(gameObject, "Hiện không còn lệnh nào để xóa!");
                }
                break;
            case SpecialBtn.ifElse:
                if (GameController.Instance.listBtnIf.Count > 0)
                {
                    Destroy(GameController.Instance.listBtnIf[GameController.Instance.listBtnIf.Count - 1]);
                    GameController.Instance.listBtnIf.RemoveAt(GameController.Instance.listBtnIf.Count - 1);
                    vitriIf--;
                }
                else
                {
                    PopupManager.Instance.ShowNotification(gameObject, "Hiện không còn lệnh nào để xóa!");
                }
                break;
            case SpecialBtn.doIf:
                if (GameController.Instance.listBtndoIf.Count > 0)
                {
                    Destroy(GameController.Instance.listBtndoIf[GameController.Instance.listBtndoIf.Count - 1]);
                    GameController.Instance.listBtndoIf.RemoveAt(GameController.Instance.listBtndoIf.Count - 1);
                    vitridoIf--;
                }
                else
                {
                    PopupManager.Instance.ShowNotification(gameObject, "Hiện không còn lệnh nào để xóa!");
                }
                break;
        }
    }
    //Add vào danh sách nút vòng lặp
    private void loop_Click()
    {
        GameObject gb = Instantiate(btn_Loop.gameObject, listBtnPos[vitri].transform);//Instantiate vào vị trí trên màn hình
        gb.transform.SetPositionAndRotation(listBtnPos[vitri].transform.position, listBtnPos[vitri].transform.rotation);
        GameController.Instance.listButton.Add(gb);//Add vào danh sách các nút chạy kịch bản
        vitri++;//Tăng index trong list
        Button lp = Instantiate(BtnloopScreen, switchScreen.transform);//Add button tắt bật screen đó
        gb = Instantiate(loopScreen, contentForScreen.transform);//Add screen đó lên
        lp.onClick.AddListener(gb.GetComponent<LoopScreen>().ShowLoopScreen);//Add sự kiện click
        GameController.Instance.listScreenAdd.Add(gb);//Add screen mới vào trong list để kiểm soát
        gb.GetComponent<LoopScreen>().posInLooplist = GameController.Instance.listScreenAdd.Count - 1;
        switchScreen.listScreen.Add(gb);//

    }
    //Add vào danh sách nút rẽ nhánh
    private void if_Click()
    {
        AudioManager.Instance.PlaySound(Sound.Button);
        GameObject gb = Instantiate(btn_If.gameObject, listBtnPos[vitri].transform);
        gb.transform.SetPositionAndRotation(listBtnPos[vitri].transform.position, listBtnPos[vitri].transform.rotation);
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
    //Hướng dẫn chơi
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
    private void OnDestroy()
    {
        GameController.Instance.listScreenAdd.Clear();
    }
}
