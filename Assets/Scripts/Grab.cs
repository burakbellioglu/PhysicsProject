using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{
    private GameObject selectedObject;
    public string tag;
    public GameObject canvas;
    public Outline outline;



    private void Start()
    {
        HideOtherObjects();

        //Outline calistir
        outline.enabled = true;


        if (selectedObject == null)
        {
            selectedObject = gameObject;
            Cursor.visible = false;
        }
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            if (selectedObject == null)
            {
                
                RaycastHit hit = CastRay();

                if (hit.collider != null)
                {
                    if (!hit.collider.CompareTag(tag) && !hit.collider.CompareTag("Canvas"))
                    {
                        canvas.SetActive(false);

                        //Outline kapat
                        outline.enabled = false;


                        ShowOtherObjects();

                        return;
                    }


                    Debug.Log("test1");

                    HideOtherObjects();

                    canvas = hit.transform.Find("Canvas_World").gameObject;
                    canvas.SetActive(true);

                    //Outline calistir
                    outline.enabled = true;

                }
               

            }
            else
            {
                Debug.Log("test");

                Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(selectedObject.transform.position).z);
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);
                selectedObject.transform.position = new Vector3(worldPosition.x, worldPosition.y, worldPosition.z); //Birakildigindaki konum

                selectedObject = null;
                Cursor.visible = true;

                canvas.SetActive(false);

                //Outline kapat
                outline.enabled = false;


                ShowOtherObjects();

            }
        }

        if (selectedObject != null)
        {

            //Outline calistir
            outline.enabled = true;

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


    public void ChangePos() //Konum butonuna basildi atamayi yap
    {
        canvas.SetActive(false);

        selectedObject = gameObject;
        Cursor.visible = false;

    }


    public void HideOtherObjects() //Fazla olusturmada buga girmemesi icin diger vectorlerin raycaste takilmasini engelleme
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);

        foreach (var object_ in objects)
        {
            object_.GetComponent<BoxCollider>().enabled = false;

        }

    }

    public void ShowOtherObjects()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);

        foreach (var object_ in objects)
        {
            object_.GetComponent<BoxCollider>().enabled = true;
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
