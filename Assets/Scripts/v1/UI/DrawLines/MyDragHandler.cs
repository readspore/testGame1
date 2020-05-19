using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MyDragHandler : MonoBehaviour, 
    IDragHandler,
    IBeginDragHandler,
    IEndDragHandler
{
    public bool dragOnHover = false;
    public bool isDragProcess = false;
    public Vector3 positionLineStart;
    public Vector3 secondDragPosition;
    public Vector3 currentDragPosition;
    public List<GameObject> nextLvlBtns = new List<GameObject>();
    public List<GameObject> childNextLvlBtns = new List<GameObject>();
    public ChildBtnHover lastHoveredItem;
    public DrawWay drawWay;

    private PointerEventData _lastPointerData;

    //Camera cam;

    private void Start()
    {
        if (!transform.gameObject.TryGetComponent<DrawWay>(out drawWay))
        {
            drawWay = transform.gameObject.AddComponent<DrawWay>();
        }
        //cam = Camera.main;
    }
    private void Update()
    {
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        drawWay.IsDragProcess = true;
        positionLineStart = transform.position;
        drawWay.SelectChildBtn(positionLineStart);
        _lastPointerData = eventData;
    }

    public void OnDrag(PointerEventData eventData)
    {
        secondDragPosition = eventData.pointerCurrentRaycast.worldPosition;
        var hoveredObj = eventData.pointerCurrentRaycast;

        if (lastHoveredItem != null)
        {
            if (childNextLvlBtns.Contains(hoveredObj.gameObject))
            {
                lastHoveredItem = hoveredObj.gameObject.GetComponent<ChildBtnHover>();
                childNextLvlBtns = hoveredObj.gameObject.GetComponent<MyDragHandler>().nextLvlBtns;
                if (childNextLvlBtns == null || childNextLvlBtns.Count == 0)
                {
                    drawWay.IsDragProcess = false;
                    CancelDrag();
                }
                lastHoveredItem.IsHovered(true);
                secondDragPosition = hoveredObj.gameObject.transform.position;
                drawWay.SelectChildBtn(secondDragPosition);
            }
        }
        else
        {
            if (nextLvlBtns.Contains(hoveredObj.gameObject))
            {
                lastHoveredItem = hoveredObj.gameObject.GetComponent<ChildBtnHover>();
                childNextLvlBtns = hoveredObj.gameObject.GetComponent<MyDragHandler>().nextLvlBtns;
                lastHoveredItem.IsHovered(true);
                secondDragPosition = hoveredObj.gameObject.transform.position;
                drawWay.SelectChildBtn(secondDragPosition);
            } 
        }
        drawWay.LastPoint = secondDragPosition;

        //else
        //{
        //    lastHoveredItem?.IsHovered(false, null);
        //    lastHoveredItem = null;
        //}
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        drawWay.IsDragProcess = false;
        _lastPointerData = null;
    }

    public void CancelDrag()
    {
        if (_lastPointerData != null)
        {
            _lastPointerData.pointerDrag = null;

            // Reset position here
        }
    }
}
