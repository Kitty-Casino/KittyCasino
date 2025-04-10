using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MusicController : MonoBehaviour
{
    public static MusicController instance;

    public AudioSource musicAudioSource;
    public AudioSource sfxAudioSource;
    public AudioClip casinoMusic;
    public AudioClip gameMusic;
    public AudioClip shopMusic;
    public AudioClip christmasMusic;

    private SceneController sceneController;

    public Slider musicSlider;
    public Slider sfxSlider;

    private float previousMusicValue = 0;
    private float previousSFXValue = 0;
    private string sceneName = "";

    private AudioClip previousClip;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        sceneController = GameObject.FindWithTag("SceneController").GetComponent<SceneController>();
    }

    void Start()
    {
        if (!PlayerPrefs.HasKey("MusicVolume"))
        {
            PlayerPrefs.SetFloat("MusicVolue", 0.3f);
        }
        if (musicSlider != null)
        {
            musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        }

        if (!PlayerPrefs.HasKey("SFXVolume"))
        {
            PlayerPrefs.SetFloat("SFXVolume", 0.3f);
        }
        if (sfxSlider != null)
        {
            sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        }
        sceneName = SceneManager.GetActiveScene().name;
        PlayMusic();
    }

    private void PlayMusic()
    {
        bool christmas = sceneController.ChristmasCheck();
        if (!christmas)
        {
            switch (SceneManager.GetActiveScene().name)
            {
                case "Casino":
                    PlayMusic(casinoMusic);
                    break;
                case "StoryCasino":
                    PlayMusic(casinoMusic);
                    break;
                case "Shop":
                    PlayMusic(shopMusic);
                    break;
                case "Bartending":
                    PlayMusic(gameMusic);
                    break;
                case "Blackjack":
                    PlayMusic(gameMusic);
                    break;
                case "Matching":
                    PlayMusic(gameMusic);
                    break;
                case "Poker":
                    PlayMusic(gameMusic);
                    break;
                case "Slots":
                    PlayMusic(gameMusic);
                    break;
                case "MainMenu":
                    PlayMusic(casinoMusic);
                    break;
            }
        }
        else
        {
            switch (SceneManager.GetActiveScene().name)
            {
                case "Casino":
                    PlayMusic(christmasMusic);
                    break;
                case "StoryCasino":
                    PlayMusic(christmasMusic);
                    break;
                case "Shop":
                    PlayMusic(shopMusic);
                    break;
                case "Bartending":
                    PlayMusic(christmasMusic);
                    break;
                case "Blackjack":
                    PlayMusic(christmasMusic);
                    break;
                case "Matching":
                    PlayMusic(christmasMusic);
                    break;
                case "Poker":
                    PlayMusic(christmasMusic);
                    break;
                case "Slots":
                    PlayMusic(christmasMusic);
                    break;
                case "MainMenu":
                    PlayMusic(christmasMusic);
                    break;
            }
        }
        
    }

    public void PlaySound(AudioClip clip)
    {
        sfxAudioSource.PlayOneShot(clip);
    }

    public void PlayMusic(AudioClip clip)
    {
        if (clip != previousClip)
        {
            musicAudioSource.clip = clip;
            musicAudioSource.Play();

            previousClip = clip;
        }
    }

    public void Update()
    {
        if (SceneManager.GetActiveScene().name != sceneName)
        {
            sceneName = SceneManager.GetActiveScene().name;
            PlayMusic();
        }

        if (musicSlider != null)
        {
            if (musicSlider.value != previousMusicValue)
            {
                musicAudioSource.volume = musicSlider.value;
                previousMusicValue = musicSlider.value;
                PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
            }

            if (sfxSlider.value != previousSFXValue)
            {
                sfxAudioSource.volume = sfxSlider.value;
                previousSFXValue = sfxSlider.value;
                PlayerPrefs.SetFloat("SFXVolume", sfxSlider.value);
            }
        }

        if (sceneController == null)
        {
            sceneController = GameObject.FindWithTag("SceneController").GetComponent<SceneController>();
        }
    }
}
