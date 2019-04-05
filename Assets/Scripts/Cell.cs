using UnityEngine;

namespace Assets.Scripts
{

    [System.Serializable]
    public class Cell : MonoBehaviour
    {
        public string Column;
        public int Row;
        public string Block;
        public int Value = 0;
        public bool IsCorrect = false;
    }
}
