using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vector : MonoBehaviour
{
    private GameObject selectedObject;

    public GameObject canvas;

    public GameObject arrow;



    public void Up()
    {
        float number = (float)arrow.transform.localScale.x;
        number += 0.20f;
        arrow.transform.localScale = new Vector3(number, arrow.transform.localScale.y, arrow.transform.localScale.z);
    }


    private void Start()
    {
        HideOtherVectors();

        //Outline calistir
        gameObject.transform.Find("Front_Pivot").GetChild(0).GetComponent<Outline>().enabled = true;
        gameObject.transform.Find("Middle_Pivot").GetChild(0).GetComponent<Outline>().enabled = true;


        if (selectedObject == null)
        {
            selectedObject = gameObject;
            Cursor.visible = false;
        }

       

    }



    public void ChangePos() //Konum butonuna basildi atamayi yap
    {
        canvas.SetActive(false);

        selectedObject = gameObject;
        Cursor.visible = false;

    }


    public void HideOtherVectors() //Fazla olusturmada buga girmemesi icin diger vectorlerin raycaste takilmasini engelleme
    {
        GameObject[] vectors = GameObject.FindGameObjectsWithTag("Vector");

        foreach (var vector in vectors)
        {
            vector.GetComponent<BoxCollider>().enabled = false;

        }

    }

    public void ShowOtherVectors()
    {
        GameObject[] vectors = GameObject.FindGameObjectsWithTag("Vector");

        foreach (var vector in vectors)
        {
            vector.GetComponent<BoxCollider>().enabled = true;
        }
    }


    //Basta hareket ettir ve istenilen yere birak
    //Tiklandiginda canvasi ac
    //Konuma bastiginda hareket edebilsin bi yere tikladiginda biraksin ve canvas kapansin




    private void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {

            if (selectedObject == null) //Secilmeyi aktif et ve atama yap
            {
                //Eger vectoru sectiysen
                //Canvasi ac ve outline

                RaycastHit hit = CastRay();
                if (hit.collider != null)
                {
                    if (!hit.collider.CompareTag("Vector"))
                    {
                        return;
                    }

                    HideOtherVectors();

                    canvas = hit.transform.Find("Vector_Canvas").gameObject;
                    canvas.SetActive(true);
                    //Outline calistir

                    hit.transform.Find("Front_Pivot").GetChild(0).GetComponent<Outline>().enabled = true;
                    hit.transform.Find("Middle_Pivot").GetChild(0).GetComponent<Outline>().enabled = true;
                }


            }

            else //Secilmisse yani hareket ederken bir kez daha mouse a tiklarsan vectoru birak
            {
                Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(selectedObject.transform.position).z);
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);
                selectedObject.transform.position = new Vector3(worldPosition.x, worldPosition.y, worldPosition.z); //Birakildigindaki konum

                selectedObject = null;
                Cursor.visible = true;

                canvas.SetActive(false);

                //Outline kapat
                gameObject.transform.Find("Front_Pivot").GetChild(0).GetComponent<Outline>().enabled = false;
                gameObject.transform.Find("Middle_Pivot").GetChild(0).GetComponent<Outline>().enabled = false;


                ShowOtherVectors();
            }
        }

        if (selectedObject != null) //Secildi
        {
            //Outline calistir
            gameObject.transform.Find("Front_Pivot").GetChild(0).GetComponent<Outline>().enabled = true;
            gameObject.transform.Find("Middle_Pivot").GetChild(0).GetComponent<Outline>().enabled = true;

            Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(selectedObject.transform.position).z);
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);
            selectedObject.transform.position = new Vector3(worldPosition.x, worldPosition.y, worldPosition.z); //Hareket konumu

            if (Input.GetKeyDown(KeyCode.Q))
            {
                selectedObject.transform.rotation = Quaternion.Euler(new Vector3(
                    selectedObject.transform.rotation.eulerAngles.x,
                    selectedObject.transform.rotation.eulerAngles.y + 90f,
                    selectedObject.transform.rotation.eulerAngles.z));
            }
        }
       
    }

    private RaycastHit CastRay()
    {
        Vector3 screenMousePosFar = new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            Camera.main.farClipPlane);
        Vector3 screenMousePosNear = new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            Camera.main.nearClipPlane);
        Vector3 worldMousePosFar = Camera.main.ScreenToWorldPoint(screenMousePosFar);
        Vector3 worldMousePosNear = Camera.main.ScreenToWorldPoint(screenMousePosNear);
        RaycastHit hit;
        Physics.Raycast(worldMousePosNear, worldMousePosFar - worldMousePosNear, out hit);

        return hit;
    }

}
