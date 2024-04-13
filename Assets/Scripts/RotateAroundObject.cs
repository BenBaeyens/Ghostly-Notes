using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundObject : MonoBehaviour
{
    public Transform target;
    public float speed = 1.0f;

    void Update()
    {
        transform.RotateAround(target.position, Vector3.up, speed * Time.deltaTime);
    }
}
