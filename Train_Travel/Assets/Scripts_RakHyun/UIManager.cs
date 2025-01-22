using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text day;
    public Text location;
    public List<string> locations;
    void Start()
    {
        int day_count = PlayerPrefs.GetInt("DayCount", 0);
        day.text = "Day " + day_count.ToString();
        location.text = locations[day_count];
    }
}
