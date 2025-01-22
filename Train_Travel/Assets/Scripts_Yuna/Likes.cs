using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Yarn.Unity;
using Yarn.Unity.ActionAnalyser;

public class Likes : MonoBehaviour
{
    public Text LikeText;  
    public GameObject sh_gage;
    public GameObject js_gage;
    public GameObject ej_gage;
    public GameObject shn_gage;
    public float displayTime = 1f;

    public GameObject sh;
    public GameObject sh_cr;
    public GameObject sh_em;
    public GameObject sh_sm;

    public GameObject js;
    public GameObject js_cr;
    public GameObject js_em;
    public GameObject js_sm;

    public GameObject ej;
    public GameObject ej_cr;
    public GameObject ej_em;
    public GameObject ej_sm;

    public GameObject shn;
    public GameObject shn_cr;
    public GameObject shn_em;
    public GameObject shn_sm;
    Dictionary<string, GameObject> characterDictionary;
    public DialogueRunner dialogueRunner;
    public static int day_count = 0;
    Color originalColor;
    public AudioSource audioSource;
    public GameObject DialogueBubble;
    public GameObject itemPop;
    public float itemPopDisplayTime = 4f;

    AudioSource bgmAudioSource;
    AudioSource sfxAudioSource;

    void Awake()
    {
        characterDictionary = new Dictionary<string, GameObject>
        {
            { "sh", sh }, { "sh_cr", sh_cr }, { "sh_em", sh_em }, { "sh_sm", sh_sm },
            { "js", js }, { "js_cr", js_cr }, { "js_em", js_em }, { "js_sm", js_sm },
            { "shn", shn }, { "shn_cr", shn_cr }, { "shn_em", shn_em }, { "shn_sm", shn_sm },
            { "ej", ej }, { "ej_cr", ej_cr }, { "ej_em", ej_em }, { "ej_sm", ej_sm }
        };
        GameObject background = GameObject.Find("Background");
        if (background != null)
        {
            bgmAudioSource = background.AddComponent<AudioSource>();
            sfxAudioSource = background.AddComponent<AudioSource>();
        }

        dialogueRunner = FindObjectOfType<DialogueRunner>();
        dialogueRunner.AddCommandHandler<string>("IncreaseLikes", IncreaseLikes);
        dialogueRunner.AddCommandHandler("CheckEnding", CheckEnding);
        dialogueRunner.AddCommandHandler<string>("ShowCharacter", ShowCharacter);
        dialogueRunner.AddCommandHandler<string>("HideCharacter", HideCharacter);
        dialogueRunner.AddCommandHandler("CheckDay", CheckDay);
        dialogueRunner.AddCommandHandler<string, string>("ChangeImage", ChangeImage);
        dialogueRunner.AddCommandHandler<string>("ChangeImage2", ChangeImage2);
        dialogueRunner.AddCommandHandler("ChatColor", ChatColor);
        dialogueRunner.AddCommandHandler("ResetColor", ResetColor);
        dialogueRunner.AddCommandHandler<string>("PlayAudio1", PlayAudio1);
        dialogueRunner.AddCommandHandler<string>("PlayAudio2", PlayAudio2);
        dialogueRunner.AddCommandHandler<string>("ChangeEndingImage", ChangeEndingImage);
        dialogueRunner.AddCommandHandler("ShowItemPop", ShowItemPop);
        
    }

    [YarnCommand("Increase_Likes")]
    public void IncreaseLikes(string npc)
    {
        if (npc == "sh")
        {
            //GameObject ej_gage = GameObject.Find("ej_gage");
            sh_gage.GetComponent<HpDirector>().shGage();
            IncreaseFavor("NPC1", 5);
            ShowLikeText("차승호 +5");
        }
        else if (npc == "js")
        {
            //GameObject ej_gage = GameObject.Find("ej_gage");
            js_gage.GetComponent<HpDirector>().jsGage();
            IncreaseFavor("NPC2", 5);
            ShowLikeText("윤지수 +5");
        }
        else if (npc == "ej")
        {
            //GameObject ej_gage = GameObject.Find("ej_gage");
            ej_gage.GetComponent<HpDirector>().ejGage();
            IncreaseFavor("NPC3", 5);
            ShowLikeText("한은지 +5");
        }
        else if (npc == "shn")
        {
            //GameObject ej_gage = GameObject.Find("ej_gage");
            shn_gage.GetComponent<HpDirector>().shnGage();
            IncreaseFavor("NPC4", 5);
            ShowLikeText("김성훈 +5");
        }
    }

    void IncreaseFavor(string npc, int amount)
    {
        int currentFavor = PlayerPrefs.GetInt(npc + "_Favor", 0);
        currentFavor += amount;
        PlayerPrefs.SetInt(npc + "_Favor", currentFavor);
        PlayerPrefs.Save();

        Debug.Log(npc + "의 호감도가 " + amount + "만큼 증가하여 " + currentFavor + "이 되었습니다.");
    }

    void ShowLikeText(string message)
    {
        LikeText.text = message;
        LikeText.gameObject.SetActive(true);
        StartCoroutine(HideLikeTextAfterDelay());
    }
    IEnumerator HideLikeTextAfterDelay()
    {
        yield return new WaitForSeconds(displayTime);
        LikeText.gameObject.SetActive(false);
    }

    [YarnCommand("Check_Ending")]
    public void CheckEnding()
    {
        int sh_likes = PlayerPrefs.GetInt("NPC1_Favor", 0);
        int js_likes = PlayerPrefs.GetInt("NPC2_Favor", 0);
        int ej_likes = PlayerPrefs.GetInt("NPC3_Favor", 0);
        int shn_likes = PlayerPrefs.GetInt("NPC4_Favor", 0);

        List<int> likesList = new List<int> { sh_likes, js_likes, ej_likes, shn_likes };
        int maxLikes = Mathf.Max(likesList.ToArray());

        if (sh_likes < 50 && js_likes < 50 && ej_likes < 50 && shn_likes < 50)
        {
            SetEnding("GameScene6");
        }
        else
        {
            if (sh_likes == maxLikes && sh_likes > 0)
            {
                SetEnding("GameScene6_sh");
            }
            else if (js_likes == maxLikes && js_likes > 0)
            {
                SetEnding("GameScene6_js");
            }
            else if (ej_likes == maxLikes && ej_likes > 0)
            {
                SetEnding("GameScene6_ej");
            }
            else if (shn_likes == maxLikes && shn_likes > 0)
            {
                SetEnding("GameScene6_shn");
            }
            else
            {
                Debug.LogWarning("No valid ending was found!");
            }
        }
    }

    void SetEnding(string endingSceneName)
    {
        PlayerPrefs.SetInt(endingSceneName, 1);
        PlayerPrefs.Save();
        LoadEndingScene(endingSceneName);
    }

    void LoadEndingScene(string endingSceneName)
    {
        SceneManager.LoadScene(endingSceneName);
    }

    [YarnCommand("Show_Character")]
    public void ShowCharacter(string characterName)
    {
        foreach (var character in characterDictionary.Values)
        {
            character.SetActive(false);
        }

        if (characterDictionary.ContainsKey(characterName))
        {
            characterDictionary[characterName].SetActive(true);
        }
        else
        {
            Debug.LogWarning($"Character {characterName} not found!");
        }
    }

    [YarnCommand("Hide_Character")]
    public void HideCharacter(string characterName)
    {
        if (characterDictionary.ContainsKey(characterName))
        {
            characterDictionary[characterName].SetActive(false);
        }
        else
        {
            Debug.LogWarning($"Character {characterName} not found!");
        }
    }

    [YarnCommand("Check_Day")]
    public void CheckDay(){
        day_count = PlayerPrefs.GetInt("DayCount", 0);
        //day_count 증가
        day_count++;
        //증가된 값을 저장
        PlayerPrefs.SetInt("DayCount", day_count);
        PlayerPrefs.Save();
        Debug.LogWarning($"Day Count: {day_count}");
    }

    [YarnCommand("Change_image")]
    public void ChangeImage(string spriteName, string sizeOption = "default")
    {
        GameObject background = GameObject.Find("Background");

        if (background != null)
        {
            Vector3 newScale = Vector3.one; // 기본값
            switch (sizeOption.ToLower())
            {
                case "기차내부":
                    newScale = new Vector3(1.32f, 1.12f, 1f);
                    break;
                case "기차외부":
                    newScale = new Vector3(1.15f, 1f, 1f);
                    break;
                case "수면실":
                    newScale = new Vector3(1.17f, 1f, 1f);
                    break;
                case "창밖":
                    newScale = new Vector3(0.66f, 0.85f, 1f);
                    break;
                case "default":
                default:
                    newScale = new Vector3(1.75f, 1f, 1f);
                    break;
            }

            background.transform.localScale = newScale;
            SpriteRenderer spriteRenderer = background.GetComponent<SpriteRenderer>();
            Sprite newSprite = Resources.Load<Sprite>($"Background/{spriteName}");

            if (newSprite != null)
            {
                spriteRenderer.sprite = newSprite;
            }
            else
            {
                Debug.LogWarning($"{spriteName} 이미지를 Resources 폴더에서 찾을 수 없습니다.");
            }
        }
        else
        {
            Debug.LogWarning("Background 오브젝트를 찾을 수 없습니다.");
        }
    }

    [YarnCommand("Change_image2")]
    public void ChangeImage2(string spriteName)
    {
        GameObject background = GameObject.Find("Background");

        if (background != null)
        {
            background.transform.localScale = new Vector3(0.53f, 0.53f, 1f);
            SpriteRenderer spriteRenderer = background.GetComponent<SpriteRenderer>();
            Sprite newSprite = Resources.Load<Sprite>($"Background/{spriteName}");

            if (newSprite != null)
            {
                spriteRenderer.sprite = newSprite;
            }
            else
            {
                Debug.LogWarning($"{spriteName} 이미지를 Resources 폴더에서 찾을 수 없습니다.");
            }
        }
        else
        {
            Debug.LogWarning("Background 오브젝트를 찾을 수 없습니다.");
        }
    }

    [YarnCommand("Chat_Color")]
    public void ChatColor()
    {
        GameObject chat = GameObject.Find("chat");

        if (chat != null)
        {
            Image chatImage = chat.GetComponent<Image>();

            if (chatImage != null)
            {
                originalColor = chatImage.color;
                chatImage.color = new Color(0.909f, 0.898f, 0.082f, 0.518f);
            }
            else
            {
                Debug.LogWarning("chat 오브젝트에 Image 컴포넌트가 없습니다.");
            }
        }
        else
        {
            Debug.LogWarning("chat 오브젝트를 찾을 수 없습니다.");
        }
    }

    [YarnCommand("Reset_Color")]
    public void ResetColor()
    {
        GameObject chat = GameObject.Find("chat");

        if (chat != null)
        {
            Image chatImage = chat.GetComponent<Image>();

            if (chatImage != null)
            {
                chatImage.color = originalColor;
            }
            else
            {
                Debug.LogWarning("chat 오브젝트에 Image 컴포넌트가 없습니다.");
            }
        }
        else
        {
            Debug.LogWarning("chat 오브젝트를 찾을 수 없습니다.");
        }
    }


    [Yarn.Unity.YarnCommand("Play_Audio1")]
    public void PlayAudio1(string clipName)
    {
        AudioClip clipToPlay = Resources.Load<AudioClip>($"Audio/{clipName}");

        if (clipToPlay != null)
        {
            bgmAudioSource.clip = clipToPlay;
            bgmAudioSource.loop = true;
            bgmAudioSource.Play();
        }
        else
        {
            Debug.LogWarning($"Resources/Audio 폴더에서 '{clipName}' 오디오 파일을 찾을 수 없습니다.");
        }
    }

    [Yarn.Unity.YarnCommand("Play_Audio2")]
    public void PlayAudio2(string clipName)
    {
        AudioClip clipToPlay = Resources.Load<AudioClip>($"Audio/{clipName}");

        if (clipToPlay != null)
        {
            sfxAudioSource.PlayOneShot(clipToPlay);
        }
        else
        {
            Debug.LogWarning($"Resources/Audio 폴더에서 '{clipName}' 오디오 파일을 찾을 수 없습니다.");
        }
    }

    [YarnCommand("Change_EndingImage")]
    public void ChangeEndingImage(string spriteName)
    {
        GameObject background = GameObject.Find("Background");

        if (background != null)
        {
            background.transform.localScale = new Vector3(0.53f, 0.53f, 1f);

            SpriteRenderer spriteRenderer = background.GetComponent<SpriteRenderer>();
            Sprite originalSprite = spriteRenderer.sprite;
            Sprite newSprite = Resources.Load<Sprite>($"Background/{spriteName}");

            if (newSprite != null)
            {
                spriteRenderer.sprite = newSprite;
                DialogueBubble.SetActive(false);
                StartCoroutine(ResetImageAfterDelay(spriteRenderer, originalSprite, 4f));
            }
            else
            {
                Debug.LogWarning($"{spriteName} 이미지를 Resources 폴더에서 찾을 수 없습니다.");
            }
        }
        else
        {
            Debug.LogWarning("Background 오브젝트를 찾을 수 없습니다.");
        }
        
    }

    private IEnumerator ResetImageAfterDelay(SpriteRenderer spriteRenderer, Sprite originalSprite, float delay)
    {
        yield return new WaitForSeconds(delay);
        spriteRenderer.sprite = originalSprite;
    }

    [Yarn.Unity.YarnCommand("Show_ItemPop")]
    public void ShowItemPop()
    {
        if (itemPop != null)
        {
            itemPop.SetActive(true);
            StartCoroutine(HideItemPopAfterDelay());
        }
        else
        {
            Debug.LogWarning("ItemPop GameObject가 설정되지 않았습니다.");
        }
    }
    private IEnumerator HideItemPopAfterDelay()
    {
        yield return new WaitForSeconds(itemPopDisplayTime);
        if (itemPop != null)
        {
            itemPop.SetActive(false);
        }
    }

    
}
