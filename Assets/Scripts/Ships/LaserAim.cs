using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserAim : MonoBehaviour
{
    [SerializeField] private Transform aimStartPoint;
    private LineRenderer lineRenderer;
    private bool shouldAim = true;
    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        if (aimStartPoint == null)
            aimStartPoint = transform.parent;
    }

    private void Update()
    {
        if (shouldAim)
        {
            lineRenderer.SetPosition(0, aimStartPoint.position);
            RaycastHit2D hit = Physics2D.Raycast(aimStartPoint.position, aimStartPoint.up);
            if (hit.collider)
            {
                lineRenderer.SetPosition(1, hit.point);
                if(hit.transform.CompareTag("Enemy"))
                {
                    SetLaserColor(Color.green);
                }
                else
                {
                    SetLaserColor(Color.red);
                }
            }
            else
            {
                lineRenderer.SetPosition(1, aimStartPoint.up * 5);
                SetLaserColor(Color.yellow);
            }
            
        }
    }

    public void SetShouldAim(bool shouldAim)
    {
        this.shouldAim = shouldAim;
    }

    private void SetLaserColor(Color color)
    {
        lineRenderer.startColor = color;
        lineRenderer.endColor = color;
    }
}
