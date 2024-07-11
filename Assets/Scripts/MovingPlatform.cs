using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Transform p1;
    [SerializeField] private Transform p2;
    [SerializeField] private Rigidbody rb;
    private Vector3 targetPosition;

    // Start is called before the first frame update
    void Start()
    {
        targetPosition = p1.position;    
    }

    void FixedUpdate()
    {
        Vector3 direction = (targetPosition - rb.position).normalized;
        rb.MovePosition(rb.position + speed * direction * Time.fixedDeltaTime);

        if (Vector3.Distance(rb.position,targetPosition) > 0.05f)
        {
            if (targetPosition == p1.position) 
                targetPosition = p2.position;
            else
                targetPosition = p1.position;
            
        }
    }
}
