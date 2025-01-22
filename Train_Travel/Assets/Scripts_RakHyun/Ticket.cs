using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ticket : MonoBehaviour
{
    public List<GameObject> UI;
    public GameObject ticket;
    public GameObject bag;
    public GameObject cancel;
    public List<GameObject> tickets;
    public List<Button> buttons;
    public List<GameObject> images;
    public GameObject background;
    private FadeManager theFade;

    void Start() {
        theFade = FindObjectOfType<FadeManager>();    
    }

    public void Active(){
        EffectSound.instance.Play(0);
        theFade.FadeOut();
        theFade.FadeIn();
        for(int i = 0; i < UI.Count; i++){
            UI[i].SetActive(false);
        }
        ticket.SetActive(true);
        bag.SetActive(true);
        cancel.SetActive(true);
        background.SetActive(true);
        TicketActive(0);
    }

    public void UnActive(){
        EffectSound.instance.Play(0);
        theFade.FadeOut();
        theFade.FadeIn();
        for(int i = 0; i < UI.Count; i++){
            UI[i].SetActive(true);
        }
        ticket.SetActive(false);
        bag.SetActive(false);
        cancel.SetActive(false);
        background.SetActive(false);
    }

    public void TicketActive(int selectedIndex){
        EffectSound.instance.Play(0);
        int index = selectedIndex + 1;
        int favor = PlayerPrefs.GetInt("NPC" + index + "_Favor", 0);
        Debug.LogWarning("NPC" + index + " favor: " + favor);
        for (int i = 0; i < tickets.Count; i++){
            Animator animator = tickets[i].GetComponent<Animator>();
            if (i == selectedIndex){
                //tickets[i].SetActive(true);
                animator.SetBool("Selected", true);
            }
            else{
                animator.SetBool("Selected", false);
                //tickets[i].SetActive(false);
            }
        }
        for(int i = 0; i<images.Count; i++){
            images[i].SetActive(false);
        }
        StartCoroutine(FavorCoroutine(favor));
    }

    IEnumerator FavorCoroutine(int favor){
        yield return new WaitForSeconds(0.3f);
        for (int i = 0; i < images.Count - 1; i++){
            if (favor >= i * 5 && favor < (i + 1) * 5){
                images[i].SetActive(true);
            }
            else{
                images[i].SetActive(false);
            }
        }
        int count = images.Count;
        if(favor >= 50){
            images[count - 1].SetActive(true);
        }
        else if(favor < 50 && favor > 30){
            images[count - 2].SetActive(true);
        }
    }
}
