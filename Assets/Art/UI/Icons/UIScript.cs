using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIScript : MonoBehaviour
{
    Texture2D mainImage;
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



    public void SetUi(string tag, int money)
    {
        switch (tag)
        {
            case "Investor":
                mainImage = investorImage;
                break;
            case "Thief":
                mainImage = thiefImage;
                break;
            case "Bribe":
                mainImage = bribeThiefImage;
                tag = "Bribe Thief";
                break;
            case "Police":
                mainImage = policeImage;
                break;
            case "Worker":
                mainImage = workerImage;
                break;
            case "Miner":
                mainImage = minerImage;
                break;
            case "Assassin":
                mainImage = assassinImage;
                break;
            case "Doctor":
                mainImage = doctorImage;
                break;

        }

        citizenImage.texture = mainImage;
        roleText.text = tag;
        moneyText.text = money.ToString();
    }
}
