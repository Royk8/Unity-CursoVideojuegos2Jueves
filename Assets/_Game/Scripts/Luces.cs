using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Luces : MonoBehaviour
{
    public bool EstaEncendido;
    public Material matEncendido, matApagado;
    public List<GameObject> listaBombillos;
    public Color encendido1, endendido2, apagado;
    public float tiempoEspera;

    void Start()
    {
        StartCoroutine(LucesCoroutine());
    }

    IEnumerator LucesCoroutine()
    {
        while (true)
        {
            int k = Random.Range(0, listaBombillos.Count);
            for(int i = 0; i < listaBombillos.Count; i++)
            {
                if (!EstaEncendido)
                {
                    listaBombillos[i].GetComponent<Renderer>().material.color = apagado;
                    continue;
                }
                if(i == k)
                {
                    listaBombillos[i].GetComponent<Renderer>().material.color = encendido1;
                }else
                {
                    listaBombillos[i].GetComponent<Renderer>().material.color = endendido2;
                }
            }                         
            yield return new WaitForSeconds(tiempoEspera);
        }
    }
}
