using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TextHandler : MonoBehaviour
{
    public GameObject pFinder; //starts active to initiate placing button, set inactive later to prevent messing up scripts

    public GameObject owTxt; //starts active, "disembodied voice" type
    public GameObject asystext; //starts active, less disembodied, but not quite implemented yet

    public GameObject panel; //starts inactive, panel for voice
    public GameObject whiteScreen; //starts inactive, only for transitions

    public GameObject smoke; //starts active, smoke effect

    public GameObject code0; //starts inactive, binary effect
    public GameObject code1; 

    public GameObject YBtn; //starts inactive, choose to continue
    public GameObject NBtn;

    string s; //internal use for text animation
    public GameObject txtArrow; //visual text advance indicator

    public GameObject powerButton; //in world "power button"
    public GameObject drawCanvas; //starts inactive, canvas to begin drawing minigame

    bool Sc1Flag; //flg to indicate that Scene 1 is complete

    string[] Scene1Txt = new string[]
    {
        "Oh, I've finally broken through!", //0
        "Pleased to meet you! I am... well, you can call me your ASYSTANT!",
        "I bet you have a lot of questions, but I think the first thing we should do is stabilize the scene.",
        "Your awakening has messed up a lot of the scenery, as you can see. So, why don't we try to fix the smoke floating around?",
        "You don't need to be a programmer, that's why I'm here! Actually, all you have to do is get rid of the power button.",
        "See how it's glitching out? That's because you shouldn't even be seeing it, the textures and permissions are messing up.",
        "Go up to it and just touch it. That should reset everything!" //6
    };

    string[] Scene2Txt = new string[]
    {
        "Whew, most of it's clearing up! Thank goodness... although... Something still isn't right, I can't materialize my body!", //0
        "This is kind of awful, I don't want to be a disembodied voice for the rest of my life!",
        "Hey, would you mind... making me a body?",
        "It wouldn't be anything too grand! Just, uh... okay, how about this?", //4
        "See that canvas? Try getting close to it and touching it, you should be able to draw me a face!" //4
    };


    void Start()
    {
        StartCoroutine(FadeIn(owTxt.GetComponent<Text>()));
        owTxt.GetComponent<Text>().text = "Look around... touch a floor on your screen...";
    }

    void Update()
    {
        if(Sc1Flag && this.gameObject.GetComponent<buttonAnimator>().fadeComplete())
        {
            whiteScreen.SetActive(true);
            StartCoroutine(FadeImg(whiteScreen.GetComponent<Image>()));
            smoke.SetActive(false);
            StartCoroutine(Scene2());
            Sc1Flag = false;
        }
    }

    // public functions for buttons/external objects to initiate

    public void pwrPressed()
    {
        StartCoroutine(pwrPlaced());
    }

    public void YesPress()
    {
        StartCoroutine(YesPressed());
    }

    public void NoPressed()
    {
        Application.Quit(0);
    }

    // IEnumerator Event Timers/Planners

    IEnumerator pwrPlaced()
    {
        pFinder.SetActive(false);
        StartCoroutine(FadeOut(owTxt.GetComponent<Text>()));
        yield return new WaitForSeconds(0.5f);
        owTxt.GetComponent<Text>().text = "Move closer... what do you think of it?";
        StartCoroutine(FadeIn(owTxt.GetComponent<Text>()));

        yield return new WaitForSeconds(5f);
        StartCoroutine(FadeOut(owTxt.GetComponent<Text>()));
        yield return new WaitForSeconds(0.5f);
        owTxt.GetComponent<Text>().text = "Would you like to ESCAPE?";
        StartCoroutine(FadeIn(owTxt.GetComponent<Text>()));
        yield return new WaitForSeconds(0.5f);
        panel.SetActive(true);
        YBtn.SetActive(true);
        NBtn.SetActive(true);
    }

    IEnumerator YesPressed()
    {
        this.gameObject.GetComponent<buttonAnimator>().onFlipped();
        panel.SetActive(false);
        owTxt.SetActive(false);
        YBtn.SetActive(false);
        NBtn.SetActive(false);
        whiteScreen.SetActive(true);
        StartCoroutine(FadeImg(whiteScreen.GetComponent<Image>()));
        code0.SetActive(true);
        code1.SetActive(true);
        yield return new WaitForSeconds(4f);
        panel.SetActive(true);
        StartCoroutine(Scene1());
    }

    IEnumerator Scene1()
    {
        yield return activeType(asystext.GetComponent<Text>(), Scene1Txt[0]);
        txtArrow.SetActive(true);
        yield return waitForTap();

        txtArrow.SetActive(false);
        yield return activeType(asystext.GetComponent<Text>(), Scene1Txt[1]);
        txtArrow.SetActive(true);
        yield return waitForTap();

        txtArrow.SetActive(false);
        yield return activeType(asystext.GetComponent<Text>(), Scene1Txt[2]);
        txtArrow.SetActive(true);
        yield return waitForTap();

        txtArrow.SetActive(false);
        yield return activeType(asystext.GetComponent<Text>(), Scene1Txt[3]);
        txtArrow.SetActive(true);
        yield return waitForTap();

        txtArrow.SetActive(false);
        yield return activeType(asystext.GetComponent<Text>(), Scene1Txt[4]);
        txtArrow.SetActive(true);
        yield return waitForTap();

        txtArrow.SetActive(false);
        yield return activeType(asystext.GetComponent<Text>(), Scene1Txt[5]);
        txtArrow.SetActive(true);
        yield return waitForTap();

        txtArrow.SetActive(false);
        yield return activeType(asystext.GetComponent<Text>(), Scene1Txt[6]);

        this.gameObject.GetComponent<buttonAnimator>().makeTouchable();
        Sc1Flag = true;

    }

    IEnumerator Scene2()
    {
        //make pwrbutton glow or something and then vanish
        //try and get the haptics working
        //ASYSTANT tells player that now that things are stable they would like a physical representation
        //use finger to draw a little friendly robot, it stays in the corner. theyve got a little key hand.

        yield return activeType(asystext.GetComponent<Text>(), Scene2Txt[0]);
        txtArrow.SetActive(true);
        yield return waitForTap();

        txtArrow.SetActive(false);
        yield return activeType(asystext.GetComponent<Text>(), Scene2Txt[1]);
        txtArrow.SetActive(true);
        yield return waitForTap();

        txtArrow.SetActive(false);
        yield return activeType(asystext.GetComponent<Text>(), Scene2Txt[2]);
        txtArrow.SetActive(true);
        yield return waitForTap();

        txtArrow.SetActive(false);
        yield return activeType(asystext.GetComponent<Text>(), Scene2Txt[3]);
        txtArrow.SetActive(true);
        yield return waitForTap();

        drawCanvas.SetActive(true);

        txtArrow.SetActive(false);
        yield return activeType(asystext.GetComponent<Text>(), Scene2Txt[4]);
        drawCanvas.GetComponent<canvasTrigger>().makeTouchable();
    }

    // IEnumerator Helper Processes

    public IEnumerator FadeIn(Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / 0.5f));
            yield return null;
        }
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
    }

    public IEnumerator FadeOut(Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / 0.5f));
            yield return null;
        }
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
    }

    public IEnumerator FadeImg(Image i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / 5f));
            yield return null;
        }
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
    }

    IEnumerator activeType(Text toPrint, string src)
    {
        toPrint.text = "";
        int s = 0;
        foreach (char c in src)
        {
            if (s == 3)
            {
                if (Input.GetKey(KeyCode.A) || Input.touchCount > 0)
                {
                    toPrint.text = src;
                    yield return new WaitForSeconds(1f);
                    yield break;
                }
                s = 0;
            }
            
            toPrint.text += c;
            s++;
            yield return new WaitForSeconds(0.02f);
        }
    }

    IEnumerator waitForTap()
    {
        while(true)
        {
            if (Input.GetKeyDown(KeyCode.A) || Input.touchCount > 0)
            {
                if (txtArrow.activeSelf)
                {
                    yield break;
                }
            }

            yield return null;
        }
    }
}
