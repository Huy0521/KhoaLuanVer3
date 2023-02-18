using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static int move;
    private string currentAnimaton;
    public float speed = 6;
    public List<Vector3> futurePosition;
    private Vector3 currentposition;
    private int cursorInMainList = 0;
    private int check = 0;
    [SerializeField] private ParticleSystem hitPartical;
    private bool checkFootStep = false;
    private int finishNumber;
    private void Awake()
    {
        currentposition = transform.localPosition;
    }
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
                for (int i = 0; i < GameController.Instance.loopNumber; i++)
                {
                    for (int j = 0; j < GameController.Instance.listBtnFor.Count; j++)
                    {
                        CalculateMove(GameController.Instance.listBtnFor[j].name);
                    }
                }
                break;
            case "btn_If(Clone)":

                for (int i = 0; i < GameController.Instance.listBtnIf.Count; i++)
                {
                    if (string.Equals(GameController.Instance.listShadedIf[i], GameController.Instance.listBtnIf[i].name))
                    {
                        check++;
                    }
                }
                if (check == GameController.Instance.listShadedIf.Count)
                {
                    for (int i = 0; i < GameController.Instance.listBtndoIf.Count; i++)
                    {
                        CalculateMove(GameController.Instance.listBtndoIf[i].name);
                    }
                }
                break;
        }
    }
    public void playCharacter()
    {
        futurePosition.Clear();
        GameController.Instance.listBtnMain.Clear();
        if (GameController.Instance.listButton.Count > 0)
        {
            for (int i = 0; i < GameController.Instance.listButton.Count; i++)
            {
                CalculateMove(GameController.Instance.listButton[i].name);
            }
            GameController.Instance.run = true;
            AudioManager.Instance.PlaySound(Sound.FootStep);
        }
    }
    private void Update()
    {
        if (GameController.Instance.run == true)
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

            transform.localPosition = Vector3.MoveTowards(transform.localPosition, futurePosition[cursorInMainList], speed * Time.deltaTime);
            if (transform.localPosition == futurePosition[cursorInMainList])
            {
                cursorInMainList++;
            }
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
                if (!checkFootStep)
                {
                    AudioManager.Instance.StopEffect();
                    checkFootStep = true;
                }
            }
        }
    }
    private void ChangeAnimationState(string newAnimation)
    {
        if (currentAnimaton == newAnimation) return;

        GetComponent<Animator>().Play(newAnimation);
        currentAnimaton = newAnimation;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Enemy":
                GetComponent<Animator>().Play("die");
                GameObject panelLose = Instantiate(PopupManager.Instance.panel_Finish.gameObject, PopupManager.Instance.canvas.transform);
                panelLose.GetComponent<PanelFinish>().configView(false);
                GameController.Instance.run = false;
                break;
            case "Finish":
                finishNumber++;
                if(finishNumber == PopupManager.Instance.currentLevel.finishZone)
                {
                    GetComponent<Animator>().Play("happy");
                    GameObject panelWin = Instantiate(PopupManager.Instance.panel_Finish.gameObject, PopupManager.Instance.canvas.transform);
                    collision.gameObject.transform.GetChild(1).gameObject.SetActive(true);
                    AudioManager.Instance.StopEffect();
                    AudioManager.Instance.PlaySound(Sound.Teleport);
                    panelWin.GetComponent<PanelFinish>().configView(true);
                    GameController.Instance.run = false;
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
                break;
        }
    }
}
