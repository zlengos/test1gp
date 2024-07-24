using System;

namespace Tools
{
    public static class EventManager
    {
        public static Action OnEnemiesDestroyed;
        public static Action<int> OnPlayerDamaged;
        public static Action OnPlayerDead;
        public static Action OnEnemyDead;
    }
}