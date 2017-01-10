using UnityEngine;
using System.Collections;

public class ScrLine : MonoBehaviour
{
    //private static Transform _lineRoot;
    public Transform From;
    public Transform To;

    public float MaxAng = 10f;
    public float RadiusLine = 190f;
    public float MaxHeight = 25f;
    public float HeightK = 1f;
    public float HeightMinus = 3f;
    public float HeightKCount = 1f;

    public bool IsVisibl = true;

    private LineRenderer lineRenderer;
    private bool LstIsVisibl;

    void Start()
    {
        if (!lineRenderer)
        {
            lineRenderer = GetComponent<LineRenderer>();
        }
        
        LstIsVisibl = IsVisibl;
        lineRenderer.enabled = IsVisibl;
    }

	void Update () {
        if(LstIsVisibl != IsVisibl)
        {
            lineRenderer.enabled = IsVisibl;
            LstIsVisibl = IsVisibl;
        }
        if (From == null || To == null)
        {
            return;
        }
        lineRenderer.SetPosition(0, From.position);
        int N = 2;
        if (Vector3.Angle(From.position, To.position) > MaxAng)
        {
            Vector3 A = From.position;
            Vector3 B = To.position;

            float Ang = Vector3.Angle(From.position, To.position);
            int n = (int)(Ang / MaxAng) - 1;
            N = n + 2;
            lineRenderer.SetVertexCount(N);
            for (int i = 1; i < n + 1; i++)
            {
                Vector3 buf;
                buf = Vector3.RotateTowards(A, B, MaxAng * i / 57, 0);
                float k;
                if(i < n-i)
                {
                    k = Mathf.Sqrt(Mathf.Sqrt(n * HeightKCount * i)) * HeightK;
                }
                else
                {
                    k = Mathf.Sqrt(Mathf.Sqrt(n * HeightKCount * (n - i))) * HeightK;
                }
                k -= HeightMinus;
                if(k > MaxHeight)
                {
                    k = MaxHeight;
                }
                buf = buf.normalized * (RadiusLine + k);
                
                lineRenderer.SetPosition(i, buf);
            }
        }
        lineRenderer.SetPosition(N - 1, To.position);
	}

    public Transform GetAnother(Transform one)
    {
        if(one == From)
        {
            return To;
        }
        else
        {
            return From;
        }
    }
}
