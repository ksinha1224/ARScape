using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class PuzTextHandler : MonoBehaviour
{
    public int piecesFilled; //keep track of where user is in puzzle

    public GameObject asystext; //text blurb for asystant
    public GameObject asystantSprite; //sprite for asystant

    bool eightflag, ftnflag;

    string[] sceneTxt = new string[]
    {
        "The menu on the left has all of the pieces! Drag it up and down to find the one you want, then drag the piece to the slot you think is the right one, and it should fit in! Good luck!", //0
        "You're almost halfway there! Keep going!", //1
        "Only a couple more, you can do this!" //2
    };

    // Start is called before the first frame update
    void Start()
    {
        LoadSprite();
        piecesFilled = 0;
        StartCoroutine(activeType(asystext.GetComponent<Text>(), sceneTxt[0]));
        eightflag = false;
        ftnflag = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(piecesFilled == 8 && !eightflag)
        {
            StartCoroutine(activeType(asystext.GetComponent<Text>(), sceneTxt[1]));
            eightflag = true;
        }

        else if (piecesFilled == 14 && !ftnflag)
        {
            StartCoroutine(activeType(asystext.GetComponent<Text>(), sceneTxt[2]));
            ftnflag = true;
        }

        else if (piecesFilled == 17)
        {
            SceneManager.LoadScene("Final", LoadSceneMode.Single);
        }
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

    IEnumerator activeType(Text toPrint, string src)
    {
        toPrint.text = "";
        foreach (char c in src)
        {
            toPrint.text += c;
            yield return new WaitForSeconds(0.02f);
        }
    }
}
