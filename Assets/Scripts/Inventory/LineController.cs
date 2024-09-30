using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BagFight
{
    public class LineController : MonoBehaviour
    {
        public LineRenderer lr;
        public GameObject pointStart;
        public GameObject pointEnd;

        private float counter;
        float distance;

        public void DrawLine(Vector2 start, Vector2 end)
        {
            counter = 0;
            pointStart.transform.position = start;
            pointEnd.transform.position = end;
            lr.SetPosition(0, pointStart.transform.position);
            distance = Vector3.Distance(pointEnd.transform.position, pointStart.transform.position);
        }

        void Update()
        {
            if (counter < distance)
            {
                Vector3 pointB = pointEnd.transform.position;
                lr.SetPosition(1, pointB);
            }
        }
    }
}