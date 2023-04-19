using UI;
using UnityEngine;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private MainUI _mainUI;
    [SerializeField]
    private AudioManager _audioManager;
    

    public static GameManager Instance;
    public IScorePoints Score { get; set; }
    public MainUI GetMainUI => _mainUI;

    public AudioManager MyAudioManager { get => _audioManager; set => _audioManager = value; }

    public GameManager()
    {
        Score = new ScorePoints();
    }

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddPoints(int points) => Score.Points += points;

} 
