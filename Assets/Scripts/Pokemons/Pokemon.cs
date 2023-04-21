using System;
using System.Collections.Generic;
using Assets.Scripts.Battle;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class Pokemon
{
    [SerializeField]
    private PokemonBase _base;

    [SerializeField]
    private int _level;

    public PokemonBase Base => _base;
    public int Level => _level;
    public List<Move> Moves { get; set; }
    public int HP { get; set; }

    public void Init()
    {
        HP = MaxHp;

        // Generate moves
        Moves = new List<Move>();

        foreach (var move in Base.LearnableMoves)
        {
            if (move.Level <= Level)
            {
                Moves.Add(new Move(move.MoveBase));
            }

            if (Moves.Count >= 4)
            {
                break;
            }
        }
    }

    public int Attack => Mathf.FloorToInt(Base.Attack * Level / 100f) + 5;
    public int Defense => Mathf.FloorToInt(Base.Defense * Level / 100f) + 5;
    public int SpecialAttack => Mathf.FloorToInt(Base.SpecialAttack * Level / 100f) + 5;
    public int SpecialDefense => Mathf.FloorToInt(Base.SpecialDefense * Level / 100f) + 5;
    public int Speed => Mathf.FloorToInt(Base.Speed * Level / 100f) + 5;
    public int MaxHp => Mathf.FloorToInt(Base.MaxHp * Level / 100f) + 10;

    public DamageDetails TakeDamage(Move move, Pokemon attacker)
    {
        // Critical hits
        var critical = 1f;
        if (Random.value * 100f <= 6.25f)
        {
            critical = 2f;
        }

        // TypeEffectiveness effectiveness
        var type = TypeChart.GetEffectiveness(move.Base.Type, Base.Type1) * TypeChart.GetEffectiveness(move.Base.Type, Base.Type2);

        // DamageDetails
        var damageDetails = new DamageDetails
        {
            TypeEffectiveness = type,
            Critical = critical,
            isFainted = false
        };

        var attack = move.Base.IsSpecial ? attacker.SpecialAttack : attacker.Attack;
        var defense = move.Base.IsSpecial ? attacker.SpecialDefense : attacker.Defense;

        var modifiers = Random.Range(0.85f, 1f) * type * critical;
        var a = (2 * attacker.Level + 10) / 250f;
        var d = a * move.Base.Power * ((float)attack / defense) + 2;
        var damage = Mathf.FloorToInt(d * modifiers);

        HP -= damage;
        if (HP <= 0)
        {
            HP = 0;
            damageDetails.isFainted = true;
        }

        return damageDetails;
    }

    public Move GetRandomMove()
    {
        var random = Random.Range(0, Moves.Count);
        return Moves[random];
    }
}
