using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour{

    public AudioSource MusicSource, SoundSource;
    public Slider MusicSlider, SoundSlider;
    public Button ResetBtn, ClosePanelBtn;
    public GameObject SetPanel;

    private void Awake()
    {
        GameObject.Find("Background").GetComponent<Animator>().speed = 0.75f;

        Button PlayBtn = GameObject.Find("PlayBtn").GetComponent<Button>();
        Button LevelsBtn = GameObject.Find("LevelsBtn").GetComponent<Button>();
        Button SetBtn = GameObject.Find("SetBtn").GetComponent<Button>();
        Button QuitBtn = GameObject.Find("QuitBtn").GetComponent<Button>();

        PlayBtn.onClick.AddListener(() =>
        {
            SoundSource.Play();
            SceneManager.LoadScene("Gameplay");
        });

        LevelsBtn.onClick.AddListener(() =>
        {
            SoundSource.Play();
            SceneManager.LoadScene("Levels");
        });

        SetBtn.onClick.AddListener(() =>
        {
            SetPanel.SetActive(true);
            SoundSource.Play();
        });

        QuitBtn.onClick.AddListener(() =>
        {
            SoundSource.Play();
            Application.Quit();
        });

        ResetBtn.onClick.AddListener(() => OnReset());

        ClosePanelBtn.onClick.AddListener(() =>
        {
            SoundSource.Play();
            SetPanel.SetActive(false);
        });

        MusicSource.volume = (float)SaveLoad.LoadObject("music");
        MusicSlider.value = MusicSource.volume;

        SoundSource.volume = (float)SaveLoad.LoadObject("sound");
        SoundSlider.value = SoundSource.volume;

        MusicSlider.onValueChanged.AddListener(delegate { MusicSource.volume = MusicSlider.value; });
        SoundSlider.onValueChanged.AddListener(delegate { SoundSource.volume = SoundSlider.value; });
    }

    private void OnReset()
    {
        SoundSource.Play();
        SaveLoad.ResetProgress();
        SetPanel.SetActive(false);
        MusicSource.volume = (float)SaveLoad.LoadObject("music");
        SoundSource.volume = (float)SaveLoad.LoadObject("sound");
        MusicSlider.value = MusicSource.volume;
        SoundSlider.value = SoundSource.volume;
    }

    private void OnDisable()
    {
        SaveLoad.SaveObject("music", MusicSource.volume);
        SaveLoad.SaveObject("sound", SoundSource.volume);
    }
}
