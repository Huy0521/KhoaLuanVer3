
using UnityEngine;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    [SerializeField] private TMP_Text txtTimer;
    public float timeRemaining = 0;
    public float otherPlayerTime = 0;
    public bool timerIsRunning = false;
    public bool otherTimeisRunning = false;
    public float timeToPlay;

    private void Start()
    {
        if(PopupManager.Instance.isArena==false)
        {
            timerIsRunning = true;
        } 
    }
    void Update()
    {
        if (timerIsRunning)
        {
                timeRemaining += Time.deltaTime;
                txtTimer.text = Utility.convertSecondToString(timeRemaining);
        }
        if(otherTimeisRunning==true)
        {
            otherPlayerTime += Time.deltaTime;
        }
    }
    public void stopTime()
    {
        PopupManager.Instance.timeRemaining = timeRemaining;
        timerIsRunning = false;
        timeRemaining = 0;
    }
    public void stopOtherPlayerTime()
    {
        otherTimeisRunning = false;
        otherPlayerTime = 0;
    }
    public void ResetTime()
    {
        timeRemaining = 0; 
        timerIsRunning = true;
    }
    public void ResetOtherTime()
    {
        otherTimeisRunning = true;
        otherPlayerTime = 0;
    }
}
