using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snow_Korea : MonoBehaviour
{
    public ParticleSystem snow;
    void Start()
    {
        snow = GetComponent<ParticleSystem>();
        int day_count = PlayerPrefs.GetInt("DayCount", 0);
        if(day_count == 0 || day_count == 1 || day_count == 2 || day_count == 5){
            snow.Play();
        }
        else{
            snow.Stop();
        }
    }
}