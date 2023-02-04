using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrappling : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private DistanceJoint2D distanceJoint2D;
    [SerializeField] private LayerMask grapplingWallMask;
    [SerializeField] private float grapplingRadius;

    private bool canGrap;

    private GameObject _wallToGrab;

    // Start is called before the first frame update
    void Start()
    {
        distanceJoint2D.enabled = false;
        lineRenderer.enabled = false;
        canGrap = true;
    }

    // Update is called once per frame
    void Update()
    {
        _wallToGrab = null;
        
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, grapplingRadius, grapplingWallMask);
        foreach (Collider2D hit in hits)
        {
            if (_wallToGrab)
            {
                float distActualWall = Vector2.Distance(transform.position, _wallToGrab.transform.position);
                float distNextWall = Vector2.Distance(transform.position, hit.transform.position);

                if (distNextWall <= distActualWall)
                {
                    _wallToGrab = hit.gameObject;
                }
            }else
            {
                _wallToGrab = hit.gameObject;
            }
        }

        if (distanceJoint2D.enabled)
        {
            lineRenderer.SetPosition(1, transform.position);
        }
    }

    private void DoGrappling()
    {
        if (_wallToGrab == null)
            return;
        
        canGrap = false;
        lineRenderer.SetPosition(0, _wallToGrab.transform.position);
        lineRenderer.SetPosition(1, transform.position);
        
        distanceJoint2D.connectedAnchor = _wallToGrab.transform.position;
        distanceJoint2D.enabled = true;
        lineRenderer.enabled = true;
    }
    
    private void DoGrapplingRelease()
    {
        canGrap = true;
        distanceJoint2D.enabled = false;
        lineRenderer.enabled = false;
    }

    private void OnGrappling()
    {
        if(canGrap)
            DoGrappling();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, grapplingRadius);
    }

    private void OnGrapplingRelease()
    {
        DoGrapplingRelease();
    }
}
