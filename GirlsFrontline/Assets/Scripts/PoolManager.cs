using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    private static PoolManager mInstance;
    public static PoolManager Instance
    {
        get { return mInstance; }
    }
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        mInstance = this;
        DontDestroyOnLoad(this);
    }
}
