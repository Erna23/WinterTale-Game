using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BGM : MonoBehaviour
{
    public static BGM instance;
    public AudioClip[] clips;
    private AudioSource source;
    private WaitForSeconds waitTime = new WaitForSeconds(0.01f);
    private bool loop = true;

    private void Awake() {
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            source = GetComponent<AudioSource>();
        }
        else{
            Destroy(this.gameObject);
        }
    }
    
    void Start()
    {
        //source = GetComponent<AudioSource>();
    }

    public void Play(int playMusicTrack){
        source.volume = 0.5f;
        source.loop = loop;
        source.clip = clips[playMusicTrack];
        source.Play();
    }

    public void Stop(){
        source.Stop();
    }

    public void FadeOutMusic(){
        StopAllCoroutines();
        StartCoroutine(FadeOutMusicCoroutine());
    }

    IEnumerator FadeOutMusicCoroutine(){
        for(float i = 1.0f; i >= 0f; i -= 0.01f){
            source.volume = i;
            yield return waitTime;
        }
    }

    public void FadeInMusic(){
        StopAllCoroutines();
        StartCoroutine(FadeInMusicCoroutine());
    }

    IEnumerator FadeInMusicCoroutine(){
        for(float i = 0.01f; i <= 1.0f; i += 0.01f){
            source.volume = i;
            yield return waitTime;
        }
    }
}