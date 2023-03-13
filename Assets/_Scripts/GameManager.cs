using UnityEngine;

public class GameManager : MonoBehaviour
{
    //[SerializeField]
    //private Mai mainUI;


    public static GameManager Instance;
    public IScore Score { get; set; }

        

    public GameManager()
    {
        //Score = new ScorePoints();
    }

    private void Awake()
    {
        if(!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //public void AddPoints(int points) => Score. += points;

}
