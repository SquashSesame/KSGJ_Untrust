using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App
{
    public class StarAnimation : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {         
            transform.GetChild(0).rotation *= Quaternion.AngleAxis (1, Vector3.forward);
            this.transform.position += new Vector3( -4.0f, -2.0f, 0 ) * Time.deltaTime;
        }
    }
}
