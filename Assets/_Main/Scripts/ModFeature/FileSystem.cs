using UnityEngine;
using System.IO;
public static class FileSystem 
{
    public static string GetRoot() 
    { return Path.Combine(Application.streamingAssetsPath, "src_filesystem", "content"); }
    
    public static string GetExternal()
    {return Path.Combine(Application.persistentDataPath, "UserScripts");}
    
    //public static string GetLuaPath(string path) 
    //{ return Path.Combine(GetRoot(), "lua", path); }
}