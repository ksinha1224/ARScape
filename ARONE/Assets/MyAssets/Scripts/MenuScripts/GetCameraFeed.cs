using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetCameraFeed : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        WebCamTexture tex = new WebCamTexture(WebCamTexture.devices[0].name);
        RawImage rawImg;
        rawImg = GetComponent<RawImage>();
        rawImg.texture = tex;
        tex.Play();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
