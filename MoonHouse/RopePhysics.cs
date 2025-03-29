using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopePhysics : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public int segmentCount = 15;
    public float segmentLength = 0.1f;
    public float ropeWidth = 0.1f;
    public Vector3 gravity = new Vector3(0f, -9.81f, 0f);
    [Space(10f)]
    public Transform startTransform;

    private List<Segment> segments = new List<Segment>();

    private void Reset()
    {
        TryGetComponent(out lineRenderer);
    }

    private void Awake()
    {
        Vector3 segmentPos = startTransform.position;
        for (int i = 0; i < segmentCount; i++)
        {
            segments.Add(new Segment(segmentPos));
            segmentPos.y -= segmentLength;
        }
    }

    private void FixedUpdate()
    {
        UpdateSegments();
        ApplyConstraint();
        DrawRope();
    }

    private void DrawRope()
    {
        lineRenderer.startWidth = ropeWidth;
        lineRenderer.endWidth = ropeWidth;
        Vector3[] segmentPositions = new Vector3[segments.Count];
        for (int i = 0; i < segments.Count; i++)
        {
            segmentPositions[i] = segments[i].position;
        }
        lineRenderer.positionCount = segmentPositions.Length;
        lineRenderer.SetPositions(segmentPositions);
    }

    private void UpdateSegments()
    {
        for (int i = 0; i < segments.Count; i++)
        {
            segments[i].velocity = segments[i].position - segments[i].previousPos;
            segments[i].previousPos = segments[i].position;
            segments[i].position += gravity * Time.fixedDeltaTime * Time.fixedDeltaTime;
            segments[i].position += segments[i].velocity;
        }
    }

    private void ApplyConstraint()
    {
        segments[0].position = startTransform.position;
        for (int i = 0; i < segments.Count - 1; i++) {
            float distance = (segments[i].position - segments[i + 1].position).magnitude;
            float difference = segmentLength - distance;
            Vector3 dir = (segments[i + 1].position - segments[i].position).normalized;

            Vector3 movement = dir * difference;
            if (i == 0)
                segments[i + 1].position += movement;
            else
            {
                segments[i].position -= movement * 0.5f;
                segments[i + 1].position += movement * 0.5f;
            }
        }
    }

    public class Segment
    {
        public Vector3 previousPos;
        public Vector3 position;
        public Vector3 velocity;

        public Segment(Vector3 _position)
        {
            previousPos = _position;
            position = _position;
            velocity = Vector3.zero;
        }
    }
}
