using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    private GUIhelper GUIHelp;
    int i = 0;

    [Header("Stats")]
    public int m_Health = 100;
    public int m_Currency;
    public int m_InitialCurrency;

    [Header("Game Logic")]
    public int m_EnemyAmount;
    public bool m_NeedsSpawnPosition = false;
    public Vector2 m_SpawnPosition;
    MusicManager MManager;
    public bool m_AlreadyInElevator = false;
    public bool m_ElevatorMusicPlaying = false;

    [Header("Panel Logic")]
    public bool m_IsGameOverPanelOn = false;
    public bool m_GameIsPaused;
    public bool m_IsInShop;

    //for upgrades
    [Header("For Upgrades")]
    public Gun[] m_WeaponsArray;
    public Gun m_CurrentWeapon;
    public int m_CurrentMagazineUpgrade = 0;
    public bool m_CanRefillAmmo = true;
    public int m_Luck;

    [Header("Others")]
    public bool m_HasRestarted;

    void Awake()
    {
        CreateInstance();
    }

    public void CreateInstance()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        m_WeaponsArray = new Gun[4];

        SceneManager.LoadScene(0);
        PlayerPrefs.DeleteAll();
        MManager = MusicManager.instance;
        ReassignObjs();
    }

    public void ReassignObjs()
    {
        GUIHelp = GameObject.FindGameObjectWithTag("GUI").GetComponent<GUIhelper>();
        m_EnemyAmount = GUIHelp.m_NumberOfEnemies;
        SetCoins(0);
        SetMaxHealth(m_Health);
    }

    public void GetCurrentWeapon()
    {
        m_CurrentWeapon = FindObjectOfType<Gun>();
    }

    public void UpgradeMagazine()
    {
        GetCurrentWeapon();
        m_CurrentMagazineUpgrade++;

        foreach (Gun item in m_WeaponsArray)
        {
            item.m_Magazine = item.m_InitialMagazine + (m_CurrentMagazineUpgrade * (int)(item.m_InitialMagazine * 0.2f));
            item.m_MaxAmmo = item.m_InitialMaxAmmo + (m_CurrentMagazineUpgrade * (int)(item.m_InitialMaxAmmo * 0.2f));
        }

        m_CurrentWeapon.UpdateGUI();
    }

    public void UpdateUpgrades()  //llamar desde el start de algo , de Gun quizas?
    {
        GetCurrentWeapon();

        foreach (Gun item in m_WeaponsArray)
        {
            item.m_Magazine = item.m_InitialMagazine + (m_CurrentMagazineUpgrade * (int)(item.m_InitialMagazine * 0.2f));
            item.m_MaxAmmo = item.m_InitialMaxAmmo + (m_CurrentMagazineUpgrade * (int)(item.m_InitialMaxAmmo * 0.2f));
        }

        m_CurrentWeapon.UpdateGUI();

        //aqui poner el de luck tambien
    }

    public void RefillAmmo()
    {
        GetCurrentWeapon();
        m_CanRefillAmmo = false; //poner en true cuando se entre en el ascensor
        i = 0;

        foreach (Gun item in m_WeaponsArray)
        {
            if(Player.GunBoughtArray[i])
                item.m_CurrentMaxAmmo = item.m_CurrentMaxAmmo + (int)(item.m_InitialMaxAmmo * 0.5f);

            i++;
        }

        m_CurrentWeapon.SaveValues();
        m_CurrentWeapon.UpdateGUI();
    }

    public void SetMaxHealth(int health)
    {
        GUIHelp.m_HealthText.text = "" + health;
        GUIHelp.m_Slider.maxValue = health;
        GUIHelp.m_Slider.value = health;
        GUIHelp.fill.color = GUIHelp.m_Gradient.Evaluate(1f);
    }

    public void SetHealth(int health)
    {
        GUIHelp.m_HealthText.text = health.ToString();
        GUIHelp.m_Slider.value = health;
        GUIHelp.fill.color = GUIHelp.m_Gradient.Evaluate(GUIHelp.m_Slider.normalizedValue);
        GUIHelp.m_psmain.startColor = GUIHelp.fill.color;
    }

    public void NextLevel()
    {
        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex) + 1);
    }

    public void LoadScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void SetCoins(int l_quantity)
    {
        m_Currency += l_quantity;
        GUIHelp.CoinText.text = m_Currency.ToString() + " $";
    }

    public void GameOver()
    {
        GUIHelp.m_GUIPanel.SetActive(false);
        GUIHelp.m_GameOverPanel.SetActive(true);
        m_IsGameOverPanelOn = true;
    }

    public void EnemyDefeated()
    {
        m_EnemyAmount -= 1;
        if (m_EnemyAmount <= 0)
        {
            GameObject.FindGameObjectWithTag("Elevator").GetComponent<ElevatorScript>().ElevatorOpen();
            m_CanRefillAmmo = true;
            SoundManager.instance.PlaySound("Elevator_Ding", 1, 1);
        }
    }

    public void ChangeMusic(AudioClip audio)
    {
        MManager.ChangeMusic(audio);
    }
}



