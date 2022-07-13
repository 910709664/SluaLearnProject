---
title: Lua基础
date: 2021-07-29 19:57:22
tags:
---

# 一 Lua入门

#### 类型和值

Lua语言有8种基本类型:nil,boolean,number,string,userdata(用户数据,文件),function,thread,table(表)

# 二 数值

- floor除法:3//2=1(向下取整)

- 不等于:~=

# 三 字符串

- 连接操作符: ..(两个点),如"Hello".."World"

- 使用双方括号表达多行字符串:page=[[...]]
- tonumber(),tostring()

#### 字符串标准库

- string.len()
- string.reverse()
- string.lower() & string.upper()
- string,sub(s,i,j)//提取(i-j)的字符串
- string.gsub("","","")//模式匹配替换
- string.find()

# 四 表

##### 构造表:a={};

1. 记录式:a={"Monday"}     a={["x"]=0,["y"]=0}
2. 列表式:a={{x=0,y=0}}//a[1]

##### 表示:a.name & a["name"]等价,前者约等于结构体,后者约等于表

#### 表标准库

- table.insert()
- table.remove()
- table.move()

# 五 函数

##### 框架:

```lua
function add(a)
	local sum=0
	return sum
end
```

#### 多返回值

```lua
s,e=string.find("hello Lua user","Lua");//s->7,e->9
```

> **当函数被作为一条单独语句调用时，其所有返回值都会被丢弃；当函数被作为表达式（例如，加法的操作数）调用时，将只保留
> 函数的第一个返回值。只有当函数调用是一系列表达式中的最后一个表达式（或是唯一一个函数表达式） 时，其所有的返回值才能被获取到。**

```lua
x,y,z=10,foo2()//x=10,y="a",z="b"
```

#### 可变长参数函数

使用(...)三个点来表达可变长参数,使用{...}来接受参数

#### table.unpack和table.pack

- table.unpack把列表转换成一组返回值(泛型)
- table.pack把参数列表转换为一个表

#### 尾调用

在一个函数f(x)里实现 return g(x) end;

#### 语法糖

```lua
foo = function(x) return 2*x end
```



# 六 补充知识

- **if then else**
- **while do end**
- **for var=初始值,长度,步长 do something end**
- 泛型far pairs
- pair会遍历table所有键值对(随机返回),ipairs固定从key值1开始,如果key对应value不存在就停止遍历

# 七 闭包

#### 闭包
嵌套函数里的内部函数可以访问外部函数的变量值,使得定义的局部变量"逃逸"出他的作用范围

#### 函数是第一类值

函数可以像变量一样使用.变量名既是函数的名字

#### 局部函数

定义方法:

```
local fact
fact=function(n)
  if n==0 then return 1
  else return n*fact(n-1)
  end
end
```

# 八 模式匹配

### 相关函数

##### string.find(s,"pattern",(option)startIndex,(option)true)

返回寻找字符串的开始位置和结束位置的索引,第三个参数为搜索开始位置,第四个参数表示是否进行简单搜索

##### string.match(s,pattern)

返回与模式匹配的子串.

比如模式是变量时:string.match(data,"%d+/%d+/%d+")=>17/7/1990

##### string.gsub(s,pattern,replaceS,(option)count)

将目标字符串中的出现模板的地方替换成替换字符串并返回.可选的第四个参数为替换次数

##### string.gmatch(s,pattern)

返回一个函数,通过返回的函数可以遍历一个字符串中所有出现的指定模式

### 模式

|  .   |         任意字符         |
| :--: | :----------------------: |
|  %a  |           字母           |
|  %c  |         控制字符         |
|  %d  |           数字           |
|  %g  |    除空格外可打印字符    |
|  %s  |         空白字符         |
|  %w  |        字母和数字        |
|  %b  | 匹配成对的字符串(如%b()) |

#### 字符集

一种"集合"模式:[%w_].加一个补字符^代表该字符的子集:[ ^0-7 ]

##### 修饰符

|  +   |      重复一次或多次      |
| :--: | :----------------------: |
|  *   |      重复零次或多次      |
|  -   | 重复零次或多次(最小匹配) |
|  ?   |   可选(出现零次或一次)   |
|  ^   | 从目标字符串开头开始匹配 |
|  $   |     匹配到字符换结尾     |



 ### 捕获

> 把需要捕获的部分放到一对**圆括号**内来指定捕获

例:key,value=string.match(pair,"(%a+)%s*=%s*(%a+)") print(key,value)

空白捕获:() 捕获模式再目标字符串中的位置

### 替换

##### string.gsub(s,pattern,function or table)

每次匹配到模式字符串时调用函数或者将表中对应该键的值作为替换字符串

# 九 日期和时间

>  Lua针对日期和时间有两种表达方式:①数字②表

##### os.time()

以数字方式返回当前日期和时间:os.time()=>1439653520=>Aug 15.2015.12:45:20

如果参数为表,会返回表中描述日期的数字

##### os.data("*t",os.time())

第一个参数为期望格式化字符串,第二个参数为数字.将会返回一个日期表

对于其他格式化字符串,会返回一个字符串

如果不带参数,则默认使用%c

##### os.difftime(table,table)

返回以秒为单位表示时间的差值

##### os.clock()

计算一段代码执行时间

##### 常见指示符(P132)

|  %a  |  星期几的简写  |
| :--: | :------------: |
|  %b  |   月份的简写   |
|  %c  |   日期和时间   |
|  %H  | 小时数(24小时) |
|  %x  |      日期      |
|  %X  |      时间      |



# 十 位和字节

### 位运算

运算符:&,|,~(按位异或),>>,<<,~(按位取反)

### 无符号型整数

> lua不支持显示的无符号整数

- 通常使用选项%u或者%x来进行处理

- lua还提供math.ult()来作为对无符号型整数的比较

### 打包和解包二进制数据

##### string.pack("option",...)

- 把值打包成"**二进制**"字符串.第一个参数为格式化字符串,用来描述如何打包数据

##### string.unpack("option",s,index)

- 对打包的结果进行解码,返回其中的数据.为了便于迭代,还会返回最后一个元素的位置.
- 第三个参数用于指定开始的位置

##### 打包选项

1. 以\0结尾字符串使用选项"z",定长使用选项"cn"和"sn"
2. 对于整型有:b(char),h(short),i(int),l(long),j
3. 对于浮点型有f,d,n

### 二进制文件

- 使用字母b

# 十一 数据结构

### 数组

#### 一维数组

- 使用整数来索引表即可实现数组,且数组大小是可变长的,一般以1作为数组的初始索引

#### 矩阵及多维数组

- 第一种方式是嵌套表
- 第二种方式是计算索引,将二维坐标转换为一维

> lua产生的矩阵一般都为稀疏矩阵

### 链表

- 每个节点使用一个表表示,链接则为一个包含指向其他表的引用简单表字段

### 队列及双端队列

- 使用两个索引分别指向第一个元素和最后一个元素(first=0,last=-1)

### 反向表/索引表

```lua
revDays={}
for k,v in pairs(days) do
    revDays[v]=k
end
```

### 集合与包

包被称为多重集合,其中的元素可以出现多次,每一个键都有对应一个计数器,当计数器不为0时我们才保留这个元素

### 字符串缓冲区

> 由于字符串是不可变值,每次链接都会产生一个副本,所以需要使用缓冲区来加快读取速率

- 一般而言,我们使用io.read("a")来读取整个文件

- 除此之外,也可以使用table.concat(table,(option)insertS)来完成.该函数可以把字符串连接起来会返回链接后的字符串

  ```lua
  local t={}
  for lint in io.lines() do
      t[#t+1]=line;
  end
  local s = table.concat(t,"\n");
  ```

### 图

- 使用两个字段组成的表表示每个节点,即name和adj(邻接点的集合)
- node={name = name,adj={}};

# 十二 数据文件和序列化

### 数据文件

>  写入数据时把数据文件写成Lua代码,当这些代码运行时,程序也就把数据重建了

例如类似:

Donale E.Ksruth,Programing,CSLI,1992

的数据文件变成Lua代码:

Entry{"Donale E.Ksruth",

​		   "Programing",

​			"CSLI",

​			1992}

> 注意,Entry{code}和Entry({code})是相同的

函数Entry会作为一个回调函数再函数dofile处理数据文件每个条目中时被调用

### 序列化

> 将序列后的数据表示为Lua代码,当这些代码被运行时,被序列化的数据便在读取程序中得到重建

我们可以使用安全的方法来括住一个字符串,那就是使用%q选项

```lua
function serialize(o)
	local t=type(o);
	if t=="number" or t=="string" or t=="boolean" or t=="nil" then
	io.write(string.format("%q",o))
	else other cases
	end
end
```

#### 保存不带循环的表

```lua
funtion serialize(o)
	local t= type(o)
	if t=="number" or t=="string" or t=="boolean" or t=="nil" then
		io.write(string.format("%q",o))
	elseif t=="table" then
    	io.write("{\n")
    	for k,v in pairs(o) do
            io.write(" ",k," = ")
            serialize(v)//递归调用
            io.write(",\n")
    	end
    	io.write("}\n")
    else
    	error("cannot serialize a".. type(o))
    end
end	
```

- 如果键不是合法值,我们可以采用io.write(string.format("[%s] = ",serialize(k)))来实现序列化

#### 保存带循环的表

...待施工

# 十三 编译,执行和错误

...待施工

# 十四 模块和包

- 一个模块就相当于一个代码库(STL).这些代码通过函数require加载,创建并返回一个表

### 函数require

用法:local m = require "math"

- 首先require函数在package.loaded中检查模块是否被加载并返回相应的值
- 如果模块尚未加载,则搜索具有指定"模块名"的Lua文件(搜索路径由package.path指定),找到相应文件后由loadfile函数进行加载
- 如果找不到指定的Lua文件就搜索相应的C标准库(搜索路径由package.cpath指定),找到后使用函数package.loadlib进行加载,这个函数会查找名为"load_modname"的函数

##### 1. 模块重命名

​	require函数运用**连字符**技巧:如果一个模块名中包含连字符,那么函数require会用连字符之前的内容来创建**luaopen_***函数的名称

##### 2. 搜索路径

 	require搜索使用的路径是一组模块,其中的每项都指定了将模块名转换为文件名的方式.这种路径都包含**可选问号**的文件名,并用**分号**隔开

​	如: **?;?.lua;/usr/local/lua/?/?.lua**

​	则调用require "sql" 时,会尝试这样打开: sql;sql.lua;/usr/local/lua/sql/sql.lua

- 函数package.searchpath返回第一个存在的文件文件名,否则返回nil并打印错误信息

##### 3.搜索器

- 一个搜索器是以模块名为参数,以对应模块的加载器或nil为返回值的函数

### 编写模块的基本方法

1. 创建一个表将所有需要导出函数放入其中,最后返回这个表

   ```lua
   local M={}
   local function new (r,i) return {r=r,i=i} end
   M.new=new
   function M.add(c1,c2) return new(c1.r+c2.r,c1.i+c2.i) end
   ...
   return M
   ```

2. 把所有函数定义为局部变量,最后构造返回一个表

   ```lua
   local funtion new (r,i) return {r=r,i=i} end
   local i =complex.new(0,1)
   ...
   return{new=new,i=i,add=add,sub=sub...}
   ```

### 子模块和包

> Lua支持层次结构的模块名,通过"点"来分隔层次.一个"包"是一颗由模块组成的完整的树

# 十五 迭代器和泛型for

### 迭代器和闭包

- 一般我们通过闭包来实现迭代器,一个闭包结构通常涉及两个函数:闭包本身和一个用于创建该闭包及其封装变量的**工厂**

  ```lua
  function values(t)
  	local i=0
  	return function() i=i+1 return t[i] end
  end
  ```

  value就是工厂,每次调用这个迭代器时,他就从列表t中返回下一个值直到值为nil为止

  ```lua
  t={10,20,30}
  for element in values(t) do
      print(element)
  end
  ```

### 泛型for的语法

- 实际上,泛型for保存了三个值:**迭代函数,不可变状态和一个控制变量**

- 泛型for语法如下:

  ```lua
  for var-list in exp-list do
  	body
  end
  ```

- 我们把变量列表的第一个称为**控制变量**,当其值为nil则循环结束

- 后面的表达式需要返回三个值:迭代函数,不可变状态和控制变量初始值

- 初始化后,for使用不可变状态和控制变量为参数调用迭代函数

### 无状态迭代器

> 无状态迭代器就是一种不保存任何状态的迭代器.从而避免创建新闭包的开销

- 一个典型的例子就是**ipair**

### 按顺序遍历表

### 迭代器的真实含义

> 上诉讲述的本质其实是一个**生成器**,真正的迭代器接受一个函数作为参数,这个函数在循环的内部调用

```lua
function allwords(f)
	for line in io.lines() do
        for word in string.gmatch(line,"%w+") do
            f(word)
        end
    end
end
```

通常,我们可以使用匿名函数作为循环体

# 十六 元表和元方法

> 元表可以修改一个值在面对一个位置操作时的行为.类似于一个基类.元方法相当于重载函数

- 元表不支持继承
- 每一个表和用户数据具有各自独立的元表,其他类型则共享一个元表
- 相关函数:*setmetatable(table,metatable)       getmetatable(table)*

### 算术运算符相关元方法

| __add | __sub  | __div | __idiv | __mod  |
| ----- | ------ | ----- | ------ | ------ |
| __pow | __band | __bor | __bxor | __bnot |

### 关系运算符相关元方法

| __eq | __lt | __le     |
| ---- | ---- | -------- |
| 等于 | 小于 | 小于等于 |

### 库定义相关的元方法

- 函数tostring总会调用__tostring元方法
- 函数pairs总会调用__pair元方法

### 表相关的元方法

#### 1. __index元方法

- 当我们查询时便会调用这个元方法

- 定义如下:

  ```lua
  mt.__index = function(_,key)
      return protetype[k]
  end
  或者
  mt.__index=prototype
  ```

#### 2. __nexindex元方法

- 当我们对一个不存在的索引赋值时变回调用这个元方法

#### 3. 具有默认值的表

- 利用__index方法
- 其他方法有对偶表示和记忆元表(弱引用表)

#### 4. 跟踪对表的方法

- 创建一个**代理**空表并且重载index和newindex方法来进行跟踪,最后返回代理表

#### 5. 只读表

- 只实现__index方法,并且在newindex方法中抛出错误

# 十七 面向对象编程

##### 点号与冒号

*当我们使用点号调用对象的方法时需要传递一个指定对象.而使用冒号则可以隐藏该参数,默认传递自身作为参数*.

### 类

- 在Lua中没有类的概念,而是利用**原型模式**来实现类

- 当对象遇到一个未知操作时会在原型中查找.如

  ```lua
  setmetatable(A,{__index=B})
  ```

  之后A就会在B中查找它没有的操作

- 基类的产生

  ```lua
  Account = {balance = 0}
  function Account:new(o)
      o=o or {}
      self.__index=self//将自身传递到元方法
      setmetatable(o,self)
      return o
  end
  ...其他方法
  ```

### 继承

- 若要派生一个 子类,首先创建一个从基类继承了所有操作的**空表**,之后再利用这个空表创建子类的实例.如:

  ```lua
  SpecialAccount = Account:new()
  s = SpecialAccount:new{limit = 1000.00}
  ```

  之后,当执行s:deposit(100)时,Lua在s中找不到deposit字段,就会查找SpecialAccount,仍找不到字段,就查找Account并最终找到deposit字段

- 我们也无需指定一种新行为还创建一个新类,而是直接在该对象上实现这个行为.如:

  ```lua
  function s:getLimit()
      return self.balance*0.10
  end
  ```

### 多重继承

...待施工

### 私有性

- Lua中没有提供私有访问符.如果不想访问一个对象的内容就不要去访问.或者在变量名称之后加一个下划线来区分

- 也能通过两个表来实现对数据的保护.一个表保存对象状态,另一个表保存对象的操作.**其中表示状态的表只保存在方法的闭包中**

  ```lua
  function newAccount(initialBalance)
      local self ={balance = initialBalance}//创建闭包
      local withdraw = function(v) self.balance = self.balance - v end
      ...其他方法
      return { withdraw = withdraw ...}//返回可访问的公有方法,私有方法不返回
  end
  ```

  这里关键在于这些方法不需要额外的self参数,而是直接访问self变量.我们也不需使用冒号来操作这些对象

  ```lua
  acc1 = newAccount(100.00)
  acc1.withdraw(40.00)
  print(acc1.getBalance()) -->60
  ```

### 单方法对象

- 当对象只有一个方法时,我们可以封装成一个单方法对象.**一个在内部保存了状态的迭代器就是一个单方法对象**

- 其中方法可以根据不同参数完成不同任务的分发方法.实现原型如下:

  ```lua
  function newObject(value)
      return function(action,v)
          if action == "get" then return value
          elseif action == "set" then value = v
          else error("invalid action")
          end
     	end
  end
  ```

  使用方法:

  ```lua
  d = newObject(0)
  print(d("get"))    -->0
  d("set",10)
  print(d("get"))   -->10
  ```

### 对偶表示(实现私有性)

- 把表当作键,同时又把对象本身当作这个表的键:

  ```lua
  key = {}
  ...
  key[table] = value
  ----------------------
  //我们把余额放在表balance中,用对象当作索引
  local balance = {}
  Account = {}
  function Account.withdraw(self,v)
      balance[self] = balance[self] - v
  end
  function Account:new(o)
      o = o or {}
      setmetatable(o,self)
      self.__index=self
      balance[o]=0 --初始余额
      return o
  end
  ```

- **缺陷:不能被垃圾回收**

# 十八 环境

> Lua中没有全局变量的语义,而是通过模拟全局环境来生成类似"全局"的变量

常见做法是把变量保存在一个表中.如_G

### 具有动态名称的全局变量

一般而言,如果我们要使用诸如"io.read"或"a.b.c.d"这样的动态(嵌套)名称时,可以通过循环和表_G得到

```lua
//得到一个动态名称的值
function getfield(f)
    local v = _G;	//设置全局变量表
    for w in string.gmatch(f,"[%a_][%w_]*") do
        v = v[w]	//嵌套获得值.如_G=io=>io=io[read](io.read)=>return io.read
    end
    return v
end
```

```lua
//设置一个动态名称的值
function setfield(f,v)
    local t = _G
    for w,d in string.gmatch(f,"([%a_][%w_]*(%.?))") do
        if d == '.' then
            t[w] = t[w] or {}	
            t = t[w]	//嵌套
        else
            t[w]=v	//嵌套到最后一个赋值
        end
    end
end
```

### 全局变量的声明

- 通过**元表**来发现访问不存在全局变量的情况

  - 第一种方法是直接改写元表的元方法,并设置_G的元表.使用函数rawset来设置全局变量

  - 另一种方法是把对全局变量的赋值限制仅在函数内进行,而代码段外层的代码则被允许赋值.使用函数debug.getinfo(2,"S")来实现

    ```lua
    __newindex = function(t,n,v)
        local w = debug.getinfo(2,"S").what	//what字段表示现在是主代码段还是普通的Lua函数还是C函数
        if w ~= "main" and w ~= "C" then
            error
         end
    	rawset(t,n,v)
    end
    //如果要检验变量是否存在,通过rawget函数来检验
    if rawget(_G,var) == nil then
        ...
    end
    ```

  - 我们亦可以使用strict.lua模块来检验全局变量

### 非全局环境

- Lua把所有自由名称转换为_ENV.自由名称.Lua在编译时会生成一个ENV字段
- 函数load使用**全局环境**初始化代码段的第一个上值,即Lua内部维护的一个普通的表

### 使用_ENV

- 赋值_ENV=nil会使得后续代码无法访问全局变量

- 我们也可以显示使用_ENV绕过局部声明

  ```lua
  a=13
  local a=12
  print(a)	-->12
  print(_ENV.a) -->13
  print(_G.a)	-->13
  ```

- _ENV的主要用途是改变代码段使用的环境.一旦环境改变,所有全局访问就都将使用新表

- 如果新环境为空,就会丢失所有的全局变量.因此首先把一些有用的值放入新环境.比如**全局环境**

  ```lua
  a=15
  _ENV = {_G = _G}	//装入全局环境
  a=1
  _G.print(_ENV.a,_G.a)	-->1	15
  ```

- 我们也可以使用继承来把旧环境装入新环境:

  ```lua
  a = 1
  local newgt = {}	//创建新环境
  setmetatable(newgt,{__index = _G})	//继承全局变量
  _ENV=newgt	//设置新环境
  print(a)	-->1
  ```

### 环境和模块

...待施工

### _ENV和load

- 函数load有一个可选的第四个参数让我们为_ENV之当一个不同的初始值

...待施工

# 十九 垃圾收集

### 弱引用表

> 用来告知Lua一个引用不应阻止对一个对象回收的机制

- 弱引用表有三种:①键为弱引用;②值为弱引用;③键和值都为弱引用

- 一个表是否为弱引用表由其中的元表中的__mode字段决定.如:

  ```lua
  a={}
  mt={__mode = "k"}//键为弱引用
  mt={__mode = "v"}//值为弱引用
  mt={__mode = "kv"}//键和值都为弱引用
  ```

- 只有"**对象**"可以从弱引用表被移除.像数字和布尔"值"是不可回收的.(如果在一个值为弱引用表中,一个数值类型相关联的键值被回收时会被一并回收删除)

### 记忆函数

- 用一个辅助表来**记忆**所有函数的执行结果

  ```lua
  local results = {}
  function mem_loadstring(s)
      local res= result[s]	//直接寻找s对象所对应的结果
      if res==nil then	//如果为空说明还未记忆
          res = assert(load(s))
          results[s] = res	//记忆
      end
      return res
  end
  ```

- 记忆还可以用于确保基类对象的唯一性

### 对象属性

- 弱引用包另一种重要应用就是将**属性**与**对象**关联起来
- *对偶表示*:将对象用作键,对象的属性用作值

### 回顾具有默认值的表

- 对偶表示

  ```lua
  //存储每个对象,查找每个对象的元表
  local defaults = {}
  setmetatable(defaults,{__mode = "k"})//将键设为弱引用
  local mt = {__index = function(t) return default[t] end}//设置元方法,传入一个对象,并返回这个对象的值
  function setDefault(t,d)
      default[t] = d	//调用元方法获得对象的值并赋默认值
      setmetatable(t,mt)	//设置拥有默认值的元表
  end
  ```

- 记忆技术

  ```lua
  //存储每个元表,查找元表然后赋给对象
  local metas = {}
  setmetatable(metas,{__mode = "v"})	//将值设为弱引用
  function setDefault(t,d)
      local mt = meta[d]	//调用__index元方法,若不存在值返回nil
      if mt == nil then
          mt = {__index = function() return d end}//为元表的元方法赋初始值
          metas[d] = mt//记录这个元表
      end
      setmetatable(t,mt)	//为该对象设置一个拥有默认值的额元表
  end
  ```

### 瞬表

> 一个具有弱引用键和强引用值的表是一个瞬表

...待施工

### 析构器

- **析构器**是一个与对象关联的函数,当该对象被回收时即调用

  ```lua
  o = {x = "hi"}
  setmetatable(o,{__gc = function(o) print(o.x) end}) //__gc为析构器元方法
  o = nil //等待被回收
  collectgarbage() --> hi
  ```

- 如果在后续需要修改元方法,我们可以设置占位符: mt = {__gc = true}

- 析构处理为逆序

#### 1. 复苏

- 当一个对象在被析构器调用时,它会重新变成活跃的.并有可能把对象存储在全局变量中,使得析构器完成后仍然可达

- 复苏必须是可传递的

  ```lua
  A = {x = "this is A"}
  B = {f = A}
  setmetatable(B,{__gc = function(o) print(o.f.x) end})
  A,B=nil
  collectgarbage() -->this is A
  ```

  B的析构器访问了A,因此A在B析构前不能被回收,必须同时复苏A和B

- 由于**复苏**的存在,Lua会分两个阶段回收具有**析构器的对象**.当垃圾收集器首次发现**具有析构器的对象**不可达时,就把这个对象复苏并将其放入**等待被析构的队列**中.一旦析构器开始执行,lua就将该对象标记为**已被析构**.当下一次垃圾收集时发现这个对象**不可达**时,就将这个对象删除.也即是我们必须调用两次collectgarbage()才能保证垃圾回收

- atexit函数

### 垃圾收集器

> 在Lua5.0以前,lua使用的是标记-清除式垃圾回收

- 标记-清除式垃圾回收:

  1. **标记**:把根节点集合标记为活跃
  2. **清理**:首先,遍历所有被标记为**需要进行析构**,但又没有标记**活跃状态**的对象并重新标记为活跃(复苏),放在一个队列中,在之后析构会用到.然后遍历**弱引用表**并从中删除键或值未被标记的元素
  3. **清除**:遍历所有对象.回收未被标记为活跃状态的对象.否则清理标记,准备下一个清理周期
  4. **析构**:调用清理阶段被分离出的对象的析构器

- 增量式垃圾回收

  	- 不需要在垃圾回收时停止主程序的执行
	- 每分配一定内存,垃圾收集器执行一小步
  	- 引入"紧急垃圾收集":当内存分配失败时,Lua会强行进行一次完整的垃圾收集

  

### 控制垃圾收集的步长(Pace)

- collectgarbage函数有几个可选参数来控制垃圾收集
  - "stop":停止垃圾收集
  - "restart":重启垃圾收集
  - "collect":默认选项,执行一次完整的垃圾收集
  - "step":参数data指明工作量,即在分配了data个字节后垃圾收集器应该做什么
  - "count":以KB为单位返回当前已用内存数
  - "setpause":设置收集器的pause参数(回收时间频率).参数data以百分比单位给出新值
  - "setstepmul":设置收集器的stepmul参数(每分配1KB内存应进行多少垃圾回收).参数data以百分比单位给出新值


# 二十 协程(Coroutine)

关于协程的函数都被放在表coroutine中.协程一般拥有四个状态:挂起,运行,正常和死亡

| 方法                | 描述                                                         |
| ------------------- | ------------------------------------------------------------ |
| coroutine.create()  | 创建一个协程,返回一个"thread"值.参数为一个函数.调用时通过resume()函数 |
| coroutine.resume()  | 重启coroutine.第一参数为一个协程.若有额外参数则会传递给协程所拥有的函数上 |
| coroutine.yield()   | 挂起coroutine等待恢复运行.若有参数则返回参数                 |
| coroutine.status()  | 查看coroutine的状态.有三种:dead,suspended,running            |
| coroutine.running() | 返回正在执行的coroutine的线程号                              |
| coroutine.wrap()    | 创建一个协程,返回一个函数.即直接调用协程即可:co(1)           |

> Lua采用的非对称协程.需要两个函数分别管理挂起和恢复协程

### 生产者-消费者问题

...待施工

### 将协程用作迭代器

- 将变量封装一个协程,在闭包里启动协程

```lua
function permgen(a,n)
    n = n or #a	//获取长度
    if n<=1 then coroutine.yield(a) //挂起并返回当前参数
    else ...
end
//生成迭代器
function permuatations(a)
    local co = coroutine.create(function() permgen(a) end)	//新建一个协程作为闭包的变量
    return function()	//创建闭包
         	local code,res = coroutine.resume(co)	//调用协程获取排序后的a
            return res
           end
end
-- 改为wrap函数
function permutations(a)
        return coroutine.wrap(function() permgen(a) end)	--创建闭包.return语句则直接调用wrap函数获取排序后的a
end
--for循环实现迭代
for p in permutations{"a","b","c"} do
        printResult(p)	--打印这个表
end
```

### 事件驱动式编程

##### I/O异步

...待施工

# 二十一 反射

> 反射是程序用来检查和修改其自身某些部分的能力
>
> 如:type和pairs允许运行时检查和遍历未知数据结构.load和require允许程序在自身追加代码或更新代码

- 本章反射主要针对是程序不能检查局部变量,开发人员不能跟踪代码的执行,函数调用对象等.通过**调试库**来完成上诉操作
- 调试库主要分为两个部分:**自省函数**和**钩子**

### 1. 自省机制

- 主要用到的函数有getinfo().第一个参数是一个函数或一个栈层次.返回一个表.一个表可能有以下数据:

  | 字段                        | 作用                                        |
  | --------------------------- | ------------------------------------------- |
  | source/short_src            | 说明函数定义的位置.后为精简版               |
  | linedefined/lastlinedefined | 函数定义的行号(第一or最后)                  |
  | what                        | 说明函数类型.有"Lua""C""main"三种类型       |
  | name/namewhat               | 函数名称/说明该函数含义.如"global""local"等 |
  | activelines                 | 包含该函数所有活跃行的集合                  |

- 当参数为一个数字n时,返回栈层次上的活跃函数数据的表.

- 除此之外,还有可选的第二参数用于指定获得哪些信息

  | n    | 选择name和namewhat                                    |
  | :--- | :---------------------------------------------------- |
  | S    | 选择source,shor_src,what,linedefined和lastlinedefined |
  | L    | 选择activelines                                       |
  | u    | 选择nup,nparams和isvararg                             |

####  访问局部变量

- 我们可以通过debug.getlocal()来检查活跃函数的局部变量.该函数有两个参数.一个是查询函数的栈层次,另一个为变量索引.返回变量名和值
- 我们还可以通过debug.setlocal(n,index,value)来改变局部变量值

####  访问非局部变量

- 通过getupvalue(闭包,index)来访问非局部变量(闭包变量)
- 也可以通过debug.setupvalue(闭包,index,value)来改变非局部变量

####  访问其他协程

- 所有调试库函数都能够接受可选协程作为第一个参数

### 2. 钩子

> 允许用户注册一个"钩子"函数,在运行时被调用

- 有四种事件能够触发一个钩子:
  - 函数call事件
  - 返回return事件
  - 执行一行新代码的line事件
  - 执行完指定数量指令后的count事件
- 我们使用debug.sethook()函数来注册钩子.第一参数为钩子函数,第二参数为掩码字符串,第三参数为count计数器.如果要关闭钩子,只需要调用无参的sethook()函数即可

### 3. 调优

- 性能调优工具主要的数据结构是两个表:Counters和Names

### 4. 沙盒

...待施工

# 二十二 C语言API总览

### 1. 简单的独立解释器

```c
#inlucde<stdio.h>
#include<string.h>
#include "lua.h"	//声明了Lua提供的基础函数,所有函数都有一个前缀lua_
#include "lauxlib.h"	//声明了辅助库的函数,以luaL_开头,使用lua.h提供的基础函数提供更高层次的抽象
#include "lualib.h"

int main(void){
    char buff[256];
    int error;
    lua_State *L = luaL_newstate();	//创建一个Lua新状态,新环境没有任何函数
    luaL_openlibs(L);	//打开Lua的标准库函数来获取函数
    
    //Lua将所有状态都保存再动态的结构体lua_State中,因此所有Lua函数都要接受一个指向该结构的指针作为"第一参数".如下文中的			  lua_pcall
    while(fgets(buff,sizeof(buff),stdin)!=NULL){
        error = luaL_loadstring(L,buff) || lua_pcall(L,0,0,0);
        if(error){
            fprintf(stderr,"%s\n",lua_tostring(L,-1));
            lua_pop(L,1);	//从栈中弹出错误信息
        }
    }
    
    lua_close(L);	//关闭Lua环境
    return 0;
}
```

### 2. 栈

- Lua和C之间通信主要依靠**"虚拟栈"**,几乎所有API都才操作这个栈中的值,也可以利用栈保存中间结果
- 通过"虚拟栈"可以解决两个问题:
  - 动态类型和静态类型体系之间不匹配
  - 自动内存管理和手动内存管理之间不匹配
- 栈中每个元素都能保存Lua中任意类型的值,当我们想从Lua中获取一个值时,Lua将指定值压入栈中.当想传递一个值给Lua时,将值压入栈中调用Lua将其从栈中弹出即可
- Lua严格按照栈的"先进后出"原则,而C语言可以像数组一样操作这个栈

#### 压入元素

- 针对每一种能用C语言类型表示的Lua数据类型,C API中都有一个对应的压栈函数
  - void lua_pushnil(luaState *L)
  - void lua_pushboolean(luaState *L,int bool)
  - void lua_pushnumber(luaState *L,lua_Number n)
  - void lua_pushinteger(luaState *L,lua_Integer n)
  - void lua_pushlstring(luaState *L,const char *s,size_t len)
  - void lua_pushstring(luaState *L,const char *s)
- 时刻确保栈中有**足够的空间**,默认空间为20.我们可以通过:
  - int lua_checkstack(luaState *L,int sz) 来检查,其中sz为我们所需要的额外栈的空间.

#### 查找元素

- C API使用索引来引用栈中元素.其中分**正数索引**和**负数索引**.正数索引中:栈底为1.负数索引中:栈顶为-1
- C API提供了名为lua_is*的函数来帮助我们检查类型.如lua_isnumber,lua_isstring
- 函数lua_type会返回栈中元素类型,每一种类型对应一个常量值.如LUA_TNIL,LUA_TNUMBER,LUA_TSTRING等
- 函数lua_to*用于从栈中获取一个值
  - int lua_toboolean(luaState *L,int index)
  - const char* lua_tolstring(luaState *L,int index,size_t *len)
  - lua_Number lua_tonumber(luaState *L,int index)
  - lua_Interger lua_tointeger(luaState *L,int index)

### 其他栈操作

```c
int lua_gettop(luaState *L)	//返回栈中元素个数
void lua_settop(luaState *L,int index)	//设置栈的空间大小
void lua_pushvalue(luaState *L,int index)	//向栈中压入一个值
void lua_rotate(luaState *L,int index,int n)	//将指定索引的元素向"栈顶"位置移动n个位置,n为负数表示向相反方向旋转
void lua_remove(luaState *L,int index)	//将删除元素"旋转"到栈顶,弹出该元素,并让所有元素下移
void lua_insert(luaState *L,int index)	//将"栈顶"元素移动到指定位置,并上移所有元素
void lua_replace(luaState *L,int index)	//弹出栈顶值并重新设置
void lua_copy(luaState *L,int fromidx,int toidx)	//将一个索引上的值复制到另一个索引上,并且原值不受影响
void getglobal(luaState *L,varname)	//将变量压入栈
```

### 3. 使用C API进行错误处理

...待施工

### 4. 内存分配

- 使用原始的lua_newstate来创建lua状态
  - lua_State* lua_newstate(lua_Alloc f,void *ud);
  - 第一参数为**"分配函数"**,另一个为用户数据
  - 分配函数必须满足以下声明:typedef void* (*lua_Alloc)(void* ud,void* ptr,size_t osize,size_t nsize);

# 二十三 拓展应用

...待施工

### 3. 调用Lua函数

- 首先将待调用函数压入栈;然后,压入函数参数;接着用lua_pcall进行实际调用;最后从栈中取出结果

- lua_pcall(luaState *L,参数个数,结果个数,错误处理)

  ```c
  double F(luaState* L,double x,double y){
      int isnum;
      double z;
      
      lua_getglobal(L,"f");	//压入lua函数
      lua_pushnumber(L,x);	//压入第一个参数
      lua_pushnumber(L,y);	//压入第二个参数
      //进行调用
      if(lua_pcall(L,2,1,0)!=LUA_OK)
          error();
      //获取栈顶的结果
      z = lua_tonumberx(L,-1,&isnum);
      if(!isnum) error;
      //将栈中的数据弹出
      lua_pop(L,1);
      //返回结果
      return z;
  }
  ```

  

# 二十四 在Lua中调用C语言

- 当Lua调用C函数时,必须先注册该函数,为Lua提供C函数的地址

# 1. C函数

- 所有在Lua中注册的函数都必须使用一个相同的**原型**:

  - typedef int (*lua_CFunction)(lua_State *L)
  - 参数为一个Lua状态指针,返回值为压入栈中的返回值个数

- 调用函数前,必须通过lua_pushcfunction注册该函数.获取一个指向C函数的指针,然后再Lua中创建一个"function"类型,代表待注册的函数

  ```c
  在简单解释器中luaL_openlibs后面添加:
  	lua_pushcfunction(L,l_sin);	//压入函数
  	lua_setglobal(l,"mysin");	//注册函数
  ```

### 2. 延续

...待施工

### 3. 模块

- 通常一个C模块只有一个用于打开库的公共函数,其他所有函数都是私有的,在C语言中被声明为static

- 首先,定义一个库函数

  ```c
  static int l_dir(lua_State *l){
  	...
  }
  ```

- 然后声明一个数组,包含函数名和函数指针,结尾以{NULL,NULL}

  ```c
  static const struct luaL_Reg mylib [] = {
      {"dir",l_dir},
      {NULL,NULL}	//哨兵
  }
  ```

- 最后使用函数luaL_newlib声明一个主函数并注册到一个新表中

  ```c
  int luaopen_mylib(lua_State* L){
      luaL_newlib(L,mylib);
      return 1;
  }
  ```

- 编写完后必须将其链接到解释器.然后就可以使用require加载这个模块了

# 二十五 编写C函数的技巧

