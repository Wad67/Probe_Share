using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalIndicator : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Rigidbody rb;

    void Start()
    {
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        Rigidbody rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.SetPosition(0,this.transform.position);
        lineRenderer.SetPosition(1, rb.velocity * 50);
    }
}
