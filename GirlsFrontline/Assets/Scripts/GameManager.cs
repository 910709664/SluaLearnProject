using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager:MonoBehaviour
{
    private static GameManager mInstance;
    public static GameManager Instance
    {
        get { 
            return mInstance;
        }
    }
    private void Awake()
    {
        mInstance = this;
    }
    
}
