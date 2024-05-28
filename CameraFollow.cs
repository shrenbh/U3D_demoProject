using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollows : MonoBehaviour
{
    Transform player;
    public float heightr, distance;
    private void Start()
    {
        player=GameObject.Find("Player").transform;
    }

    private void Update()
    {
        Quaternion dir = Quaternion.LookRotation(player.position - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, dir, Time.deltaTime);
    }
}
