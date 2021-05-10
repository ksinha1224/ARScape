using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour
{
    private Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {
        pos = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    /*
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
        }

        else
        {
            Debug.Log("OnDrop Reached");
        }
    }
    */

    public bool closeEnough(Vector3 p)
    {
        if(Vector3.Distance(p, pos) < 50)
        {
            return true;
        }

        return false;
    }

}
