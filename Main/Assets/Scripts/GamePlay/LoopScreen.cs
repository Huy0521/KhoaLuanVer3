using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LoopScreen : MonoBehaviour
{
    public int loopNumber = 0;
    public int posInLooplist;
    public int vitriloop;
    [SerializeField] private Text txt_Loop;
    public List<GameObject> listPosLoop;
    [SerializeField] private Panel_DieuKhien panelDieukhien;
    public List<GameObject> listBtnFor;
    private void OnEnable()
    {
        GameController.Instance.chooseBtn = SpecialBtn.loop; 
        SwipeManager.OnSwipeDetected += OnSwipeDetected;
    }
    private void OnDisable()
    {
        GameController.Instance.chooseBtn = SpecialBtn.none;
        SwipeManager.OnSwipeDetected -= OnSwipeDetected;
    }
    void OnSwipeDetected(Swipe direction, Vector2 swipeVelocity)
    {

        if (direction == Swipe.Up)
        {
            loopNumber++;
            txt_Loop.text = loopNumber.ToString();
        }
        if (direction == Swipe.Down)
        {
            if (loopNumber > 0)
            {
               loopNumber--;
                txt_Loop.text = loopNumber.ToString();
            }
        }
    }
    private void OnDestroy()
    {
        SwipeManager.OnSwipeDetected -= OnSwipeDetected;
    }
    public void ShowLoopScreen()
    {
        for(int i=0;i<GameController.Instance.listScreenAdd.Count;i++)
        {
            GameController.Instance.listScreenAdd[i].SetActive(false);
        }    
        gameObject.SetActive(true);
        panelDieukhien.posOfList = posInLooplist;
    }
}
