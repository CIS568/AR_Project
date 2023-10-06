using Needle.XR.ARSimulation;
using Needle.XR.ARSimulation.ExampleComponents;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace MyFirstARGame
{
    public class Init : MonoBehaviour
    {
        bool done = false;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        static void OnBeforeSceneLoad()
        {
            new GameObject().AddComponent<Init>();
        }

        private void Update()
        {
            if(!done)
            {
                var ex = FindObjectOfType<InstantiateAtRaycastHitExample>();
                if (ex)
                {
                    ex.enabled = false;
                    done = true;
                }
            }
        }
    }
}
