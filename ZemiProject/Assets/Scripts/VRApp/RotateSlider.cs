using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotateSlider : MonoBehaviour
{
    // Inspector‚©‚ç•¡”‚ÌGameObject‚ğŠi”[
    public Transform[] targetTransforms;

    Quaternion[] defaultRotations;
    
    // Start is called before the first frame update
    void Start()
    {
        // ”z—ñ‚Ì‰Šú‰»
        defaultRotations = new Quaternion[targetTransforms.Length];

        // ŠeTransform‚Ì‰Šú‰ñ“]‚ğ•Û‘¶
        for(int i = 0; i < targetTransforms.Length; i++)
        {
            defaultRotations[i] = targetTransforms[i].rotation;
        }
    }

    // ‰¡—p
    public void OnSliderValueChanged(float val)
    {
        // Slider‚Ì’l‚É‰‚¶‚Ä‰ñ“]Šp“x‚ğŒvZ
        float rotationAngle = val * 360f;

        // ŠeGameObject‚ğ‰ñ“]
        for(int i = 0; i < targetTransforms.Length; i++)
        {
            targetTransforms[i].rotation = Quaternion.Euler(0, rotationAngle, 0);
        }
    }

    // c—p
    public void Tate_SliderValueChanged(float val)
    {
        // Slider‚Ì’l‚É‰‚¶‚Ä‰ñ“]Šp“x‚ğŒvZ
        float rotationAngle = val * 360f;

        // ŠeGameObject‚ğ‰ñ“]
        for (int i = 0; i < targetTransforms.Length; i++)
        {
            targetTransforms[i].rotation = Quaternion.Euler(rotationAngle, 0, 0);
        }
    }
}