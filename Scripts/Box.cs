using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    [Range(0, 3)] public int para;
    Rigidbody rigid;

    LightManager lightManage;
    public int index = 0;
    public Transform currentTarget;
    public Vector3 pos;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        lightManage = FindObjectOfType<LightManager>();
        index = Random.Range(0, lightManage.lights.Length);
    }

    private void Start()
    {
        GetComponent<MeshRenderer>().material.color = ColorManage.SetColor(para);
        SetNewTarget();
    }

    Transform parent; // box的父物体

    private void Update()
    {
        parent = transform.parent;
        if (!parent)
        {
            MoveTowardsTarget();
            rigid.isKinematic = false;
        }
        else
        {
            rigid.isKinematic = true;
        }
    }

    public float moveSpeed = 3.0f;
    public float rotateSpeed = 360.0f;
    public float stoppingDistance = 2.1f;

    void SetNewTarget()
    {
        currentTarget = lightManage.lights[index].transform;
        pos = currentTarget.position;
    }

    void MoveTowardsTarget()
    {
        float moveDistance = Vector3.Distance(transform.position, pos);

        if (moveDistance > stoppingDistance)
        {
            // 转向目标点
            Quaternion targetRotation = Quaternion.LookRotation(pos - transform.position);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);

            // 移动到目标点
            Vector3 moveDirection = (pos - transform.position).normalized;
            rigid.MovePosition(transform.position + moveDirection * moveSpeed * Time.deltaTime);
        }
        else
        {
            // 达到目标点，选择下一个目标点
            index = (index + 1) % lightManage.lights.Length;
            SetNewTarget();
        }
    }
}
