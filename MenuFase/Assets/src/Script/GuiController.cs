using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class GuiController : MonoBehaviour
{
    //M�todo utilizado pelos Bot�es da cena para indicar qual fase deve ser carregada.
    //public string sceneName;
    public void GoToScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }
}