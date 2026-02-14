using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelManagement : MonoBehaviour
{
    [SerializeField] Animator animator;
    public void OnMainButtonCLick(string name)
    {
        if (name == "Play") { animator.SetTrigger("Start"); }
    }
    public void LoadScene(string sceneName) { SceneManager.LoadScene(sceneName); }
}
