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

    private List<string> alphabetList = new List<string>() { "A", "B", "C", "D", "E", "F", "G", "H", "I" };

    private string levelPath;
    private int rowCounter = 1;
    public static List<Cell> AllCells;

    public GameObject inputFelder;

    private void Start()
    {
        AllCells = new List<Cell>();

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
                    var cell = AllCells.First(x => x.Column == alphabetList[columncounter] && x.Row == rowCounter);
                    var cellInputField = cell.gameObject.GetComponent<InputField>();

                    cell.Value = number;
                    cell.IsCorrect = true;
                    cellInputField.text = cell.Value.ToString();
                    cellInputField.readOnly = true;
                }
                columncounter++;
            }
            rowCounter++;
        }
    }

    public void ControlInput(Cell UsedCell)
    { 
        try 
	    {
            rowList = new List<Cell>();
            columnList = new List<Cell>();
            blockList = new List<Cell>();

            Cell cell = AllCells.First(x => x.Column == UsedCell.Column && x.Row == UsedCell.Row);

            Text cellText = cell.gameObject.GetComponent<InputField>().GetComponentInChildren<Text>();
            cell.Value = Convert.ToInt32(cellText.text);

            rowList.AddRange(AllCells.Where(x => x.Row == cell.Row && x.Value != 0).ToList());
            columnList.AddRange(AllCells.Where(x => x.Column == cell.Column && x.Value != 0).ToList());
            blockList.AddRange(AllCells.Where(x => x.Block == cell.Block && x.Value != 0).ToList());

            if (rowList.Any(x => x.Value == cell.Value && x.Column != cell.Column && x.Block != cell.Block) ||
                columnList.Any(x => x.Value == cell.Value && x.Row != cell.Row && x.Block != cell.Block) ||
                blockList.Any(x => x.Value == cell.Value && x.Column != cell.Column && x.Row != cell.Row))
            {
                cellText.color = new Color(1, 0, 0);
                cell.IsCorrect = false;
            }
            else
            {
                cellText.color = new Color(0, 0, 0);
                cell.IsCorrect = true;
            }
        }
        catch (Exception)
        {
	        throw;
        }

    }
}
