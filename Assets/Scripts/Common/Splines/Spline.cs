using System.Collections.Generic;
using Infrastructure.Utils;
using UnityEngine;

namespace Common.Splines
{
    public class Spline : MonoBehaviour
    {
        [SerializeField] private int _segmentsAmount;
        [SerializeField] private bool _isCycled;
        [SerializeField] private Anchor[] _anchors;

        private readonly List<Vector2> _points = new List<Vector2>();

        public List<Vector2> Points
        {
            get
            {
                if (_points.Count <= 0)
                    CalculatePointsPositions();

                return _points;
            }
        }

        private void CalculatePointsPositions()
        {
            for (int i = 0; i < _anchors.Length; i++)
            {
                Anchor currentAnchor = _anchors[i];
                Anchor nextAnchor;

                if (i < _anchors.Length - 1)
                    nextAnchor = _anchors[i + 1];
                else if (_isCycled)
                    nextAnchor = _anchors[0];
                else
                    return;

                List<Vector2> points = GetPoints(currentAnchor, nextAnchor);
                
                _points.AddRange(points);
            }
        }
        
        private List<Vector2> GetPoints(Anchor currentAnchor, Anchor nextAnchor)
        {
            List<Vector2> points = new List<Vector2>();

            for (int i = 0; i < _segmentsAmount + 1; i++)
            {
                float step = (float)i / _segmentsAmount;
                
                Vector2 point = BezierCurve.GetPoint(currentAnchor.Position, currentAnchor.FirstHandlePosition,
                    currentAnchor.SecondHandlePosition, nextAnchor.Position, step);

                points.Add(point);
            }

            return points;
        }
        
        #if UNITY_EDITOR
        [SerializeField] private bool _drawGizmos = true;
        [SerializeField] private bool _stopCalculation;
        
        private readonly List<Vector2> _editorPoints = new List<Vector2>();

        private void OnDrawGizmos()
        {
            if (_drawGizmos == false)
                return;
            
            Gizmos.color = Color.white;

            if (_stopCalculation == false)
            {
                _editorPoints.Clear();
            
                for (int i = 0; i < _anchors.Length; i++)
                {
                    Anchor currentAnchor = _anchors[i];
                    Anchor nextAnchor;

                    if (i < _anchors.Length - 1)
                        nextAnchor = _anchors[i + 1];
                    else if (_isCycled)
                        nextAnchor = _anchors[0];
                    else
                        break;

                    List<Vector2> points = GetPoints(currentAnchor, nextAnchor);
                
                    _editorPoints.AddRange(points);
                }
            }

            for (int i = 0; i < _editorPoints.Count; i++)
            {
                if (i + 1 >= _editorPoints.Count)
                    return;
                
                Gizmos.DrawLine(_editorPoints[i], _editorPoints[i + 1]);
            }
        }
        #endif
    }
}