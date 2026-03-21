using System.IO;
using NLua;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Main.Scripts.ModFeature
{
    public class ModLoader : MonoBehaviour
    {
        [Space(10)]
        [Header("= Moduleleles =")]
        [SerializeField] private FixedJoystick moveJoystick;
        [SerializeField] private ControlManager lookArea;
    
        [Space(10)]
        [Header("= Obj =")]
        [SerializeField] TMP_InputField codeField;
        [SerializeField] TMP_InputField nameField;
        [SerializeField] Transform consoleContent;
        [SerializeField] private GameObject goPlaceholder;
        [SerializeField] private GameObject ConsoleMsgPrefab;
    
        private Lua _lua;
        private LuaFunction _thinkFunc;
        private LuaFunction _physThinkFunc;
        private LuaFunction _slowThinkFunc;

        private HookManager _hookManager;

        void Start()
        {
            AutoRun();               // ITS TEMPORARY THING FOR EASY DEBUG
        }

        private void Awake()
        {
            _lua = new Lua();
            _hookManager = new HookManager();
            _lua.LoadCLRPackage();

            _lua.DoString("hook = {}");
            _lua["hook"] = _hookManager; 
            //_lua.RegisterFunction("hook.Add", _hookManager, typeof(HookManager).GetMethod("Add"));
            
            // Registration //
            _lua.RegisterFunction("Entity", this, typeof(ModLoader).GetMethod("GetEntity"));
            _lua.RegisterFunction("print", this, typeof(ModLoader).GetMethod("Print"));
            _lua.RegisterFunction("FrameTime", this, typeof(ModLoader).GetMethod("FrameTime"));
            _lua.RegisterFunction("TickInterval", this, typeof(ModLoader).GetMethod("TickInterval"));
            _lua.RegisterFunction("GetMouseX", this, typeof(ModLoader).GetMethod("GetMouseX"));
            _lua.RegisterFunction("GetMouseY", this, typeof(ModLoader).GetMethod("GetMouseY"));
            _lua.RegisterFunction("GetAxisX", this, typeof(ModLoader).GetMethod("GetAxisX"));
            _lua.RegisterFunction("GetAxisY", this, typeof(ModLoader).GetMethod("GetAxisY"));
            _lua.RegisterFunction("UnityPrint", this, typeof(ModLoader).GetMethod("UnityPrint"));
            _lua.RegisterFunction("GetVersion", this, typeof(ModLoader).GetMethod("GetVersion"));
            _lua.RegisterFunction("GetRender", this, typeof(ModLoader).GetMethod("GetRender"));
            _lua.RegisterFunction("GetSystem", this, typeof(ModLoader).GetMethod("GetSystem"));
            _lua.RegisterFunction("GetEntity", this, typeof(ModLoader).GetMethod("GetEntity"));
            _lua.RegisterFunction("include", this, typeof(ModLoader).GetMethod("Include"));
            
            // Small details
            _lua["Time"] = typeof(Time);
            _lua["SystemInfo"] = typeof(SystemInfo);
            _lua["Screen"] = typeof(Screen);
            _lua["Application"] = typeof(Application);
            _lua["SceneManager"] = typeof(SceneManager);
        }

        public void RunLuaCode()
        {
            try
            {
                _lua.DoString(codeField.text);
                //_thinkFunc = _lua["Think"] as LuaFunction;
            }
            catch (System.Exception e)
            { Debug.LogError("Что-то мать его создает скриптовые ошибки" + System.Environment.NewLine + e.Message); }
        }
    
    
        // GLua hooks //

        public void Update() // I KNOW about this problem
        {
            if (_hookManager == null) return;
            _hookManager.Call("Think");
        }

        
        // GLua func. //

        public LuaEntity GetEntity(string name) 
        {
            GameObject obj = GameObject.Find(name);
            if (obj == null) return null;
            return new LuaEntity(obj); 
        }

        public LuaRender GetRender()
        { return new LuaRender(); }

        public LuaSystem GetSystem()
        { return new LuaSystem(); }
        
        public void Print(object obj)      // This func. is real shi // BUT its beautiful lol :)
        {
            //string colorTag = "#ffffff";
            string text = obj.ToString();
            
            if (text.ToLower().Contains("error") || text.ToLower().Contains("exception"))
            { text = $"<b><color=#ffcc00>[!] Что-то мать его создает скриптовые ошибки:</color></b>\n<size=80%>{text}</size>"; }
            else 
            { text = $"<color=#57bbff>[LUA]</color> {text}"; }
            
            GameObject ConsoleMsg = Instantiate(ConsoleMsgPrefab, consoleContent);
            TMP_Text tmpText = ConsoleMsg.GetComponent<TMP_Text>();
            
            tmpText.text = text;
        }
        
        public double FrameTime()
        { return (double)UnityEngine.Time.deltaTime; }
        public double TickInterval()
        { return (double)UnityEngine.Time.fixedDeltaTime; }
        
        public float GetMouseX()
        { float val = lookArea.deltaX; lookArea.deltaX = 0; return val; }
        public float GetMouseY()
        { float val = lookArea.deltaY; lookArea.deltaY = 0; return val; }

        public void Include(string path)
        {
            path = System.IO.Path.Combine(FileSystem.GetExternal(), path);

            if (System.IO.File.Exists(path))
            { _lua.DoFile(path); }
            else
            { Print("error: File not found at this location");}
        }
        
        // Additional func. // (This wasnt in gmod or it didnt match)
        
        public void UnityPrint(object msg)
        { Debug.Log("NLua: " + msg.ToString()); }
        
        public float GetAxisX()
        { return moveJoystick.Horizontal; }
    
        public float GetAxisY()
        { return moveJoystick.Vertical; }
        
        public string GetVersion() 
        { return Application.version; }
        
        // ModLoader func. //

        public void SaveScript()
        {
            string path = FileSystem.GetExternal();
            path = System.IO.Path.Combine(path, nameField.text + ".lua");
            System.IO.File.WriteAllText(path, codeField.text);
            
            Debug.Log(path);
        }

        public void AutoRun()
        {
            string path = System.IO.Path.Combine(FileSystem.GetExternal(), ".autorun");
            string[] files = Directory.GetFiles(path, "*.lua");

            for (int i = 0; i < files.Length; i++)
            {
                try
                { _lua.DoFile(files[i]); }
                catch (System.Exception e)
                { Print("PLACEHOLDER " + Path.GetFileName(files[i]) + ": " + e.Message); }
            }
        }
    }
    
}