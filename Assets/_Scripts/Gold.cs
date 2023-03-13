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
        //GameManager.Instance.Score.add    
    }
}
