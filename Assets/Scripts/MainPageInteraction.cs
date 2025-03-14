using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events; 
public class MainPageInteraction: MonoBehaviour
{

	public UnityEvent onClickEvent;

    void OnMouseDown()
    {
		onClickEvent.Invoke(); 
    }
}