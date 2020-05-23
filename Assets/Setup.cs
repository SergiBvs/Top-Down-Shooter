using UnityEngine;

public class Setup : MonoBehaviour
{

    GameObject gM;
    GameObject MM;

    [Header("Music Manager")]
    MusicManager mManager;
    public AudioClip[] m_ElevatorMusic;
    public AudioClip[] m_GameMusic;
    public AudioClip m_StoreMusic;

    // Start is called before the first frame update
    void Awake()
    {
        gM = new GameObject("GameManager");
        gM.AddComponent<GameManager>();

        GameObject a = Instantiate(gM);
        a.GetComponent<GameManager>().CreateInstance();


        MM = new GameObject("MusicManager");
        MM.AddComponent<MusicManager>();
        MM.AddComponent<AudioSource>();
        MM.AddComponent<AudioHighPassFilter>().enabled=false;

        GameObject b = Instantiate(MM);
        mManager = b.GetComponent<MusicManager>();
        mManager.CreateInstance();

    }

    private void Start()
    {
        MusicManager.instance.m_MusicSource = MusicManager.instance.GetComponent<AudioSource>();
        MusicManager.instance.m_MusicSource.loop = true;
        MusicManager.instance.m_ElevatorMusic = m_ElevatorMusic;
        MusicManager.instance.m_GameMusic = m_GameMusic;
        MusicManager.instance.m_StoreMusic = m_StoreMusic;

        GameManager.instance.ChangeMusic(MusicManager.instance.m_GameMusic[0]);
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}
