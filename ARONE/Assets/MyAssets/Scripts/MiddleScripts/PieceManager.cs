using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceManager : MonoBehaviour
{    
    public GameObject[] groups;

    public GameObject eventSys;

    bool g1collected, g2collected, g3collected, g4collected;
    bool startCollecting;

    Camera mc;

    float dist = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        g1collected = false;
        g2collected = false;
        g3collected = false;
        g4collected = false;

        startCollecting = false;

        mc = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        //moveActivePieces();
        checkDistance();

        if(g1collected && g2collected && g3collected && g4collected)
        {
            eventSys.GetComponent<TextMiddle>().ActivatePuzzle();
        } 
    }

    public void initCol()
    {
        startCollecting = true;
    }

    public void setCollected(int gn)
    {
        if(startCollecting)
        {
            if (gn == 1)
            {
                groups[0].SetActive(false);
                g1collected = true;
            }

            else if(gn == 2)
            {
                groups[1].SetActive(false);
                g2collected = true;
            }

            else if(gn == 3)
            {
                groups[2].SetActive(false);
                g3collected = true;
            }

            else if (gn == 4)
            {
                groups[3].SetActive(false);
                g4collected = true;
            }
        } 
    }

    /*
    void moveActivePieces()
    {
        if (!g1collected)
        {
            for (int i = 0; i < g1pieces.Length; i++)
            {
                float r = Random.Range(-5.0f, 5.0f);
                g1pieces[i].transform.position = new Vector3 (g1pieces[i].transform.position.x, g1pieces[i].transform.position.y + r * Time.deltaTime, g1pieces[i].transform.position.z);
            }
        }

        if (!g2collected)
        {
            for (int i = 0; i < g2pieces.Length; i++)
            {
                float r = Random.Range(-5.0f, 5.0f);
                g2pieces[i].transform.position = new Vector3(g2pieces[i].transform.position.x, g2pieces[i].transform.position.y + r * Time.deltaTime, g2pieces[i].transform.position.z);
            }
        }

        if (!g3collected)
        {
            for (int i = 0; i < g3pieces.Length; i++)
            {
                float r = Random.Range(-5.0f, 5.0f);
                g3pieces[i].transform.position = new Vector3(g3pieces[i].transform.position.x, g3pieces[i].transform.position.y + r * Time.deltaTime, g3pieces[i].transform.position.z);
            }
        }

        if (!g4collected)
        {
            for (int i = 0; i < g4pieces.Length; i++)
            {
                float r = Random.Range(-5.0f, 5.0f);
                g4pieces[i].transform.position = new Vector3(g4pieces[i].transform.position.x, g4pieces[i].transform.position.y + r * Time.deltaTime, g4pieces[i].transform.position.z);
            }
        }
    }
    */

    void checkDistance()
    {
        if (!g1collected)
        {
            Vector3 g1vec = mc.transform.position - groups[0].transform.position;
            float g1mag = g1vec.magnitude;

            if(g1mag < dist)
            {
                groups[0].GetComponent<BoxCollider>().enabled = true;
            }

            else
            {
                groups[0].GetComponent<BoxCollider>().enabled = false;
            }
        }

        if (!g2collected)
        {
            Vector3 g2vec = mc.transform.position - groups[1].transform.position;
            float g2mag = g2vec.magnitude;

            if (g2mag < dist)
            {
                groups[1].GetComponent<BoxCollider>().enabled = true;
            }

            else
            {
                groups[1].GetComponent<BoxCollider>().enabled = false;
            }
        }

        if (!g3collected)
        {
            Vector3 g3vec = mc.transform.position - groups[2].transform.position;
            float g3mag = g3vec.magnitude;

            if (g3mag < dist)
            {
                groups[2].GetComponent<BoxCollider>().enabled = true;
            }

            else
            {
                groups[2].GetComponent<BoxCollider>().enabled = false;
            }
        }

        if (!g4collected)
        {
            Vector3 g4vec = mc.transform.position - groups[3].transform.position;
            float g4mag = g4vec.magnitude;

            if (g4mag < dist)
            {
                groups[3].GetComponent<BoxCollider>().enabled = true;
            }

            else
            {
                groups[0].GetComponent<BoxCollider>().enabled = false;
            }
        }
    }

}
