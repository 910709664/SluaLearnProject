using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class HexagonAstar//A*寻路
{
    public List<Hexagon> AstarSearch(Hexagon start, Hexagon target,Tilemap tilemap)
    {
        if (start == target) return null;
        List<Hexagon> mOpenList = new List<Hexagon>();
        List<Hexagon> mCloseList = new List<Hexagon>();
        Hexagon now = start;
        List<Vector3Int> vector3s = GameManager.Instance.GetRangeTiles(now.vector,1);
        mOpenList.Add(now);//先添加到开放列表
        bool isFinded = false;
        //六个邻居节点
        Hexagon[] neigh = now.GetNeighborsList();
        for (int i = 0; i < vector3s.Count; i++)
        {
            neigh[i] = new Hexagon(vector3s[i]);
        }
        while (!isFinded)
        {
            mOpenList.Remove(now);
            mCloseList.Add(now);
            foreach (var neighbor in neigh)
            {
                if (neighbor == null) continue;
                if (neighbor.vector == target.vector)//找到目标,设置其父亲
                {
                    isFinded = true;
                    target.SetFather(now);
                }
                if (mCloseList.Contains(neighbor) || !tilemap.HasTile(neighbor.vector))//跳过关闭列表或不能通过的点
                {
                    continue;
                }
                //在开放列表中,重新估算G,H
                if (mOpenList.Contains(neighbor))
                {
                    float assueGValue = neighbor.G + now.G;
                    Debug.Log(assueGValue);
                    if (assueGValue < neighbor.G)
                    {
                        mOpenList.Remove(neighbor);
                        neighbor.G = assueGValue;
                        mOpenList.Add(neighbor);//重新排序
                    }
                }
                //不在开放列表中,估算H,G,并设置其父节点
                else
                {
                    neighbor.H = neighbor.GetHValue(target);
                    neighbor.G = neighbor.G + now.G;
                    mOpenList.Add(neighbor);
                    neighbor.SetFather(now);
                }
            }
            if (mOpenList.Count <= 0)
            {
                Debug.Log("Can' Find");
                break;
            }
            else
            {
                now = mOpenList[0];//F值最低的点为父节点
                vector3s = GameManager.Instance.GetAroundTile(now.vector);
                for (int i = 0; i < vector3s.Count; i++)
                {
                    neigh[i] = new Hexagon(vector3s[i]);
                }
            }
        }

        mOpenList.Clear();
        mCloseList.Clear();
        List<Hexagon> route = new List<Hexagon>();
        //添加到路线列表
        if (isFinded)
        {
            Hexagon hex = target;
            while (hex != start)
            {
                route.Add(hex);
                Hexagon fatherHex = hex.GetFather();//寻找其父节点
                hex = fatherHex;
            }
            route.Add(hex);//将起点添加
        }
        route.Reverse();//反转得到正序路线
        return route;
    }

}
