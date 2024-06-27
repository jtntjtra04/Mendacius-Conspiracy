using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    public bool on_fade = false;
    public void FadeActive()
    {
        on_fade = true;
    }
    public void FadeDeactive()
    {
        on_fade = false;
    }
}
