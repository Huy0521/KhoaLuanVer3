using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private int finishNumber;
    private int numberLoopScreen = 0;
    private int cursorInMainList = 0;
    public float speed = 6;
    private string currentAnimaton;
    public List<Vector3> futurePosition;
    private Vector3 currentposition;
    [SerializeField] private ParticleSystem hitPartical;
    private bool checkFootStep = false;
    private bool checkReplay = false;
    int ifNumber = 0;
    int ifJson = 0;
    private void Awake()
    {
        currentposition = transform.localPosition;
    }
    //tính toán tọa độ bước đi
    private void CalculateMove(string move)
    {
       
        switch (move)
        {
            case "btn_Left(Clone)":
                currentposition = currentposition + new Vector3(-1.7f, 0, 0);
                GameController.Instance.listBtnMain.Add(move);
                futurePosition.Add(currentposition);
                break;
            case "btn_Right(Clone)":
                currentposition = currentposition + new Vector3(1.7f, 0, 0);
                GameController.Instance.listBtnMain.Add(move);
                futurePosition.Add(currentposition);
                break;
            case "btn_Up(Clone)":
                currentposition = currentposition + new Vector3(0, 1.3f, 0);
                GameController.Instance.listBtnMain.Add(move);
                futurePosition.Add(currentposition);
                break;
            case "btn_Down(Clone)":
                currentposition = currentposition + new Vector3(0, -1.3f, 0);
                GameController.Instance.listBtnMain.Add(move);
                futurePosition.Add(currentposition);
                break;
            case "btn_Loop(Clone)":
                LoopScreen loopScreen = GameController.Instance.listScreenAdd[numberLoopScreen].GetComponent<LoopScreen>();
                for (int i = 0; i < loopScreen.loopNumber; i++)
                {
                    for (int j = 0; j < loopScreen.listBtnFor.Count; j++)
                    {
                        CalculateMove(loopScreen.listBtnFor[j].name);
                    }
                }
                numberLoopScreen++;
                break;
            case "btn_If(Clone)":       
               
                    string a = "";
                    IfScreen ifScreen = GameController.Instance.listScreenAdd[ifNumber].GetComponent<IfScreen>();
                if (ifScreen.listBtnIf.Count>0&&ifScreen.listBtndoIf.Count>0)
                {
                    for (int j = 0; j < ifScreen.listBtnIf.Count; j++)
                    {
                        switch (ifScreen.listBtnIf[j].name)
                        {
                            case "btn_Left(Clone)":
                                a = a + "Left";
                                break;
                            case "btn_Right(Clone)":
                                a = a + "Right";
                                break;
                            case "btn_Down(Clone)":
                                a = a + "Down";
                                break;
                            case "btn_Up(Clone)":
                                a = a + "Up";
                                break;
                            case "btn_X(Clone)":
                                a = a + "X";
                                break;
                        }
                    }
                    if (string.Equals(a, PopupManager.Instance.currentLevel.ifMove[ifJson]))
                    {
                        for (int k = 0; k < ifScreen.listBtndoIf.Count; k++)
                        {
                            CalculateMove(ifScreen.listBtndoIf[k].name);
                        }
                    }
                    ifJson++;
                }
                ifNumber++;
                break;
        }
    }
    public void playCharacter()
    {
        futurePosition.Clear();

        GameController.Instance.listBtnMain.Clear();
        for (int i = 0; i < GameController.Instance.listButton.Count; i++)
        {
            CalculateMove(GameController.Instance.listButton[i].name);
        }
        GameController.Instance.run = true;
        AudioManager.Instance.PlaySound(Sound.FootStep);
    }
    private void Update()
    {
        if (GameController.Instance.run == true )
        {
            if(GameController.Instance.listBtnMain.Count > 0)
            {
                switch (GameController.Instance.listBtnMain[cursorInMainList])
                {
                    case "btn_Left(Clone)":
                        GetComponent<SpriteRenderer>().flipX = false;
                        ChangeAnimationState("walk_Side");
                        break;
                    case "btn_Right(Clone)":
                        GetComponent<SpriteRenderer>().flipX = true;
                        ChangeAnimationState("walk_Side");
                        break;
                    case "btn_Up(Clone)":
                        ChangeAnimationState("walk_Up");
                        break;
                    case "btn_Down(Clone)":
                        ChangeAnimationState("walk_Down");
                        break;
                }
                //di chuyển nhân vật
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, futurePosition[cursorInMainList], speed * Time.deltaTime);
                if (transform.localPosition == futurePosition[cursorInMainList])
                {
                    cursorInMainList++;
                }
                //Check xem đã hết list bước phải đi chưa sau đó chạy anim idle
                if (cursorInMainList == futurePosition.Count)
                {
                    GameController.Instance.run = false;
                    if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("walk_Up"))
                    {
                        ChangeAnimationState("idle_Up");
                    }
                    else if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("walk_Down"))
                    {
                        ChangeAnimationState("idle_Down");
                    }
                    else if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("walk_Side"))
                    {
                        if (GameController.Instance.listBtnMain[cursorInMainList - 1] == "btn_Right(Clone)")
                        {
                            GetComponent<SpriteRenderer>().flipX = true;
                            ChangeAnimationState("idle_Side");
                        }
                        else if (GameController.Instance.listBtnMain[cursorInMainList - 1] == "btn_Left(Clone)")
                        {
                            GetComponent<SpriteRenderer>().flipX = false;
                            ChangeAnimationState("idle_Side");
                        }
                    }
                    if (checkFootStep == false && checkReplay == false)
                    {
                        AudioManager.Instance.StopEffect();
                        checkFootStep = true;
                        Invoke("Replay", 0.6f);
                    }
                }
            }
            else
            {
                Invoke("Replay", 0.5f);
            }
        }
    }
    //đổi Anim
    private void ChangeAnimationState(string newAnimation)
    {
        if (currentAnimaton == newAnimation) return;

        GetComponent<Animator>().Play(newAnimation);
        currentAnimaton = newAnimation;
    }
    //Chơi lại
    private void Replay()// Hàm được gọi bằng Ivoke nên khi findAll refence sẽ ko tìm thấy nơi sử dụng(Lưu ý ko xóa nhầm)
    {
        GameController.Instance.run = false;
        AudioManager.Instance.PlaySound(Sound.Button);
        Destroy(PopupManager.Instance.currentMap);
        Destroy(PopupManager.Instance.currentDashboard.gameObject);
        Destroy(gameObject);
        PopupManager.Instance.currentMap = Instantiate(PopupManager.Instance.mapToReload);
        PopupManager.Instance.currentDashboard = Instantiate(PopupManager.Instance.userPlay, PopupManager.Instance.canvas.transform);
        GameController.Instance.ResetGameController();

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Enemy":
                GetComponent<Animator>().Play("die");
                /*   GameObject panelLose = Instantiate(PopupManager.Instance.panel_Finish.gameObject, PopupManager.Instance.canvas.transform);
                   panelLose.GetComponent<PanelFinish>().configView(false);*/
                GameController.Instance.run = false;
                Invoke("Replay", 0.8f);
                break;
            case "Finish":
                finishNumber++;
                if (finishNumber == PopupManager.Instance.currentLevel.finishZone)
                {
                    GetComponent<Animator>().Play("happy");
                    GameObject panelWin = Instantiate(PopupManager.Instance.panel_Finish.gameObject, PopupManager.Instance.canvas.transform);
                    collision.gameObject.transform.GetChild(1).gameObject.SetActive(true);
                    AudioManager.Instance.StopEffect();
                    AudioManager.Instance.PlaySound(Sound.Teleport);
                    panelWin.GetComponent<PanelFinish>().configView(true);
                    GameController.Instance.run = false;
                    collision.gameObject.SetActive(false);
                    checkReplay = true;
                }
                else
                {
                    collision.gameObject.transform.GetChild(1).gameObject.SetActive(true);
                    AudioManager.Instance.PlaySound(Sound.Teleport);
                    LeanTween.alpha(gameObject, 1, 1f).setOnComplete(() => { collision.gameObject.SetActive(false); });
                }
                break;
            case "Boundary":
                GameController.Instance.run = false;
                ChangeAnimationState("idle_Up");
                hitPartical.gameObject.SetActive(true);
                Invoke("Replay", 0.8f);
                break;
            case "Teleport":
                break;
        }
    }
}
