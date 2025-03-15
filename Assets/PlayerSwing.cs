using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerSwing : MonoBehaviour
{
    public LineRenderer lr;
    public float pullSpeed = 2f;
    public float swingForce = 10f;

    private DistanceJoint2D dj;
    private Rigidbody2D rb;
    private Transform swingAnchor;
    private bool isSwinging = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dj = gameObject.AddComponent<DistanceJoint2D>();
        dj.enabled = false;
        lr.enabled = false;
    }

    private void Update()
    {
        if (isSwinging)
        {
            Vector2 ropeDirection = (swingAnchor.position - transform.position).normalized;
            Vector2 perpendicular = new Vector2(ropeDirection.y, -ropeDirection.x);
            
            if (Input.GetKey(KeyCode.A))
            {
                rb.AddForce(-perpendicular * swingForce, ForceMode2D.Force);
            }

            if (Input.GetKey(KeyCode.D))
            {
                rb.AddForce(perpendicular * swingForce, ForceMode2D.Force);
            }

            if (Input.GetKey(KeyCode.W))
            {
                dj.distance -= pullSpeed * Time.deltaTime;
            }

            if (Input.GetKey(KeyCode.S))
            {
                dj.distance += pullSpeed * Time.deltaTime;
            }

            UpdateRope();
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StopSwinging();
            }
        }


    }

    public void Swing(Transform anchor)
    {
        swingAnchor = anchor;
        dj.connectedAnchor = anchor.position;
        dj.distance = Vector2.Distance(transform.position, anchor.position);
        dj.enabled = true;
        isSwinging = true;
        
        lr.enabled = true;
        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, anchor.position);

        rb.gravityScale = 1f;

        dj.distance--;
    }

    void StopSwinging()
    {
        isSwinging = false;
        dj.enabled = false;
        lr.enabled = false;
        
        GetComponent<PlayerMovement>().ToggleMovement();
    }

    void UpdateRope()
    {
        if (isSwinging)
        {
            lr.SetPosition(0, transform.position);
            lr.SetPosition(1, swingAnchor.position);
        }
    }
}
