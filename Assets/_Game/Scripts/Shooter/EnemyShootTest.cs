using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootTest : MonoBehaviour
{
    public Transform crosshair;
    public Transform target;
    public Transform otherTarget;

    public float lerpSpeed = 0.1f;
    public float radious;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 shootTarget = target.transform.position + new Vector3(
            Random.Range(-radious, radious),
            Random.Range(-radious, radious),
            Random.Range(-radious, radious));

        otherTarget.position = shootTarget;


        crosshair.position = Vector3.Lerp(crosshair.position, shootTarget, lerpSpeed * Time.deltaTime);
    }
}
