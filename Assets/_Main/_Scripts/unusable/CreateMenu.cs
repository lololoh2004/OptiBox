using UnityEngine;
using UnityEngine.UI;

public class CreateMenu : MonoBehaviour
{
    [SerializeField] GameObject menuBG;
    [SerializeField] Transform menuTransform;
    [SerializeField] GameObject[] obj;
    [SerializeField] Sprite[] sprite;
    [SerializeField] int menuWidth = 1;
    
    int line = 1;
    int buttonCount = 0;

    public void Spawn(GameObject spawnObj)
    {
        print("GoFuckYourself");
    }

    void Open_Menu()
    {
        for(int i = 0; i < obj.Length; i++)
        {
            for (int j = 0; j < menuWidth; j++)
            {
                GameObject newInstantiate = Instantiate(obj[j], menuTransform);
                newInstantiate.transform.localPosition = new Vector3(-45 + (45 * j), 60 + 30 * -line, 0);
                Image newInstantiateIcon = newInstantiate.GetComponent<Image>();
                
                newInstantiateIcon.sprite = sprite[buttonCount];
                buttonCount++;
            }
            line++;
            buttonCount++;
        }
        
    }

    void Start()
    {
        Open_Menu();
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            return;
        }
    }
}
