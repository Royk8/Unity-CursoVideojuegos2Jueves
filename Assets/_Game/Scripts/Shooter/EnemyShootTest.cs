using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootTest : MonoBehaviour
{
    public Transform crosshair;
    public Transform target;
    public Transform otherTarget;
    public Transform raycaster;

    public float lerpSpeed = 0.1f;
    public float radious;

    // Update is called once per frame
    void Update()
    {
        Vector3 shootTarget = target.transform.position + new Vector3(
            Random.Range(-radious, radious),
            Random.Range(-radious, radious),
            Random.Range(-radious, radious));

        otherTarget.position = shootTarget;

        Vector3 apuntado = Vector3.Lerp(crosshair.position, shootTarget, lerpSpeed * Time.deltaTime);

        Ray ray = new Ray(raycaster.position, apuntado - raycaster.position);
        Debug.DrawRay(ray.origin, ray.direction * 15, Color.yellow);
        if (Physics.Raycast(ray.origin, ray.direction, out RaycastHit hit, 15))
        {

        }
    }
}
