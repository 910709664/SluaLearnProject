using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SLua;
using System.IO;
using UnityEngine.UI;
public class MainLua : MonoBehaviour {

    public LuaSvr LuaSvr;
    public LuaTable self;
    public delegate void FuncDel();

    public FuncDel l_start;
    public FuncDel l_update;
    
	// Use this for initialization
	void Start () {
        LuaSvr = new LuaSvr();
        LuaSvr.init(null, OnLuaFunc);
        if (l_start != null) l_start();
	}
	
	// Update is called once per frame
	void Update () {
        if (l_update != null)
            l_update();
	}

    private void OnLuaFunc()
    {
        LuaState.main.loaderDelegate += LuaLoader;  //添加loader代理
        self = (LuaTable)LuaSvr.start("MyGameLua");

        LuaFunction luafun_start = (LuaFunction)self["Start"];
        LuaFunction luafun_update = (LuaFunction)self["Update"];

        l_start = luafun_start.cast<FuncDel>();
        l_update = luafun_update.cast<FuncDel>();
    }

    //加载指定文件路径的lua文件
    private byte[] LuaLoader(string fn,ref string absoluteFn)
    {
        string filepath = Application.dataPath + "/Scripts/LuaFile/" + fn+".txt";
        return File.ReadAllBytes(filepath);
    }
}
