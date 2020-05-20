using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragHandler :
    MonoBehaviour,
    IDragHandler,
    IBeginDragHandler,
    IEndDragHandler
{
    public GameObject rootItem;
    public GameObject lastHoveredItem;
    public LineRenderer lineRenderer;
    public List<GameObject> childBtns;
    public List<Vector3> childBtnsActivePositions;
    public Vector3 newPos;
    void Start()
    {
        rootItem = transform.gameObject;
        lineRenderer.positionCount = 0;
        lineRenderer.material.color = Color.white;
        //lineRenderer.positionCount = 2;
        //lineRenderer.startWidth = 0.1f;
        //lineRenderer.endWidth = 0.2f;

        //lineRenderer.SetPosition(0, rootItem.transform.position);
        //lineRenderer.SetPosition(1, itemTarget.transform.position);
        //Debug.Log("root " + lineRenderer.GetPosition(0));
        //Debug.Log("target " + lineRenderer.GetPosition(1));

        HidChildBtns();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, transform.position);
        ShowChildBtns();
    }

    void Update()
    {
        if (lineRenderer.positionCount == 1)
        {
            lineRenderer.SetPosition(0, transform.position);
        }
        else if (lineRenderer.positionCount == 2)
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, (newPos == null ? transform.position : newPos));
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        lineRenderer.SetPosition(0, transform.position);
        newPos = eventData.pointerCurrentRaycast.worldPosition;
        var hoveredObj = eventData.pointerCurrentRaycast;
        if (childBtns.Contains(hoveredObj.gameObject))
        {
            hoveredObj.gameObject.GetComponent<BtnChildDragHandler>().IsHovered(true);
            lastHoveredItem = hoveredObj.gameObject;
            newPos = lastHoveredItem.transform.position;
        }
        else
        {
            if (lastHoveredItem != null)
                lastHoveredItem?.GetComponent<BtnChildDragHandler>().IsHovered(false);
            lastHoveredItem = null;
        }
        lineRenderer.SetPosition(1, newPos);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (lastHoveredItem != null)
        {
            lastHoveredItem.GetComponent<BtnChildDragHandler>().Choose();
        }
        //lineRenderer.SetPosition(1, rootItem.transform.position);
        lineRenderer.positionCount = 0;
        HidChildBtns();
    }

    void HidChildBtns()
    {
        foreach (var btn in childBtns)
        {
            btn.SetActive(false);
            //childBtnsActivePositions.Add(btn.transform.position);
            //btn.transform.position = rootItem.transform.position;
            //btn.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        }
    }

    void ShowChildBtns()
    {
        for (int i = 0; i < childBtns.Count; i++)
        {
            childBtns[i].SetActive(true);
            //childBtns[i].transform.position = childBtnsActivePositions[i];
            //childBtns[i].transform.localScale = Vector3.one;
        }
    }
}
