using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App
{
    /// <summary>
    /// 規約同時に時の保存データ
    /// </summary>
    [System.Serializable]
    public class Agreement
    {
        public bool isAgree = false;
        public System.DateTime onDate;

        public Agreement()
        {
            isAgree = false;
        }

        public void SetAgreement()
        {
            isAgree = true;
            onDate = System.DateTime.UtcNow;
        }

        public void CopyFrom(Agreement other)
        {
            isAgree = other.isAgree;
            onDate = other.onDate;
        }
    }
}

