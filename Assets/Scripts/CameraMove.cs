using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform Target;
    private Vector3 moveVector,startDistance;
 
    void Update()
    {
        moveVector = Target.position;

        moveVector.z = -1;
        moveVector.y = Target.position.y;
        moveVector.x = Target.position.x;
        
        transform.position = moveVector;
    }
}
