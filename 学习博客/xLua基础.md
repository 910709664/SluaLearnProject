---
title: xLua基础
date: 2021-04-13 22:34:27
tags:
---

# 
<!--more--> 

# XLUA基础(一)
#### 1.创建lua虚拟机环境

```c#
using XLua;
LuaEnv env=new env();//创建
env.Dispose();//销毁
```

#### 2.加载Lua文件

>  lua文件必须为.txt后缀

```c#
env.DoString("require 'filename'");//加载与脚本同一文件夹下的Resources里的lua文件
```

#### 3.自定义loader

```c#
using System.IO
private byte[] CustomMyLoader(ref string filename)
{
        string path = Application.dataPath + "/LuaScript/" + filename + ".lua.txt";//datapath为根目录
        string content = File.ReadAllText(path);
        byte[] result = System.Text.Encoding.UTF8.GetBytes(content);
        return result;
}
public void LuaEnv.AddLoader(CustomMysLoader)
```

# XLUA基础(二) C#调用lua
#### 1.获取lua的全局变量

> lua的变量名与C#声明变量名需要一致

```c#
int a=env.Global.Get<int>("a");
string str=env.Global.Get<string>("str");
bool isDie=env.Global.Get<bool>("isDie");
```

#### 2.访问全局table

- 通过class

> 将lua table中的键名与C#中的字段名保持一致，并且都是为public 

```c#
class Person{}
Person p=env.Global.Get<Person>("person");//将person表映射到Person类中并实例化
```

此处通过class映射方式为值拷贝

- 通过interface

>  注意这里的interface上加上了一个[CSharpCallLua]的标签 

```c#
[CSharpCallLua]
interface IPerson
{
    string name { get; set; }
    int b { get; set; }
    double c { get; set; }
    void f1(int c);
    void f2(int a,int b);
    void f3(string str);
}
IPerson p =env.Global.Get<IPerson>("person");
```

此处interface通过引用拷贝

```lua
lua文件
f2=function(arg,a,b)//需要把自身作为第一个参数传递
function person:f1(c)//当使用:解析符时不需要添加第一个参数
function person.f3(arg,str)//当使用.解析符时需要添加第一个参数
```

- 通过Dictionary<T1,T2>和List<T>

```c#
//通过Dictionary,其分别对用索引值/值
Dictionary<string,object> dic=env.Global.Get<Dictionary<string,object>>("person");
foreach(var key in dic.Keys){
    print(key+" "+dic[key]);
}
//通过List,其对应数组值
List<object> list=env.Global.Get<List<object>>("person");
foreach(var val in list){
    print(val);
}
```

```lua
lua文件
person={
    name="rean",
    b=120,
    12,2,2,2,2,"siki",3.3,
}
```

- 通过LuaTable

```c#
LuaTable luaTable=env.Global.Get<LuaTable>("person");
print(luaTable.Get<int>("b"));
print(luaTable.Get<string>("name"));
```

#### 3.访问全局函数

- 通过委托映射

> 若使用C#自带的委托需要配置, 配置文件就在XLua/Example/ExampleGenConfig.cs ,然后在Unity重新GenerateCode
>
> 若自定义委托,则需要加上[CSharpCallLua]的标签,并在Unity中ClearCode或者GenerateCode

```c#
using System;
//使用Action
Action<int,int> add=env.Global.Get<Action<int,int>>("add");
add(11,22);
//使用自定义委托
[CSharpCallLua]
public delegate Add(int a,int b,out int rs,out int rb);//返回多个参数时使用ref或者out,out不需要初始化
Add add=env.Global.Get<Add>("add2");
int rs,rb;
add(11,22,out rs,out rb);
print(rs+" "+rb);
add=null//当使用完委托时需要清除引用
```

```lua
lua文件
function add(a,b)
	print(a+b)
end
funtion add2(a,b)//当返回多个参数时
	print(a+b)
	return a,b
end
```

- 通过LuaFunction映射

```c#
LuaFunction add=env.Global.Get<LuaFunction>("add");
add.Call(1,3);
```

# XLua基础(三) Lua调用C#

#### new C#对象

```lua
local go=CS.UnityEngine.GameObject
local go1=go.GameObject()
local go2=go.GameObject("myLua")-- 重载
print(go1.name,go2.name)
```

使用时需要用到CS.命名空间.类名

#### 访问静态方法

```lua
print(CS.UnityEngine.Time.deltaTime)
CS.UnityEngine.Time.timeScale=0.5-- 属性
```

#### 访问成员属性和方法

```lua
local camera=gameObject.Find("Main Camera");
camera.name="update by lua"
--调用成员方法使用:,若用.需要传递自身作为第一个参数
local cameraCom=camera:GetComponent("Camera")
gameObject.Destroy(cameraCom)
```

#### 事件

```lua
public event Action TestEvent;//在c#声明一个事件

--lua 

local function lua_event_callback1() print('lua_event_callback1') end
local function lua_event_callback2() print('lua_event_callback2') end
testobj:TestEvent('+', lua_event_callback1)//使用+来添加事件
testobj:CallEvent()
testobj:TestEvent('+', lua_event_callback2)
testobj:CallEvent()
testobj:TestEvent('-', lua_event_callback1)//使用-来清除事件
testobj:CallEvent()
testobj:TestEvent('-', lua_event_callback2)
```

