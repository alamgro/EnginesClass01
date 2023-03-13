using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour
{
    [SerializeField]
    private int _points;

    public int GetPoints => _points;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        GameManager.Instance.Score.AddPoints(_points);
        GameManager.Instance.GetMainUI.UpdateScore(GameManager.Instance.Score);
        Destroy(gameObject);
    }
}
