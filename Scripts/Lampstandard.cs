using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lampstandard : MonoBehaviour
{
    [Range(0, 3)] public int para;
    private Color color;
    private Material material;
    public bool isBright;

    private void Awake()
    {
        color = ColorManage.SetColor(para);
        Transform child = transform.GetChild(0); // ȷ��������ȷ���Ӷ���
        if (child != null)
        {
            MeshRenderer meshRenderer = child.GetComponent<MeshRenderer>();
            if (meshRenderer != null)
            {
                material = meshRenderer.material;
                material.color = color;
            }
            else
            {
                Debug.LogError("��������û�� MeshRenderer ���");
            }
        }
        else
        {
            Debug.LogError("�Ҳ���������");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        MeshRenderer otherRenderer = other.GetComponent<MeshRenderer>();
        if (otherRenderer != null && otherRenderer.material.color == color)
        {
            isBright = true;
            material.color = color;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        MeshRenderer otherRenderer = other.GetComponent<MeshRenderer>();
        if (otherRenderer != null && otherRenderer.material.color == color)
        {
            isBright = false;
            material.color = Color.white;
        }
    }
}
