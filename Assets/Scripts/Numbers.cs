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
    private List<Cell> rowList;
    private List<Cell> columnList;
    private List<Cell> blockList;
    private List<Cell> IdenticalCells;

    private List<string> alphabetList = new List<string>() { "A", "B", "C", "D", "E", "F", "G", "H", "I" };

    private string levelPath;
    private int rowCounter = 1;

    public static List<Cell> AllCells;

    public GameObject inputFelder;
    public static int sudokuInt;

    private void Start()
    {
        AllCells = new List<Cell>();

        levelPath = Application.streamingAssetsPath + $@"\sudoku{sudokuInt}.txt";
        AllCells = inputFelder.GetComponentsInChildren<Cell>().ToList();
        foreach (var row in File.ReadAllLines(levelPath).ToList())
        {
            int columncounter = 0;
            foreach (char charNumber in row)
            {
                int number = (int)char.GetNumericValue(charNumber);
                if (number != 0)
                {
                    Cell cell = AllCells.First(x => x.Column == alphabetList[columncounter] && x.Row == rowCounter);
                    InputField cellInputField = cell.gameObject.GetComponent<InputField>();

                    cell.Value = number;
                    cellInputField.text = cell.Value.ToString();

                    cell.IsCorrect = true;
                    cell.gameObject.GetComponentInChildren<Text>().color = new Color((float)0.5, (float)0.5, (float)0.5, 1);
                    cell.IsPrescribed = true;
                    cellInputField.readOnly = true;
                }
                columncounter++;
            }
            rowCounter++;
        }
    }

    public void ControlInput(Cell changedCell)
    {
        Cell cell = AllCells.First(x => x.Column == changedCell.Column && x.Row == changedCell.Row);
        Text cellText = cell.gameObject.GetComponent<InputField>().GetComponentInChildren<Text>();

        if (cellText.text != "")
        {
            cell.Value = Convert.ToInt32(cellText.text);

            FillLists(cell);
            FindIdenticalCells(cell);

            if (IdenticalCells.Count > 0)
            {
                cell.gameObject.GetComponentInChildren<Text>().color = new Color(1, 0, 0);
                cell.IsCorrect = false;
                foreach (var identicalCell in IdenticalCells)
                {
                    if (identicalCell.IsPrescribed != true)
                    {
                        identicalCell.gameObject.GetComponentInChildren<Text>().color = new Color(1, 0, 0);
                        identicalCell.IsCorrect = false;
                        cell.InfluencedCells.Add(identicalCell);
                    }
                }
                ChangeRedCells(cell.InfluencedCells.Distinct().ToList());
            }
            else
            {
                cell.gameObject.GetComponentInChildren<Text>().color = new Color(0, 0, 0);
                cell.IsCorrect = true;
                ChangeRedCells(cell.InfluencedCells);
            }
        } else
        {
            cell.Value = 0;
        }
    }
    

    private void FindIdenticalCells(Cell cell)
    {
        IdenticalCells = new List<Cell>();
        
        IdenticalCells.AddRange(rowList.Where(x => x.Value == cell.Value && x.Column != cell.Column));
        IdenticalCells.AddRange(columnList.Where(x => x.Value == cell.Value && x.Row != cell.Row));
        IdenticalCells.AddRange(blockList.Where(x => x.Value == cell.Value && x.Column != cell.Column && x.Row != cell.Row));
    }

    private void ChangeRedCells(List<Cell> cells)
    {
        foreach (var cell in cells)
        {
            FillLists(cell);
            FindIdenticalCells(cell);

            if (IdenticalCells.Count > 0)
            {
                foreach (var identicalCell in IdenticalCells)
                {
                    if (identicalCell.IsPrescribed != true)
                    {
                        identicalCell.gameObject.GetComponentInChildren<Text>().color = new Color(1, 0, 0);
                        identicalCell.IsCorrect = false;
                    }
                }
            }
            else
            {
                cell.gameObject.GetComponentInChildren<Text>().color = new Color(0, 0, 0);
                cell.IsCorrect = true;
            }
        }

    }

    private void FillLists(Cell cell)
    {
        rowList = new List<Cell>();
        columnList = new List<Cell>();
        blockList = new List<Cell>();

        rowList.AddRange(AllCells.Where(x => x.Row == cell.Row && x.Value != 0).ToList());
        columnList.AddRange(AllCells.Where(x => x.Column == cell.Column && x.Value != 0).ToList());
        blockList.AddRange(AllCells.Where(x => x.Block == cell.Block && x.Value != 0).ToList());
    }


}
