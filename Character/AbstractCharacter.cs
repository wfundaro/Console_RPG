using DevoirMaison2021.BoardContent;
using DevoirMaison2021.Power;
using System;
using System.Collections.Generic;

namespace DevoirMaison2021.Character
{
    abstract class AbstractCharacter
    {
        public Board Board { get; set; }
        public string Name { get; }
        public StatsCharacter BaseStats { get; } = new StatsCharacter();
        public StatsCharacter CurrentStats { get; } = new StatsCharacter();
        public int NbDamageTaken { get; set; } = 0;
        public (int, int) AttackRandomRange { get; set; } = (1, 100);
        public (int, int) DefenseRandomRange { get; set; } = (1, 100);
        public List<Modifier> Modifiers { get; set; } = new List<Modifier>();
        public IPower Power { get; set; }
        public bool IsHidden { get; set; } = false;
        public bool CanUseCamouflage { get; set; } = false;
        public long TimeBeforeAttack { get; set; }
        public long TimeBeforePower { get; set; }
        public enum DegatType { NORMAL, SACRE, IMPIE };
        public DegatType TypeDeDegat { get; set; } = DegatType.NORMAL;
        public float PercentNormalDamage { get; set; } = 1.0f;
        public float PercentPoisonDamage { get; set; } = 0.0f;
        public int PoisonRate { get; set; } = 0;
        public long TimePoisonDamage { get; set; }
        public const long TIME_TO_APPLY_POISON = 5000;
        public readonly int MAX_JET_VITESSE = 101;
        public ConsoleColor color = ConsoleColor.Yellow;
        public event EventHandler EventDeadHandler;
        public AbstractCharacter(string name, int attack, int defense, float attackSpeed, int damages, int maximumLife, int currentLife, float powerSpeed, IPower _power)
        {
            Name = name;
            BaseStats.Attack = attack;
            BaseStats.Defense = defense;
            BaseStats.AttackSpeed = attackSpeed;
            BaseStats.Damages = damages;
            BaseStats.MaximumLife = maximumLife;
            BaseStats.CurrentLife = currentLife;
            BaseStats.PowerSpeed = powerSpeed;
            CurrentStats.Attack = attack;
            CurrentStats.Defense = defense;
            CurrentStats.AttackSpeed = attackSpeed;
            CurrentStats.Damages = damages;
            CurrentStats.MaximumLife = maximumLife;
            CurrentStats.CurrentLife = currentLife;
            CurrentStats.PowerSpeed = powerSpeed;
            Power = _power;
        }
        public virtual void UsePower()
        {
            this.Power?.UsePower(this, Board);
        }
        public virtual void SpecialAttack(BoardCharacter target, int damageInflicted)
        {
            // aucune attaque spéciale de base
        }
        public virtual void LaunchAttack()
        {
            BoardCharacter target = Board.GetRandomCharacterNoDead(this);
            int attackJet = CurrentStats.Attack + Board.Random.Next(AttackRandomRange.Item1, AttackRandomRange.Item2 + 1);
            if (target != null)
            {
                MyLog($"{Name} attaque {target.Character.Name}");
                int damageInflicted = target.Character.Launchdefense(this, attackJet);
                target.Character.IsHidden = false;
                SpecialAttack(target, damageInflicted);
            }
            else
            {
                MyLog($"{Name} ne trouve personne à attaquer");
            }
        }
        public virtual void InflictedHitDelay(int normalDamage)
        {
            Modifier hitDelay = new Modifier(Modifier.EnumModifierType.HIT_DELAY, normalDamage);
            Modifiers.Add(hitDelay);
        }

        public virtual void InflictedPoison(AbstractCharacter attacker, int totalDamage)
        {
            int poisonDamage = (int)(totalDamage * attacker.PercentPoisonDamage);
            if (poisonDamage > 0)
            {
                PoisonRate += poisonDamage;
                MyLog($"{Name} est empoisonné son taux d'empoisonnement est de {PoisonRate}");
            }
        }
        public virtual void InflictedSpecial(AbstractCharacter attacker, int normalDamage)
        {
            // Aucun coup spécial infligé de base
        }
        public virtual int Launchdefense(AbstractCharacter attacker, int attackJet)
        {
            int defenseJet = CurrentStats.Defense + Board.Random.Next(DefenseRandomRange.Item1, DefenseRandomRange.Item2 + 1);
            int attackMarge = attackJet - defenseJet;
            int normalDamage = 0;
            IsHidden = false;
            if (attackMarge > 0)
            {
                int totalDamage = (attackMarge * attacker.CurrentStats.Damages) / 100;
                normalDamage = (int)(totalDamage * attacker.PercentNormalDamage);
                CurrentStats.CurrentLife -= normalDamage;
                MyLog($"{Name} avec une marge {attackMarge} subit {normalDamage} de dommages normaux, vie actuelle {CurrentStats.CurrentLife}");
                InflictedPoison(attacker, totalDamage);
                InflictedSpecial(attacker, normalDamage);
                if (CurrentStats.CurrentLife <= 0)
                {
                    CharacterDead();
                }
                else
                {
                    InflictedHitDelay(normalDamage);
                }
            }
            else
            {
                MyLog($"{Name} résiste à l'attaque -> jet de défense {defenseJet} contre jet attaque {attackJet} sa vie reste à {CurrentStats.CurrentLife}");
            }
            NbDamageTaken += normalDamage;
            return normalDamage;
        }
        public virtual void LaunchAction(long deltaTime)
        {
            // On vérifie si on subit des effets de poison
            if (PoisonRate > 0)
            {
                TimePoisonDamage += deltaTime;
                if (TimePoisonDamage >= TIME_TO_APPLY_POISON)
                {
                    MyLog($"╔╩╦ {Name} subit {PoisonRate} dommages de poison");
                    CurrentStats.CurrentLife -= PoisonRate;
                    NbDamageTaken += PoisonRate;
                    TimePoisonDamage = 0;
                    if (CurrentStats.CurrentLife <= 0)
                    {
                        CharacterDead();
                        return;
                    }
                }
            }
            TimeBeforePower += deltaTime;
            long responseTimePower = (long)((1000 / CurrentStats.PowerSpeed) - Board.Random.Next(1, MAX_JET_VITESSE));
            // Si on peut lancer un sort
            if (responseTimePower <= TimeBeforePower)
            {
                MyLog($"­░ {responseTimePower} ms ░­ {Name} utilise son pouvoir ░░░");
                UsePower();
                TimeBeforePower = 0;
            }

            // On ajoute les modifier éventuels (hit delay ou attack speed)
            long responseTimeAttack = 0;
            float modificationAttackSpeed = 0.0f;
            List<Modifier> copyModifiers = new List<Modifier>(Modifiers);
            copyModifiers.ForEach(m =>
            {
                switch (m.ModifierType)
                {
                    case Modifier.EnumModifierType.HIT_DELAY:
                        responseTimeAttack += (int)m.value;
                        Modifiers.Remove(m);
                        break;
                    case Modifier.EnumModifierType.ATTACK_SPEED:
                        m.Time += deltaTime;
                        if (m.Time >= m.Duration)
                        {
                            Modifiers.Remove(m);
                        }
                        else
                        {
                            modificationAttackSpeed += (float)m.value;
                        }
                        break;
                }
            });
            TimeBeforeAttack += deltaTime;
            responseTimeAttack += (long)((1000 / (CurrentStats.AttackSpeed + modificationAttackSpeed)) - Board.Random.Next(1, MAX_JET_VITESSE));
            // Si on peut lancer une attaque
            if (responseTimeAttack <= TimeBeforeAttack)
            {
                MyLog($"{responseTimeAttack} ms il est temps de lancer une attaque");
                LaunchAttack();
                TimeBeforeAttack = 0;
            }
        }
        public virtual void OtherCharacterIsDead(AbstractCharacter characterDead) { }
        public void CharacterDead()
        {
            EventDead(EventArgs.Empty);
        }
        protected virtual void EventDead(EventArgs e)
        {
            EventDeadHandler?.Invoke(this, e);
        }

        public void MyLog(string text)
        {
            if (Board.ShowLog)
            {
                Console.ForegroundColor = color;
                Console.WriteLine($"Board {Board.NumberBoard} {text}");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }
}
