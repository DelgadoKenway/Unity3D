using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class Interact : MonoBehaviour
{
    public Text collectText;

    public UiMenu ui;

    public GameObject player;

    private const float intDistance = 2f;

    private bool collected;

    public Sprite objectImage;

    public string objectText;
    public string questionText;
    public string answerText1;
    public string answerText2;
    public string answerText3;

    public int correctAnswer;

    private void OnMouseExit()
    {
        collectText.enabled = false;
    }
    private void OnMouseOver()
    {
        float dist = Vector3.Distance(player.transform.position, transform.position);

        if (dist < intDistance && !collected)
        {
            collectText.enabled = true;

            if (Input.GetKeyDown(KeyCode.E))
            {
                collectText.enabled = false;
                this.gameObject.GetComponent<ParticleSystem>().Stop();
                collected = true;
                ui.showIPanel(this.gameObject);
            }
        }
        else
        { 
            collectText.enabled = false;
        }
    }
}
