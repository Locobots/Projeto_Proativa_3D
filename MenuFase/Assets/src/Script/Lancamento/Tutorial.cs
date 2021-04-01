﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
  public GameObject arrow;
  public GameObject menuButton, startButton, restoreButton, nextButton;
  public GameObject velocitySlider, cameraSlider, angleSlider;

  public int index;
  public bool isTutorial = false;

  private dialog robotDialog;

  void Start()
  {
    robotDialog = FindObjectOfType<dialog>();
    arrow.SetActive(false);
  }

  public void StartTutorial()
  {
    isTutorial = true;
    index = -1;
    arrow.SetActive(true);
  }

  void Update()
  {
    if (isTutorial)
    {
      if (index == -1)
      {
        Button button = menuButton.GetComponent<Button>();
        button.onClick.AddListener(delegate { this.FirstPressButtonMenu(); });

        UpdateArrowPosition(menuButton, 0.90f);
        index++;
      }
      else if (index == 1)
      {

        Button button1 = menuButton.GetComponent<Button>();
        Button button2 = startButton.GetComponent<Button>();
        Button button3 = restoreButton.GetComponent<Button>();

        button1.interactable = false;
        button2.interactable = false;
        button3.interactable = false;

        SliderCameraAvaliable();
      }
      else if (index == 2)
      {
        SliderAngleAvaliable();
      }
      else if (index == 3)
      {
        SliderVelocityAvaliable();
      }
    }
  }

  void UpdateArrowPosition(GameObject gameObject, float corretionFactor)
  {
    RectTransform transform = gameObject.GetComponent<RectTransform>();
    RectTransform arrowTransform = arrow.GetComponent<RectTransform>();

    arrowTransform.position = new Vector3(
      transform.position.x * corretionFactor,
      transform.position.y,
      transform.position.z
    );
  }

  public void FirstPressButtonMenu()
  {
    Button button = menuButton.GetComponent<Button>();
    button.onClick.RemoveListener(delegate { this.FirstPressButtonMenu(); });

    index++;
    robotDialog.StopTalk();

    UpdateArrowPosition(cameraSlider, 0.76f);
  }

  void SliderCameraAvaliable()
  {
    Slider vSlider = velocitySlider.GetComponent<Slider>();
    Slider aSlider = angleSlider.GetComponent<Slider>();
    Slider cSlider = cameraSlider.GetComponent<Slider>();

    vSlider.interactable = false;
    aSlider.interactable = false;


    if (cSlider.value >= 1)
    {
      index++;
      UpdateArrowPosition(angleSlider, 0.76f);
    }
  }

  void SliderAngleAvaliable()
  {
    Slider aSlider = angleSlider.GetComponent<Slider>();

    aSlider.interactable = true;

    if (aSlider.value > 5)
    {
      index++;
      UpdateArrowPosition(velocitySlider, 0.76f);
    }
  }

  void SliderVelocityAvaliable()
  {
    Slider velocity = velocitySlider.GetComponent<Slider>();
    velocity.interactable = true;

    if (velocity.value > 5)
    {
      index++;

      Button button1 = startButton.GetComponent<Button>();
      Button button2 = menuButton.GetComponent<Button>();
      Button button3 = startButton.GetComponent<Button>();
      Button button4 = restoreButton.GetComponent<Button>();

      button1.onClick.AddListener(delegate { this.PressButtonStart(); });

      UpdateArrowPosition(startButton, 0.76f);
      button2.interactable = true;
      button3.interactable = true;
      button4.interactable = true;
    }
  }

  public void PressButtonStart()
  {
    Button button = startButton.GetComponent<Button>();
    button.onClick.RemoveListener(delegate { this.PressButtonStart(); });

    index++;

    button = menuButton.GetComponent<Button>();
    button.onClick.AddListener(delegate { this.SecondPressButtonMenu(); });

    UpdateArrowPosition(menuButton, 0.90f);
  }

  void SecondPressButtonMenu()
  {
    Button button = menuButton.GetComponent<Button>();
    button.onClick.RemoveListener(delegate { this.SecondPressButtonMenu(); });

    index++;

    button = restoreButton.GetComponent<Button>();
    button.onClick.AddListener(delegate { this.PressButtonRestore(); });

    UpdateArrowPosition(restoreButton, 0.85f);
  }

  public void PressButtonRestore()
  {
    Button button = restoreButton.GetComponent<Button>();
    button.onClick.RemoveListener(delegate { this.PressButtonRestore(); });

    isTutorial = false;
    robotDialog.Talk();
    index++;

    button = nextButton.GetComponent<Button>();
    button.onClick.AddListener(delegate { this.PressNextIndex(); });
    robotDialog.Skip();
    UpdateArrowPosition(nextButton, 0.92f);
  }

  public void PressNextIndex()
  {
    Button button = nextButton.GetComponent<Button>();
    button.onClick.RemoveListener(delegate { this.PressNextIndex(); });
    
    arrow.SetActive(false);
    isTutorial = false;
  }
}
