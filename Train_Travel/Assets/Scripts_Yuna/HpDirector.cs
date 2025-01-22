using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HpDirector : MonoBehaviour
{
    GameObject sh_gage;
    GameObject js_gage;
    GameObject ej_gage;
    GameObject shn_gage;

    // Start is called before the first frame update
    void Start()
    {
        this.sh_gage = GameObject.Find("sh_gage");
        this.js_gage = GameObject.Find("js_gage");
        this.ej_gage = GameObject.Find("ej_gage");
        this.shn_gage = GameObject.Find("shn_gage");
        UpdateGages();
    }

    void UpdateGages()
    {
        float amount1 = PlayerPrefs.GetInt("NPC1_Favor", 0) / 50f;
        float amount2 = PlayerPrefs.GetInt("NPC2_Favor", 0) / 50f;
        float amount3 = PlayerPrefs.GetInt("NPC3_Favor", 0) / 50f;
        float amount4 = PlayerPrefs.GetInt("NPC4_Favor", 0) / 50f;

        this.sh_gage.GetComponent<Image>().fillAmount = Mathf.Clamp01(amount1);
        this.js_gage.GetComponent<Image>().fillAmount = Mathf.Clamp01(amount2);
        this.ej_gage.GetComponent<Image>().fillAmount = Mathf.Clamp01(amount3);
        this.shn_gage.GetComponent<Image>().fillAmount = Mathf.Clamp01(amount4);
    }

    public void shGage()
    {
        UpdateGage(this.sh_gage, 0.1f);
    }

    public void jsGage()
    {
        UpdateGage(this.js_gage, 0.1f);
    }

    public void ejGage()
    {
        UpdateGage(this.ej_gage, 0.1f);
    }

    public void shnGage()
    {
        UpdateGage(this.shn_gage, 0.1f);
    }
    
    void UpdateGage(GameObject gage, float increment)
    {
        Image gageImage = gage.GetComponent<Image>();
        gageImage.fillAmount = Mathf.Clamp01(gageImage.fillAmount + increment);
    }

}
