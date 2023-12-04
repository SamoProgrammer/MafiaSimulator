using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIScript : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI stateText;
    [SerializeField] Texture2D workerImage;
    [SerializeField] Texture2D minerImage;
    [SerializeField] Texture2D thiefImage;
    [SerializeField] Texture2D bribeThiefImage;
    [SerializeField] Texture2D assassinImage;
    [SerializeField] Texture2D doctorImage;
    [SerializeField] Texture2D policeImage;
    [SerializeField] Texture2D investorImage;
    [SerializeField] RawImage citizenImage;
    [SerializeField] TextMeshProUGUI roleText;
    [SerializeField] TextMeshProUGUI moneyText;



    public void SetUi(Character character)
    {
        Texture2D mainImage = workerImage;
        string characterState = "";
        switch (character.tag)
        {
            case "Investor":
                mainImage = investorImage;
                characterState = character.GetComponent<Investor>().investorStates.ToString();
                break;
            case "Thief":
                mainImage = thiefImage;
                characterState = character.GetComponent<Thief>().thiefState.ToString();
                break;
            case "Bribe":
                mainImage = bribeThiefImage;
                tag = "Bribe Thief";
                characterState = character.GetComponent<Thief>().thiefState.ToString();
                break;
            case "Police":
                mainImage = policeImage;
                characterState = character.GetComponent<Police>().policeState.ToString();
                break;
            case "Worker":
                mainImage = workerImage;
                characterState = character.GetComponent<Investor>().investorStates.ToString();
                break;
            case "Miner":
                mainImage = minerImage;
                characterState = character.GetComponent<Investor>().investorStates.ToString();
                break;
            case "Assassin":
                mainImage = assassinImage;
                characterState = character.GetComponent<Investor>().investorStates.ToString();
                break;
            case "Doctor":
                mainImage = doctorImage;
                characterState = character.GetComponent<Investor>().investorStates.ToString();
                break;

        }

        citizenImage.texture = mainImage;
        roleText.text = tag;
        moneyText.text = character.money.ToString();
    }
}
