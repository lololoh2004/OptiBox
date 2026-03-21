using System.Collections.Generic;
using NLua;

public class HookManager
{
    private Dictionary<string, Dictionary<string, LuaFunction>> _hooks = new Dictionary<string, Dictionary<string, LuaFunction>>();

    public void Add(string hook,string id, object luaFuncObj)
    {
        if (!_hooks.ContainsKey(hook))
        { _hooks[hook] = new Dictionary<string, LuaFunction>(); }
        _hooks[hook][id] = luaFuncObj as LuaFunction;
    }
    public void Call(string hook, params object[] args)
    {
        if (!_hooks.ContainsKey(hook))
        { return; }
        foreach (LuaFunction luaFunc in _hooks[hook].Values)
        { luaFunc.Call(args); }
    }
}
