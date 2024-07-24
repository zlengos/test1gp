using Configs;
using Enemies;
using Interfaces;
using TMPro;
using Tools;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class RestartCanvasHandler : MonoBehaviour, IConfigurable
    {
        #region Fields

        [SerializeField] private EnemyHandler enemyHandler;
        [SerializeField] private EnemyFactoryWithPool enemyFactory;
        [SerializeField] private GameConfig config;
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private Player.Player player;

        [SerializeField] private TextMeshProUGUI infoText;
        [SerializeField] private Image infoImage;

        #region Runtime

        private string _winText;
        private string _loseText;
        
        private Sprite _winSprite;
        private Sprite _loseSprite;

        #endregion

        #endregion
        
        #region Listening

        private void OnEnable()
        {
            EventManager.OnEnemiesDestroyed += OnEnemiesDestroyed;
            EventManager.OnPlayerDead+= OnPlayerDead;
            Initialize();
        }


        private void OnDisable()
        {
            EventManager.OnEnemiesDestroyed -= OnEnemiesDestroyed;
            EventManager.OnPlayerDead -= OnPlayerDead;
        }

        public void Initialize()
        {
            CacheSettings();
        }

        public void CacheSettings()
        {
            _winText = config.restartCanvasConfig.winText;
            _loseText = config.restartCanvasConfig.loseText;
            _winSprite = config.restartCanvasConfig.winSprite;
            _loseSprite = config.restartCanvasConfig.loseSprite;
        }

        #endregion
        
        private void OnEnemiesDestroyed()
        {
            enemyFactory.ClearAll();
            enemyHandler.enabled = false;
            infoText.text = _winText;
            infoImage.sprite = _winSprite;
            
            CanvasHandler.ShowCanvas(canvasGroup);
        }
        
        private void OnPlayerDead()
        {
            player.enabled = false;
            infoText.text = _loseText;
            infoImage.sprite = _loseSprite;
            
            CanvasHandler.ShowCanvas(canvasGroup);
        }

        public void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}