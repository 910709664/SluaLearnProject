using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vertex : MonoBehaviour
{   
    [SerializeField]
    private int data;
    public Vertex(int vertex)
    {
        data = vertex;
    }
    public int Data
    {
        get { return data; }
        set { data = value; }
    }
}
