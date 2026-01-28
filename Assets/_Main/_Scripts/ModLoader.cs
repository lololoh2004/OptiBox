using UnityEngine;
using NLua;
using TMPro;

public class ModLoader : MonoBehaviour
{
    [Space(10)]
    [Header("= Main cfg =")]
    [SerializeField] TMP_InputField inputField;
    private Lua lua;

    void Start()
    {
        lua = new Lua();
        lua.LoadCLRPackage(); // For Acces to Files and Scripts

        lua["this"] = this;
    }

    public void RunLuaCode()
    {
        try
        {
            Debug.Log("Lua code is running!");
            lua.DoString(inputField.text);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Что-то создаёт скриптовые ошибки" + System.Environment.NewLine + e.Message);
        }
    }
    public void UnityLog(string message)
    {
        Debug.Log("Lua: " + message);
    }
}