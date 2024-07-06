using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneOnInput : MonoBehaviour
{
    // El nombre de la escena a la que quieres cambiar, o "Exit" para salir del juego
    public string targetScene;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    // Método que se llamará cuando el objeto es clickeado
    void OnMouseDown()
        {
            if (targetScene == "Exit")
            {
                // Salir del juego
                Application.Quit();
            }
            else
            {
                // Cambiar a la escena especificada
                SceneManager.LoadScene(targetScene);
            }
        }
   
}