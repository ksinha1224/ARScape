using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class canvasTrigger : MonoBehaviour
{
    bool touchable;

    void Start()
    {
        touchable = false;
    }

    void Update()
    {
        if(touchable && close() && Input.touchCount > 0)
        {
            SceneManager.LoadScene("DrawingScene", LoadSceneMode.Single);
        }   
    }

    public void makeTouchable()
    {
        touchable = true;
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
}
