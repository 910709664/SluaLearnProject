using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SLua;
using System;
using UnityEngine.UI;

public class LuaFileManager : MonoBehaviour {

    public LuaSvr LuaSvr;
    public LuaTable self;
    public delegate void FuncDel();

    public FuncDel l_start;
    public FuncDel l_update;
    public FuncDel l_onEnable;
    public FuncDel l_onDisable;
	// Use this for initialization
	void Start () {
        LuaSvr = new LuaSvr();//初始化虚拟机
        LuaSvr.init(null, OnLuaFunc);//完成Unity import操作

        if (l_start != null) l_start();
	}
	
	// Update is called once per frame
	void Update () {
        if (l_update != null)
            l_update();
	}
    private void OnLuaFunc()
    {   

        self = (LuaTable)LuaSvr.start("LuaFile/MyLuaBag");//默认根目录为Resources下
        //获取Lua文件下的函数
        LuaFunction luafunc_start = (LuaFunction)self["Start"];
        LuaFunction luafunc_update = (LuaFunction)self["Update"];
        LuaFunction luafunc_onEnable = (LuaFunction)self["OnEnable"];
        LuaFunction luafunc_onDisable = (LuaFunction)self["OnDisable"];

        l_start = luafunc_start.cast<FuncDel>();
        l_update = luafunc_update.cast<FuncDel>();
        l_onEnable = luafunc_onEnable.cast<FuncDel>();
        l_onDisable = luafunc_onDisable.cast<FuncDel>();
    }

    public GameObject go;
    public void testFunc()
    {
        
    }
    
    //还可自定义loader方法
    //private static byte[] LuaResourcesFileLoader(string luaFile,string fn)
    //{
    //    string filename = Application.dataPath + "/Scripts/Lua/" + strFile.Replace('.', '/') + ".txt";
    //    return File.ReadAllBytes(filename);
    //}
}
