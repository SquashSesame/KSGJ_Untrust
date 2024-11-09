using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace App
{
    public class ScreenResolution : MonoBehaviour
    {
        [SerializeField] private CanvasScaler canvasScaler = null;
        [SerializeField] private Vector2 resolution = new Vector2(720, 1334);
        
        // Start is called before the first frame update
        void Start()
        {
            if (canvasScaler == null)
            {
                canvasScaler = GetComponent<CanvasScaler>();
            }

            if (canvasScaler != null)
            {
                canvasScaler.referenceResolution = resolution;
            }
        }
    }
}
