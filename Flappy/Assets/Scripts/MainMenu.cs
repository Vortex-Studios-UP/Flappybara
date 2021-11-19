using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    
    //NOTA IMPORTANTE: Tener las escenas en "Build Settings" para que funcione
    public void  GameScene()
    {
        SceneManager.LoadScene("CollisionTest");//Poner el nombre textual de la escena que buscamos
    }

    public void CargarEscena(string Escena)
    {
        SceneManager.LoadScene(Escena);//Escribiendo en el inspector que escena queremos usar
        //NOTA IMPORTANTE: Solo para uso de desarrollador
    }
}
