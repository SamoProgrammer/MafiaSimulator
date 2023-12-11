using Unity.VisualScripting;
using UnityEngine;

public class PolicePOV : MonoBehaviour
{
    Police police;
    private void Start()
    {
        police = GetComponentInParent<Police>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Thief" || other.tag == "Bribe" || other.tag == "Assassin")
        {
            Debug.Log(other.name);
            if (other.tag == "Assassin")
            {

                police.assassinScript = other.GetComponent<Assassin>();
                if (police.assassinScript.assasinState == AssassinStates.Killing)
                {
                    if (police.policeState == PoliceStates.Patroling)
                    {
                        police.policeState = PoliceStates.Chasing;
                        police.suspect = other.GameObject();
                    }
                }
            }
            else if (other.tag == "Thief" || other.tag == "Bribe")
            {
                police.thiefScript = other.GetComponent<Thief>();
                if (police.thiefScript.tag == "Bribe" && police.thiefScript.money > police.bribeAmount)
                {
                    if (police.bribeCooldownTimer > police.bribeCoolDown)
                    {
                        police.money += police.bribeAmount;
                        police.thiefScript.money -= police.bribeAmount;
                        police.bribeCooldownTimer = 0;
                    }
                    else
                    {
                        if (police.thiefScript.thiefState == ThiefStates.StealingMoney || police.thiefScript.thiefState == ThiefStates.GoingToStealMoney)
                        {
                            if (police.policeState == PoliceStates.Patroling)
                            {
                                police.policeState = PoliceStates.Chasing;
                                police.suspect = other.GameObject();
                            }
                        }
                    }

                }
                else
                {
                    if (police.thiefScript.thiefState == ThiefStates.StealingMoney || police.thiefScript.thiefState == ThiefStates.GoingToStealMoney || police.assassinScript.assasinState == AssassinStates.Killing)
                    {
                        if (police.policeState == PoliceStates.Patroling)
                        {
                            police.policeState = PoliceStates.Chasing;
                            police.suspect = other.GameObject();
                        }
                    }
                }
            }


        }
    }

}

