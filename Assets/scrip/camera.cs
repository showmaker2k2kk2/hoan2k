using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    Transform target;
    Vector3 velocity = Vector3.zero;
    [Range(0, 1)]
    public float smoothTime;
    public Vector3 positionOfset;

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;

    }
    private void LateUpdate()
    {
        Vector3 targetposition = target.position+positionOfset;
        transform.position = Vector3.SmoothDamp(transform.position, targetposition, ref velocity, smoothTime);

    }
    
}
