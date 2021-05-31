using UnityEngine;
using System.Collections;
public class GuiController : MonoBehaviour
{
    //M�todo utilizado pelos Bot�es da cena para indicar qual fase deve ser carregada.
    public void GoToScene(string sceneName)
    {
        //Utiliza o m�todo da classe SceneController para carregar a nova scene.
        //Note que estamos acessando a classe de uma forma st�tica, isso ocorre por conta da heran�a com a classe Singleton.
        SceneController.getInstance().LoadScene(sceneName);
    }
}