using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiMenu : MonoBehaviour
{
    public List<GameObject> CollectedObjects = new List<GameObject>();
    public List<GameObject> AllObjects = new List<GameObject>();
    public List<GameObject> remObjects = new List<GameObject>();

    public GameObject questionPanel;  
    public GameObject interactionPanel;
    public GameObject finalPanel;
    public GameObject pausePanel;
    public GameObject crosshair;
    public GameObject player;
    
    private GameObject ColObject;

    public int quizScore = 1;
    public int objIndex = 0;

    public List<int> Score = new List<int>();

    public TextMeshProUGUI q;
    public TextMeshProUGUI a1;
    public TextMeshProUGUI a2;
    public TextMeshProUGUI a3;
    public TextMeshProUGUI popupText;
    public TextMeshProUGUI finalText;
    public TextMeshProUGUI finalbuttonText;

    public Button finalbutton;

    public Image popupImage;

    public bool panelOpen;

    public void showQpanel(bool state)
    {
        lockPlayer(state);

        if (state) 
        { 
            q.text = CollectedObjects[objIndex].GetComponent<Interact>().questionText;
            a1.text = CollectedObjects[objIndex].GetComponent<Interact>().answerText1;
            a2.text = CollectedObjects[objIndex].GetComponent<Interact>().answerText2;
            a3.text = CollectedObjects[objIndex].GetComponent<Interact>().answerText3;
        }
        questionPanel.SetActive(state);
        panelOpen = state;
    }

    public void showIPanel(GameObject obj)
    {
        crosshair.SetActive(false);
        popupText.text = obj.GetComponent<Interact>().objectText;
        popupImage.sprite = obj.GetComponent<Interact>().objectImage;
        ColObject = obj;
        lockPlayer(true);
        interactionPanel.SetActive(true);
        panelOpen = true;
    }

    public void AnswerButton(int selectedButton)
    {
        if (selectedButton == CollectedObjects[objIndex].GetComponent<Interact>().correctAnswer) quizScore += 3;
        if (objIndex == 2)
        {
            showQpanel(false);
            foreach (var item in CollectedObjects) remObjects.Remove(item);
            CollectedObjects.Clear();
            Score.Add(quizScore);
            quizScore = 1;
            objIndex = 0;

            if(remObjects.Count == 0)
            {
                panelOpen = true;
                lockPlayer(true);
                var avg = Score.Average();                
                finalText.text = "Your score was: "+ avg;
                if (avg < 5)
                {
                    finalText.text += " and it was too low \n You must retry the level.";
                    finalbuttonText.text = "Retry!";
                    finalbutton.onClick.AddListener(delegate { changeGame(SceneManager.GetActiveScene().name); }); ;
                }
                else
                {
                    finalText.text += " Congratulations!";
                    finalbuttonText.text = "Continue";
                    finalbutton.onClick.AddListener(delegate { changeGame("1. Main Menu");  } );;
                }

                finalPanel.SetActive(true);
            }
        }
        else
        {
            objIndex++;
            showQpanel(true);
        }    
    }

    public void CloseInteraction()
    {
        interactionPanel.SetActive(false);
        lockPlayer(false);
        CollectedObjects.Add(ColObject.gameObject);
        panelOpen = false;
        if (CollectedObjects.Count == 3)
        {
            objIndex = 0;
            showQpanel(true);
        }
    }
    private void lockPlayer(bool state)
    {     
        CameraMan.moveCamera = !state;
        crosshair.SetActive(!state);
        player.gameObject.GetComponent<PlayerMovement>().enabled = !state;
        if(!state) Cursor.lockState = CursorLockMode.Locked;
        else Cursor.lockState = CursorLockMode.None;
        Cursor.visible = state;
    }

    private void Start()
    {  
        lockPlayer(false);
        questionPanel.SetActive(false);
        interactionPanel.SetActive(false);
        pausePanel.SetActive(false);
        finalPanel.SetActive(false);

        foreach (var item in GameObject.FindGameObjectsWithTag("Collectable"))
        {
            AllObjects.Add(item);
            remObjects.Add(item);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !panelOpen)
        {
            lockPlayer(!pausePanel.activeSelf);
            pausePanel.SetActive(!pausePanel.activeSelf);
        }
    }

    public void changeGame(string scene)
    {
        SceneManager.LoadScene(scene);
    }

}
