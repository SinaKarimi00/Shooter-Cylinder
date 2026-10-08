using Features.DependencyInjection.Main;
using Features.MainScript.Main;
using Features.Observer.Main;
using Features.Popup.Application;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Features.Popup.Main.Popups
{
    public class EndGamePopup : MonoBehaviour, IPopup
    {
        [SerializeField] private Button reloadButton;
        [SerializeField] private TextMeshProUGUI killAmount;
        [SerializeField] private TextMeshProUGUI timeAmount;
        [SerializeField] private TextMeshProUGUI score;
        private EventService _eventService;

        private void Awake()
        {
            reloadButton.onClick.AddListener(Reload);
            _eventService = DependencyInjector.Instance.GetDependency<EventService>();
        }

        private void Reload()
        {
            Time.timeScale = 1;
            _eventService.Reset();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void Setup()
        {
            var killCount = GameDataPref.KillCount;
            var gamePlayTime = GameDataPref.GamePlayTime;
            var scoreText = CalculateScore().ToString("0.00");

            killAmount.text = killCount.ToString();
            timeAmount.text = gamePlayTime.ToString("0.00");
            score.text = $"Score is: {scoreText}";
            Time.timeScale = 0;

            float CalculateScore() => (killCount * gamePlayTime);
        }

        public void Close()
        {
            Destroy(gameObject);
        }
    }
}