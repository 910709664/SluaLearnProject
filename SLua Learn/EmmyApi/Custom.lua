---@class Custom
---@field public Item Int32
local Custom={ }
---@public
---@param l IntPtr
---@return Int32
function Custom.instanceCustom(l) end
---@public
---@param l IntPtr
---@return Int32
function Custom.staticCustom(l) end
---@public
---@param t Type
---@return string
function Custom:getTypeName(t) end
---@public
---@return IFoo
function Custom:getInterface() end
.Custom = Custom