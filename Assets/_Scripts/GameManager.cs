using UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private MainUI _mainUI;

    public static GameManager Instance;
    public IScorePoints Score { get; set; }
    public MainUI GetMainUI => _mainUI;

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
