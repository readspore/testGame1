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

    void Start()
    {
        rootItem = transform.gameObject;
        lineRenderer.positionCount = 0;
        //lineRenderer.positionCount = 2;
        //lineRenderer.startWidth = 0.1f;
        //lineRenderer.endWidth = 0.2f;

        //lineRenderer.SetPosition(0, rootItem.transform.position);
        //lineRenderer.SetPosition(1, itemTarget.transform.position);
        //Debug.Log("root " + lineRenderer.GetPosition(0));
        //Debug.Log("target " + lineRenderer.GetPosition(1));
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, rootItem.transform.position);
    }

    public void OnDrag(PointerEventData eventData)
    {
        var newPos = eventData.pointerCurrentRaycast.worldPosition;
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
    }

}
