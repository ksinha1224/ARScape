using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class drawingCanvas : MonoBehaviour
{
    public GameObject brush;
    public float brushSize = 0.1f;
    public RenderTexture rtex;

    bool allowUserInput;

    public GameObject flavorText;
    string toWrite;
    Color transparent = new Color(0, 0, 0, 0);

    public Material[] colors; 
    /*
     * 0 - red
     * 1 - orange
     * 2 - yellow
     * 3 - green
     * 4 - blue
     * 5 - teal
     * 6 - purple
     * 7 - pink
    */

    void Start()
    {
        toWrite = "ASYSTANT: Use your finger to sketch, and change colors with the panel on the side! When you're" +
            "done just press Save. To start over, hit Erase.";
        StartCoroutine(activeType(flavorText.GetComponent<Text>(), toWrite));
        allowUserInput = true;
    }

    void Update()
    {
        if(Input.GetMouseButton(0) && allowUserInput)
        {
            var Ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(Ray, out hit))
            {
                var go = Instantiate(brush, hit.point + Vector3.up * 0.1f, Quaternion.identity, transform);
                go.transform.localScale = Vector3.one * brushSize;
            }
        }
    }

    public void Save()
    {
        allowUserInput = false;
        StartCoroutine(CoSave());
    }

    public void Erase()
    {
        Object[] allBrush = GameObject.FindGameObjectsWithTag("brush");
        foreach(GameObject obj in allBrush)
        {
            Destroy(obj);
        }
    }

    public void setRed()
    {
        brush.GetComponent<MeshRenderer>().material = colors[0];
    }

    public void setOrange()
    {
        brush.GetComponent<MeshRenderer>().material = colors[1];
    }

    public void setYellow()
    {
        brush.GetComponent<MeshRenderer>().material = colors[2];
    }

    public void setGreen()
    {
        brush.GetComponent<MeshRenderer>().material = colors[3];
    }

    public void setBlue()
    {
        brush.GetComponent<MeshRenderer>().material = colors[4];
    }

    public void setTeal()
    {
        brush.GetComponent<MeshRenderer>().material = colors[5];
    }

    public void setPurple()
    {
        brush.GetComponent<MeshRenderer>().material = colors[6];
    }

    public void setPink()
    {
        brush.GetComponent<MeshRenderer>().material = colors[7];
    }

    //IEnumerator helpers

    IEnumerator activeType(Text toPrint, string src)
    {
        toPrint.text = "";
        foreach (char c in src)
        {
            toPrint.text += c;
            yield return new WaitForSeconds(0.02f);
        }
    }

    IEnumerator CoSave()
    {
        yield return new WaitForEndOfFrame();

        toWrite = "Saving now, please wait a minute...";
        StartCoroutine(activeType(flavorText.GetComponent<Text>(), toWrite));

        RenderTexture.active = rtex;

        var tx2d = new Texture2D(rtex.width, rtex.height);
        tx2d.ReadPixels(new Rect(0, 0, rtex.width, rtex.height), 0, 0);

        for (int y = 0; y < tx2d.height; y++)
        {
            for (int x = 0; x < tx2d.width; x++)
            {
                if (tx2d.GetPixel(x,y) == Color.white)
                {
                    tx2d.SetPixel(x, y, transparent);
                }
            }
        }

        var data = tx2d.EncodeToPNG();

        File.WriteAllBytes(Application.persistentDataPath + "/ASYSTANT.png", data);

        SceneManager.LoadScene("Middle", LoadSceneMode.Single);
    }
}
