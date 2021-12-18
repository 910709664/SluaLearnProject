---@class HelloWorld
---@field public cc Color32
---@field public someAct UnityAction
---@field public Item Object
---@field public Item Object
local HelloWorld={ }
---@public
---@return void
function HelloWorld.say() end
---@public
---@return Byte[]
function HelloWorld.bytes() end
---@public
---@param array Int16[]
---@return void
function HelloWorld.int16Array(array) end
---@public
---@return Vector3[]
function HelloWorld.vectors() end
---@public
---@param a Nullable`1
---@return void
function HelloWorld.nullf(a) end
---@public
---@return IEnumerator
function HelloWorld:y() end
---@public
---@return Dictionary`2
function HelloWorld:foo() end
---@public
---@return Dictionary`2[]
function HelloWorld:foos() end
---@public
---@param x Dictionary`2[]
---@return Int32
function HelloWorld:gos(x) end
---@public
---@return Dictionary`2
function HelloWorld:too() end
---@public
---@return List`1
function HelloWorld:getList() end
---@public
---@param t LuaTable
---@return void
function HelloWorld.setv(t) end
---@public
---@return Int32
function HelloWorld.getNegInt() end
---@public
---@return LuaTable
function HelloWorld.getv() end
---@public
---@param t Type
---@return void
function HelloWorld.ofunc(t) end
---@public
---@param go GameObject
---@return void
function HelloWorld.ofunc(go) end
---@public
---@param a Int32
---@return void
function HelloWorld.AFunc(a) end
---@public
---@param a Single
---@return void
function HelloWorld.AFunc(a) end
---@public
---@param a string
---@return void
function HelloWorld.AFunc(a) end
---@public
---@param a number
---@return void
function HelloWorld.AFunc(a) end
---@public
---@return void
function HelloWorld:perf() end
---@public
---@param v Vector3
---@return void
function HelloWorld.testvec3(v) end
---@public
---@param go GameObject
---@return void
function HelloWorld.testset(go) end
---@public
---@param go GameObject
---@return void
function HelloWorld.test2(go) end
---@public
---@param go GameObject
---@return void
function HelloWorld.test3(go) end
---@public
---@param go GameObject
---@return void
function HelloWorld.test4(go) end
---@public
---@param go GameObject
---@return Vector3
function HelloWorld.test5(go) end
---@public
---@param str string
---@param args Object[]
---@return void
function HelloWorld.func6(str, args) end
---@public
---@param func LuaFunction
---@return void
function HelloWorld:func7(func) end
---@public
---@param a Int32
---@return void
function HelloWorld:func7(a) end
---@public
---@param result List`1
---@return void
function HelloWorld:func8(result) end
---@public
---@return void
function HelloWorld.byteArrayTest() end
---@public
---@param arr Transform[]
---@return void
function HelloWorld.transformArray(arr) end
---@public
---@param objs Object[]
---@return void
function HelloWorld.setObjs(objs) end
.HelloWorld = HelloWorld