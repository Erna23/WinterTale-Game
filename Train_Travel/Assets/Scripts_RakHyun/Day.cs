using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day : MonoBehaviour
{
    public List<GameObject> btnDay;
    public List<GameObject> btnScene;
    private int day_count;
    public  int bgm1;
    public int bgm2;
    void Start()
    {
        day_count = PlayerPrefs.GetInt("DayCount", 0);
        foreach (var btn in btnDay)
        {
            btn.SetActive(false);
        }
        if(day_count <= 2){
            BGM.instance.Play(bgm1);
        }
        else if(day_count > 2){
            BGM.instance.Play(bgm2);
        }
    }

    public void DayActive(){
        EffectSound.instance.Play(0);
        btnDay[day_count].SetActive(true);
    }

    public void Cancel(){
        EffectSound.instance.Play(0);
        btnDay[day_count].SetActive(false);
    }

    public void Yes(){
        EffectSound.instance.Play(0);
        btnDay[day_count].SetActive(false);
        btnScene[day_count].SetActive(true);
    }
}
