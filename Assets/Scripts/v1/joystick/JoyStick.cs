using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace BeterJoyStick
{
    public class JoyStick :
            MonoBehaviour,
            IDragHandler,
            IBeginDragHandler,
            IEndDragHandler
    {

        public Text T_HelperText;
        public Vector2 jsCenter;
        public Vector2 touchPosition;
        public int screenPercentMaxJsVal = 16; // percent of screen with to joystik value be maximum 
        [SerializeField]
        float maxAxisMagnitude;

        Vector2 result;

        public Vector2 Result { get => result; set => result = value; }

        void Start()
        {
            maxAxisMagnitude = Screen.width * screenPercentMaxJsVal / 100;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            jsCenter = eventData.pointerCurrentRaycast.screenPosition;
        }

        public void OnDrag(PointerEventData eventData)
        {
            touchPosition = eventData.position;
            var target = touchPosition - jsCenter;
            var res = Vector2.zero;
            res.x = target.x == 0 ? 0 : Mathf.Clamp(target.x / maxAxisMagnitude, -1, 1);
            res.y = target.y == 0 ? 0 : Mathf.Clamp(target.y / maxAxisMagnitude, -1, 1);
            Result = res;
            //print(Result);
            //T_HelperText.text = res.ToString();
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            jsCenter = Vector2.zero;
            touchPosition = Vector2.zero;
            Result = Vector2.zero;
        }
    }
}
