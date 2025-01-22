using UnityEngine;

public class DayManager : MonoBehaviour
{
    public int currentDay = 0;
    public GameObject[] popups;

    void Start()
    {
        UpdatePopup();
    }

    public void UpdatePopup()
    {
        foreach (GameObject popup in popups)
        {
            popup.SetActive(false);
        }

        if (currentDay >= 1 && currentDay <= popups.Length)
        {
            popups[currentDay - 1].SetActive(true);
        }
    }

    public void SetDay(int day)
    {
        currentDay = day;
        UpdatePopup();
    }
}
