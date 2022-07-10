using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    private static PathManager mInstance;
    public static PathManager Instance
    {
        get { return mInstance; }
    }
    [SerializeField] private int mNum=0;
    public GraphPathWay Martixs;
    private void Awake()
    {
        Init();
    }
    private void Init()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }

        mInstance = this;
        //DontDestroyOnLoad(this);
        Martixs = new GraphPathWay(mNum);
        for(int i = 0; i < mNum; i++)
        {   
            //设置顶点属性（如普通点，机场，补给点等）循环注意顺序
            Martixs.SetVertex(i, transform.GetChild(i).GetComponent<Vertex>());

        }
        //连接顶点
        Martixs.SetMartix(0, 1, 1);
        Martixs.SetMartix(1, 2, 1);
        Martixs.SetMartix(1, 3, 1);
        Martixs.SetMartix(2, 4, 1);
        Martixs.SetMartix(3, 4, 1);
    }
}
