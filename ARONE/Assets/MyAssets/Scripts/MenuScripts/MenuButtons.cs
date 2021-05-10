using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButtons : MonoBehaviour
{
    public GameObject text;

    public void PressPlay()
    {
        SceneManager.LoadScene("Intro", LoadSceneMode.Single);
    }

    public void PressContact()
    {
        Text txt = text.GetComponent<Text>();
        if (text.activeSelf)
        {
            StartCoroutine(FadeOut(txt));
        }

        else
        {
            StartCoroutine(FadeIn(txt));
            text.SetActive(true);
        }
    }

    public IEnumerator FadeIn(Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / 0.5f));
            yield return null;
        }
    }

    public IEnumerator FadeOut(Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / 0.5f));
            yield return null;
        }

        text.SetActive(false);
    }

}
