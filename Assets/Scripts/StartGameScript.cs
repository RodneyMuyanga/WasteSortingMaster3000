using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameScript : MonoBehaviour
{
    void OnMouseDown()
    {
        SceneManager.LoadScene("MemeoryLeakFixed+Prefabs"); 
    }
}