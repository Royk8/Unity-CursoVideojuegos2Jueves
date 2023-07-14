using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lanzador : MonoBehaviour
{
    public float potencia;
    public float velocidadRotacion;
    public GameObject prefabProyectil;
    public Transform puntoRecarga;

    private GameObject proyectil;
    private Rigidbody rb;

    void Start()
    {
        CargarProyectil();
    }

    void Update()
    {
        Lanzar();
        Rotar();
    }

    void Rotar()
    {
        float inputHorizontal = Input.GetAxis("Horizontal");
        float inputVertical = Input.GetAxis("Vertical");

        transform.Rotate(new Vector3(inputVertical, inputHorizontal, 0) * Time.deltaTime * velocidadRotacion);
    }

    void LanzarDeVerdad()
    {
        if (proyectil != null)
        {
            rb.isKinematic = false;
            rb.AddForce(transform.forward * potencia, ForceMode.Impulse);
            proyectil.transform.parent = null;
            rb = null;
            proyectil = null;
            StartCoroutine(RecargaCorutina());
        }
        else
        {
            Debug.Log("Sin municion");
        }
    }

    void Lanzar()
    {
        if (Input.GetButtonDown("Jump"))
        {
            LanzarDeVerdad();
        }
    }

    [ContextMenu("CargarProyectil")]
    void CargarProyectil()
    {
        proyectil = Instantiate(prefabProyectil, puntoRecarga.position, puntoRecarga.rotation);
        proyectil.transform.parent = transform;
        rb = proyectil.GetComponent<Rigidbody>();
    }

    public IEnumerator RecargaCorutina()
    {
        yield return new WaitForSeconds(1);
        CargarProyectil();
    }


    // new Vector3(0, 1, 0)
    // Vector3.up ==> (0, 1, 0)
    // Vector3.right ==> (1, 0, 0)
    // Vector3.forward ==> (0, 0, 1)
}
