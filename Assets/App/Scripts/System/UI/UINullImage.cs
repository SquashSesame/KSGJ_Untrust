using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App
{
    public class UINullImage : UnityEngine.UI.Image
    {
        static readonly Color colTransPink = new Color(1, 0, 1, 0.5f);

        void Start()
        {
            this.color = colTransPink;
        }
    }
}