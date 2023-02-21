using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
public enum Sound { Start, Button, Win, Lose, Teleport, FootStep }
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AudioSource musicSource, effectsSource;
    [SerializeField] private AudioClip startGame;
    [SerializeField] private AudioClip buttonSound;
    [SerializeField] private AudioClip winSound;
    [SerializeField] private AudioClip loseSound;
    [SerializeField] private AudioClip teleport;
    [SerializeField] private AudioClip footStep;
    private void Awake()
    {
        if (Instance == null)
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
                musicSource.clip = startGame;
                musicSource.Play();
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
            case Sound.Teleport:
                effectsSource.PlayOneShot(teleport);
                break;
            case Sound.FootStep:
                effectsSource.clip = footStep;
                effectsSource.Play();
                break;
        }
    }
    public void StopEffect()
    {
        effectsSource.Stop();
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
