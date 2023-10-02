using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Carpismalar : MonoBehaviour
{
    [Header("Toplar")]
    public Ball top1;
    public Ball top2;

    [Header("UI")]
    public GameObject infoPanel;

    [Header("Materials")]
    public PhysicMaterial wallPhysics;




    private void Update()
    {
        infoPanel.transform.Find("Top1").Find("Hiz").Find("Value").GetComponent<TextMeshProUGUI>().text = top1.realspeed.ToString("F2");
        infoPanel.transform.Find("Top2").Find("Hiz").Find("Value").GetComponent<TextMeshProUGUI>().text = top2.realspeed.ToString("F2");
    }


    public void StartSimulation()
    {
        top1.enabled = true;
        top2.enabled = true;
    }

    public void ResetSimulation()
    {
        SceneManager.LoadScene("Lab");
    }

    public void Gravity()
    {
        if (EventSystem.current.currentSelectedGameObject.transform.gameObject.GetComponent<Toggle>().isOn)
        {
            top1.GetComponent<Rigidbody>().useGravity = true;
            top2.GetComponent<Rigidbody>().useGravity = true;
        }
            
        else
        {
            top1.GetComponent<Rigidbody>().useGravity = false;
            top2.GetComponent<Rigidbody>().useGravity = false;
        }                
    }
    public void Friction()
    {
        if (EventSystem.current.currentSelectedGameObject.transform.gameObject.GetComponent<Toggle>().isOn)
        {
            top1.GetComponent<Rigidbody>().drag = 1;
            top2.GetComponent<Rigidbody>().drag = 1;
        }

        else
        {
            top1.GetComponent<Rigidbody>().drag = 0;
            top2.GetComponent<Rigidbody>().drag = 0;
        }
    }
    public void Elastic()
    {
        if (EventSystem.current.currentSelectedGameObject.transform.gameObject.GetComponent<Toggle>().isOn)
            wallPhysics.bounciness = 1;

        else
            wallPhysics.bounciness = 0;
    }

    public void ChangeColorTop1(Material mat)
    {
        top1.GetComponent<MeshRenderer>().material = mat;
    }

    public void ChangeColorTop2(Material mat)
    {
        top2.GetComponent<MeshRenderer>().material = mat;
    }

    public void KutleTop1(Slider slider)
    {
        top1.rb.mass = slider.value;
    }

    public void HizTop1(Slider slider)
    {
        top1.hiz = (int)slider.value;      
    }

   
    public void KutleTop2(Slider slider)
    {
        top2.rb.mass = slider.value;
    }

    public void HizTop2(Slider slider)
    {
        top2.hiz = (int)slider.value * -1;
    }


}
