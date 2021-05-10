using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private GameObject piecesMenu;
    private GameObject dragSpace;

    public GameObject txtHandler;

    public Canvas cv;
    private RectTransform rct;
    private CanvasGroup cg;

    private bool moveable;

    private Vector3 pos;
    private int tagno;

    public GameObject matchingSlot;

    private void Start()
    {
        piecesMenu = GameObject.FindGameObjectWithTag("piecesMenu");
        dragSpace = GameObject.FindGameObjectWithTag("dragSpace");
        tagno = int.Parse(gameObject.tag);
        rct = GetComponent<RectTransform>();
        cg = GetComponent<CanvasGroup>();
        moveable = true;
    }

    void Update()
    {
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(!moveable)
        {
            return;
        }

        else
        {
            pos = gameObject.transform.position;
            gameObject.transform.parent = dragSpace.transform;
            cg.alpha = 0.6f;
            cg.blocksRaycasts = false;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        cg.alpha = 1;

        if (matchingSlot.GetComponent<Slot>().closeEnough(gameObject.transform.position))
        {
            Vector3 newPos = matchingSlot.transform.position;
            matchingSlot.gameObject.SetActive(false);
            gameObject.transform.position = newPos;
            txtHandler.GetComponent<PuzTextHandler>().piecesFilled++;
        }

        else
        {
            cg.blocksRaycasts = true;
            gameObject.transform.position = pos;
            gameObject.transform.parent = piecesMenu.transform;
            int ind = getrealIndex();
            gameObject.transform.SetSiblingIndex(ind);
        }
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(moveable)
        {
            rct.anchoredPosition += eventData.delta / cv.scaleFactor;
        }
    }

    private int getrealIndex()
    {
        if (tagno == 14)
        {
            return 0;
        }

        else if (tagno == 8)
        {
            return 1;
        }

        else if (tagno == 4)
        {
            return 2;
        }

        else if (tagno == 9)
        {
            return 3;
        }

        else if (tagno == 16)
        {
            return 4;
        }

        else if (tagno == 10)
        {
            return 5;
        }

        else if (tagno == 7)
        {
            return 6;
        }

        else if (tagno == 2)
        {
            return 7;
        }

        else if (tagno == 5)
        {
            return 8;
        }

        else if (tagno == 6)
        {
            return 9;
        }

        else if (tagno == 12)
        {
            return 10;
        }

        else if (tagno == 1)
        {
            return 11;
        }

        else if (tagno == 15)
        {
            return 12;
        }

        else if (tagno == 11)
        {
            return 13;
        }

        else if (tagno == 13)
        {
            return 14;
        }

        else if (tagno == 3)
        {
            return 15;
        }

        else //(tagno == 17)
        {
            return 16;
        }
    }
}
