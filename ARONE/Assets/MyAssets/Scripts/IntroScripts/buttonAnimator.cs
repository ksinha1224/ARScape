using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonAnimator : MonoBehaviour
{
    public GameObject btn;
    public Sprite[] frames;
    public Sprite[] glowFrames;
    bool animate;
    bool touchable;
    bool faded;

    SpriteRenderer sr;

    void Start()
    {
        sr = btn.GetComponent<SpriteRenderer>();
        animate = false;
        touchable = false;
        faded = false;
    }

    void Update()
    {
        if (touchable && close() && Input.touchCount > 0)
        {
            StartCoroutine(aniGlow());
            Handheld.Vibrate();
            touchable = false;
        }
    }

    public void onFlipped()
    {
        if (!animate)
        {
            animate = true;
            StartCoroutine(ani());
        }
    }

    public void makeTouchable()
    {
        touchable = true;
    }

    public bool fadeComplete()
    {
        return faded;
    }

    private bool close()
    {
        Vector3 pos = Camera.main.transform.position - gameObject.transform.position;
        if (pos.magnitude < 1.8f)
        {
            return true;
        }

        return false;
    }

    IEnumerator ani()
    {
        while(animate)
        {
            for (int i = 0; i < frames.Length; i++)
            {
                sr.sprite = frames[i];
                yield return new WaitForSeconds(0.12f);
            }
        }
    }

    IEnumerator aniGlow()
    {
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1);
        for (int i = 0; i < glowFrames.Length; i++)
        {
            sr.sprite = glowFrames[i];
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, sr.color.a - (Time.deltaTime / 5f));
            yield return new WaitForSeconds(0.05f);
        }
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0);
        btn.SetActive(false);
        faded = true;
    }
}
