using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour
{
    public GameObject train;
    public GameObject day;
    public GameObject minigame;
    public GameObject day_btn;
    public GameObject minigame_btn;
    private int day_count;
    private void Awake() {
        day.SetActive(false);
        minigame.SetActive(false);
        day_btn.SetActive(false);
        minigame_btn.SetActive(false);
    }
    void Start()
    {
        Animator animator = train.GetComponent<Animator>();
        animator.SetBool("Move", true);
        StartCoroutine(UICoroutine());
    }

    IEnumerator UICoroutine(){
        yield return new WaitForSeconds(1.5f);
        day.SetActive(true);
        minigame.SetActive(true);
        day_btn.SetActive(true);
        minigame_btn.SetActive(true);
    }
}
