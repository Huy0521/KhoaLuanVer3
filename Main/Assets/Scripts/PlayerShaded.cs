using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShaded : MonoBehaviour
{
    Vector3 currentposition;
    public List<Vector3> futurePosition;
    private string currentAnimaton;
    int cursorInMainList = 0;
    public float speed = 6;
    void Awake()
    {
        currentposition = transform.localPosition;
    }
    void Start()
    {
        StartCoroutine(playCharacter());
    }
    void CalculateMove(string move)
    {
        switch (move)
        {
            case "btn_Left(Clone)":
                currentposition = currentposition + new Vector3(-2.5f, 0, 0);
                futurePosition.Add(currentposition);
                break;
            case "btn_Right(Clone)":
                currentposition = currentposition + new Vector3(2.5f, 0, 0);
                futurePosition.Add(currentposition);
                break;
            case "btn_Up(Clone)":
                currentposition = currentposition + new Vector3(0, 1.9f, 0);
                futurePosition.Add(currentposition);
                break;
            case "btn_Down(Clone)":
                currentposition = currentposition + new Vector3(0, -1.9f, 0);
                futurePosition.Add(currentposition);
                break;
        }
    }
     IEnumerator playCharacter()
    {
        futurePosition.Clear();
        yield return new WaitForEndOfFrame();
        if (GameController.Instance.listButton.Count > 0)
        {
            for (int i = 0; i < GameController.Instance.listButton.Count; i++)
            {
                CalculateMove(GameController.Instance.listButton[i].name);

            }
            GameController.Instance.shadedRun = true;
        }

    }
    private void Update()
    {
        if (GameController.Instance.shadedRun == true)
        {
            if (cursorInMainList < GameController.Instance.listButton.Count)
            {
                    switch (GameController.Instance.listButton[cursorInMainList].name)
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
                        GameController.Instance.shadedRun = false;
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
                            if (GameController.Instance.listButton[cursorInMainList - 1].name == "btn_Right(Clone)")
                            {
                                GetComponent<SpriteRenderer>().flipX = true;
                                ChangeAnimationState("idle_Side");
                            }
                            else if (GameController.Instance.listButton[cursorInMainList - 1].name == "btn_Left(Clone)")
                            {
                                GetComponent<SpriteRenderer>().flipX = false;
                                ChangeAnimationState("idle_Side");
                            }
                        }
                    }
            }
        }
    }
    void ChangeAnimationState(string newAnimation)
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
                Debug.Log("in");
                GameController.Instance.listShadedIf.Add(GameController.Instance.listButton[cursorInMainList+1].name);
                GameController.Instance.listShadedIf.Add("btn_X(Clone)");
                break;
            case "Finish":
            
                break;
            case "Boundary":
                GameController.Instance.shadedRun = false;
                GameController.Instance.listShadedIf.Add(GameController.Instance.listButton[cursorInMainList+1].name);
                Debug.Log("cout"+GameController.Instance.listButton.Count); 
                Debug.Log("coutList " + cursorInMainList);
                Debug.Log(futurePosition[cursorInMainList]);
                Debug.Log("name:"+ GameController.Instance.listButton[cursorInMainList].name);
                GameController.Instance.listShadedIf.Add("btn_X(Clone)");
                Debug.Log("Boud");
                break;
        }
    }
}
