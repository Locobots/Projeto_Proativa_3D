using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

//Singleton<T> o T � uma esp�cie de template que ser� definido pelas outras classes/scripts que herdarem
//essa classe. Para entender melhor como usar esse T basta olhar a classe SceneController.
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    //Usado para armazenar a �nica inst�ncia que deve existir desse objeto no jogo.
    private static T instance;

    //Usado para coletar a inst�ncia dessa classe e conseguir utilizar os m�todos.
    public static T getInstance()
    {
        if (instance == null)
            instance = (T)FindObjectOfType(typeof(T));

        return instance;
    }

    //Se for sobrescrever este m�todo em alguma classe filha, lembrar de utilizar o comando base.SingletonStart();
    void Start()
    {
        SingletonStart();
    }

    //Usado para manter somente uma inst�ncia do objeto que utiliza essa classe. Caso seja carregada 
    //uma scene em que esse objeto j� exista, o m�todo ir� destruir o objeto da scene e manter o mais antigo.
    public bool SingletonStart()
    {
        //Caso a vari�vel instance j� esteja preenchida e o gameobject for diferente do que est� sendo executado, indica que esse � um novo objeto que deve ser destru�do,
        //caso n�o esteja preenchido indica que � o primeiro objeto e ele deve ser mantido em todas as scenes.
        if (instance && !instance.gameObject.Equals(gameObject))
        {
            Destroy(gameObject);

            //Retorna falso indicando que esse objeto foi destru�do e que as vari�veis n�o devem ser inicializadas.
            return false;
        }
        else
        {
            //Usado para indicar que esse gameo bject n�o deve ser destru�do ao fazer as transi�oes da tela.
            DontDestroyOnLoad(gameObject);


            //Caso a instance esteja preenchida n�o � preciso localizar ela novamente, isso pode acontecer caso algum script
            //chame a fun��o getInstance antes do unity executar o start da classe que est� herdando a Singleton. Isso pode acontecer
            //caso voc� utilize o valor de uma classe Singleton na fun��o Start de uma outra classe. Caso voc� opte por utilizar algum m�todo de uma classe singleton
            //dentro do Start de uma outra classe, voc� deve utilizar somente valores da classe singleton que voc� preencher via inspector.
            if (instance == null)
                //Usado para encontrar o objeto do tipo do template na scene. 
                //Deve existir somente um desse objeto na scene, e n�o ser� preciso existir ele em nenhuma scene posterior
                //pois esse objeto ser� mantido.
                instance = (T)FindObjectOfType(typeof(T));

            //Retorna verdadeiro indicando que esse objeto n�o foi destru�do e que as vari�veis devem ser inicializadas.
            return true;
        }
    }
}
