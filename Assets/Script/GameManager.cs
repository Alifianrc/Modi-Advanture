using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static int GameMode;
    public static int GameLevel;
    bool gameIsOver = false;

    public GameObject menuPanel;
    public GameObject tantanganButton;
    public GameObject tantanganPanel;
    public GameObject tantanganMulaiButton;
    public GameObject tantanganTutupButton;
    public GameObject CountDownPanel;
    public Text[] soalTantangan;
    public GameObject[] checkMark;
    public bool[] correctQuestion = new bool[3];
    bool GameSaved;
    public static bool mulaiTantangan = false;
    bool mulaiGame = false;
    bool questionCorrect = false;
    float countDownTime = 4.5f;
    public Text countDownText;
    float TimeLimit;
    public GameObject TimeLimiter;
    public Text timeLimiterText;
    public Transform housePoint;

    public GameObject winPanel;
    public Text winScore;
    public GameObject losePanel;
    public Text loseScore;
    float timeScore;

    GameData data = new GameData();
    Question[] question = new Question[3];
    int questionMax = 3;

    public GameObject[] tumbuhan;
    float objectSpawnMinRadius = 50;
    float objectSpawnMaxRadius = 1000;
    int objectMaxSpawn = 50;
    int finalScore;
    int objectCount = 0;

    public Transform playerPosition;

    public GameObject theCamera;

    public LayerMask TerrainLayerMask;

    // Start is called before the first frame update
    void Start()
    {
        // Spawn all tumbuhan
        spawnMaterial();
        GameSaved = false;
        Debug.Log(GameLevel);

        if (GameMode == 0)
        {
            // Hide cursor
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        // Tantangan Mode
        else if (GameMode == 1)
        {
            TantanganMode();            
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Cursor visibility
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else if (Input.GetKeyUp(KeyCode.LeftAlt) && !GameMenuManager.menuIsActive)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        // Stop camera movement
        if (GameMenuManager.menuIsActive)
        {
            theCamera.SetActive(false);
        }
        else if(!GameMenuManager.menuIsActive)
        {
            theCamera.SetActive(true);
        }

        if (GameMode == 1)
        {
            // Tantangan Mode
            TantanganUpdate();

            if (Input.GetKeyDown(KeyCode.Tab))
            {
                OpenTantangan();
            }
            else if (Input.GetKeyUp(KeyCode.Tab))
            {
                CloseTantangan();
            }
        }   
    }

    void spawnMaterial()
    {
        // Spawn all material in range
        for (int i = 0; i < tumbuhan.Length; i++)
        {
            for (int j = 0; j < objectMaxSpawn; j++)
            {
                Instantiate(tumbuhan[i], randomSpawn(), transform.rotation * Quaternion.Euler(45f, 0f, 0f));
            }
            objectCount += objectMaxSpawn;
        }        
    }
    Vector3 randomSpawn()
    {
        Vector3 spawnPoint = new Vector3();
        spawnPoint = new Vector3(Random.value - 0.5f, 0, Random.value - 0.5f).normalized * Random.Range(objectSpawnMinRadius, objectSpawnMaxRadius);
        spawnPoint.x += playerPosition.position.x;
        //spawnPoint.y = 5f;
        spawnPoint.z += playerPosition.position.z;

        RaycastHit hit;
        if(Physics.Raycast(new Vector3(spawnPoint.x,110f,spawnPoint.z),Vector3.down,out hit, Mathf.Infinity, TerrainLayerMask))
        {
            spawnPoint.y = hit.point.y + 1;
        }

        return spawnPoint;
    }
    public void reduceObject()
    {
        objectCount--;
    }

    void LoadSoal()
    {
        // Load soal
        List<int> history = new List<int>();
        for (int i = 0; i < questionMax; i++)
        {
            int rand = Random.Range(0, data.questMaxNumber);
            bool repeating = false;
            if (i > 0)
            {
                foreach (int a in history)
                {
                    if (a == rand)
                    {
                        i--;
                        repeating = true;
                        break;
                    }
                    else
                    {
                        repeating = false;
                    }
                }
            }
            if (!repeating)
            {
                history.Add(rand);

                if(GameLevel == 1)
                {
                    question[i] = data.GetQuestionMudah(rand);
                    soalTantangan[i].text = question[i].question;

                    // Load Time Limit
                    TimeLimit = data.GetTimeLimitMudah();
                }
                else if(GameLevel == 2)
                {
                    question[i] = data.GetQuestionSedang(rand);
                    soalTantangan[i].text = question[i].question;

                    // Load Time Limit
                    TimeLimit = data.GetTimeLimitSedang();
                }
                else if (GameLevel == 3)
                {
                    question[i] = data.GetQuestionSulit(rand);
                    soalTantangan[i].text = question[i].question;

                    // Load Time Limit
                    TimeLimit = data.GetTimeLimitSulit();
                }
                timeScore = TimeLimit;

                correctQuestion[i] = false;
            }
        }

       
    }
    void TantanganMode()
    {
        LoadSoal();

        // Pause Game
        Time.timeScale = 0;

        tantanganButton.SetActive(true);
        TimeLimiter.SetActive(true);
        

        // Open Tantangan Panel
        tantanganPanel.SetActive(true);
    } 
    public void MulaiTantanganButton()
    {
        // Resume Game
        Time.timeScale = 1;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        // Set Button
        tantanganMulaiButton.SetActive(false);
        tantanganTutupButton.SetActive(true);

        // Close Tantangan Panel
        tantanganPanel.SetActive(false);     

        // Open countdown panel
        CountDownPanel.SetActive(true);
        mulaiTantangan = true;
    }
    void TantanganUpdate()
    {
        if (mulaiTantangan)
        {
            countDownTime -= Time.deltaTime;
            if(countDownTime >= 1.5)
            {
                countDownText.text = (countDownTime - 1).ToString("f0");
            }
            else
            {
                countDownText.text = "ADVENTURE!";
            }
            
            if (countDownTime <= 0.5)
            {
                CountDownPanel.SetActive(false);
                mulaiGame = true;
                mulaiTantangan = false;
            }
        }
        else if (mulaiGame)
        {
            TimeLimit -= Time.deltaTime;
            if(TimeLimit >= 0)
            {
                timeLimiterText.text = TimeLimit.ToString("f0");
            }
            
            if (TimeLimit <= 0 && !gameIsOver)
            {
                // Player lose
                tantanganPanel.SetActive(false);
                menuPanel.SetActive(false);

                SetFinalScore();

                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                GameMenuManager.menuIsActive = true;

                losePanel.SetActive(true);
                gameIsOver = true;
            }
            else
            {
                foreach (bool a in correctQuestion)
                {
                    if (a == false)
                    {
                        questionCorrect = false;
                        break;
                    }
                    else
                    {
                        questionCorrect = true;
                    }
                }
            }
        }

        // If correct and player come back to house
        if (questionCorrect && Vector3.Distance(playerPosition.position, housePoint.position) <= 5)
        {
            tantanganPanel.SetActive(false);
            menuPanel.SetActive(false);

            SetFinalScore();

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            GameMenuManager.menuIsActive = true;

            winPanel.SetActive(true);
            gameIsOver = true;

            SaveGameOver();            
        }
    }
    void SetFinalScore()
    {
        int correctItem = 0;
        foreach (bool a in correctQuestion)
        {
            if (a == true)
            {
                correctItem++;
            }
        }

        finalScore = (int)(correctItem / 3 * 70) + (int)(TimeLimit / timeScore * 30);

        winScore.text = "Point : " + finalScore.ToString();
        loseScore.text = "Point : " + finalScore.ToString();
    }
    void SaveGameOver()
    {
        if (!GameSaved)
        {
            GameSaved = true;
            if (GameLevel == 1)
            {
                TheData data = new TheData (SaveData.LoadData());
                data.SetSedang(true);
                if(finalScore > data.GetScoreMudah())
                {
                    data.SetScoreMudah(finalScore);
                }
                SaveData.SaveProgress(data);
            }
            else if (GameLevel == 2)
            {
                TheData data = new TheData(SaveData.LoadData());
                data.SetSusah(true);
                if(finalScore > data.GetScoreSedang())
                {
                    data.SetScoreSedang(finalScore);
                }
                SaveData.SaveProgress(data);
            }
            else if(GameLevel == 3)
            {
                TheData data = new TheData(SaveData.LoadData());
                if(finalScore > data.GetScoreSusah())
                {
                    data.SetScoreSusah(finalScore);
                }
                SaveData.SaveProgress(data);
            }
        }
    }
    public void CheckObjectTaken(string name)
    {
        int i = 0;
        foreach(Question a in question)
        {
            if (a.answer == name)
            {
                correctQuestion[i] = true;
                checkMark[i].SetActive(true);
            }
            i++;
        }
    }

    public void OpenTantangan()
    {
        tantanganPanel.SetActive(true);
        GameMenuManager.menuIsActive = true;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public void CloseTantangan()
    {
        tantanganPanel.SetActive(false);
        GameMenuManager.menuIsActive = false;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
