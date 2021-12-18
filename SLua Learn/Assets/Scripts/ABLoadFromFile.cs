using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SLua;

//使用该标签后便可以在Lua中调用C#的类
[CustomLuaClass]
public class ABLoadFromFile : MonoBehaviour {

	public static GameObject LoadAB(string filepath,string filename)
    {
        AssetBundle ab = AssetBundle.LoadFromFile(filepath);
        GameObject go = ab.LoadAsset<GameObject>(filename);
        return go;
    }

    public static GameObject[] LoadABAll(string filepath)
    {
        AssetBundle ab = AssetBundle.LoadFromFile(filepath);
        GameObject[] goArr = ab.LoadAllAssets<GameObject>();
        return goArr;
    }
    
}
