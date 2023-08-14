using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparadorArma : MonoBehaviour
{
    public Transform canon;
    public float alcance;
    public LayerMask layerMask;
    public GameObject efectoImpacto;


    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Disparar();
        }
    }

    public void Disparar()
    {
        Ray rayo = new Ray(canon.position, canon.up);
        Debug.DrawRay(rayo.origin, rayo.direction * alcance, Color.yellow, 5f);
        RaycastHit hit;

        if(Physics.Raycast(rayo, out hit, alcance, layerMask))
        {
            GameObject impactEffect = Instantiate(efectoImpacto, hit.point, Quaternion.FromToRotation(Vector3.up ,hit.normal));
            Destroy(impactEffect, 4f);
        }
    }
}
