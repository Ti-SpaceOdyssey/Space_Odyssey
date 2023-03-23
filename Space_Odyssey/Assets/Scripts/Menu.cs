using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    public void LoadScene(int indexScene)
    {
        SceneManager.LoadScene(indexScene);
    }

    public void SairJogo(){
        Application.Quit();
    }
}
