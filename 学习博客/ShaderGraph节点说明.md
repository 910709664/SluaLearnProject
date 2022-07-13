---
title: ShaderGraph节点说明
date: 2021-05-31 16:29:07
tags:
---

# Artistic
# Channel

 

| Split | 分离RGBA通道,分别取RGBA四个通道的值 |
| :---: | ----------------------------------- |
|       |                                     |
|       |                                     |
|       |                                     |

# Input

 

| Screen Position | 屏幕的RGBA值 |
| :-------------: | ------------ |
|     Screen      | 屏幕的宽高   |
|                 |              |
|                 |              |

# Math

 

|  Distance  | 返回输入值的行列式                                           |
| :--------: | ------------------------------------------------------------ |
|   Divide   | 除,A为被除数,B为除数                                         |
|    Step    | 如In值大于等于Edge,返回1,否则返回0                           |
| SmoothStep | 如果In小于Edge1，则返回0，如果In大于Edge2，则返回1.**Hermite插值在开始加速并在最后减速** |
|    Lerp    | 输出区间在[A,B]里,T为比值.若A为0,B为5,T为0.5,输出2.5         |
|  Subtract  | 减,A-B                                                       |

# UV

 

| Tiling and Offset | 平铺与平移 |
| :---------------: | ---------- |
|                   |            |
|                   |            |
|                   |            |

