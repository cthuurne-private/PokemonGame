using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private PlayerController playerController;

    [SerializeField]
    private BattleSystem battleController;

    [SerializeField]
    private Camera worldCamera;

    private GameState state;

    private void Start()
    {
        playerController.OnEncountered += StartBattle;
        battleController.OnBattleOver += EndBattle;
    }

    private void Update()
    {
        if (state == GameState.FreeRoam)
        {
            playerController.HandleUpdate();
        }
        else if (state == GameState.Battle)
        {
            battleController.HandleUpdate();
        }
    }

    private void StartBattle()
    {
        state = GameState.Battle;
        battleController.gameObject.SetActive(true);
        worldCamera.gameObject.SetActive(false);

        var playerParty = playerController.GetComponent<PokemonParty>();
        var wildPokemon = FindObjectOfType<MapArea>().GetComponent<MapArea>().GetRandomWildPokemon();

        battleController.StartBattle(playerParty, wildPokemon);
    }

    private void EndBattle(bool won)
    {
        state = GameState.FreeRoam;
        battleController.gameObject.SetActive(false);
        worldCamera.gameObject.SetActive(true);
    }
}

public enum GameState
{
    FreeRoam,
    Battle
}
