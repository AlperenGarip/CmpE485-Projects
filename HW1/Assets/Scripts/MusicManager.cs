using UnityEngine;
using TMPro;

public class MusicManager : MonoBehaviour
{
    [Header("Button Labels")]
    public TMP_Text[] buttonLabels;

    [Header("Label Text")]
    public string musicOnText  = "Music";
    public string musicOffText = "<s>Music</s>";

    private AudioSource audioSource;
    private const string PrefKey = "MusicOn";
    private bool isMusicOn = true;

    void Start()
    {
        // Load saved preference (default: on)
        isMusicOn = PlayerPrefs.GetInt(PrefKey, 1) == 1;

        audioSource = GetComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.loop = true;
            // Only start playing if the player left music ON
            // Note: Make sure "Play On Awake" is UNCHECKED on the AudioSource in Inspector
            if (isMusicOn)
                audioSource.Play();
            else
                audioSource.Stop(); // ensure it never starts if preference is off
        }
        UpdateLabel();
    }

    public void ToggleMusic()
    {
        if (audioSource == null) return;

        isMusicOn = !isMusicOn;
        PlayerPrefs.SetInt(PrefKey, isMusicOn ? 1 : 0); // save preference
        PlayerPrefs.Save();

        if (isMusicOn)
            audioSource.UnPause();
        else
            audioSource.Pause();

        UpdateLabel();
    }

    void UpdateLabel()
    {
        if (buttonLabels == null) return;
        string text = isMusicOn ? musicOnText : musicOffText;
        foreach (TMP_Text label in buttonLabels)
        {
            if (label != null) label.text = text;
        }
    }

    public bool IsMusicOn() => isMusicOn;
}
