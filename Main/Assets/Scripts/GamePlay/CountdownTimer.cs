
using UnityEngine;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    [SerializeField] private TMP_Text txtTimer;
    public float timeRemaining = 0;
    public bool timerIsRunning = false;
    public float timeToPlay;

    private void Start()
    {
        timerIsRunning = true;
    }
    void Update()
    {
        if (timerIsRunning)
        {
                timeRemaining += Time.deltaTime;
                txtTimer.text = Utility.convertSecondToString(timeRemaining);
        }
    }
    public void stopTime()
    {
        PopupManager.Instance.timeRemaining = timeRemaining;
        timerIsRunning = false;
        timeRemaining = 0;
    }
}
