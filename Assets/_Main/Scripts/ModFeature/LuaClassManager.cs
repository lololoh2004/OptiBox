using UnityEngine;

public class LuaSystem
{
    public string GetOS() { return SystemInfo.operatingSystem; }
    public int GetRAM() { return SystemInfo.systemMemorySize; }
}

public class LuaRender
{ public string GetRenderer() { return SystemInfo.graphicsDeviceName; } }
public class LuaObject
{
    protected GameObject _obj;
    //protected Transform _transform;
    
    public bool IsValid()
    { return _obj != null; }
    
    public void SetPos(float x, float y, float z) 
    { if (_obj != null) { _obj.transform.position = new Vector3(x, y, z); } }

    public Vector3 GetPos()
    {
        if (_obj == null) { return Vector3.zero; }
        return _obj.transform.position;
    }

    public void SetAngles(float p, float y, float r)
    {
        if (_obj == null) { return; }
        _obj.transform.localEulerAngles = new Vector3(p, y, r);
    }
    
    public Vector3 GetForward() 
    { return _obj.transform.forward; }
    
    public Vector3 GetRight() 
    { return _obj.transform.right; }
    
    public Vector3 GetUp() 
    { return _obj.transform.up; }
}

public class LuaEntity : LuaObject
{
    public LuaEntity(GameObject obj) 
    { _obj = obj; }

    public Rigidbody GetPhysicsObject()
    { return _obj.GetComponent<Rigidbody>(); }
    
    public void ApplyForceCenter(float x, float y, float z)
    {
        if (_obj == null) { return; }
        Rigidbody rb = _obj.GetComponent<Rigidbody>();
        rb?.AddForce(new Vector3(x, y, z), ForceMode.Impulse);
    }

    public void SetVelocity(float x, float y, float z)
    {
        if (_obj == null) { return; }
        Rigidbody rb = _obj.GetComponent<Rigidbody>();
        if (rb == null) { return; }
        rb.velocity = new Vector3(x,y,z);
    }
}
