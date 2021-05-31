using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
//Herda a classe Singleton para indicar que deve existir somente um desse objeto em todo o jogo.
public class SceneController : Singleton<SceneController>
{
    //Usado para indicar que come�ou a fazer a transi��o entre as scenes.
    private bool isLoading;
    //Usado para indicar que deve ser iniciada a transi��o entre as scenes.
    private bool startLoading;
    //Usado para guarda o nome da scene de destino.
    private string targetSceneName;
    //Usado para indicar o tempo m�nimo que a tela de loading deve se manter ativa.
    //Caso o valor seja 0 o tempo m�nimo ser� o tempo de carregamento da nova cena.
    private float minLoadingTime;
    void Start()
    {
        //Usado para ativar o m�todo que est� na classe Singleton.
        if (base.SingletonStart())
        {
            //Inicializa as v�riaveis
            startLoading = false;
            isLoading = false;
            targetSceneName = "";
            minLoadingTime = 1;
        }
    }
    void Update()
    {
        // Come�a a carregar a nova scene caso esteja indicado para carregar e ainda n�o esteja carregando.
        if (startLoading && !isLoading)
            StartCoroutine(StartLoadScene());
    }
    //Retorna um IEnumerator para ser utilizado no StartCoroutine, isso serve para que seja poss�vel parar a execu��o
    //somente deste m�todo durante um determinado per�odo de tempo e voltar da onde parou. Como pode ser visto mais adiante.
    public IEnumerator StartLoadScene()
    {
        //Indica que come�ou a carregar e com isso evita de ficar chamando esse m�todo repetidamente
        isLoading = true;

        //Ativa a Trigger "show" do objeto FadeImage indicando que a anima��o "FadeIn" deve ser iniciada
        GameObject.Find("FadeImage").GetComponent<Animator>().SetTrigger("show");
        //Interrompe a execu��o at� o final do frame, isso � feito para que d� tempo para que a anima��o seja trocada.
        //antes de executar o resto do c�digo.
        //yield return new WaitForEndOfFrame();
        //Se estiver rodando a anima��o FadeIn indica para esperar a quantidade de segundos que a anima��o dura.
        if (GameObject.Find("FadeImage").GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("FadeIn"))
            yield return new WaitForSeconds(GameObject.Find("FadeImage").GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
        //Come�a a carregar a cena de loading de forma ass�ncrona, com isso a execu��o do jogo n�o fica travada.
        //AsyncOperation op = SceneManager.LoadSceneAsync("LoadingScene");
        //Espera at� que a cena de loading termine de carregar. OBS: A cena de loading deve ser leve para que seja
        //carregada r�pida.
       // while (!op.isDone)
         //   yield return new WaitForEndOfFrame();

        //Utilizado para garantir que a tela de load vai ficar ativa durante o tempo m�nimo que foi definido.
        float timeLoading = Time.time + minLoadingTime;
        //Come�a a carregar a cena indicada como destino de forma ass�ncrona.
        AsyncOperation op = SceneManager.LoadSceneAsync(targetSceneName);
        //Utilizado para que n�o ative a cena automaticamente ap�s terminar de carregar, isso � feito para que possamos
        //mostrar a barra de progresso e porcentagem do carregamento.
        op.allowSceneActivation = false;
        //Enquanto n�o terminou de carregar, repete esses comandos frame a frame.
       // while (!op.isDone)
        //{
            //Usado para calcular e guardar a porcentagem de carregamento da nova scene.
            //Obs: o op.progress retorna um valor de 0 at� 1 indicando a porcentagem de carregamento e a ativa��o da scene,
            //por�m quando utilizamos o op.allowSceneActivation = false. Ele carrega a scene mas n�o chega ativar.
            //Nesse caso quando ele retorna o valor 0.9 indica que a cena foi totalmente carregada e que devemos ativar ela.
            //Por isso o valor � dividido por 0.9 para determinar a porcentagem.
          //  float percent = (op.progress / 0.9f) * 100;
            //Atualiza os valores na scene de Loading para mostrar a porcentagem em forma de texto e preencher a barra.
          //  GameObject.Find("ProgessBar").GetComponent<Slider>().value = percent;
          //  GameObject.Find("Percent").GetComponent<Text>().text = String.Format("{0:0}", percent) + "%";
          //Quando a porcetagem chegar a 100 para de repetir.
          //  if (percent == 100)
          //      break;
            //Usado para esperar at� o outro fram
            //e antes de repetir os comandos.
          //  yield return new WaitForEndOfFrame();
       // }
        //Caso tenha carregado a cena mais r�pido que o tempo m�nimo que a tela de Loading deve ficar ativa
        //espera at� passar o tempo desejado.
        while (Time.time < timeLoading)
            yield return new WaitForEndOfFrame();
        //A scene j� foi totalmente carregada e aqui indica para o Unity ativar ela.
        op.allowSceneActivation = true;
        //Reseta as vari�veis que controla se deve ou n�o carregar uma nova scene, isso � feito para que possamos chamar este
        //m�todo novamente.
        startLoading = false;
        isLoading = false;
        targetSceneName = "";
    }
    //Esse m�todo ser� chamado por outros scripts para indicar que deve carregar uma nova scene.
    public void LoadScene(string targetSceneName)
    {
        //Indica o nome da scene de destino e indica que deve iniciar a transi��o das telas.
        this.targetSceneName = targetSceneName;
        startLoading = true;
    }
}