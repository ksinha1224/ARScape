using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class helpbtn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void pressedFromScene()
    {
        SceneManager.LoadScene("Help", LoadSceneMode.Additive);
    }

    public void pressedFromHelp()
    {
        SceneManager.UnloadSceneAsync("Help");
    }
}
