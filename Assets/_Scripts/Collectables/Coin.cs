using UnityEngine;
using UnityEngine.Events;

namespace Collectables
{
    public class Coin : MonoBehaviour, ICollectable<int>
    {
        [SerializeField]
        private int _value;
        [SerializeField]
        private UnityEvent _OnCollected;

        public int Value => _value;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player")) return;

            _OnCollected?.Invoke();
            Collect();
        }

        public void Collect()
        {
            GameManager.Instance.Score.AddPoints(_value);
            GameManager.Instance.GetMainUI.UpdateScore(GameManager.Instance.Score);
            Destroy(gameObject);
        }


    } 
}

