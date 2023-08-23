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
    public GameObject bullet;
    public Transform canon;
    public float bulletSpeed;
    public float ShootCooldown;

    private float lastShoot;
    private NavMeshAgent agente;
    private Transform jugador;
    private Vector3 ultimaPosicion;
    private EnemyAnimator animator;
    

    private void Start()
    {
        agente = GetComponent<NavMeshAgent>();
        animator = GetComponent<EnemyAnimator>();
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
        animator.SetShoot(true);
        Shoot();
    }

    public void Atacar()
    {
        //Ataca al jugador con su arma.
        Shoot();
    }

    public void Buscar()
    {
        //Revisa la ultima posicion conocida del jugador.
        agente.SetDestination(ultimaPosicion);
        animator.SetShoot(false);
    }

    public void Shoot()
    {
        if((lastShoot + ShootCooldown) > Time.time)
        {
            return;
        }
        GameObject bullet = Instantiate(this.bullet, canon.transform.position, 
            Quaternion.FromToRotation(Vector3.up, canon.eulerAngles));
        StartCoroutine(TravelBullet(bullet, (jugador.position + Vector3.up - bullet.transform.position).normalized * Time.deltaTime));
        lastShoot = Time.time;
    }

    IEnumerator TravelBullet(GameObject bullet, Vector3 direction)
    {
        float aliveTime = 5;
        while(aliveTime > 0)
        {
            aliveTime -= Time.deltaTime;
            bullet.transform.position += direction * bulletSpeed;
            yield return null;
        }
        Destroy(bullet);
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