using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameRoomManager : MonoBehaviour
{
    [SerializeField] private GameObject gameRoomPrefab;

    private GameObject gameRoomUI;
    private GameObject gameGrid;

    private int[] gameBoard;

    private Button[] gameGridButtons;

    private string playerLetter = "";


    private void Start()
    {
        gameGrid = Instantiate(gameRoomPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        gameGrid = GameObject.Find("GameGrid");
        gameGridButtons = gameGrid.GetComponentsInChildren<Button>();

        gameBoard = new[] { 0, 0, 0, 0, 0, 0, 0, 0, 0};

        //for (int i = 0; i < gameGridButtons.Length; i++)
        //{
        //    int k = i;
        //    gameGridButtons[i].onClick.AddListener(delegate { OnSquareClick(k); });
        //}

        foreach (Button button in gameGridButtons)
        {
              //button.onClick.AddListener(OnSquareClick());
        }

    }

    private void OnSquareClick(Button button)   
    {
        //gameGridButtons[squareNum].GetComponentInChildren<TMP_Text>().text = playerLetter;
    }

    

     
}
