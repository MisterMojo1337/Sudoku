using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoadMaster : MonoBehaviour
{
    public void ChangeToLevelauswahl()
    {
        SceneManager.LoadScene("LevelSelect");
    }
    public void ChangeToSudoku(int sudokuNr)
    {
        Numbers.sudokuInt = sudokuNr;
        SceneManager.LoadScene("GameScene");
    }
}
