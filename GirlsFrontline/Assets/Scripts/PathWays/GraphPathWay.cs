using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphPathWay
{
    private Vertex[] vertexs;
    private int[,] martixs;
    private int numEdge;
    //构造函数，默认两点之间一条连线
    public GraphPathWay(int num)
    {
        vertexs = new Vertex[num];
        martixs = new int[num, num];
        numEdge = num;
    }
    public int NumEdge
    {
        get { return numEdge; }
        set { numEdge = value; }
    }
    public Vertex GetVertex(int index)
    {
        return vertexs[index];
    }
    /// <summary>
    /// 设置顶点属性
    /// </summary>
    /// <param name="index"></param>
    /// <param name="vertex"></param>
    public void SetVertex(int index,Vertex vertex)
    {
        vertexs[index] = vertex;
    }
    public int GetMartix(int index1,int index2)
    {
        return martixs[index1, index2];
    }
    /// <summary>
    /// 双向图
    /// </summary>
    /// <param name="index1"></param>
    /// <param name="index2"></param>
    /// <param name="n">权重</param>
    public void SetMartix(int index1,int index2,int n)
    {
        martixs[index1, index2] = n;
        martixs[index2, index1] = n;
    }
    /// <summary>
    /// 单向图
    /// </summary>
    /// <param name="index1"></param>
    /// <param name="index2"></param>
    /// <param name="n">权重</param>
    public void SetOneMartix(int index1,int index2,int n)
    {
        martixs[index1, index2] = n;
    }
    public int GetIndex(Vertex vertex)
    {
        int i = -1;
        for(i = 0; i < vertexs.Length; i++)
        {
            if (vertexs[i].Equals(vertex))
            {
                return i;
            }
        }
        return i;
    }
    /// <summary>
    /// 判断该顶点是否存在
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    public bool IsNode(Vertex v)
    {
        foreach(Vertex vertex in vertexs)
        {
            if (v.Equals(vertex))
            {
                return true;
            }
        }
        return false;
    }
    /// <summary>
    /// 是否有边
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    public bool IsEdge(Vertex v1,Vertex v2)
    {
        if (!IsNode(v1) || !IsNode(v2))
        {
            Debug.Log("Vertex is not belong to Grpah");
            return false;
        }
        if (martixs[GetIndex(v1), GetIndex(v2)] >= 1)
        {
            Debug.Log("Have Edge");
            return true;
        }
        else
        {
            Debug.Log("Not Have Edge");
            return false;
        }
    }
    /// <summary>
    /// 连接新的一条边
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <param name="n"></param>
    public void SetEdge(Vertex v1,Vertex v2,int n)
    {
        if (!IsNode(v1) || !IsNode(v2))
        {
            Debug.Log("Vertex is not belong to Grpah");
            return;
        }
        if (n <= 0)
        {
            return;
        }
        martixs[GetIndex(v1), GetIndex(v2)] = n;
        martixs[GetIndex(v2), GetIndex(v1)] = n;
        numEdge++;
    }
    /// <summary>
    /// 删除一条边
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <param name="n"></param>
    public void DeleteEdge(Vertex v1,Vertex v2,int n)
    {
        if (!IsNode(v1) || !IsNode(v2))
        {
            Debug.Log("Vertex is not belong to Grpah");
            return;
        }
        if (martixs[GetIndex(v1), GetIndex(v2)] > 0)
        {
            martixs[GetIndex(v1), GetIndex(v2)] = 0;
            martixs[GetIndex(v2), GetIndex(v1)] = 0;
            numEdge--;
        }
    }
}
