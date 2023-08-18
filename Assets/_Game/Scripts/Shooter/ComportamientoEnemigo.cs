using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ComportamientoEnemigo : MonoBehaviour
{
    public EstadoEnemigo estado = EstadoEnemigo.Patrullando;
    public Transform ojos;
    public float distanciaVisual;
    public GameObject fantasmaDelJugador;

    private NavMeshAgent agente;
    private Transform jugador;
    private Vector3 ultimaPosicion;
    

    private void Start()
    {
        agente = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        switch (estado)
        {
            case EstadoEnemigo.Patrullando:
                Patrullar();
                break;
            case EstadoEnemigo.Persiguiendo:
                Perseguir();
                break;
            case EstadoEnemigo.Atacando:
                Atacar();
                break;
            case EstadoEnemigo.Buscando:
                Buscar();
                break;
        }
    }

    public void Patrullar()
    {
        //Camina entre diferentes puntos en el mapa
    }

    public void Perseguir()
    {
        agente.SetDestination(jugador.position);
    }

    public void Atacar()
    {
        //Ataca al jugador con su arma.
    }

    public void Buscar()
    {
        //Revisa la ultima posicion conocida del jugador.
        agente.SetDestination(ultimaPosicion);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Un jugador entro en el area

            Vector3 rayoAlEnemigo = other.transform.position - ojos.position;
            Ray rayo = new Ray(ojos.position, rayoAlEnemigo + Vector3.up);

            if(Physics.Raycast(rayo, out RaycastHit hit, distanciaVisual))
            {
                //Rayo detecta algo: Enemigo Viendo algo
                if (hit.collider.CompareTag("Player"))
                {
                    //VIMOS AL JUGADOR!!!
                    
                    jugador = other.transform;
                    ultimaPosicion = other.transform.position;

                    if(estado == EstadoEnemigo.Buscando)
                    {
                        fantasmaDelJugador.SetActive(false);
                    }

                    estado = EstadoEnemigo.Persiguiendo;
                }
                else
                {
                    //HAY ALGO OBSTRUYENDO
                    if(estado == EstadoEnemigo.Persiguiendo || estado == EstadoEnemigo.Atacando)
                    {
                        estado = EstadoEnemigo.Buscando;
                        fantasmaDelJugador.transform.position = ultimaPosicion;
                        fantasmaDelJugador.SetActive(true);
                    }
                }

            }
            {
                //No esta viendo nada.
            }


        }
    }
}