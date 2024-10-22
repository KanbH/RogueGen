using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundTest : MonoBehaviour
{
    public GameObject m_MyObject, m_NewObject;
    Collider2D m_Collider, m_Collider2;

    void Start()
    {
        //Check that the first GameObject exists in the Inspector and fetch the Collider
        if (m_MyObject != null)
            m_Collider = m_MyObject.GetComponentInChildren<CompositeCollider2D>();

        //Check that the second GameObject exists in the Inspector and fetch the Collider
        if (m_NewObject != null)
            m_Collider2 = m_NewObject.GetComponentInChildren<CompositeCollider2D>();

        m_NewObject.transform.position = m_NewObject.transform.position + m_NewObject.transform.position + m_NewObject.transform.position;
    }

    void Update()
    {
        //If the first GameObject's Bounds enters the second GameObject's Bounds, output the message
        if (m_Collider.bounds.Intersects(m_Collider2.bounds))
        {
            Debug.Log("Bounds intersecting");
        }
    }
}
