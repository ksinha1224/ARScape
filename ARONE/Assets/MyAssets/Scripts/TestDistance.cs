using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestDistance : MonoBehaviour
{
    public GameObject piece1;
    //public GameObject piece2;
    //public GameObject piece3;
    Vector3 delta1;
    //Vector3 delta2;
    //Vector3 delta3;
    float dist = 2.0f;

    public GameObject text;
    public GameObject btn;
    public GameObject planeFinder;

    Camera mc;

    bool floorFound;

    // Start is called before the first frame update
    void Start()
    {
        mc = Camera.main;
        floorFound = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (floorFound && piece1.activeSelf)
        {
            delta1 = mc.transform.position - piece1.transform.position;
            //delta2 = mc.transform.position - piece2.transform.position;
            //delta3 = mc.transform.position - piece3.transform.position;

            if (delta1.magnitude < dist)
            {
                piece1.GetComponent<BoxCollider>().enabled = true;
                text.GetComponent<Text>().text = "Piece 1 enabled, distance: " + delta1.magnitude.ToString();

                if (Input.touchCount > 0)
                {
                    text.GetComponent<Text>().text = "Piece 1 collected!";
                    piece1.SetActive(false);                    
                }
            }

            if (delta1.magnitude > dist)
            {
                piece1.GetComponent<BoxCollider>().enabled = false;
                text.GetComponent<Text>().text = "Piece 1 disabled, distance: " + delta1.magnitude.ToString();
            }
        }

        if (!floorFound)
        {
            text.GetComponent<Text>().text = "Find the floor";
        }

        if (floorFound && !piece1.activeSelf)
        {
            text.GetComponent<Text>().text = "Piece 1 collected, try resetting!";
        }
    }

    public void onFloor()
    {
        floorFound = true;
        planeFinder.SetActive(false);
    }

    public void reset()
    {
        if (floorFound && !piece1.activeSelf)
        {
            piece1.SetActive(true);
        }
    }
}
