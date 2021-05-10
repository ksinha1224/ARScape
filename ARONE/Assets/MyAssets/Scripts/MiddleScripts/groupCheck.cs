using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groupCheck : MonoBehaviour
{
    public GameObject pcManager;
    private string t;

    // Start is called before the first frame update
    void Start()
    {
        t = gameObject.tag;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.GetComponent<BoxCollider>().enabled && Input.touchCount > 0)
        {
            if(t == "group1")
            {
                pcManager.GetComponent<PieceManager>().setCollected(1);
            }

            else if(t == "group2")
            {
                pcManager.GetComponent<PieceManager>().setCollected(2);
            }

            else if(t == "group3")
            {
                pcManager.GetComponent<PieceManager>().setCollected(3);
            }

            else //if(t == "group4")
            {
                pcManager.GetComponent<PieceManager>().setCollected(4);
            }    
        }
    }
}
