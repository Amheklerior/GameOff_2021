using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The target to follow")]
    private Transform target;
    [SerializeField]
    private float moveSpeed = 0.3f;

    private Vector3 offset;
    private Vector3 prevPos;
    // Start is called before the first frame update
    void Start()
    {
        offset = target.transform.position - transform.position;
        prevPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(prevPos, target.transform.position - offset, moveSpeed);
        prevPos = transform.position;
    }
}
