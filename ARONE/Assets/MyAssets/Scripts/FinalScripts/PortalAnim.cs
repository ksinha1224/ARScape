using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalAnim : MonoBehaviour
{
    public GameObject portal;
    public Sprite[] frames;

    SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        sr = portal.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startAnimation()
    {
        StartCoroutine(ani());
    }

    IEnumerator ani()
    {
        while(true)
        {
            for (int i = 0; i < frames.Length; i++)
            {
                sr.sprite = frames[i];
                yield return new WaitForSeconds(0.12f);
            }
        }
        
    }
}
