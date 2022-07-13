---
title: 'c#常用特性'
date: 2020-02-26 17:40:48
tags:
	- c#
	- unity
---
# C#在unity开发中常用的特性
	众所周知，C#的特性在多人开发时的便捷性与实用性得到许多开发者的喜爱，所以我在这里整理一下自己学习unity开发中一些常用的特性
<!--more--> 

` [ExecuteInEditMode] `

> **在编辑模式下运行该“脚本程序”**

` [DisallowMultipleComponent] `

> **不允许该脚本在同一组件中添加多次**

` [System.Obsolete] `

> **表示该方法已过时** 在项目中多用于提醒队友更改相似的方法

` [SelectionBase] `

> **通过挂载在父物体上可以直接选中父物体**

`[HelpURL("链接")]`
> **组件上会出现一个问号（帮助),点击可跳转到该页面，通常为项目学习地址**

` [HideInInspector] `
> **隐藏公共变量**

`using UnityEditor;
 using UnityEditor.Callbacks;
 [OnOpenAsset(序号)] `

> **函数执行顺序**

`[DefaultExecutionOrder(序号)]`
> **脚本执行顺序**

`[Header("描述")]`
> **在变量头顶增加分类标题**

`[Tooltip("介绍")]`
> **给变量添加解释，当鼠标停留在变量上时**

`[TextArea]`
> **文本框**

`[SerializeField]`
> **私有变量可视化**

`[MenuItem("")]`
> **菜单栏**必须是静态方法

`[CustomEditor(typeof(脚本名称))]`
> **方法继承自Editor，重写OnInspectorGUI方法**

详细请看<https://www.bilibili.com/video/av91073680>