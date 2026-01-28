using UnityEngine;
public class EditMenu : MonoBehaviour
{
    //[SerializeField] bool json = false;
    [SerializeField] GameObject editorMenu;
    bool opened = false;

    private void Start(){ editorMenu.SetActive(opened); }
    public void OpenMenu()
    {
        opened = !opened;
        editorMenu.SetActive(opened);
    }
}
