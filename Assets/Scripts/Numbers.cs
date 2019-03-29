using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Numbers : MonoBehaviour
{
    private List<Cell> rowList = new List<Cell>();
    private List<Cell> columnList = new List<Cell>(); 
    private List<Cell> blockList = new List<Cell>() ;

    private List<string> alphabetList = new List<string>() { "A", "B", "C", "D", "E", "F", "G", "H", "I" };

    private string levelPath;
    private int rowCounter = 1;
    private List<Cell> AllCells;

    public GameObject inputFelder;

    private void Start()
    {
        levelPath = Application.streamingAssetsPath + @"\" + "sudoku1" + ".txt";
        AllCells = inputFelder.GetComponentsInChildren<Cell>().ToList();
        foreach (var row in File.ReadAllLines(levelPath).ToList())
        {
            int columncounter = 0;
            foreach (char charNumber in row)
            {
                int number = (int)char.GetNumericValue(charNumber);
                if (number != 0)
                {
                    AllCells.First(x => x.Column == alphabetList[columncounter] && x.Row == rowCounter).Value = number;
                    var cell = AllCells.First(x => x.Column == alphabetList[columncounter] && x.Row == rowCounter);
                    var cellInputField = cell.gameObject.GetComponent<InputField>();

                    //cell.Value = number;
                    cellInputField.text = cell.Value.ToString();
                    cellInputField.readOnly = true;
                } 
                columncounter++;
            }
            rowCounter++;
        }
    }

    public void ControlInput(Cell cell)
    {
        var cellText = cell.gameObject.GetComponent<InputField>().GetComponentInChildren<Text>();
        cell.Value = Convert.ToInt32(cellText.text);

        rowList.AddRange(AllCells.Where(x => x.Row == cell.Row).ToList());
        columnList.AddRange(AllCells.Where(x => x.Column == cell.Column).ToList());
        blockList.AddRange(AllCells.Where(x => x.Block == cell.Block).ToList());

        if (rowList.Any(x => x.Value == cell.Value)  || columnList.Any(x => x.Value == cell.Value) || blockList.Any(x => x.Value == cell.Value))
        {
            cellText.color = new Color(1, 0, 0);
        }
        else
        {
            cellText.color = new Color(0, 0, 0);
        }
        AllCells = inputFelder.GetComponentsInChildren<Cell>().ToList();
    }
}
