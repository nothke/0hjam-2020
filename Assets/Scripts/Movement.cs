using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody _rb;
    Rigidbody rb { get { if (!_rb) _rb = GetComponent<Rigidbody>(); return _rb; } }

    private void Awake()
    {
        GuyMovement.totalInfected = 0;
        GuyMovement.totalPpl = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            Application.LoadLevel(0);
    }

    void FixedUpdate()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane p = new Plane(Vector3.up, Vector3.zero);
        p.Raycast(ray, out float distance);
        Vector3 groundPos = ray.GetPoint(distance);

        groundPos.y = 0;

        Vector3 dir = (groundPos - transform.position).normalized;
        dir.y = 0;
        transform.rotation = Quaternion.LookRotation(dir, Vector3.up);

        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        rb.AddForce(input * 500);


    }
}
