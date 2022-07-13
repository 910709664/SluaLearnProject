---
title: Shader光照模型
date: 2020-05-23 16:18:01
tags:
---

# Unity Shader中的光照模型

<!--more--> 

## BRDF光照模型
## 标准光照模型
### 自发光
$$
Cemissive=Memissive
$$
### 环境光
$$
Cambient=Gambient
$$
### 漫反射
兰伯特定律
$$
Cdiffuse=(Clight·Mdiffuse)max(0,n,l)
$$
半兰伯特光照模型
$$
Cdiffuse=(Clight·Mdiffuse)(0.5(n·l)+0.5)
$$
### 高光反射
Phone模型
$$
Cspecular=(Clight·Mspecular)max(0,v,r)^m,	v为视角方向,r为反射方向
$$
$$
r=reflect(i,n),  i为入射方向，n为法线方向
$$
Blinn模型
$$
h=v+l/(|v+l|),	l为光源方向
$$
$$
Cspecular=(Clight·Mspecular)max(0,n,h)^m ,n为法线方向
$$

### 凹凸映射
#### 法线纹理
$$
pixel=(normal+1)/2
$$
### Unity常用函数
```c#
UnityObjectToClipPos(vertex)//将模型空间的坐标转换到裁剪空间
UnityObjectToWorldNormal(v.normal)//从模型空间的坐标转换世界空间的法线方向
UnityWorldSpaceLightDir(float4 v)//从模型空间的坐标获得该点到光源的光照方向，仅向前渲染，无归一化
UnityWorldSpaceViewDir(float4 v)//从模型空间的坐标获得该点到摄像机的观察方向
```

