using System;
using System.IO;
using UnityEngine;

namespace TicTacToe
{
    [Serializable]
    public struct CountData
    {
        public int PlayerPoints, EnemyPoints;

        public static readonly string SavePath = Path.Combine(Application.persistentDataPath, "count_save.json");
    }
}