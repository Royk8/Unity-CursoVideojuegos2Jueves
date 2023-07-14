using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorObjetivo : MonoBehaviour
{
    public List<GameObject> listaDiscos;
    public int puntajeQueSuma = 5;

    //private void OnCollisionExit(Collision collision)
    //private void OnCollisionStay(Collision collision)
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Projectil"))
        {
            foreach (GameObject disco in listaDiscos)
            {
                Rigidbody rb = disco.GetComponent<Rigidbody>();
                rb.isKinematic = false;
                MeshCollider mc = disco.GetComponent<MeshCollider>();
                mc.enabled = true;

                Vector3 direccion = new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f), Random.Range(0, 10f));
                rb.AddForce(direccion, ForceMode.Impulse);
            }

            PuntajeSingleton.Instance.CambiarPuntaje(puntajeQueSuma);
            puntajeQueSuma = 0;
            Destroy(transform.parent.gameObject, 10f);
        }
    }
}
