using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserAim : MonoBehaviour
{
    [SerializeField] private Transform aimStartPoint;
    [SerializeField] private float laserDistance = 5f;
    private LineRenderer lineRenderer;
    private bool shouldAim;
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
            RaycastHit2D hit = Physics2D.Raycast(aimStartPoint.position, aimStartPoint.right, laserDistance, ~PlayerShipController.PlayerLayer);
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
                lineRenderer.SetPosition(1, aimStartPoint.position + aimStartPoint.right * laserDistance);
                SetLaserColor(Color.yellow);
            }
        }
        else
        {
            SetLaserColor(new Color(0,0,0,0));
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
