using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects.Variables;
using UnityEngine;

public class PlayerGrappling : MonoBehaviour
{
    [SerializeField] private BoolVariable GrappinUnlocked;
    
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private DistanceJoint2D distanceJoint2D;
    [SerializeField] private LayerMask grapplingWallMask;
    [SerializeField] private float grapplingRadius;

    private bool _canGrap;
    private bool _groping;

    private GameObject _wallToGrab;

    // Start is called before the first frame update
    void Start()
    {
        distanceJoint2D.enabled = false;
        lineRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        _wallToGrab = null;
        
        _canGrap = false;
        
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
            
            _canGrap = true;
        }

        if (distanceJoint2D.enabled)
        {
            lineRenderer.SetPosition(1, transform.position);
        }

        if (PlayerMovement.Instance.GrapplingOccurs == false && _groping == true)
            DoGrapplingRelease();

    }

    private void DoGrappling()
    {
        if (_wallToGrab == null)
            return;

        PlayerMovement.Instance.GrapplingOccurs = true;
        _groping = true;
        lineRenderer.SetPosition(0, _wallToGrab.transform.position);
        lineRenderer.SetPosition(1, transform.position);
        
        distanceJoint2D.connectedAnchor = _wallToGrab.transform.position;
        distanceJoint2D.enabled = true;
        lineRenderer.enabled = true;
    }
    
    private void DoGrapplingRelease()
    {
        if(PlayerMovement.Instance.GrapplingOccurs == true)
            PlayerMovement.Instance.GrapplingOccurs = false;
        
        _groping = false;
        distanceJoint2D.enabled = false;
        lineRenderer.enabled = false;
    }

    private void OnGrappling()
    {
        if(_canGrap && GrappinUnlocked.value == true)
            DoGrappling();
    }

    private void OnGrapplingRelease()
    {
        if(GrappinUnlocked.value == true)
            DoGrapplingRelease();
    }
}
