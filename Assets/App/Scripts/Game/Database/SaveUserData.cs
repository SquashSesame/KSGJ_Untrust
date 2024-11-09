using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace App
{
    public class SaveUserData : SingletonDontDestroy<SaveUserData>
    {
        [SerializeField] private GameData _gameData = new GameData();
        
        /// <summary>
        /// ゲームデータ
        /// </summary>
        public GameData Data
        {
            get { return _gameData; }
        }
    }
}