using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Sound { Start,Button,Win,Lose }
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [SerializeField] private AudioSource musicSource,effectsSource;
    [SerializeField] private AudioClip startGame;
    [SerializeField] private AudioClip buttonSound;
    [SerializeField] private AudioClip winSound;
    [SerializeField] private AudioClip loseSound;
    private void Awake()
    {
        if(Instance==null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void PlaySound(Sound _sound)
    {
        switch (_sound)
        {
            case Sound.Start:
                musicSource.PlayOneShot(startGame);
                break;
            case Sound.Button:
                effectsSource.PlayOneShot(buttonSound);
                break;
            case Sound.Win:
                musicSource.PlayOneShot(winSound);
                break;
            case Sound.Lose:
                effectsSource.PlayOneShot(loseSound);
                break;
        }
    }
    public void ChangeMusicVolume(float value)
    {
        musicSource.volume = value;
    }
    public void ChangeEffectVolume(float value)
    {
       effectsSource.volume = value;
    }
}
