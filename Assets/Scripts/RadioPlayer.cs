using UnityEngine;
using UnityEngine.UI;

public class RadioPlayer : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] playlist;
    private int currentTrack = 0;

    public Button playPauseButton;
    public Sprite playIcon;
    public Sprite pauseIcon;
    public Image buttonImage;

    public Button nextButton;
    public Button prevButton;

    void Start()
    {
        audioSource.clip = playlist[currentTrack];
        UpdateButtonIcon();

        playPauseButton.onClick.AddListener(TogglePlayPause);
        nextButton.onClick.AddListener(PlayNext);
        prevButton.onClick.AddListener(PlayPrevious);
    }

    void TogglePlayPause()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Pause();
        }
        else
        {
            audioSource.Play();
        }

        UpdateButtonIcon();
    }

    void PlayNext()
    {
        currentTrack = (currentTrack + 1) % playlist.Length;
        PlayCurrentTrack();
    }

    void PlayPrevious()
    {
        currentTrack = (currentTrack - 1 + playlist.Length) % playlist.Length;
        PlayCurrentTrack();
    }

    void PlayCurrentTrack()
    {
        audioSource.clip = playlist[currentTrack];
        audioSource.Play();
        UpdateButtonIcon();
    }

    void UpdateButtonIcon()
    {
        buttonImage.sprite = audioSource.isPlaying ? pauseIcon : playIcon;
    }
}
