using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolaMundo : MonoBehaviour
{
    public int x = 1;
    public string s = "fue culpa de unity";
    public string n = "No ";
    public bool b = false;
    bool f = true;

    void Start()
    {
        float y = 3.51f;
        Debug.Log(n+s);
    }


    void Update()
    {
        Debug.Log(x);
    }
}
