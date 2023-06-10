using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource theme;
    public Toggle toggleTheme;
    public Toggle toggleSounds;
    public bool mutedTheme;
    public bool mutedSounds;
    // Start is called before the first frame update
    void Start()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
        DontDestroyOnLoad(theme);

        toggleTheme.isOn = false;
        toggleSounds.isOn = false;
    }


    public void ToggleMusic(bool newValue)
    {
        mutedTheme = newValue;

        if (!mutedTheme)
        {
            toggleTheme.isOn = false;
            theme.volume = 1f;
        }
        else
        {
            toggleTheme.isOn= true;
            theme.volume = 0f;
        }
    }
    public void ToggleSounds( bool newValuee)
    {
        mutedSounds= newValuee;
        if (!mutedSounds)
        {
            toggleSounds.isOn = false;
            AudioListener.volume = 1f;
        }
        else
        {
            toggleSounds.isOn= true;
            AudioListener.volume= 0f;
        }

    }
}
