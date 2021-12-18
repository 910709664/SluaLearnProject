---@class Deleg
---@field public d GetBundleInfoDelegate
---@field public s SimpleDelegate
---@field public daction Action`2
---@field public dx GetBundleInfoDelegate
local Deleg={ }
---@public
---@return void
function Deleg.callD() end
---@public
---@param a Action`1
---@param b Action`1
---@return void
function Deleg.setcallback2(a, b) end
---@public
---@param f Func`1
---@return void
function Deleg.testFunc(f) end
---@public
---@param f Action`2
---@return void
function Deleg.testAction(f) end
---@public
---@param f Action`2
---@return void
function Deleg.testDAction(f) end
---@public
---@return void
function Deleg.callDAction() end
---@public
---@param f Func`3
---@return Func`3
function Deleg.getFunc(f) end
.Deleg = Deleg