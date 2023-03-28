﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using UnityEngine.SceneManagement;
using Photon.Realtime;
using BaseClass;

public class Panel_DieuKhien : MonoBehaviourPunCallbacks
{
    public bool enoughPlayer = false;
    public float otherPlayerTime;
    public float myTime;
    [Header("List")]
    public List<GameObject> listBtnPos;//List GameObject để Instantiate button
    [SerializeField] private List<PlayerInfor> listPlayerInfor = new List<PlayerInfor>();
    [Header("Int")]
    public int vitri;//Con trỏ xác định index trong listBtnPos
    public int posOfList; //Con trỏ xác định index trong GameController.Instance.listScreenAdd
    private int clickState = 0;//Biến điếm dùng để check các bước trong game tutorial
    public int numberPlayerAns = 0;
    public int playerEndMove = 0;
    [Header("GameObject")]
    [SerializeField] private GameObject Zone;//Lấy vị trí GameObject để Instantiate số bước được đi từ json
    [SerializeField] private GameObject postisionForbtn;//Instantiate ô trống vào để điền nút
    [SerializeField] private GameObject IfScreen;
    [SerializeField] private GameObject loopScreen;//Instantiate màn hình cho chủ đề vòng lặp
    [SerializeField] private GameObject contentForScreen;//Vị trí để Instantiate GameObject vào
    [SerializeField] private GameObject panelAllsetting;//Chứa Panel cài đặt
    [SerializeField] private GameObject panelSelectlevel;
    [SerializeField] private GameObject switchScreen;//Hỗ trợ việc tắt bật các màn
    [SerializeField] private GameObject header;
    [SerializeField] private GameObject body;
    [SerializeField] private GameObject btnZone;//Khu vực các nút điều khiểu cho GamePlay dùng làm game tutorial
    [SerializeField] private GameObject panelExit;
    [Header("Button")]
    [SerializeField] private Button btn_Left;//Nút rẽ trái
    [SerializeField] private Button btn_Right;//Nút rẽ phải
    [SerializeField] private Button btn_Up;//Nút đi lên
    [SerializeField] private Button btn_Down;//Nút đi xuống
    [SerializeField] private Button btn_Loop;//Nút vòng lặp
    [SerializeField] private Button btn_If;
    public Button btn_No;
    [SerializeField] private Button btn_Play;//Nút chơi
    [SerializeField] private Button btn_Delete;//Nút xóa
    [SerializeField] private Button btn_Close;//Nút thoát game
    [SerializeField] private Button BtnloopScreen;//Nút bật màn vòng lặp
    [SerializeField] private Button BtnIfScreen;//Nút bật màn rẽ nhánh
    [SerializeField] private Button BtnMainScreen;//Nút bật màn hình chính
    [Header("Scripts")]
    public CountdownTimer Time;//Script đếm thời gian
    [SerializeField] private CustomMask customMask;//Script panel phủ để làm game tutorial
    [Header("UI")]

    [SerializeField] private RectTransform mainScreen;//Màn hình chứa các điều khiểu dc chọn cho GamePlay dùng làm gametutorial
    [SerializeField] private RectTransform leftGameSceen;//mà hình bên trái khu hiển thị màn chơi dùng làm gametutorial
    [SerializeField] private Image background;//Nền phía sau
    [SerializeField] private TMP_Text description;//Text hướng dẫn chơi
    [SerializeField] private Sprite lavaEnvironment;//Ảnh nền
    [SerializeField] private Sprite earthEnvironment;//Ảnh nền
    [SerializeField] private Sprite spaceEnvironment;//Ảnh nền
    [SerializeField] private Sprite btnPlayOff;
    [SerializeField] private Sprite btnPlayOn;
    private void Start()
    {
        if (PopupManager.Instance.isArena == true)
        {
            listPlayerInfor[0].gameObject.SetActive(true);
            listPlayerInfor[1].gameObject.SetActive(true);
        }
        else
        {
            listPlayerInfor[0].gameObject.SetActive(false);
            listPlayerInfor[1].gameObject.SetActive(false);
        }
        /*header.GetComponent<RectTransform>().localPosition = new Vector3(33,-30,0);
        btnZone.GetComponent<RectTransform>().localPosition = new Vector3(-0.2f, -700, 0);
        body.GetComponent<RectTransform>().localPosition = new Vector3(-145, 4.9f, 0);*/
        LeanTween.moveLocalY(header, 200, 0.65f).setEaseOutQuad();
        LeanTween.moveLocalY(btnZone, -390, 0.65f).setEaseOutQuad();
        LeanTween.moveLocalX(body, -450f, 0.65f).setEaseOutQuad();
        //Reset giá trị khi mới bắt đầu game
        GameController.Instance.ResetGameController();
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
        //Đổi ảnh nền theo hành tinh được chọn
        switch (PopupManager.Instance.loaibai)
        {
            case Loaibai.tuantu:
                background.sprite = spaceEnvironment;
                if (PopupManager.instance.currentLevel.level == 3.ToString())
                {
                    PopupManager.Instance.ShowNotification(gameObject, "Hố đen đang biến động! Nhưng nhiệm vụ của chúng ta không thay đổi. Hãy đi qua tất cả hố đen!", 5f, null);

                }
                break;
            case Loaibai.vonglap:
                background.sprite = lavaEnvironment;
                if (PopupManager.Instance.listmap.vonglap[0].star < 1)
                {
                    customMask.gameObject.SetActive(true);
                    customMask.GetComponent<Canvas>().sortingLayerName = "Ground";
                    description.text = "Mỗi hành tinh đều có cách vận hành riêng, tận dụng được cách vận hành đó sẽ đem lại nhiều lợi ích trên hành trình!";
                }
                if (PopupManager.instance.currentLevel.level == 4.ToString())
                {
                    PopupManager.Instance.ShowNotification(gameObject, "Hành tinh Vòng Lặp sở hữu cho mình những ngọn lửa ngàn độ. Đừng dại gì mà chạm vào chúng!", 5f, null);
                }
                break;
            case Loaibai.renhanh:
                background.sprite = earthEnvironment;
                if (PopupManager.Instance.listmap.renhanh[0].star < 1)
                {
                    customMask.gameObject.SetActive(true);
                    customMask.GetComponent<Canvas>().sortingLayerName = "Ground";
                    description.text = "Thời tiết thật tồi tệ! Xem ra chúng ta đang ở hành tinh Rẽ Nhánh.";
                }
                break;
        }

        //Instantiate các ô trống để chứa nút
        if (PopupManager.Instance.currentLevel.sobuoc == 0)
        {
            for (int i = 0; i < 4; i++)
            {
                GameObject gb = Instantiate(postisionForbtn.gameObject, Zone.transform);
                listBtnPos.Add(gb);
            }
        }
        else
        {
            for (int i = 0; i < PopupManager.Instance.currentLevel.sobuoc; i++)
            {
                GameObject gb = Instantiate(postisionForbtn.gameObject, Zone.transform);
                listBtnPos.Add(gb);
            }
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
                        GameObject gb = Instantiate(IfScreen, contentForScreen.transform);
                        move(btn_If);
                        GameController.Instance.listScreenAdd.Add(gb);
                        gb.GetComponent<IfScreen>().posInLooplist = GameController.Instance.listScreenAdd.Count - 1;
                        btn.onClick.AddListener(gb.GetComponent<IfScreen>().ShowIfScreen);
                        gb.GetComponent<IfScreen>().currentBtn = btn.gameObject;
                        break;
                }
            }
        }
        Setup();
    }
    //Set các giá trị cơ bản
    private void Setup()
    {
        BtnMainScreen.onClick.AddListener(OpenMainScreen_click);
        btn_Left.onClick.AddListener(left_Click);
        btn_Right.onClick.AddListener(right_Click);
        btn_Up.onClick.AddListener(up_Click);
        btn_Down.onClick.AddListener(down_Click);

        btn_Delete.onClick.AddListener(delete_Click);
        btn_Loop.onClick.AddListener(loop_Click);
        btn_If.onClick.AddListener(if_Click);
        btn_No.onClick.AddListener(no_Click);
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
        if (PopupManager.Instance.isArena == true)
        {
            btn_Loop.gameObject.SetActive(true);
            btn_Play.onClick.AddListener(PlayinArena);
        }
        else
        {
            btn_Play.onClick.AddListener(play_Click);
        }
        btn_No.gameObject.SetActive(false);
    }
    //Set kích cỡ cho các nút 
    private void SetRectransfrom(GameObject gb)
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
                    gb.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
                    SetRectransfrom(gb);
                    GameController.Instance.listButton.Add(gb);
                    vitri++;
                }
                else
                {
                    PopupManager.Instance.ShowNotification(gameObject, "Chuỗi câu lệnh đã đạt số lượng tối đa. Không thể thêm!", 1.8f, null);
                }
                break;
            case SpecialBtn.loop:
                LoopScreen loopScreen = GameController.Instance.listScreenAdd[posOfList].GetComponent<LoopScreen>();
                if (loopScreen.vitriloop < loopScreen.listPosLoop.Count)
                {
                    GameObject gb = Instantiate(btn.gameObject, loopScreen.listPosLoop[loopScreen.vitriloop].transform);
                    gb.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
                    SetRectransfrom(gb);
                    loopScreen.listBtnFor.Add(gb);
                    loopScreen.vitriloop++;
                }
                break;
            case SpecialBtn.ifElse:
                IfScreen ifScreen = GameController.Instance.listScreenAdd[posOfList].GetComponent<IfScreen>();
                if (ifScreen.vitriIf < ifScreen.listpostionIf.Count)
                {
                    GameObject gb = Instantiate(btn.gameObject, ifScreen.listpostionIf[ifScreen.vitriIf].transform);
                    gb.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
                    SetRectransfrom(gb);
                    ifScreen.listBtnIf.Add(gb);
                    ifScreen.vitriIf++;
                }
                else
                {
                    PopupManager.Instance.ShowNotification(gameObject, "Chuỗi câu lệnh đã đạt số lượng tối đa. Không thể thêm!", 1.8f, null);
                }
                break;
            case SpecialBtn.doIf:
                IfScreen doIfScreen = GameController.Instance.listScreenAdd[posOfList].GetComponent<IfScreen>();
                if (doIfScreen.vitridoIf < doIfScreen.listpostiondoIf.Count)
                {
                    GameObject gb = Instantiate(btn.gameObject, doIfScreen.listpostiondoIf[doIfScreen.vitridoIf].transform);
                    gb.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
                    SetRectransfrom(gb);
                    doIfScreen.listBtndoIf.Add(gb);
                    doIfScreen.vitridoIf++;
                }
                else
                {
                    PopupManager.Instance.ShowNotification(gameObject, "Chuỗi câu lệnh đã đạt số lượng tối đa. Không thể thêm!", 1.8f, null);
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
                    //Chặn việc xóa các lệnh cố định của chủ đề rẽ nhánh
                    if (PopupManager.Instance.currentLevel.buocAo.Length < GameController.Instance.listButton.Count)
                    {
                        if (GameController.Instance.listButton[GameController.Instance.listButton.Count - 1].name.Equals("btn_Loop(Clone)"))
                        {
                            Destroy(GameController.Instance.listScreenAdd[GameController.Instance.listScreenAdd.Count - 1].GetComponent<LoopScreen>().currentBtn);
                            Destroy(GameController.Instance.listScreenAdd[GameController.Instance.listScreenAdd.Count - 1]);
                            GameController.Instance.listScreenAdd.RemoveAt(GameController.Instance.listScreenAdd.Count - 1);

                        }
                        if (GameController.Instance.listButton[GameController.Instance.listButton.Count - 1].name.Equals("btn_If(Clone)"))
                        {
                            Destroy(GameController.Instance.listScreenAdd[GameController.Instance.listScreenAdd.Count - 1].GetComponent<IfScreen>().currentBtn);
                            Destroy(GameController.Instance.listScreenAdd[GameController.Instance.listScreenAdd.Count - 1]);
                            GameController.Instance.listScreenAdd.RemoveAt(GameController.Instance.listScreenAdd.Count - 1);
                        }
                        vitri--;
                        Destroy(GameController.Instance.listButton[GameController.Instance.listButton.Count - 1]);
                        GameController.Instance.listButton.RemoveAt(GameController.Instance.listButton.Count - 1);
                    }
                    else
                    {
                        PopupManager.Instance.ShowNotification(PopupManager.Instance.canvas, "Không thể xóa các bước đi cố định!", 1.8f, null);
                    }

                }
                else
                {
                    PopupManager.Instance.ShowNotification(PopupManager.Instance.canvas, "Hiện không còn lệnh nào để xóa!", 1.8f, null);
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
                    PopupManager.Instance.ShowNotification(gameObject, "Hiện không còn lệnh nào để xóa!", 1.8f, null);
                }
                break;
            case SpecialBtn.ifElse:
                IfScreen ifScreen = GameController.Instance.listScreenAdd[posOfList].GetComponent<IfScreen>();
                if (ifScreen.listBtnIf.Count > 0)
                {
                    Destroy(ifScreen.listBtnIf[ifScreen.listBtnIf.Count - 1]);
                    ifScreen.listBtnIf.RemoveAt(ifScreen.listBtnIf.Count - 1);
                    ifScreen.vitriIf--;

                }
                else
                {
                    PopupManager.Instance.ShowNotification(gameObject, "Hiện không còn lệnh nào để xóa!", 1.8f, null);
                }
                break;
            case SpecialBtn.doIf:
                IfScreen doIfScreen = GameController.Instance.listScreenAdd[posOfList].GetComponent<IfScreen>();
                if (doIfScreen.listBtndoIf.Count > 0)
                {
                    Destroy(doIfScreen.listBtndoIf[doIfScreen.listBtndoIf.Count - 1]);
                    doIfScreen.listBtndoIf.RemoveAt(doIfScreen.listBtndoIf.Count - 1);
                    doIfScreen.vitridoIf--;
                }
                else
                {
                    PopupManager.Instance.ShowNotification(gameObject, "Hiện không còn lệnh nào để xóa!", 1.8f, null);
                }
                break;
        }
    }
    //Add vào danh sách nút vòng lặp
    private void loop_Click()
    {
        AudioManager.Instance.PlaySound(Sound.Button);
        if (vitri < listBtnPos.Count)
        {
            GameObject gb = Instantiate(btn_Loop.gameObject, listBtnPos[vitri].transform);//Instantiate vào vị trí trên màn hình
            gb.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
            SetRectransfrom(gb);
            //gb.transform.SetPositionAndRotation(listBtnPos[vitri].transform.position, listBtnPos[vitri].transform.rotation);
            GameController.Instance.listButton.Add(gb);//Add vào danh sách các nút chạy kịch bản
            vitri++;//Tăng index trong list
            Button lp = Instantiate(BtnloopScreen, switchScreen.transform);//Add button tắt bật screen đó
            gb = Instantiate(loopScreen, contentForScreen.transform);//Add screen đó lên
            lp.onClick.AddListener(gb.GetComponent<LoopScreen>().ShowLoopScreen);//Add sự kiện click
            GameController.Instance.listScreenAdd.Add(gb);//Add screen mới vào trong list để kiểm soát
            gb.GetComponent<LoopScreen>().posInLooplist = GameController.Instance.listScreenAdd.Count - 1;
            gb.GetComponent<LoopScreen>().currentBtn = lp.gameObject;
        }
        else
        {
            PopupManager.Instance.ShowNotification(gameObject, "Chuỗi câu lệnh đã đạt số lượng tối đa. Không thể thêm!", 1.8f, null);
        }
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
    private void PlayinArena()
    {
        AudioManager.Instance.PlaySound(Sound.Button);
        /*        btn_Play.enabled = false;
                btn_Delete.enabled = false;
                btn_Play.image.sprite = btnPlayOff;*/
        if (enoughPlayer == true)
        {
            if (GameController.Instance.listButton.Count > 0)
            {
                btn_Play.enabled = false;
                btn_Delete.enabled = false;
                btn_Play.image.sprite = btnPlayOff;
                PopupManager.Instance.playerControllerInArena.SendAnsReady();
            }
            else
            {
                PopupManager.Instance.ShowNotification(PopupManager.Instance.canvas, "Hiện không có câu lệnh để thực hiện!", 1.8f, null);
            }
        }
        else
        {
            PopupManager.Instance.ShowNotification(PopupManager.Instance.canvas, "Cần đủ 2 người để bắt đầu thi đấu!", 1.8f, null);
        }

    }
    private void play_Click()
    {
        AudioManager.Instance.PlaySound(Sound.Button);

        if (GameController.Instance.listButton.Count > 0)
        {
            btn_Play.enabled = false;
            btn_Delete.enabled = false;
            btn_Play.image.sprite = btnPlayOff;
            PopupManager.Instance.playerController.playCharacter();
            Time.stopTime();
        }
        else
        {
            PopupManager.Instance.ShowNotification(PopupManager.Instance.canvas, "Hiện không có câu lệnh để thực hiện!", 1.8f, null);
        }

    }
    public void ResetPlayClick()
    {
        btn_Play.enabled = true;
        btn_Delete.enabled = true;
        btn_Play.image.sprite = btnPlayOn;
        for (int i = 0; i < GameController.Instance.listButton.Count; i++)
        {
            Destroy(listBtnPos[i].transform.GetChild(0).gameObject);
        }
        PopupManager.Instance.currentDashboard.numberPlayerAns = 0;
        vitri = 0;
        Time.ResetTime();
        Time.ResetOtherTime();
    }
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void close_Click()
    {
        AudioManager.Instance.PlaySound(Sound.Button);
        if (PopupManager.Instance.isArena == false)
        {
            Destroy(gameObject);
            Destroy(PopupManager.Instance.currentMap);
            Instantiate(panelSelectlevel, PopupManager.Instance.canvas.transform);
            AudioManager.Instance.StopEffect();
            GameController.Instance.ResetGameController();
            PopupManager.Instance.currentLevel = new Map();
        }
        else
        {
            GameController.Instance.ResetGameController();
            PhotonNetwork.LeaveRoom();
            PhotonNetwork.Disconnect();
        }

    }
    private void OpenMainScreen_click()
    {
        for (int i = 0; i < GameController.Instance.listScreenAdd.Count; i++)
        {
            GameController.Instance.listScreenAdd[i].SetActive(false);
        }
    }
    public void panelIf_click()
    {
        GameController.Instance.chooseBtn = SpecialBtn.ifElse;
        //btn_Yes.gameObject.SetActive(true);
        btn_No.gameObject.SetActive(true);
    }
    public void paneldoIfclick()
    {
        GameController.Instance.chooseBtn = SpecialBtn.doIf;
        //btn_Yes.gameObject.SetActive(false);
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
        if (PopupManager.Instance.loaibai == Loaibai.tuantu)
        {
            switch (clickState)
            {
                case 1:
                    customMask.target = btnZone.GetComponent<RectTransform>();
                    description.text = "BẢNG ĐIỀU KHIỂN: Dùng để gửi tín hiệu di chuyển cho phi hành gia.";
                    break;
                case 2:
                    customMask.target = mainScreen;
                    description.text = "MÀN THÔNG TIN: hiển thị các tín hiệu được gửi.";
                    break;
                case 3:
                    customMask.target = btn_Play.GetComponent<RectTransform>();
                    description.text = "Gửi lệnh cho phi hành gia thực hiện các tín hiệu đã được gửi trong màn hình trên.";
                    break;
                case 4:
                    customMask.target = btn_Delete.GetComponent<RectTransform>();
                    description.text = "Xóa: Lệnh sẽ thực hiện xóa từ tín hiệu gần nhất.";
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
        else if (PopupManager.Instance.loaibai == Loaibai.vonglap)
        {
            switch (clickState)
            {
                case 1:
                    description.text = "Chúng ta đang ở hành tinh Vòng Lặp.";
                    break;
                case 2:
                    customMask.target = btn_Loop.GetComponent<RectTransform>();
                    description.text = "Sử dụng câu lệnh vòng lặp, để hạn chế số lượng tín hiệu cần truyền.";
                    break;
                case 3:
                    loop_Click();
                    customMask.target = GameController.Instance.listScreenAdd[0].GetComponent<LoopScreen>().currentBtn.GetComponent<RectTransform>();
                    description.text = "Nhấn vào để mở màn hình tín hiệu dành cho vòng lặp";
                    break;
                case 4:
                    GameController.Instance.listScreenAdd[0].SetActive(true);
                    customMask.target = GameController.Instance.listScreenAdd[0].GetComponent<LoopScreen>().GetComponent<RectTransform>();
                    description.text = "Nơi hiển thị các câu lệnh dùng cho vòng lặp.";
                    break;
                case 5:
                    customMask.target = GameController.Instance.listScreenAdd[0].GetComponent<LoopScreen>().txt_Loop.GetComponent<RectTransform>();
                    description.text = "Hiển thị số lần lặp lại của vòng lặp này.";
                    break;
                case 6:
                    customMask.target = BtnMainScreen.GetComponent<RectTransform>();
                    description.text = "Nhấn vào đây để trở về màn hình câu lệnh chính.";
                    break;
                case 7:
                    GameController.Instance.listScreenAdd[0].SetActive(false);
                    customMask.gameObject.SetActive(false);
                    break;
            }
        }
        else if (PopupManager.Instance.loaibai == Loaibai.renhanh)
        {
            IfScreen ifScreen = GameController.Instance.listScreenAdd[0].GetComponent<IfScreen>();
            switch (clickState)
            {
                case 1:
                    description.text = "Lớp sương mù kì lạ làm cho tín hiệu truyền tới phi hành gia trở nên trập trờn.";
                    break;
                case 2:
                    customMask.target = mainScreen;
                    description.text = "Do đó phi hành gia phải tự di chuyển trong sương mà ko có tầm nhìn phía trước.";
                    break;
                case 3:
                    customMask.target = GameController.Instance.listButton[0].GetComponent<RectTransform>();
                    description.text = "May thay bạn vẫn có thể xen vào đó các tín hiệu quan trọng giúp anh ấy tránh khỏi nguy hiểm!";
                    break;
                case 4:
                    customMask.target = ifScreen.currentBtn.GetComponent<RectTransform>();
                    description.text = "Nhấn vào để mở màn hình tín hiệu dành cho rẽ nhánh";
                    break;
                case 5:
                    GameController.Instance.listScreenAdd[0].SetActive(true);
                    customMask.target = ifScreen.GetComponent<RectTransform>();
                    description.text = "Nơi hiển thị các câu lệnh dùng cho cấu trúc rẽ nhánh.";
                    break;
                case 6:
                    description.text = "Cấu trúc rẽ nhánh được chia làm 2 phần gồm: Câu điều kiện và hành động thực hiện.";
                    break;
                case 7:
                    customMask.target = ifScreen.ifZone.GetComponent<RectTransform>();
                    description.text = "Nơi chứa câu lệnh điều kiện. Nếu điều kiện được thoải mãn hành động bên dưới sẽ được thực hiện.";
                    break;
                case 8:
                    customMask.target = ifScreen.doZone.GetComponent<RectTransform>();
                    description.text = "Nơi chứa câu lệnh hành động. Câu lệnh hành động chỉ được thực hiện khi thoải mãn câu lệnh điều kiện.";
                    break;
                case 9:
                    description.text = "Bạn đã sẵn sàng chưa? Hãy chinh phục hành tinh này thôi nào!";
                    break;
                case 10:
                    GameController.Instance.listScreenAdd[0].SetActive(true);
                    customMask.gameObject.SetActive(false);
                    break;
            }
        }

    }

    public void UpdatePlayerInfor()
    {
        int i = 0;
        if (PhotonNetwork.CurrentRoom != null)
        {
            foreach (KeyValuePair<int, Player> player in PhotonNetwork.CurrentRoom.Players)
            {
                listPlayerInfor[i].SetPlayerInfor(player.Value);
                i++;
            }
            if (i == 2)
            {
                if (PopupManager.Instance.playerControllerInArena != null)
                {
                    PopupManager.Instance.playerControllerInArena.view.RPC("StartTime", RpcTarget.All);
                }
            }
        }
    }
}
