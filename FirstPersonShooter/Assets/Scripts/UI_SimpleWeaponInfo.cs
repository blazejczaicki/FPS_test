using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_SimpleWeaponInfo : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_TextMeshPro;

    public void SetInfo(List<ObjectPhysicalMaterials> mats)
    {
        string txt = "";
        foreach (ObjectPhysicalMaterials mat in mats) 
        {
            txt += mat.ToString() + "\n";
        }
        m_TextMeshPro.text = txt;
    }
}
