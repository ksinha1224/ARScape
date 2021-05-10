using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class FinalTxtHandler : MonoBehaviour
{
    public GameObject whiteScreen; //starts inactive, only for transitions
    public GameObject asystantSprite; //sprite for asystant

    public GameObject floorFinder;

    public GameObject txtArrow; //visual text advance indicator
    public GameObject asystext; //starts active, less disembodied, but not quite implemented yet

    private PortalAnim pAnim;

    string[] Scene4Txt = new string[]
    {
        "You did great! The portal will appear as soon as you find the floor to anchor it, and then you'll be able to leave!", //0
        "Why don't you go ahead and try that now? You remember how to find the floor, right? Look at the ground and tap the white box?", //1
        "There we go! Oh? It's behind you!",
        "I guess you really have to go now, huh? I have so much I wanted to share with you, it's a real shame... but I'll leave you with this:",
        "The world can be a freaky place, with all the technology, but I'll be watching over you, so rest assured! And above everything else...",
        "When a full version of this game comes out, be sure to get it, okay?",
        "Well, until then... farewell, friend!. It's been fun!" //6
    };

    // Start is called before the first frame update
    void Start()
    {
        LoadSprite();
        pAnim = gameObject.GetComponent<PortalAnim>();
        StartCoroutine(Scene4p1());
    }

    public void BeginTheEnd()
    {
        pAnim.GetComponent<PortalAnim>().startAnimation();
        floorFinder.SetActive(false);
        StartCoroutine(SceneTheEnd());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Scene4p1()
    {
        yield return activeType(asystext.GetComponent<Text>(), Scene4Txt[0]);
        txtArrow.SetActive(true);
        yield return waitForTap();

        txtArrow.SetActive(false);
        yield return activeType(asystext.GetComponent<Text>(), Scene4Txt[1]);
        floorFinder.SetActive(true);
    }

    IEnumerator SceneTheEnd()
    {
        txtArrow.SetActive(false);
        yield return activeType(asystext.GetComponent<Text>(), Scene4Txt[2]);
        txtArrow.SetActive(true);
        yield return waitForTap();

        txtArrow.SetActive(false);
        yield return activeType(asystext.GetComponent<Text>(), Scene4Txt[3]);
        txtArrow.SetActive(true);
        yield return waitForTap();

        txtArrow.SetActive(false);
        yield return activeType(asystext.GetComponent<Text>(), Scene4Txt[4]);
        txtArrow.SetActive(true);
        yield return waitForTap();

        txtArrow.SetActive(false);
        yield return activeType(asystext.GetComponent<Text>(), Scene4Txt[5]);
        txtArrow.SetActive(true);
        yield return waitForTap();

        txtArrow.SetActive(false);
        yield return activeType(asystext.GetComponent<Text>(), Scene4Txt[6]);
        yield return EndGame();
    }

    void LoadSprite()
    {
        Texture2D tex;
        byte[] data;

        data = File.ReadAllBytes(Application.persistentDataPath + "/ASYSTANT.png");
        tex = new Texture2D(2, 2);
        if (tex.LoadImage(data))
        {
            asystantSprite.GetComponent<Image>().sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0, 0), 100.0f);
        }
    }

    IEnumerator EndGame()
    {
        whiteScreen.SetActive(true);
        yield return FadeImg(whiteScreen.GetComponent<Image>());
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    IEnumerator FadeImg(Image i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / 5f));
            yield return null;
        }
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
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
