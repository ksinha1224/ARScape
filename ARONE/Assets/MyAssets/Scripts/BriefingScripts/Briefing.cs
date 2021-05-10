using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Briefing : MonoBehaviour
{
    public GameObject toText;
    string[] txt = new string[]
    {      
        "For the best experience, please ensure you have a few feet of space in each direction around you before you begin this game.",
        "Please note that all information and events surrounding the story of this game are completely fictional."
    };
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(brief());
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    public void skip()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    IEnumerator brief()
    {
        yield return activeType(toText.GetComponent<Text>(), txt[0]);
        yield return new WaitForSeconds(2);
        yield return activeType(toText.GetComponent<Text>(),txt[1]);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    IEnumerator activeType(Text toPrint, string src)
    {
        toPrint.text = "";
        foreach (char c in src)
        {
            if (Input.GetKey(KeyCode.A) || Input.touchCount > 0)
            {
                toPrint.text = src;
                yield break;
            }
            toPrint.text += c;
            yield return new WaitForSeconds(0.05f);
        }
    }
}
