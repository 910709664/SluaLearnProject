---
title: Xlua热补丁
date: 2021-04-15 16:53:02
tags:
---

# Unity热更新
<!--more--> 
## HotFix代码部分
### 使用前
#####	添加宏
1. 添加HOTFIX_ENABLE宏,（在Unity3D的File->Build Setting->Scripting Define Symbols下添加.编辑器、各手机平台这个宏要分别设置！如果是自动化打包，要注意在代码里头用API设置的宏是不生效的，需要在编辑器设置.
2. 执行XLua/Generate Code菜单.
3. 注入，构建手机包这个步骤会在构建时自动进行，编辑器下开发补丁需要手动执行"XLua/Hotfix Inject In Editor"菜单。打印“hotfix inject finish!”或者“had injected!”才算成功，否则会打印错误信息
### 使用时
##### 标识需要热更新的类型
1. 直接在类里头打Hotfix标签,在方法打上LuaCallCSharp标签

2. 在一个static类的static字段或者属性里头配置一个列表。属性可以用于实现的比较复杂的配置，比如根据Namespace做白(黑)名单.注意，高版本Unity需要把配置文件放Editor目录下

   [配置表]: https://github.com/Tencent/xLua/blob/master/Assets/XLua/Examples/ExampleGenConfig.cs
   
   ```c#
   //如果涉及到Assembly-CSharp.dll之外的其它dll，如下代码需要放到Editor目录
   public static class HotfixCfg
   {
       [Hotfix]
       public static List<Type> by_field = new List<Type>()
       {
           typeof(HotFixSubClass),
           typeof(GenericClass<>),
       };
   
       [Hotfix]
       public static List<Type> by_property
       {
           get
           {
               return (from type in Assembly.Load("Assembly-CSharp").GetTypes()
                       where type.Namespace == "XXXX"
                       select type).ToList();
           }
       }
   }
   ```

##### 当遇到重载函数时
- 如Random,可以调用Lua自身的函数库或者调用Unity的方法来拓展

##### 冒号还是点号(: .)
- lua在访问C#对象中的非静态方法时，使用冒号
- lua在访问lua class 对象中的方法时，使用冒号
- 其余情况都用点号
### 相关API
##### 函数

```lua
xlua.hotfix(CS.typename,'funcname',function(self)
        ...
end)
```

##### 私有变量

```lua
xlua.private_accessible(CS.typename)//给予访问私有变量的所有权
self.name=;
```

##### 先执行C#代码在执行Lua代码

```lua
local util=require'util'//需要先将util文件放入到与lua文件所在的文件夹下
util.hotfix_ex(CS.typename,'name',function(self)
	self.name(self)
	...
end)
```

## 打AB包
- 选中资源，在Inspector下的小窗口通过编辑器的UI界面即可方便的将资源标记为AssetBundle资源，并且一个资源和它对应的AssetBundle的映射将会在资源数据库（AssetDatabase）中被创建。

![](C:\Users\诺瓦鱼\Pictures\blog\QQ图片20210310160451.png)

```c#
//Unity相关代码
using UnityEditor;
using System.IO;
[MenuItem("Asset\Build AssetBundles")]
public class CreateAB{
	static void BuildAllAssetBundles(){
		string dir="AssetBundles";
		if(Directory.Exists(dir)==false)
			Directory.CreateDicrectory(dir);
		BuildPipeline.BuildAssetBundles(dir,BuildAssetBundleOptions.None,BuildTarget.Standalone.Windos64);//创建AB包
	}
}
```

##### 加载AB包

```c#
//lua框架加载
Dictionary<string,GameObject> dic=new Dictionary<string,GameObject>();
public static void LoadAB(string filename,string filepath){
    AssetBundle ab=AssetBundle.LoadFromFile("filepath");
	GameObject go=ab.LoadAsset<GameObject>("filename");
    dic.Add(filename,go);
}
public static GameObject GetGO(string GoName){
    return dic[GoName];
}
```

## 服务器资源加载
##### ab包
```c#
using UnityEngine.NetWorking;
//使用协程
[LuaCallCSharp]
public void LoadResource(string filename,string filepath){
	StartCoroutine(LoadResourceCoroutine(filename,filepath));
}
IEnumerator LoadResourceCoroutine(string filename,string filepath){
	UnityWebRequest request=UnityWebRequest.GetAssetBundle(@"http://"+网址+"filepath");
	yield return request.SendWebRequest();
	AssetBundle ab=(request.downloadHandler as DownloadHandlerAssetBundle).assetBundle;
	GameObject go=ab.LoadAsset<GameObject>("filename");
    dic.Add(filename,go);
}
```

##### lua代码

```c#
using UnityEngine.NetWorking;
//使用协程
IEnumerator LoadResourceCoroutine(){
    UnityWebRequest request=UnityWebRequest.Get(@"http://"+网址+filename+".lua.txt");
    yield return request.SendWebRequest();
    string str=request.downloadHandler.text;
    File.WriteAllText(@"本地路径",str);
}
```

------

# AB包学习
### 分组策略
##### 依赖打包
- 将材质贴图与预制体分开打包,加载时需要**一起加载**,预制体则会自动依赖材质贴图包
- 经常更新的文件放在一个包里

### ab包依赖
- 如果a包依赖于b包,那么需要加载完两个包,加载不用注意顺序,在使用才能使用a包中的材质
### mainfet
##### 1. CRC,MD5和SHA1校验

##### 2. 加载mainfest文件

```c#
AssetBundle ab=AssetBundle.LoadFromFIle(mainfestFilePaht);
AssetBundleMainfest mainfest=assetBundle.LoadAsset<AssetBundleMainfest>("AssetBundleMainfest");
string[] dependencie = mainfest.GetAllDependencies("assetBundle");//assetBundle指代某个ab包
foreach(string dependency in dependencies){
	AssettBundle.LoadFromFile(Path.Combine(assetBundlePath,dependency));//Path.Combine处理分隔符
}
foreach(string name in mainfest.GetAllAssetBundle()){
    print(name);
}
```

### 卸载AB包

```c#
//当 unloadAllLoadedObjects为false 时，将释放捆绑包本身中的压缩文件数据，但已从该捆绑包中加载的任何对象实例将保持不变
//当 unloadAllLoadedObjects为true 时，也将销毁从该捆绑包加载的所有对象。如果场景中有 GameObjects 引用这些资源，则对它们的引用将丢失
public void Unload (bool unloadAllLoadedObjects);//在场景切换时使用
Resouces.UnloadUnusedAssets();
```

