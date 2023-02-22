    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class swipeNumberLoop : MonoBehaviour
{
    [SerializeField] private Text txt_Loop;
    void Start()
    {
        SwipeManager.OnSwipeDetected += OnSwipeDetected;
    }
    void OnSwipeDetected(Swipe direction, Vector2 swipeVelocity)
    {

        if (direction == Swipe.Up)
        {
            GameController.Instance.loopNumber++;
            txt_Loop.text = GameController.Instance.loopNumber.ToString();
        }
        if (direction == Swipe.Down)
        {
            if (GameController.Instance.loopNumber > 0)
            {
                GameController.Instance.loopNumber--;
                txt_Loop.text = GameController.Instance.loopNumber.ToString();
            }
        }
    }
    private void OnDestroy()
    {
        SwipeManager.OnSwipeDetected -= OnSwipeDetected;
    }
}

