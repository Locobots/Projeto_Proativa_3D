using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMusic : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSourceTutorial;
    [SerializeField] private AudioSource _audioSourceSecondary;

    [SerializeField] private Animator _animator;

    public static ChangeMusic Instancia;

    private void Awake()
    {
        Instancia = this;
    }

    public void TrocarParaMusicaSecundaria()
    {
        //Coloca a m�sica no audio source secund�rio
        //_audioSourceSecondary.clip = musica;

        //Pega o tempo da m�sica principal
        _audioSourceSecondary.time = _audioSourceTutorial.time;

        //Toca a m�sica secund�ria
        _audioSourceSecondary.Play();

        //Troca para a m�sica secund�ria
        _animator.Play("secondary_musicEnter");

        //Chama Coroutine para parar a m�sica principal
        StartCoroutine(PararAudioSource(_audioSourceTutorial));
    }

   

    private IEnumerator PararAudioSource(AudioSource audioSource)
    {
        //Espera 1 segundo para esperar acabar a transi��o
        yield return new WaitForSeconds(2.5f);
        audioSource.Stop();
    }
}
