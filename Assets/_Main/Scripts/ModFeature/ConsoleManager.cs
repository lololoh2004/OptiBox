using TMPro;
using UnityEngine;

public class ConsoleManager : MonoBehaviour
{
    [SerializeField] TMP_Dropdown consoleMode;
    [SerializeField] private GameObject[] consoleWindows;
    private void Start()
    { ChangeWindow(); }
    
    public void ChangeWindow()
    { HideAll(); consoleWindows[consoleMode.value].gameObject.SetActive(true); }
    
    private void HideAll()
    {
        foreach (GameObject window in consoleWindows)
        { window.gameObject.SetActive(false); }
    }
}
