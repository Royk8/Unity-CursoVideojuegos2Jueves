using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PuntajeSingleton : MonoBehaviour
{
    #region Singleton
    public static PuntajeSingleton Instance;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            DestroyImmediate(gameObject);
            return;
        }
        Instance = this;
        //Solo en caso de necesitar que el objeto viva en varias escenas.
        DontDestroyOnLoad(gameObject);
    }
    #endregion

    public int puntaje;
    public TMP_Text textoPuntaje;

    public void CambiarPuntaje(int nuevosPuntos)
    {
        puntaje += nuevosPuntos;
        textoPuntaje.text = puntaje.ToString();
    }
}
