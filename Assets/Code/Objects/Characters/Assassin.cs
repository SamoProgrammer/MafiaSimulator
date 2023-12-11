using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Assassin : Character
{
    public AssassinStates assasinState = AssassinStates.Idle;
    [SerializeField] int killCooldown;
    float killTimer;
    [SerializeField] GameObject charactersGameObject;
    List<Character> characters;
    List<Character> killableCharacters = new List<Character>();
    Character victim;
    [SerializeField] GameObject prison;
    [SerializeField] GameObject prisonOutput;
    float jailTimer;
    [SerializeField] float jailTime;

    protected override void Start()
    {
        base.Start();
        characters = charactersGameObject.GetComponentsInChildren<Character>().ToList();
        killTimer = killCooldown;
        foreach (var citizen in characters)
        {
            if (citizen.tag == "Worker" || citizen.tag == "Miner" || citizen.tag == "Investor")
            {
                killableCharacters.Add(citizen);
            }
        }

    }

    protected override void PerformUpdate()
    {
        // assassin will not start killing after game started immediately

        switch (assasinState)
        {
            case AssassinStates.Idle:
                killTimer += Time.deltaTime;
                break;
            case AssassinStates.Killing:
                KillCharacter();
                break;
            case AssassinStates.Arrested:
                characterDestination = prison.transform.position;
                break;
            case AssassinStates.InPrison:
                jailTimer += Time.deltaTime;
                transform.position = prison.transform.position;
                if (jailTimer > jailTime)
                {
                    transform.position = prisonOutput.transform.position;
                    assasinState = AssassinStates.Idle;
                }
                break;
            default:
                break;
        }

        if (killTimer > killCooldown)
        {
            victim = killableCharacters[Random.Range(0, killableCharacters.Count)];
            if (assasinState == AssassinStates.Idle)
            {
                killTimer = 0;
                assasinState = AssassinStates.Killing;

            }
        }


    }

    public void KillCharacter()
    {

        characterDestination = victim.transform.position;
        if (Vector3.Distance(transform.position, victim.transform.position) < 1f)
        {
            victim.health = 0;
            victim.transform.Rotate(0, 0, -90f);
            victim.OnCharacterDeath();
            assasinState = AssassinStates.Idle;
        }

    }


}

