using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagActive : MonoBehaviour
{
    public GameObject btnMiniGame;
    public GameObject theBag;
    public GameObject btnMain;
    public GameObject btnDay;

    public void Active(){
        EffectSound.instance.Play(0);
        btnMiniGame.SetActive(false);
        theBag.SetActive(true);
        btnMain.SetActive(true);
        btnDay.SetActive(false);
    }

    public void UnActive(){
        EffectSound.instance.Play(0);
        btnMiniGame.SetActive(true);
        theBag.SetActive(false);
        btnMain.SetActive(false);
        btnDay.SetActive(true);
    }
}
