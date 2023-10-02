using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextChanger : MonoBehaviour
{
    public TextMeshProUGUI target;


    private Slider myText;
    private void Awake()
    {
        myText = GetComponent<Slider>();
    }


    private void Update()
    {
        target.text = myText.value.ToString();
    }

}
