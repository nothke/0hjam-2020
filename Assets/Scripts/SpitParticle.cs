using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitParticle : MonoBehaviour
{
    float timealive = 3;

    public bool isInfected = false;

    Rigidbody _rb;
    Rigidbody rb { get { if (!_rb) _rb = GetComponent<Rigidbody>(); return _rb; } }
    private IEnumerator Start()
    {
        rb.AddForce(transform.forward * 100);

        yield return new WaitForSeconds(Random.Range(2.0f, 3.0f));
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isInfected)
            return;

        var compo = collision.collider.gameObject.GetComponentInParent<GuyMovement>();

        if (compo)
        {
            compo.Infect();
        }
    }

}
