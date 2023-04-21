using UnityEngine;

[CreateAssetMenu(fileName = "Move", menuName = "Pokemon/Create new move")]
public class MoveBase : ScriptableObject
{
    [SerializeField]
    private string name;

    [TextArea]
    [SerializeField]
    private string description;

    [SerializeField]
    private PokemonType type;

    [SerializeField]
    private int power;

    [SerializeField]
    private int accuracy;

    [SerializeField]
    private int pp;

    // Properties
    public string Name => name;
    public string Description => description;
    public PokemonType Type => type;
    public int Power => power;
    public int Accuracy => accuracy;
    public int Pp => pp;

    public bool IsSpecial
    {
        get
        {
            if (type is PokemonType.Fire or PokemonType.Water or PokemonType.Grass or PokemonType.Ice or PokemonType.Electric or PokemonType.Dragon)
            {
                return true;
            }

            return false;
        }
    }
}
