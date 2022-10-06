using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserAim : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private bool shouldAim = true;
    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (shouldAim)
        {
            lineRenderer.SetPosition(0, transform.position);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up);
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
                lineRenderer.SetPosition(1, transform.up * 5);
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
