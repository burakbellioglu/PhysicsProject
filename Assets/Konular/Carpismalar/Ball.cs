using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Rigidbody rb;

    [Header("Kullanici Degerleri")]
    public int hiz;
    public int agirlik;
    
    [Header("Hesaplama")]
    public float realspeed;

    [Header("Durumu")]
    public bool carpisma;



    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        agirlik = (int)rb.mass;
    }

    private void Start()
    {
        rb.velocity = new Vector3(hiz, 0, 0);

        StartCoroutine(CalculatorSpeed());
    }


    IEnumerator CalculatorSpeed()
    {
        bool playing = true;

        while (playing)
        {
            Vector3 prevPosition = transform.position;

            yield return new WaitForFixedUpdate();

            realspeed = (Vector3.Distance(transform.position, prevPosition) / Time.fixedDeltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Carpismalar"))
        {
            carpisma = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        carpisma = false;
    }


}
