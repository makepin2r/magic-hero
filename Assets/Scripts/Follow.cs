using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset; // 위치 고정값

    // Update is called once per frame
    void Update()
    {
        transform.position = target.position + offset;
    }
}
