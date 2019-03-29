using UnityEngine;

namespace Assets.Scripts
{

    [System.Serializable]
    public class Cell : MonoBehaviour
    {
        public string Column;
        public int Row;
        public int? Value;
        public string Block;
    }
}
