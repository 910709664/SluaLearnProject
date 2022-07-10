using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hexagon
{
    public Hexagon father;
    private Hexagon[] Neighbors = new Hexagon[6];
    public Hexagon(Vector3Int vector)
    {
        this.vector = vector;
    }
    public Vector3Int vector = Vector3Int.zero;
    float g = 1f;
    public float G
    {
        get { return g; }
        set { g = value; }
    }
    float h;
    public float H
    {
        get { return h; }
        set { h = value; }
    }
    //TODO
    public Hexagon[] GetNeighborsList()
    {
        return Neighbors;
    }
    public void SetFather(Hexagon father)
    {
        this.father = father;
    }
    public Hexagon GetFather()
    {
        return father;
    }
    public float GetHValue(Hexagon target)
    {
        return Vector3Int.Distance(this.vector, target.vector);
    }
}