using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    private bool won = false;
    public Canvas canvas;
    private void Update()
    {
        if (Numbers.AllCells.All(x => x.IsCorrect == true) && won == false)
        {
            canvas.gameObject.SetActive(true);
            won = true;
        }
    }
}
