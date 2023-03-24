using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEventListenerHandler : MonoBehaviour
{
    public void OnButtonHover() => AudioManager.instance.ButtonHover();
}
