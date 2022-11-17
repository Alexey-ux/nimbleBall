using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Levels : MonoBehaviour{
    public Button[] levelButtons = new Button[15];
    private bool[] pastLevels = new bool[14];

    public AudioSource MusicSource, SoundSource;

    private void Awake(){
        GameObject.Find("Background").GetComponent<Animator>().speed = 0.75f;

        pastLevels = (bool[])SaveLoad.LoadObject("levels");
        MusicSource.volume = (float)SaveLoad.LoadObject("music");
        SoundSource.volume = (float)SaveLoad.LoadObject("sound");

        Button btn = GameObject.Find("ExitBtn").GetComponent<Button>();
        btn.onClick.AddListener( () => {
            SoundSource.Play();
            SceneManager.LoadScene("Menu");
        });

        for (int i = 0; i < 14; i++){
            if (pastLevels[i])
            {
                levelButtons[i + 1].interactable = true;
                levelButtons[i + 1].transform.GetChild(1).gameObject.SetActive(false);
            }
            else
                levelButtons[i + 1].interactable = false;
        }
    }

    public void LoadLevel(int level)
    {
        SoundSource.Play();
        SaveLoad.SaveObject("LEVEL", level);
        SceneManager.LoadScene("Gameplay");
    }

    private void OnDisable()
    {
        SaveLoad.SaveObject("levels", pastLevels);
    }
}