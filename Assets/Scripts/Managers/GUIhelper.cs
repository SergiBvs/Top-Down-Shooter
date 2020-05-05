using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GUIhelper : MonoBehaviour
{
    public TextMeshProUGUI m_AmmoText;
    public Slider m_Slider;
    public Gradient m_Gradient;
    public Image fill;

    void Start()
    {
        GameManager.instance.ReassignObjs();
    }
}
