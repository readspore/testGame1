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

    void Start()
    {
        if (!transform.gameObject.TryGetComponent<LineRenderer>(out lineRenderer))
        {
            lineRenderer = transform.gameObject.AddComponent<LineRenderer>();
        }
        lineRenderer.startWidth = 1f;
        lineRenderer.endWidth = 0.2f;
        lineRenderer.positionCount = 2;
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
