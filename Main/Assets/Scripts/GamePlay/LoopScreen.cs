using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LoopScreen : MonoBehaviour
{
    public int loopNumber = 0;//Số lần lặp của vòng Loop đó
    public int posInLooplist;//Vị trí index trong listScreenAdđ
    public int vitriloop;//Vị trí index hiện tại trong listPosLoop
    [SerializeField] private Text txt_Loop;//Hiển thị số lần lặp
    public List<GameObject> listPosLoop;//Lưu vị trí để Instantiate nút vào
    [SerializeField] private Panel_DieuKhien panelDieukhien;//truy cập tới panelDieukhien trên scene
    public List<GameObject> listBtnFor;//Lưu nút được dùng trong vòng lặp
    //Chuyển trạng thái sang đang chọn vòng lặp, bật swipe
    private void OnEnable()
    {
        GameController.Instance.chooseBtn = SpecialBtn.loop; 
        SwipeManager.OnSwipeDetected += OnSwipeDetected;
    }
    //Chuyển trạng thái sang default, tắt swipe
    private void OnDisable()
    {
        GameController.Instance.chooseBtn = SpecialBtn.none;
        SwipeManager.OnSwipeDetected -= OnSwipeDetected;
    }
    void OnSwipeDetected(Swipe direction, Vector2 swipeVelocity)
    {
        //Tăng giá trị lặp
        if (direction == Swipe.Up)
        {
            loopNumber++;
            txt_Loop.text = loopNumber.ToString();
        }
        //Giảm giá trị lặp
        if (direction == Swipe.Down)
        {
            if (loopNumber > 0)
            {
               loopNumber--;
                txt_Loop.text = loopNumber.ToString();
            }
        }
    }
    //tắt swipe
    private void OnDestroy()
    {
        SwipeManager.OnSwipeDetected -= OnSwipeDetected;
    }
    //Hiển thị màn hình vòng lặp
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
