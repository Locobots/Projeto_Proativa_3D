﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class position : MonoBehaviour
{
  public Transform personagem;
  private Transform ObjectCamera;

  private float oldPositionX;
  private float sliderOption;
  
  void Start()
  {
    ObjectCamera = GetComponent<Transform>();

    SliderAux(1);
    sliderOption = 1;
    oldPositionX = personagem.position.x;
  }

  void Update()
  {
    if (oldPositionX != personagem.position.x)
    {
      SliderAux(sliderOption);
      oldPositionX = personagem.position.x;
    }
  }

  public void SliderAux(float parametro)
  {
    sliderOption = parametro;

    switch (parametro)
    {
      case 0:
        ObjectCamera.eulerAngles = new Vector3(-4, 90, 0);

        ObjectCamera.position = new Vector3(
          personagem.position.x,
          personagem.position.y + 1,
          personagem.position.z
        );
        break;

      case 1:
        ObjectCamera.eulerAngles = new Vector3(30, 90, 0);

        ObjectCamera.position = new Vector3(
          personagem.position.x - 10,
          personagem.position.y + 10,
          personagem.position.z
        );
        break;

      case 2:
        ObjectCamera.eulerAngles = new Vector3(10, 0, 0);

        ObjectCamera.position = new Vector3(
          personagem.position.x,
          personagem.position.y + 10,
          personagem.position.z - 20
        );
        break;
    }
  }
}
