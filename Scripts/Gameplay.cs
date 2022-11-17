using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Gameplay : MonoBehaviour
{
    public AudioSource MusicSource, SoundSource;
    public AudioClip Click, Hit_wall, Figure_broken, Ball_lose, Lose, Win;
    public AudioClip[] Hit_figure = new AudioClip[3];

    public GameObject[] Figures = new GameObject[5];
    
    private Text DefeatScore;
    private Image HeartsPanel;
    private GameObject Platform;

    public GameObject LosePanel, WinPanel, PausePanel;

    public GameObject BallPrefab, Ball;

    private Button PauseBtn;
    public Button ClosePauseBtn;

    public Sprite[] ThreeHearts = new Sprite[4];
    public GameObject[] LevelSets = new GameObject[15];

    private int figuresOnLevel;

    private int level, lvs;
    private int lives
    {
        get { return lvs; }
        set
        {
            lvs = value;
            switch (lvs)
            {
                case 3:
                    HeartsPanel.sprite = ThreeHearts[3];
                    break;
                case 2:
                    HeartsPanel.sprite = ThreeHearts[2];
                    break;
                case 1:
                    HeartsPanel.sprite = ThreeHearts[1];
                    break;
                case 0:
                    HeartsPanel.sprite = ThreeHearts[0];
                    PanelStopGame(LosePanel);
                    PauseBtn.interactable = false;
                    break;
            }
        }
    }

    public bool SetRandomLevel;

    private void Awake()
    {
        GameObject.Find("Background").GetComponent<Animator>().speed = 1.5f;

        DefeatScore = GameObject.Find("DefeatScore").GetComponent<Text>();
        HeartsPanel = GameObject.Find("HeartsPanel").GetComponent<Image>();

        PauseBtn = GameObject.Find("PauseBtn").GetComponent<Button>();
        PauseBtn.onClick.AddListener(() => PanelStopGame(PausePanel) );
        ClosePauseBtn.onClick.AddListener(() => PanelStopGame(PausePanel) );

        Platform = GameObject.Find("Platform");

        Ball = Instantiate(BallPrefab, BallPrefab.transform.position, transform.rotation);

        lives = 3;

        level = (int)SaveLoad.LoadObject("LEVEL");
        if (SetRandomLevel)
            level = 14;

        if (level < 14)
            LevelSets[level].SetActive(true);
        else
            RandomLevel();

        figuresOnLevel = LevelSets[level].transform.childCount;
        
        MusicSource.volume = (float)SaveLoad.LoadObject("music");
        SoundSource.volume = (float)SaveLoad.LoadObject("sound");
    }

    private void Update()
    {
        DefeatScore.text = (figuresOnLevel - LevelSets[level].transform.childCount) + "/" + figuresOnLevel;

        if (Ball == null)
        {
            Ball = Instantiate(BallPrefab, Vector2.zero, transform.rotation);
            lives--;
        }

        if(LevelSets[level].transform.childCount == 0 && !WinPanel.activeSelf)
        {
            PanelStopGame(WinPanel);
            PauseBtn.interactable = false;
            SaveLoad.SaveObject("levels", NewPastLevel());
        }
    }

    private void StopGame(GameObject gameObj, bool stop)
    {
        gameObj.SetActive(stop);
        Cursor.visible = true;

        if (stop)
        {
            Destroy(Platform.GetComponent<Platform>());
        }
        else
        {
            Platform.AddComponent<Platform>();
        }

        Ball.GetComponent<Rigidbody2D>().simulated = !stop;
        Ball.GetComponent<Ball>().enabled= !stop;
    }

    private void PanelStopGame(GameObject panel)
    {
        StopGame(panel, !panel.activeSelf);
    }

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    private bool[] NewPastLevel()
    {
        bool[] pastLevels = new bool[14];
        for(int i = 0; i < 14; i++)
        {
            if (i <= level)
                pastLevels[i] = true;
            else
                pastLevels[i] = false;
        }
        return pastLevels;
    }

    private void RandomLevel()
    {
        for (float x = -1.2f; x <= 1.2f; x += 0.6f)
        {
            for (float y = 2.4f; y >= 0.4f; y -= 0.6f)
            {
                Vector2 position = new Vector2(x, y);
                int[] probNum = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1,
                    1, 2, 2, 2, 2, 2, 2, 3, 3, 3, 3, 4, 4}; // To range probability btw figures
                int random = probNum[Random.Range(0, probNum.Length)];

                GameObject newFigure = Instantiate(Figures[random], position, transform.rotation);
                newFigure.transform.SetParent(LevelSets[14].transform);
            }
        }
        LevelSets[14].SetActive(true);
    }
}

