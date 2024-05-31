using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessing : MonoBehaviour
{
    private PostProcessVolume postprocessing_volume;
    private ColorGrading color_grading;

    private void Awake()
    {
        postprocessing_volume = GetComponent<PostProcessVolume>();
    }
    private void Start()
    {
        if(postprocessing_volume.profile.TryGetSettings(out color_grading))
        {
            color_grading.saturation.value = 0;
        }
    }
    public void UpdateSaturation()
    {
        if(color_grading != null)
        {
            color_grading.saturation.value -= 20f;
        }
    }
    public void ResetSaturation()
    {
        if(color_grading != null)
        {
            color_grading.saturation.value = 100f;
        }
    }
    public void ChangeRedScene()
    {
        if(color_grading != null)
        {
            color_grading.colorFilter.value = new Color(1f, 0, 0);
        }
    }
    public void ResetColorScene()
    {
        if(color_grading != null)
        {
            color_grading.colorFilter.value = Color.white;
        }
    }
}
