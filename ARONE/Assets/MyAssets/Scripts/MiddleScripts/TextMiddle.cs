using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class TextMiddle : MonoBehaviour
{
    public GameObject asystext; //starts active, less disembodied, but not quite implemented yet

    public GameObject floorFinder; //starts inactive, becomes active shorly after start

    public GameObject panel; //starts active, panel for voice
    public GameObject whiteScreen; //starts inactive, only for transitions

    public GameObject txtArrow; //visual text advance indicator

    public GameObject asystantSprite; //sprite for asystant

    public GameObject pManager;
    public GameObject failsafebtn;

    string[] Scene3Txt = new string[]
    {       
        "Wow, look at that! I look pretty good like this, if I do say so myself.", //0
        "Oh? Why is everything here 2-dimensional when the world you can see is 3-dimensional?",
        "Basically the interference between our worlds makes everything slightly glitchy. You can already tell, right?",
        "There are a few things that aren't working correctly. Finding the floor to place things is really difficult, for one. I can't really explain too much right now, since-", //3
        "Darn, the system doesn't like us talking, I think you're being forced out!",
        "If you don't leave this place correctly, you might suffer some bad side effects, so we need to figure out how to do this safely...",
        "Oh, I know! Here, try finding the floor again. I know I just said it was difficult, but you should be able to do it one more time!", //6

        "These puzzle pieces will fit together into a portal, and using that should get you out safely. Go ahead, get close and tap them to collect them! There are 4 groups, just tap one piece to " +
        "get everything in the group!", //7

        "Great work! Now just piece the portal together, and you should be able to leave!" //8
    };

    // Start is called before the first frame update
    void Start()
    {
        LoadSprite();
        StartCoroutine(Scene3());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void failSafePuzzleStart()
    {
        StartCoroutine(switchToPuzzle());
    }

    public void floorPlaced()
    {
        floorFinder.SetActive(false);
        StartCoroutine(Scene3p2());
    }

    public void ActivatePuzzle()
    {
        StartCoroutine(switchToPuzzle());
    }

    void LoadSprite()
    {
        Texture2D tex;
        byte[] data;

        data = File.ReadAllBytes(Application.persistentDataPath + "/ASYSTANT.png");
        tex = new Texture2D(2, 2);
        if(tex.LoadImage(data))
        {
            asystantSprite.GetComponent<Image>().sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0, 0), 100.0f);
        }
    }

    IEnumerator Scene3()
    {
        yield return activeType(asystext.GetComponent<Text>(), Scene3Txt[0]);
        txtArrow.SetActive(true);
        yield return waitForTap();

        txtArrow.SetActive(false);
        yield return activeType(asystext.GetComponent<Text>(), Scene3Txt[1]);
        txtArrow.SetActive(true);
        yield return waitForTap();

        txtArrow.SetActive(false);
        yield return activeType(asystext.GetComponent<Text>(), Scene3Txt[2]);
        txtArrow.SetActive(true);
        yield return waitForTap();

        txtArrow.SetActive(false);
        yield return activeType(asystext.GetComponent<Text>(), Scene3Txt[3]);
        txtArrow.SetActive(true);
        yield return waitForTap();

        whiteScreen.SetActive(true);
        StartCoroutine(FlashWhite(whiteScreen.GetComponent<Image>()));
        yield return new WaitForSeconds(1);

        txtArrow.SetActive(false);
        yield return activeType(asystext.GetComponent<Text>(), Scene3Txt[4]);
        txtArrow.SetActive(true);
        yield return waitForTap();

        txtArrow.SetActive(false);
        yield return activeType(asystext.GetComponent<Text>(), Scene3Txt[5]);
        txtArrow.SetActive(true);
        yield return waitForTap();

        txtArrow.SetActive(false);
        yield return activeType(asystext.GetComponent<Text>(), Scene3Txt[6]);
        floorFinder.SetActive(true);
    }

    IEnumerator Scene3p2()
    {
        yield return activeType(asystext.GetComponent<Text>(), Scene3Txt[7]);
        pManager.GetComponent<PieceManager>().initCol();
        StartCoroutine(waitForFailsafe());
    }

    IEnumerator waitForFailsafe()
    {
        yield return new WaitForSeconds(15);
        failsafebtn.SetActive(true);
    }

    IEnumerator switchToPuzzle()
    {
        yield return activeType(asystext.GetComponent<Text>(), Scene3Txt[8]);
        SceneManager.LoadScene("Puzzle", LoadSceneMode.Single);
    }

    IEnumerator FlashWhite(Image i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0)
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
        while (true)
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
