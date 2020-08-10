using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mark : MonoBehaviour
{
    public RaycastHit hit;
    public bool isColl;

    public bool RaycastCheck() // PARA EVITAR QUE O PLAYER CONSTRUA DUAS CASAS NO MESMO LUGAR
    {
        Debug.DrawRay(transform.position, transform.up * 5f, Color.red);
        Ray ray = new Ray(transform.position, Vector3.up);

        if (Physics.Raycast(ray, out hit, 2f))
        {
            return true;
        }
        else
        {
            return false;
        }

        
    }
}
