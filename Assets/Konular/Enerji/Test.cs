using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();    
    }


    private void Start()
    {
        rb.velocity = new Vector3(1, 0, 0);

        StartCoroutine(CalculatorSpeed());
    }

    IEnumerator CalculatorSpeed()
    {
        bool playing = true;

        while (playing)
        {
            Vector3 prevPosition = transform.position;

            yield return new WaitForFixedUpdate();

            float realspeed = (Vector3.Distance(transform.position, prevPosition) / Time.fixedDeltaTime);

            Debug.Log(realspeed);
        }
    }
}
