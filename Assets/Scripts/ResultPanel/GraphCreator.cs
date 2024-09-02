using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GraphCreator : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI pointer;
    private LineRenderer lineRenderer;
    private List<Dot> dots = new();
    private float maxValueContainerY = 210F;
    private float maxValueY = 1;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.positionCount = 8;
    }

    public void drawGraph(float newDot)
    {
        foreach (Dot dot in dots)
        {
            dot.ID += 1;
            //Debug.Log(dot.ID);
        }

        dots.Add(new Dot(0, newDot));
        //Debug.Log(dots.Count);

        if (dots.Count == 9)
        {
            if (dots[0].NumberValue == maxValueY)
                maxValueY = 1;
            dots.RemoveAt(0);
        }

        int x = 25;

        foreach (Dot dot in dots)
        {
            if (dot.NumberValue > maxValueY)
                maxValueY = dot.NumberValue;
        }

        foreach (Dot dot in dots)
        {
            lineRenderer.SetPosition(dot.ID, 
                new Vector3(x + 50 * dot.ID, dot.NumberValue / maxValueY * maxValueContainerY + 25, 0));
        }

        pointer.SetText(newDot.ToString());

        pointer.transform.localPosition = new Vector3(440, 
            newDot / maxValueY * maxValueContainerY + 50, 0);
    }

    private class Dot
    {
        public int ID { get; set; }
        public float NumberValue { get; private set; }

        public Dot(int iD, float numberValue)
        {
            ID = iD;
            NumberValue = numberValue;
        }
    }
}
