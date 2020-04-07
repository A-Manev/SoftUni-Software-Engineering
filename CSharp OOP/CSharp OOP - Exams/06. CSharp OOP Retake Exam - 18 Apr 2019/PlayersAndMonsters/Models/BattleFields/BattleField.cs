namespace PlayersAndMonsters.Models.BattleFields
{
    using System;
    using System.Linq;

    using Contracts;
    using Players;
    using Players.Contracts;

    public class BattleField : IBattleField
    {
        private const int DEFAULT_DAMAGE_POINTS_FOR_BEGINNER = 30;
        private const int DEFAULT_HEALTH_POINTS_FOR_BEGINNER = 40;

        public void Fight(IPlayer attackPlayer, IPlayer enemyPlayer)
        {
            if (attackPlayer.IsDead || enemyPlayer.IsDead)
            {
                throw new ArgumentException("Player is dead!");
            }

            CheckForBeginners(attackPlayer, enemyPlayer);

            attackPlayer.Health += attackPlayer.CardRepository.Cards.Sum(x => x.HealthPoints);
            enemyPlayer.Health += enemyPlayer.CardRepository.Cards.Sum(x => x.HealthPoints);

            while (true)
            {
                var attackPlayerTotalDamagePoints = attackPlayer.CardRepository.Cards.Sum(d => d.DamagePoints);
                enemyPlayer.TakeDamage(attackPlayerTotalDamagePoints);

                if (enemyPlayer.Health == 0)
                {
                    break;
                }

                var enemyPlayerTotalDamagePoints = enemyPlayer.CardRepository.Cards.Sum(d => d.DamagePoints);
                attackPlayer.TakeDamage(enemyPlayerTotalDamagePoints);

                if (attackPlayer.Health == 0)
                {
                    break;
                }
            }
        }

        private static void CheckForBeginners(IPlayer attackPlayer, IPlayer enemyPlayer)
        {
            if (enemyPlayer.GetType() == typeof(Beginner))
            {
                enemyPlayer.Health += DEFAULT_HEALTH_POINTS_FOR_BEGINNER;

                foreach (var card in enemyPlayer.CardRepository.Cards)
                {
                    card.DamagePoints += DEFAULT_DAMAGE_POINTS_FOR_BEGINNER;
                }
            }

            if (attackPlayer.GetType() == typeof(Beginner))
            {
                attackPlayer.Health += DEFAULT_HEALTH_POINTS_FOR_BEGINNER;

                foreach (var card in attackPlayer.CardRepository.Cards)
                {
                    card.DamagePoints += DEFAULT_DAMAGE_POINTS_FOR_BEGINNER;
                }
            }
        }
    }
}
