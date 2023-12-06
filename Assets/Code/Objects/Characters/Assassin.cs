using System.Collections.Generic;
using System.Linq;
using UnityEngine;


class Assassin : Character
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
        if (assasinState == AssassinStates.Idle)
        {
            killTimer += Time.deltaTime;
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
        if (assasinState == AssassinStates.Killing)
        {
            KillCharacter();
        }
        if (assasinState == AssassinStates.InPrison)
        {
            characterDestination = prison.transform.position;
            jailTimer += Time.deltaTime;
            transform.position = prison.transform.position;
            if (jailTimer > jailTime)
            {
                transform.position = prisonOutput.transform.position;
                assasinState = AssassinStates.Idle;
            }
        }

    }

    public void KillCharacter()
    {

        characterDestination = victim.transform.position;
        movementEnabled = true;
        if (Vector3.Distance(transform.position, victim.transform.position) < 1f)
        {
            victim.health = 0;
            victim.transform.Rotate(0, 0, -90f);
            victim.OnCharacterDeath();
            assasinState = AssassinStates.Idle;
            movementEnabled = false;
        }


    }

}

