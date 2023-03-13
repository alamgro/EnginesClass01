using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public class MainUI : MonoBehaviour
    {
        [SerializeField]
        private UIDocument _uiDocument;

        private Label _labelScore;

        void Awake()
        {
            _labelScore = _uiDocument.rootVisualElement.Q<Label>("LblScore");
        }

        public void UpdateScore(IScorePoints score) => _labelScore.text = $"Score: {score.Points}";
    //public void AddPoints(int points) => 
}

}