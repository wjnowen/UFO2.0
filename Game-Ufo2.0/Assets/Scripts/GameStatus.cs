﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
  
public class GameStatus : MonoBehaviour
{
    public GameObject myCanvas, roundText, scoreText, roundChangeMessage;
    private int round = 1;
    private int score = 0;
    private int trial = 0;
    private bool start = false;

    private GameObject canvasObj, roundObj, scoreObj, roundChangeMessageObj;

    public int getRound()
    {
        return round;
    }

    private void setRound(int num)
    {
        round = num;
        roundObj.GetComponent<Text>().text = "Round: " + round;
    }

    public bool canStart()
    {
        return start;
    }

    public void addTrial()
    {
        trial++;
        if (trial >= 10)
        {
            StartCoroutine(displayRound());
            trial = 0;
            setRound(getRound()+1);
        }
    }
    
    public int getScore()
    {
        return score;
    }

    public void setScore(int num)
    {
        score = num;
        scoreObj.GetComponent<Text>().text = "Score: " + score;
    }
    

    void Start()
    {
        SSDirector.getInstance().setGameStatus(this);

        canvasObj = Instantiate(myCanvas);
        canvasObj.transform.position = new Vector3(0, 0, 0);
        roundObj = Instantiate(roundText, canvasObj.transform);
        roundObj.transform.Translate(canvasObj.transform.position);
        roundObj.GetComponent<Text>().text = "Round: " + round;

        scoreObj = Instantiate(scoreText, canvasObj.transform);
        scoreObj.transform.Translate(canvasObj.transform.position);
        scoreObj.GetComponent<Text>().text = "Score: " + score;

        roundChangeMessageObj = Instantiate(roundChangeMessage, canvasObj.transform);
        roundChangeMessageObj.transform.Translate(canvasObj.transform.position);
        roundChangeMessageObj.GetComponent<Text>().text = "";

        StartCoroutine(displayRound());
    }

    void Update() {}

    IEnumerator displayRound()
    {
        start = false;
        yield return new WaitForSeconds(1);
        if (DiskFactory.getInstance().isLaunching())
        {
            yield return new WaitForSeconds(3);
        }
        roundChangeMessageObj.GetComponent<Text>().text = "ROUND  " + getRound();
        yield return new WaitForSeconds(0.6f);
        roundChangeMessageObj.GetComponent<Text>().text = "   GO!   ";
        yield return new WaitForSeconds(0.6f);
        roundChangeMessageObj.GetComponent<Text>().text = "";
        start = true;
    }
}
