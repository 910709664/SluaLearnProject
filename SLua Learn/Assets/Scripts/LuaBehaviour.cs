using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SLua;

public class LuaBehaviour : MonoBehaviour {

    public string luaFilePath;

    LuaTable self;
    public delegate void LuaFunc();

    LuaFunc l_update;
    LuaFunc l_start;

	// Use this for initialization
	void Start () {
        LuaState.main.doFile(luaFilePath);
        self = (LuaTable)LuaState.main.run("main");
        LuaFunction update = (LuaFunction)self["update"];
        LuaFunction start = (LuaFunction)self["start"];

        l_update = update.cast<LuaFunc>();
        l_start = start.cast<LuaFunc>();

        if (l_start != null) l_start();
	}
	
	// Update is called once per frame
	void Update () {
        if (l_update != null) l_update();
	}


}
