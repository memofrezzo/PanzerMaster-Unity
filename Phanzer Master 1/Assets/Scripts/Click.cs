using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 0 es el botón izquierdo del mouse
        {
            SceneManager.LoadScene("LvlScene");
        }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
    }
}
