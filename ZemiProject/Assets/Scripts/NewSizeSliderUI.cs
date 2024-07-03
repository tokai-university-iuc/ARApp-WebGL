using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HandMR
{
    public class NewSizeSliderUI : MonoBehaviour
    {
        public Transform[] targetTransforms;

        Vector3[] defaultScales;

        void Start()
        {
            defaultScales = new Vector3[targetTransforms.Length];

            for (int i = 0; i < targetTransforms.Length; i++)
            {
                defaultScales[i] = targetTransforms[i].localScale;
            }
        }

        public void ValueChange(float val)
        {
            for(int i = 0; i < targetTransforms.Length; i++)
            {
                targetTransforms[i].localScale = defaultScales[i] * val;
            }
        }
    }
}
