using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSound : MonoBehaviour
{
    public static EffectSound instance;
    public AudioClip[] soundEffects; // 여러 효과음을 담는 배열
    private AudioSource source;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        source = GetComponent<AudioSource>();
        if (source == null)
        {
            source = gameObject.AddComponent<AudioSource>();
        }
    }

    // 효과음을 재생하는 함수
    public void Play(int soundIndex)
    {
        source.PlayOneShot(soundEffects[soundIndex]);
    }
}
