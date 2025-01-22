using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    public List<GameObject> dayPanels;

    void Start(){
        int day_count = PlayerPrefs.GetInt("DayCount", 0);
        if(day_count >= 6){
            PlayerPrefs.DeleteKey("DayCount");
            day_count = PlayerPrefs.GetInt("DayCount", 0);
        }
        Debug.LogWarning(day_count);
        foreach (var panel in dayPanels)
        {
            panel.SetActive(false);
        }
        if (day_count >= 0 && day_count <= dayPanels.Count)
        {
            dayPanels[day_count].SetActive(true);
        }
    }
}
