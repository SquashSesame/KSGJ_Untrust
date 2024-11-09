using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Serialization;
using System.Linq;
using UnityEngine.Events;

namespace App
{
    /// <summary>
    /// ユーザー・データ
    /// </summary>
    [System.Serializable]
    public class User
    {
        public int UserId;             // サーバー側からの付与ID

        public User()
        {
            Reset();
        }

        public void Reset()
        {
        }

        public void CopyFrom(User other)
        {
            UserId = other.UserId;
        }
    }

}