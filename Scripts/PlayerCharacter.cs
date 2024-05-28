using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerCharacter : MonoBehaviour
{
    Rigidbody rig;
    Transform body;
    Transform hand;
    private void Start()
    {
        rig=GetComponent<Rigidbody>();
        body = transform.Find("Body");
        Material bodyMaterial = body.GetComponent<MeshRenderer>().material;
        bodyMaterial.color = Color.black;
        hand = body.Find("Hands");
        BoxCollider[] hands = hand.GetComponentsInChildren<BoxCollider>();
        foreach (var hand in hands)
        {
            hand.GetComponent<MeshRenderer>().material.color = Color.black;
        }
    }

    public float moveSpeed;
    public void PlayerToMove(float x, float z)
    {
        x *= Time.deltaTime * moveSpeed;
        z*= Time.deltaTime * moveSpeed;
        rig.velocity = new Vector3(x, rig.velocity.y, z);
    }

    public float limit_x, limit_z;

    //Player�ƶ�λ������
    public void PosLimit()
    {
        Vector3 pos = transform.position;
        if (pos.x < -limit_x)
        {
            pos.x = -limit_x;
        }
        else if (pos.x > limit_x)
        {
            pos.x = limit_x;
        }
        if (pos.z < -limit_z)
        {
            pos.z = -limit_z;
        }
        else if (pos.z > limit_z)
        {
            pos.z=limit_z;
        }
        transform.position=pos;
    }

    public LayerMask layer;
    
    //Player�ϰ�����Ƶķ���
    public void BodyCharacter()
    {
        body.position = transform.position + Vector3.up;
        Quaternion rota = new Quaternion(0, body.rotation.y, 0, body.rotation.w);
        body.rotation = rota;

        Ray ray=Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hits;
        if (Physics.Raycast(ray, out hits, 100, layer)) 
        {
            Debug.DrawLine(Camera.main.transform.position,hits.point,Color.red);
            Vector3 lookTarget = new Vector3(hits.point.x, body.position.y, hits.point.z);
            body.LookAt(lookTarget);
        }
    }

    public bool stretching = false;
    bool stretch = true;
    public float maxLength;
    public float pullSpeed;

    //Player�ֱ������ķ���
    public void Pull()
    {
        //������������ʱ
        if (stretching)
        {
            Vector3 scale = hand.localScale;            
            if (stretch)
            {
                scale.z += Time.deltaTime * pullSpeed;
            }
            else
            {
                scale.z-=Time.deltaTime * pullSpeed;
            }
            //�ֱ����ص�ԭ�еĳ��ȣ��ָ�ԭ������ֹͣ��������״̬
            if (scale.z <= 1)
            {
                scale.z = 1;
                stretching = false;
            }
            //�ֱ۴ӳ�ʼ״̬��ǰ�쳤���ﵽ���Դ󳤶�ʱ����
            if (scale.z ==1)
            {
                stretch = true;
            }
            else if (scale.z >= maxLength)
            {
                stretch = false;
            }
            hand.localScale = scale;
        }
    }

    Transform box;
    
    //�����������ײ����ļ��
    private void OnTriggerEnter(Collider other)
    {
        if (!box && stretching && other.GetComponent<Box>())
        {
            box=other.transform;
        }
    }

    //Player��ȡ����
    public void PickUp()
    {
        if (box)
        {
            stretch = false;
            box.parent = body;
            Vector3 pos = body.position + body.forward * 2;
            box.position = Vector3.MoveTowards(box.position, pos, Time.deltaTime * pullSpeed);
            box.rotation = Quaternion.Lerp(box.rotation, body.rotation, Time.deltaTime * 5);
        }
    }

    //Player�������ӵķ���
    public void PutDown()
    {
        if (box)
        {
            box.SetParent(null);
            box = null;
        }
    }
}
