using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;

namespace Collectables
{
    public class Coin : MonoBehaviour, ICollectable<int>
    {
        [SerializeField]
        private int _value;
        [SerializeField]
        private UnityEvent _OnCollected;
        [SerializeField]
        private AudioClip _audioClip;
        [SerializeField]
        private AudioMixerGroup _audioMixerGroup;

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
            GameManager.Instance.MyAudioManager.PlayAudio(_audioClip, _audioMixerGroup);
        }


    } 
}

