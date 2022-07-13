---
title: C prime plus 学习笔记
date: 2020-12-07 20:10:54
tags:
---

# C++笔记
<!--more--> 

# 第二章 开始学习c++

1. using namespace std放在函数外
2. using namespace std放在函数内
3. using std::cout 编译命令
4. 直接使用std::cout

# 第三章 处理数据
- 初始化：int i={}; or int i={1}; or int i{};
- 无符号超越限制则取另一端范围的取值
- whcar_t 关键字 前缀字L  使用wcin和wcout输出
- char16_t和char32_t 前缀字：u和U
- 强制类型转换函数:static_cast<type>(value);
-  auto关键字（c#的var）

# 第四章 复合类型
##### 数组
- 声明 int nums[]{};
- 注意不能将一个数组赋予另一个数组
- 动态数组：int*nums=new int[10];

##### 字符串
- 声明：char ps[]{};
- 使用strcpy or strncpy()来给一个字符数组赋值
- 读取输入:cin.getline(),get(); //getline将丢弃换行符，get将保留换行符
- string类:需要用到<string>头文件
- 指针与字符串：如果给cout传递一个字符数组，将打印该字符串而不是地址

##### 指针
- 声明: int* p1,*p2;对每个指针变量，都需要一个*
- new关键字：通过new分配内存，int *p1=new int,在“堆”中分配内存
- delete关键字：释放指针指向的内存，但不删除指针本身.对空指针应用delete是安全的
- 存储方式：自动存储（栈），静态存储(栈)，动态存储（堆）

##### 模板类
- vector类：声明 vector<type> name(n),n可以为常数或变量，存储在堆
- array类：声明 array<type,n> name,n为常数，存储在栈

# 第五章 循环和关系表达式
- C风格字符串使用strcmp()函数进行字符串的比较
- 基于范围的for循环  for(double &x: prices){};
##### 循环输入
1. cin  //使用哨兵字符，无法输入空格和换行符
2. cin.get(char)
3. 文件尾条件：cin.eof(),cin.fail()

# 第六章 分支语句和逻辑运算符
- 字符函数库cctype
#### 简单文件的输入/输出
##### 输出
- 包含头文件fstream
- using namespace std;
- open(),close()方法
- 使用<<输出对象

##### 输入
- 包含头文件fstream
- using namespace std;
- open(),close()方法
- 使用>>输入对象
- get(),getline(),eof(),fail(),good()方法
- ifstream可用作测试条件来检测最后一个读取是否成功

# 第七章 函数——C++编程模块
- 提供函数定义，提供函数原型，调用函数
- 为防止函数修改内容，可在形参中添加const关键字

##### 函数指针
- 声明：double (*pf)(int);

# 第八章 函数探幽
#### 内联函数
- 节省了函数跳地址执行的时间，增加了内存使用
- 使用关键字inline，通常将定义和原型放在一起
- 在类中声明定义的函数自动成为内联函数

#### 引用变量
- 使用&+变量名声明，必须初始化

#### 引用传递
**临时变量，引用参数和const**
 如果引用参数为const，则在下面两种情况生成临时变量

- 实参类型正确，但不是左值*
- 实参类型不正确，但可以转换为正确的类型

**引用结构**
1. 返回值为引用时，效率更高，但要注意不能返回函数终止时不存在的内存引用（不是临时变量），同样也应该避免返回指向临时变量的指针。
2. 解决办法为返回作为参数的引用

#### 默认参数
 作用为省略实参时自动使用的一个值
- 对于带参数列表的函数，必须从右往左赋值
- 只有原型指定了默认值

#### 函数重载
- 注意类型引用和类型本身为同一个特征标

#### 函数模板
 是通用的函数描述，通常使用“<u>泛型</u>”实现

**建立模板**
- 声明和定义：template<typename T>

##### 显式具体化
- 声明和定义：template<> void Swap<job>(job &,job &);
- 显示实例化：template void Swap<char>(char &,char &);//直接作用main函数内

**函数模板的发展**
- 使用decltype关键字：decltype(x+y) xpy;来完成对xpy的类型推断
- 函数返回类型推断：auto h(T1 x,T2 y)->decltype(x+y)

# 第九章 内存模型和名称空间
### 单独编译
##### 将文件分成三个部分：
1. 头文件:包含声明与函数原型
2. 源代码文件：包含结构与函数实现
3. 源代码文件：包含调用与实现

##### 头文件经常包含的内容：
- 函数原型
- 使用#define和const定义的符号常量
- 结构，类声明
- 模板声明
- 内联函数

### 存储连续性，作用域和链接性
##### 自动存储连续性
- 存储连续性为自动，作用域为局部，没有链接性

##### 静态持续变量
- 存储连续性为静态
1. 外部链接性：代码块外部声明
- 使用extern声明另一个文件的全局变量
2. 内部链接性：代码块外部并用static声明
- 将隐藏全局变量
3. 无链接性：代码块内部并用static声明

##### 说明符和限定符
- const函数：保证成员函数不修改调用对象的值（**针对无参**)
- volatile关键字将告诉编译器不用进行寄存器的优化
- mutable关键字用来指出某个const变量成员也可以被修改

##### 名称空间
- 链接性为外部
- 可以使用：：解析来使用里面的名称

# 第十章 对象和类
##### 封装
1. 将实现细节放在一起并和他们的抽象分开
2. 数据隐藏
3. 将类定义和声明放在不同文件中
4. 将实现细节放在私有部分中

##### 类和结构的区别
唯一区别结构默认访问是public，而类为private

### 结构函数与析构函数
##### 结构函数
- 列表初始化： Stock stock={};
##### 析构函数
- 声明：~tyepname（）,无参
- 调用:1.静态对象:程序结束后自动调用。2.自动存储对象：代码块执行完毕后。3.new对象：使用delete释放时调用

# 第十一章 使用类

### 运算符重载
- 格式:类名 + operator +  运算符(+-*/ ) +  (参数列表)
- 例:Time operator+(const Vector &b) const

##### 重载限制
1. 重载后运算符必须有一个为用户定义的类型,防止为标准类型重载
2. 使用运算符不能违背原来的规则
3. 某些运算符不能重载: . , :: , ?:

### 友元函数
- 定义:**非成员函数访问类的私有成员**
- 声明:friend 类名 operator*(double m,const 类名 &t);
- 定义:不使用friend关键字以及类的限定符 类名::

# 第十二章 类和动态内存分配

- 在类声明中声明的静态成员需要在其方法文件中定义,**并且所有的类对象都共享一个静态变量**.当静态成员为const或枚举类型时可在声明中初始化

### 特殊成员函数
#### 默认构造函数
##### 成员初始化列表

```
C_name::C_name(int n,int m):mem1(n),mem2(0),mem3(n*m+2){
//
}
```

- 只有构造函数能使用
- 必须用来初始化非静态const成员和引用数据成员
- 被初始化顺序必须和声明一致

#### 默认析构函数
#### 复制构造函数

> 用于初始化过程中(包括按值传递时)和**返回对象**时调用的函数

##### 默认复制构造函数
> 浅复制:逐个复制非静态成员的值
##### 显式复制构造函数
> 深度复制:**将地址所指向的副本赋给用于new初始化的成员**

#### 赋值运算符
 与复制构造函数类似的浅复制与深度复制,修改时有几点需要注意
 1. 需要使用delete来释放以前分配的数据
 2. 避免将对象赋给自身,应当提前检查
 3. 函数应当返回一个指向调用对象的引用

#### 静态类成员函数
1. **不能通过对象来调用静态成员函数**,如果该函数是公有的,则可以通过作用域解析符来调用.如 类名::静态类成员函数
2. 静态类成员函数**只能使用静态数据成员**

### 总结new时的注意事项
- 当在构造函数使用new则要在析构函数使用delete,其中注意格式对应(有[]和无[])
- 如果有多个构造函数,则new必须以相同方式使用
- 需要定义一个**显式复制构造函数和赋值运算符**来完成深度复制(deep copy)

### 总结返回对象
##### 返回指向const对象的引用
如果函数返回的是**传递给他的参数**,则可以通过返回const引用来提高效率

```c++
const Vector & Max(const Vector & v1,const Vector & v2)
{
	if(v1.mag()>v2.mag()) return v1;
	else return v2;
}
```
##### 返回指向非const对象的引用
常用于重载赋值运算符以及cout<<运算符中

##### 返回对象
当返回对象为局部变量时,不能返回该对象的引用,应直接返回对象

##### 返回const对象

### 总结指针的对象小结
- 可以将指针初始化已有的对象,就不必用new分配内存

```c++
String * first=&saying[0];
```

- 可以使用new来初始化指针,这将创建一个新的对象,需要使用delete

```c++
String * favorite=new String(saying[0]);
```

- 可以使用->运算符访问类方法
- 可以使用*运算符获得对象

#### 定位new运算符
> 需要用到头文件<new>
> 作用:**创建对象但是不分配内存，而是在已有的内存块上面创建对象**。用于需要反复  创建并删除的对象上，可以降低分配释放内存的性能消耗
- 表达式:指针 p =new (buffer) 对象;
- 删除:不能使用delete直接删除,需要人为调用其析构函数
- 当需要管理两个缓冲区时,使用sizeof函数来分割内存地址

### 复习各种技术
#### 重载<<运算符
> 友元函数

```c++
ostream & operator(ostream & os,const c_name & obj){
	os<<...;
	return os;
}
```
#### 转换函数

# 第十三章 类继承

### 公有派生(is a关系)
- 使用关键字**public**
- 继承基类的数据成员
- 可以使用基类的方法
- **派生类需要自己的构造函数**

> 特殊关系:基类指针和基类引用可以隐式指向派生类对象,其包括实参和形参形式

##### **多态**
- 在派生类中重新定义基类的方法
- 虚方法

##### 虚方法
> 使用新关键字:**virtual**,仅在声明方法原型中使用
- 当使用虚方法时,将会根据引用或指针指向对象类型选择方法
- **基类**需要声明一个虚析构函数,确保释放派生对象时,将调用相应对象的析构函数保证正确的顺序
- 构造函数和友元函数不能为虚方法
- 如果重新定义虚方法,需要保持和其函数原型一致

### 静态联编和动态联编
##### 静态联编
> 联编为将源代码中的函数调用解释为执行特定的函数代码.静态联编则为在编译过程中完成

##### 动态联编
> 在程序运行时选择正确的虚方法的代码

#### 虚函数的工作原理

给每个对象添加一个隐藏对象.隐藏对象中保存了一个指向函数地址数组的指针的**虚函数表**

| 指针 | 地址 |
| :--: | :--: |
|  *p  | 2064 |

### 访问控制
- 使用关键字**protect**
- 使派生类能直接访问基类的保护成员,并对外部隐藏该保护成员

### 抽象基类
> 通过使用**纯虚函数**来实现.表达式: vitual 函数 =0; 在类中可以**不定义该函数**
- 不能创建抽象基类对象
- 抽象基类必须包含至少一个纯虚函数

### 继承和动态内存分配
##### 当基类使用new而派生类不使用new
- 不需要为继承类定义显示析构函数,显示复制构造函数,重载赋值运算符操作

##### 当基类使用new且派生类使用new
- 需要为继承类定义显示析构函数,显示复制构造函数,重载赋值运算符操作

1. 析构函数:自动完成

   ```c++
   baseDMA::~baseDMA(){
   `delete[] label;
   }
   hasDMA::~hasDMA(){
   `delete[] style;
   }
   ```

   

2. 复制构造函数:使用初始化成员列表调用基类的复制构造函数

   ```c++
   hasDMA::hasDMA(const hasDMA & hs):baseDMA(hs){
   	style=new char[std::strlen(hs.style)+1];
   	std::strcpy(style,hs.style);
   }
   ```

   

3. 重载运算符:使用作用域解析运算符显示调用基类的赋值运算符

   ```c++
   baseDMA & baseDMA::operator=(const baseDMA & rs)
   {
       if (this == &rs)
           return *this;
       delete [] label;
       label = new char[std::strlen(rs.label) + 1];
       std::strcpy(label, rs.label);
       rating = rs.rating;
       return *this;
   }
   
   hasDMA & hasDMA::operator=(const hasDMA &hs){
   	if(this=&hs)
   		return *this;
   	baseDMA::operator=(hs);////*this=hs;
   	delete[] style;
   	style = new char[std::strlen(hs.style)+1];
   	std::strcpy(style,hs.style);
   	return *this;
   }
   ```

   
# 第十四章 C++中的代码重用

### 包含对象成员的类(has-a关系)
#### valarray类
> 需要包含<valarray>库,声明:valarray<type> name;

##### explict 关键字
> 防止单参数构造函数发生**隐式转换**

```c++
Student doh("Homo",10);
doh=5;
```

上诉代码将5转换成Student(5)的临时对象赋值给doh.

### 私有继承
> 使用私有继承,基类公有成员和保护成员都将成为派生类的私有成员,且不能使用基类的接口

- 使用关键字:private
- 使用类名来标识成员初始化列表

```c++
Student(const char *str,const double *pd,int n) : std::string(str),ArrayDb(pd,n){}
```
- 使用作用域解析符::来访问基类的方法
- 使用强制类型转换来访问基类对象

```c++
(const string &) stu
```

##### 保护继承
> 基类的公有成员和保护成员都成为派生类的保护成员.

与私有继承类似,区别为保护继承可以在第三代类中继续使用基类的公有接口

### 多重继承

....

### 类模板

#### 定义类模板
> 声明:template <typename T>

##### 如何使用指针模板
- 让**调用程序**提供**指针数组**来管理指向

#### 非类型参数(表达式参数)
> 声明:template<typename T,int n>

- 表达式可以实整形,枚举,引用,指针
- 优点是可以用new和delete管理堆内存,执行速度快
- 缺点是会为每个数组生成自己模板

#### 模板多功能性
1. 嵌套使用模板
	
	```c++
	ArrayTP<ArrayTP<int,5>,10> two;
	```
	
	two为包含10个元素的数组,其中每个元素为包含5个int数据的数组
	
2. 使用多个类型参数

   ```c++
   template<typename T1,typename T2>
   ```

3. 默认类型模板参数

   ```c++
   template<typename T1,typename T2=int>
   ```

### 模板具体化

1. 隐式实例化
	编译器在需要对象之前,不会生成类的隐式实例化
	
	```c++
	ArrayTP<double 30>*pt;
	pt=new ArrayTP<double 30>;
	```
	
	第二条语句会导致编译器生成类定义,并创建一个对象

2. 显示实例化

   ```c++
   template class ArrayTP<string,100>;
   ```
   
3. 显式具体化
	有时候需要对特殊类型实例化的行为进行修改使其行为不同.(**如>比较和strcmp()比较,一个值,一个指针**)
	定义模板如下
	
	```c++
	template <> class Classname<>
	```
	
4. 部分具体化
   限制部分模板的通用性
   
   ```c++
   template <class T1> class Pair<T1,int>{};
   ```
   
   
### 成员模板
> 模板可以用作结构,类或模板类的成员

### 将模板用作参数
```c++
template<template<typename T> class Thing>
class Crab
```

template<typename T> class 是类型,Thing是参数.假设有下列声明:

```c++
Crab<Stack> s1;
```

则Stack必须为一个模板类.

- 也可以混合模板参数和常规参数,如下:

  ```c++
  template<template<typename T> class Thing,typename U,typename V>
  class Crab{
  private:
  	Thing<U> s1;
  	Thing<V> s2;
  }
  声明:Crab<Stack,int,double> nebulla;
  ```

### 模板类和友元
1. 非模板友元
	需要在声明中使用**模板作为参数**并在定义中**显式具体化**.

	```c++
	声明:
	template <typename T>
	class HasFriend
	{
	public:
	    friend void reports(HasFriend<T> &);
	};
	定义:
	void reports(HasFriend<int> & hf)//具体化为int
	{
	    cout << "HasFriend<int>: "<<hf.item << endl;
	}
	void reports(HasFriend<double> & hf)//具体化为double
	{
	    cout << "HasFriend<double>: " << hf.item << endl;
	}
	```
	
2. 约束模板友元

> 使友元函数本身成为模板,让类的每一个具体化都获得与友元匹配的具体化

​		首先,在类定义前面声明每个模板函数

```c++
template<typename T> void counts();
template<typename T> void report(T &);
```

​		然后,在函数中将模板声明为友元

```c++
template <typename TT>
class HasFriendT
{
public:
    friend void counts<TT>();//无参数时必须使用<TT>语法指出其具体化
    friend void report<>(HasFriendT<TT> &);//<>指出这是模板具体化
};
```

​		最后,为友元提供模板定义

```c++
template<typename T>
void counts()
{
    cout << "template size: " << sizeof(HasFriendT<T>) << ";";
    cout << "template counts(): " << HasFriendT<T>::ct << endl;
}
template <typename T>
void report(T & hf)
{
    cout << hf.item << endl;
}
```


3. 非约束模板友元
> 通过在类里面声明模板创建非约束友元函数.对于非约束友元,其参数与模板类类型参数是不同的

```c++
template <typename T>
class ManyFriend
{
public:
    template<typename C, typename D> friend void show2(C&, D&);
};
template<typename C, typename D>void show2(C &c, D &d)
{
    cout << c.item << ", " << d.item << endl;

}
```


### 模板别名(c++11)

- 可以使用typedef 为模板具体化指定别名
- 可以使用using为模板具体化指定别名

# 第十五章 友元,异常和其他
## 友元
#### 友元类
> 当既不是is-a关系也不是has-a关系时使用友元类(类似事件)

```c++
class Tv{
	friend class Remote;
}
```

- 友元类可以访问原始类中的所有成员

#### 友元成员函数
> 当友元类大多数方法使用原始类的公有接口实现时

- 注意声明顺序,使用**前向声明**可以避免循环依赖

```c++
class Tv;//提前声明
class Remote{...;}
class Tv{...;}
```

## 嵌套类
- 与包含的差别:包含意味着将类对象作为另一个类的成员,而对类进行嵌套**不创建类成员**,而是定义了一种类型,该类型仅在包含嵌套类声明的类中有效.

- 作用域
- 模板嵌套

## 异常
#### 1.调用abort():终止程序,返回一个随实现而异的值
#### 2.返回错误码:使用函数的返回值指出问题
#### 3.异常机制
- 引发异常
- 使用处理程序捕获异常
- 使用try块

```c++
try{某函数;}
catch(const char*){}//处理c字符串
catch(bad_hmean &hg){}//将对象用作异常类型,此处对象虽然时引用,但实质是副本
某函数{throw "..."}//类似返回语句,将控制权转交给包含try块的函数
```

##### 3.1 栈解退
假设程序出现异常,程序将释放栈中内存,一直释放到找到一个位于try块的返回地址,控制权转到块尾的处理程序.对于栈中的自动对象,其析构函数将被调用.
##### 3.2 exception类
> 需要头文件<exception>,也可用作基类,其有一个what()的虚函数返回一个字符串

1. stdexcept异常类
	
	> 该文件定义了logic_error和runtime_ error类
2. bad_alloc异常和new
3. 空指针和new

##### 3.3 未捕获异常
	未捕获异常程序首先调用terminate()函数,默认情况下terminate()函数调用abort()函数.也可以修改set_terminate()函数改变terminate()调用的函数


#### RTTI
	RTTI是运行阶段类型识别的简称
C++有3个支持RTTI的元素(**只适用于包含虚函数的类**)
- dynamic cast运算符
- typeid运算符
- type_info结构

#### 类型转换运算符
- dynamic_cast

  ```c++
  dynamic_cast<typename>(expression)
  ```

  能够使得类进行安全的转换,若不能转换则返回空

- const_cast

  ```c++
  const_cast<typename>(expression)
  ```

  改变const或volatile的标签,使得一个常量能够暂时修改

- staic_cast

  ```c++
  static_cast<typename>(expression)
  ```

  能够安全地进行隐式转换,否则抛错

- reinterpret_cast

# 第十六章 string类和标准模板库

## String类
##### String类输入

```c++
string stuff;
cin>>stuff;
getline(cin,stuff)//getline(cin,stuff,':')
```

string版本的getline()函数从输入中读取字符,并将其存储到到目标string中,直到下列三种情况之一:
- 到达文件尾,此时输入流efobit被设置,fail()和eof()放回true
- 遇到分界字符.此时将分界字符从输入流中删去,但不存储它
- 字符数达到最大值,输入流的failbit将被设置,fail()返回true

## 智能指针模板类
	智能指针是行为类似于指针的类对象
> 需包含头文件memory	

- 所有权指针

  ```c++
  unique_ptr<type> name(new type)//可以使用new[]分配内存
  unique_prt<type> ps1(new type);
  ps2=move(ps1)//move函数将其转换为右值引用
  ```

- 引用计数指针

  ```c++
  shared_prt<type> name(new type)
  ```

  

##### 如何选择智能指针
- 如果程序要使用多个指向同一个对象的指针,应选择shared_ptr
- 如果程序不需要多个指向同一个对象的指针,可以使用unique_ptr

## 标准模板库
#### 迭代器
#### STL函数
- for_each()
- random_shuffle()//随机排列区间的元素
- sort()
#### 容器

## 泛型编程
#### 迭代器类型
- 输入迭代器
- 输出迭代器
- 正向迭代器
- 双向迭代器
- 随机访问迭代器

#### 概念,改进和模型
1. 将指针用于迭代器
- copy()
- ostream_iterator和istream_iterator
2. 其他迭代器
- reverse_iterator
- back_insert_iterator
- insert_iterator

#### 容器种类
##### 序列容器
1. vector<> 2. deque<> 3. list<> 4. forward_list<> 5. queue<> 6. priority_queue<> 7. stack<>

##### 关联容器(基于树结构))
1. set<T>  2.multiset<> 3. map<> 4.multimap<T,T>

##### 无序关联容器(基于哈希表)

## 函数对象
	也叫函数符.这包括函数名,指向函数的指针和重载()运算符


#### 函数符概念
- 生成器
- 一元函数
- 二元函数
- 返回bool值的一元函数是谓词
- 返回bool值的二元函数是二元谓词

#### 预定义的函数符
	包含在头文件<function>中

#### 自适应函数符和函数适配器
	上述预定义的函数符都是自适应的.
	原因是它携带了标识参数类型和返回类型的typedef成员,分别为result_type,first_argument_type和second_argument_type
	意义是函数适配器可以使用函数对象,并认为存在typedef成员
STL可以使用bind1st和bind2nd来完成将自适应二元函数转换为一元函数

bind1st与bind2nd的参数位置相反

```c++
transform(gr8.begin(),gr8.end(),out,bind1st(multiplies<double>(),2.5));//将gr8每个元素与2.5相乘
```

## 算法
	对于算法.首先它们都是用模板来提供泛型;其次,它们都使用迭代器来提供访问容器中的数据通用部分

#### 算法组
STL将算法分成4组:
- 非修改式序列操作
- 修改式序列操作
- 排序和相关操作
- 通用数字运算

前三个包含在头文件<algorithm>中,最后一个包含在头文件<numeric>

#### 算法通用特征
- 就地版本
- 复制版本

返回一个指向超尾的迭代器

## 其他库
	头文件<complex><random>

##### 模板initializer_list
> 初始化列表语法,包含头文件<initializer_list>

```c++
std::vector<double> payments{45.99,30.23,98.01};
```

# 第十七章 输入,输出和文件

## 概述
#### 流和缓冲区
	c++把输入和输出看作字节流.通常,使用缓冲区能高效处理输入和输出

#### iostream文件
- cin对象对应标准输入流
- cout对象对应标准输出流
- cerr对象对应标准错误流.这个流不会被缓冲
- clog对象对应标准错误流.这个流被缓冲
- 对象代表流,存储与对象相关的数据成员

#### 重定向

## 使用cout进行输出
#### 重载的<<运算符
1. 输出和指针

	C++使用指向字符串的地址来表示字符串.对于指针,需要使用(void*)转换
2. 拼接输出

#### 其他ostream方法
```c++
cout.put()//用于显示字符
cout.write(char*,int)//用于显示字符串
```

#### 刷新输出缓冲区
- flush控制符刷新缓冲区
- endl控制符刷新缓冲区并插入一个换行符

#### 用cout进行格式化
1. 修改显示时的计数系统

```c++
hex(cout)
cout<<hex
```

2. 调整字段宽度

```c++
cout.width()//返回当前设置
cout.width(int)//将宽度设置为i个空格并返回以前的宽度值
```

3. 填充字符

```c++
cout.fill(char)
```

4. 设置浮点数精度

```c++
cout.precision(int)
```

5. 打印末尾的0和小数点

```c++
cout.setf(fmtflag)
```

6. 标准控制符

```
cout<<left<<fixed//见p609表格
```
7. 头文件iomanip

```
setprecision(int);
setfill(char);
setw(int);
```

## 使用cin进行输入
#### cin>>检查输入
	跳过空白(空格,换行符和制表符),直到遇到非空白字符.

> 对于单字符,读取该字符.其他模式下,读取从非空白字符开始到与目标类型不匹配的一个字符的全部内容

#### 流状态
	流状态由三个ios_base元素组成.分别为:eofbit,badbit,failbit.可设置为1或0

1. 设置状态

   ```c++
   //默认参数为0
   clear(eofbit)//设置eofbit为1,其他两位为0
   setstate(eofbit)//设置eofbit为1,不影响其他位
   ```

2. IO和异常

   ```c++
   cin.exception()
   ```

3. 流状态的影响

#### 其他istream类方法
##### 单字符输入
- get(char &)//读取下一个字符,包括空白00
- get(void)//返回一个int整形
##### 字符串输入
- get(char*,int,(char))
- getline(char *,int,(char))
- ingore(int,int=EOF)
##### 意外字符串输入

无输入或者超过最大字符数时

##### 其他方法

- read(char*,int)//读取指定的字节,并存储到相应位置中
- peek()//返回下一个字符
- gcount()//返回最后一个**非格式化**方法的字符数
- putback()//将一个字符插入到输入字符串中,被插入的字符时下一条输入语句中读取的第一个字符

## 文件输入和输出
#### 简单的文件I/O
> 包含头文件fstream

当需要写入or读取文件时:
1. 创建一个ofstream/ifstream对象管理输出/输入流
2. 将该对象与特定文件关联起来
3. 使用cout/cin方法

```c++
ofstream fout;
fout.open("name");
fout<<"string"/fout>>"string"
fout.close();//缓冲区仍保留
```

#### 流状态检查
- file.is_open()

#### 文件模式

| 常量             | 含义                   |
| :--------------- | :--------------------- |
| ios_base::in     | 打开文件读取           |
| ios_base::out    | 打开文件写入           |
| ios_base::ate    | 打开文件并移到文件尾   |
| ios_base::app    | 追加到文件尾           |
| ios_base::trunc  | 如果文件存在则截断文件 |
| ios_base::binary | 二进制文件             |

#### 随机存取
	指移动到文件的任何位置

- 创建fstream类对象

- 调用其seekg()或seekp()方法

  ```c++
  seekg(streamoff,ios_base::seekdir)//偏移
  seekg(streampos)//移动
  tellg()/tellp()//返回当前位置
  ```

## 内核格式化
	读取string对象的格式化信息并将其写入string对象中被称为内核格式化

> 包含头文件<sstream>

创建一个ostringstream对象,则可以将信息写入其中

```c++
ostringstream outstr;
string str="";
outstr<<str;
```

创建一个istringstream对象,则可以与字符串关联并管理

```c++
string str="";
istringstream instr(str);
string word;
instr>>word;//每次读取一个单词
```

# 第十八章 探讨C++新标准

## 复习
##### 新类型
##### 统一的初始化
1. 缩窄
2. std::intializer_list

##### 声明
1. auto

2. decltype

3. 返回类型后置 

   ```c++
   template<typename T,typename U)
   auto eff(T t,U u)->decltype(T*U){}
   ```

4. 模板别名using=

5. nullptr

##### 智能指针
- unique_ptr
- shared_ptr
- weak_ptr

##### 作用域内枚举
- enum class New1{};
- enum struct New2{};

##### 对类的修改
1. 显示转换运算符

   使用explicit关键字防止单参数自动转换

2. 类内成员初始化

##### 右值引用
	使用&&表示,右值通常为表达式,且不能对其使用地址运算符

## 移动语义和右值引用
	移动语义实际上避免了移动原始数据,而只是修改了记录

##### 移动构造函数
	需将原来的指针设置为空指针,且不能在参数声明中使用const
	1. 与右值引用匹配
	2. 提供移动构造函数

##### 赋值
	移动语义也适用于赋值运算符

##### 强制移动
	使用头文件<utility>中的std::move()函数

## 新的类功能

##### 默认的方法与禁用的方法
- 使用关键字default显示地声明方法的默认版本

  ```c++
  class SomeClass{
  public:SomeClass()=default;
  }
  ```

- 关键字delete可用于禁止编译器使用特定方法和特定转换

  ```c++
  class SomeClass{
  public:SomeClass(const SomeClass &)=delete;
  		void redo(int)=delete;
  }
  ```

##### 委托构造函数
##### 继承构造函数

```c++
class DR:public BS{
public:using BS::BS;
}
```

##### 管理虚方法override和final
- 使用overrider说明符指出所覆盖的虚函数

- 使用final说明符禁止派生类覆盖其虚方法

  ```c++
  virtual void f(char *ch) const override{}
  virtual void f(char ch)const final{}
  ```

## Lambda函数
```c++
也称匿名函数,表达式为[](int x){reutnr ;}
```

1. 可给lambda指定名称

   ```
   auto mod3=[](int x){return x%3==0;}
   ```

2. lambda可访问作用域内的任何动态变量

   ```
   若指定变量名,如[z],按指访问变量;若加上&,则按引用访问变量.
   [&]能够按引用访问所有动态变量,[=]能够按值访问所有变量.
   也可混合使用,如[ted,&ed],[&,ed].
   ```

## 包装器
- bind

- men_fn

- reference_wrapper

- function

  ```
  funciton<double(double)> ef1=dub;
  ```

## 可变参数模板
1. 模板和函数参数包

   ```c++
   template<typename... Args>
   void show_list(Args... args){}
   ```

   其中,省略号为元运算符,Args是一个模板参数包,args是一个函数参数包

2. 展开参数包

   ```c++
   template<typename... Args>
   void show_list(Args... args){show_list(args...);}
   ```

   将省略号放在函数参数包后面即是展开函数包

3. 递归

   ```c++
   template<typename T,typename... Args>
   void show_list(T value,Args... args){}
   ```

   

## C++11新增的其他功能
##### 并行编程
	添加关键字thread_local实现多线程

##### 新增的库
```c++
头文件<random>提供更复杂的随机数
头文件<chrono>提供时间间隔
头文件<regex>支持正则
```