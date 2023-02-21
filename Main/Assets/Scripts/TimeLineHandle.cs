using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
public class TimeLineHandle : MonoBehaviour
{
    private void Start()
    {
        Invoke("NextScence",5);
    }
    private void NextScence()
    {
        PopupManager.Instance.goFromCutScene = true;
        SceneManager.LoadScene("SampleScene");
    }
}
