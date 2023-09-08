using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorCelling : MonoBehaviour
{
    [SerializeField] Transform m_playerTransform;
    [SerializeField] float m_distanceBeforeReset;

    void FixedUpdate()
    {
        float distanceToPlayer = m_playerTransform.position.z - transform.position.z;
        print("distance :" + distanceToPlayer);
        if(distanceToPlayer > m_distanceBeforeReset) transform.position = new Vector3(0f,0f,m_playerTransform.position.z);
    }
}
