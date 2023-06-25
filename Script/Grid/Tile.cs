using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GridType
{
    Reachable = 1,
    Unreachable = 2,
    Start = 3,
    End = 4,
    
}

public class Tile : MonoBehaviour
{
    [SerializeField] private Color m_NormalColor;
    [SerializeField] private Color m_OffSetColor;
    [SerializeField] private GameObject m_HightLight;
    public void InitView(bool isOffSet)
    {
        var spriteRender = GetComponent<SpriteRenderer>();
        spriteRender.color = isOffSet ? m_OffSetColor : m_NormalColor;
    }

    protected void OnMouseEnter()
    {
        m_HightLight.SetActive(true);
    }

    protected void OnMouseExit()
    {
        m_HightLight.SetActive(false);
    }
}
