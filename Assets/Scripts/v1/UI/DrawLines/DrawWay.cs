using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawWay : MonoBehaviour
{
    public Vector3 lastPoint;
    public List<Vector3> points = new List<Vector3>();
    //public int pointsCount = 2;

    public LineRenderer lineRenderer;
    private bool isDragProcess = false;

    public Vector3 LastPoint
    {
        get => lastPoint;
        set
        {
            lastPoint = value;
            points.RemoveAt(points.Count - 1);
            points.Add(lastPoint);
            ReDraw();
        }
    }

    public List<Vector3> Points { get => points; set => points = value; }
    public bool IsDragProcess
    {
        get => isDragProcess;
        set
        {
            isDragProcess = value;
            if (!isDragProcess)
            {
                points = new List<Vector3>();
            }
        }
    }

    public List<GameObject> T_points = new List<GameObject>();
    public GameObject T_imageLine;
    void Start()
    {
        if (!transform.gameObject.TryGetComponent<LineRenderer>(out lineRenderer))
        {
            lineRenderer = transform.gameObject.AddComponent<LineRenderer>();
        }
        lineRenderer.startWidth = 1f;
        lineRenderer.endWidth = 0.2f;
        lineRenderer.positionCount = 2;

        if (T_points.Count != 0)
        {
            var cam = Camera.main;
            var posPoints = new Vector3[T_points.Count];
            for (int i = 0; i < T_points.Count; i++)
            {
                var vp = T_points[i].transform.position;
                posPoints[i] = cam.ScreenToViewportPoint( vp );
                posPoints[i] = vp;

                //var r1 = cam.ScreenToWorldPoint(vp);
                //var r2 = cam.WorldToScreenPoint(vp);
                //var r3 = cam.ScreenToViewportPoint(vp);
                //var r4 = cam.ViewportPointToRay(vp);
                //var r5 = cam.ViewportToScreenPoint(vp);
                //var r6 = cam.ViewportToWorldPoint(vp);


                //Debug.Log("vp " + vp);
                //Debug.Log("r1 " + r1);
                //Debug.Log("r2 " + r2);
                //Debug.Log("r3 " + r3);
                //Debug.Log("r4 " + r4);
                //Debug.Log("r5 " + r5);
                //Debug.Log("r6 " + r6);
                //Debug.Log("- - - - - - - - -");
            }
            for (int i = 0; i < posPoints.Length; i++)
            { 
                if (i % 2 == 0)
                {
                    //T_imageLine.transform.position = posPoints[i];
                    var height = 3;
                    var width = (posPoints[i + 1] - posPoints[i]).magnitude;
                    T_imageLine.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
                    Vector3 targetDirection = T_points[i + 1].transform.position - T_points[i].transform.position;
                    var angleBetween = Vector3.Angle(T_points[i].transform.position, targetDirection);
                    //Debug.Log("angleBetween " + angleBetween);
                    T_imageLine.transform.rotation *= Quaternion.Euler(0, angleBetween, angleBetween);
                    T_imageLine.transform.position = posPoints[i] - (posPoints[i] - posPoints[i + 1]) / 2;
                    //T_imageLine.transform.LookAt(posPoints[i + 1]);
                    //T_imageLine.transform.rotation.y = 0;
                    //Debug.Log("p1 " + posPoints[i]);
                    //Debug.Log("p2 " + posPoints[i + 1]);
                }
            }
            //lineRenderer.positionCount = posPoints.Length;
            //lineRenderer.SetPositions(posPoints);
        }
    }

    void ReDraw()
    {
        var posPoints = new Vector3[Points.Count];
        for (int i = 0; i < Points.Count; i++)
        {
            posPoints[i] = Points[i];
        }
        lineRenderer.SetPositions(posPoints);
    }

    public void SelectChildBtn(Vector3 newBtnPosition)
    {
        if (Points.Count != 0)
            Points.RemoveAt(Points.Count - 1);
        Points.Add(newBtnPosition);
        Points.Add(newBtnPosition);
        lineRenderer.positionCount = Points.Count;
    }
}
