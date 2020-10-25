using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuyMovement : MonoBehaviour
{
    Rigidbody _rb;
    Rigidbody rb { get { if (!_rb) _rb = GetComponent<Rigidbody>(); return _rb; } }

    public Transform head;
    public float forceMult = 10;

    bool move;

    public GameObject spitPrefab;

    public Material sickMat;

    public Transform spitPivot;

    public static int totalPpl = 0;
    public static int totalInfected = 0;


    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    void Init()
    {
        totalInfected = 0;
        totalPpl = 0;
    }

    IEnumerator Start()
    {

        totalPpl++;

        if (infected)
            Infect(true);

        while (true)
        {
            Vector3 dir = Random.insideUnitSphere.normalized;
            dir.y = 0;
            transform.rotation = Quaternion.LookRotation(dir, Vector3.up);

            move = true;
            yield return new WaitForSeconds(3);
            move = false;
            yield return new WaitForSeconds(1);
            Spit();
        }
    }

    void FixedUpdate()
    {
        if (move)
        {
            rb.AddForce(transform.forward * forceMult);
        }
    }

    public bool infected = false;

    public void Infect(bool force = false)
    {
        if (!force && infected)
            return;
        Debug.Log("INFECTED");
        infected = true;

        var mr = head.GetComponent<MeshRenderer>();
        mr.material = sickMat;

        totalInfected++;
    }

    void Spit()
    {
        for (int i = 0; i < 10; i++)
        {
            Vector3 spread = Random.insideUnitSphere * 0.7f;
            float distance = 6;

            Vector3 dir = (head.forward + spread).normalized;

            var go = Instantiate(spitPrefab, spitPivot.position + Random.insideUnitSphere * 0.2f, Quaternion.LookRotation(dir));

            if (infected)
                go.GetComponent<SpitParticle>().isInfected = true;

            /*
            RaycastHit hit;
            if (Physics.Raycast(head.position, head.forward + spread, out hit, distance))
            {
                Debug.DrawRay(head.position, dir * distance, Color.red, 1);

                if (hit.collider.transform.GetComponentInParent<GuyMovement>())
                {
                    hit.collider.transform.GetComponentInParent<GuyMovement>().Infect();
                }
            }*/
        }

        Debug.Log("Spat");
    }
}
