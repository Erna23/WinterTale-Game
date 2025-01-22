using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour
{
    public Image black;
    private Color color;
    private WaitForSeconds wait = new WaitForSeconds(0.01f);

    public void FadeOut(float speed = 0.05f){
        StopAllCoroutines();
        StartCoroutine(FadeOutCoroutine(speed));
    }

    IEnumerator FadeOutCoroutine(float speed){
        color = black.color;
        while(color.a < 1f){
            color.a += speed;
            black.color = color;
            yield return wait;
        }
    }

    public void FadeIn(float speed = 0.05f){
        StopAllCoroutines();
        StartCoroutine(FadeInCoroutine(speed));
    }

    IEnumerator FadeInCoroutine(float speed){
        color = black.color;
        color.a = 1f;
        while(color.a > 0f){
            color.a -= speed;
            black.color = color;
            yield return wait;
        }
    }
}
