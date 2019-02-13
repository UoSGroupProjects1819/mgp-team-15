using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    private Transform EndPoint;
    public Teleport EndPorter;
    private bool wait = false;

    private void Start()
    {
        EndPoint = EndPorter.transform;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (wait) { return; }
        if (collision.transform.tag == "Player")
        {
            EndPorter.wait = true;
            collision.transform.position = EndPoint.transform.position;
            StartCoroutine("waitReset");
        }
    }

    private IEnumerator waitReset()
    {
        yield return new WaitForSeconds(2);
        EndPorter.wait = false;
    }
}
