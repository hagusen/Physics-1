using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform target;


 

    // Update is called once per frame
    void Update()
    {

        Vector3 x =  Vector2.Lerp(transform.position, target.position, .01f);
        x.z = -10;
        transform.position = x;

    }
}
