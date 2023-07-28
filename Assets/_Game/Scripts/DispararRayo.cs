using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DispararRayo : MonoBehaviour
{
    public float distancia;
    public LayerMask mask;
    public GameObject prefab;

    public GameObject selectedAlly;
    public GameObject selectedEnemy; 
    private Camera camera;
    private bool siguiendo;

    private void Start()
    {
        camera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            //StartCoroutine(DisparaCoroutine(5)); //Disparar el rayo por tiempo
            DisparaRayo();

        if(siguiendo)
        {
            selectedAlly.GetComponent<NavMeshAgent>().SetDestination(selectedEnemy.transform.position);
        }
    }

    void DisparaRayo()
    {
        Ray rayo = camera.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(rayo.origin, rayo.direction * distancia, Color.green);

        RaycastHit hit;        

        if (Physics.Raycast(rayo, out hit, distancia, mask))
        {
            if (hit.collider.CompareTag("Seleccionable"))
            {
                selectedAlly = hit.collider.gameObject;
            }
            else if (hit.collider.CompareTag("Enemy"))
            {
                selectedEnemy = hit.collider.gameObject;
                if(selectedAlly != null)
                {
                    siguiendo = true;
                }
            }
            else if (hit.collider.CompareTag("Piso"))
            {
                if(selectedAlly != null)
                {
                    selectedAlly.GetComponent<NavMeshAgent>().SetDestination(hit.point);
                    siguiendo = false;
                }
            }

            //Instantiate(prefab, hit.point, Quaternion.identity); //Aparecen cubos con el clic.
        }
        else
        {
            selectedAlly = null;
        }
    }

    IEnumerator DisparaCoroutine(float tiempo)
    {
        while(tiempo > 0)
        {
            DisparaRayo();
            tiempo -= Time.deltaTime;
            yield return null;
        }        
    }
}
