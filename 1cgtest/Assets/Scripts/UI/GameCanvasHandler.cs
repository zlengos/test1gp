using System.Globalization;
using TMPro;
using Tools;
using UnityEngine;

namespace UI
{
    public class GameCanvasHandler : MonoBehaviour
    {
        #region Fields

        [SerializeField] private Player.Player player;
        [SerializeField] private TextMeshProUGUI healthText;

        #endregion

        #region Listening

        private void OnEnable()
        {
            EventManager.OnPlayerDamaged += OnPlayerDamaged;
        }

        private void OnDisable()
        {
            EventManager.OnPlayerDamaged -= OnPlayerDamaged;
        }

        private void Start()
        {
            healthText.text = (player.PlayerHealth).ToString(CultureInfo.InvariantCulture);
        }

        private void OnPlayerDamaged(int damage)
        {
            healthText.text = (player.PlayerHealth -1).ToString(CultureInfo.InvariantCulture);
        }

        #endregion
    }
}