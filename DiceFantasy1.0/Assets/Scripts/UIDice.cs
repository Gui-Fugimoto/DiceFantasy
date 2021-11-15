using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDice : MonoBehaviour
{
    [SerializeField]
    Dice dice;

    private Image img;
    private Sprite[] imageSides;

    // Start is called before the first frame update
    void Start()
    {
        //img = GetComponent<Image>();
       // imageSides = Resources.LoadAll<Sprite>("Dies/");
        //img.sprite = imageSides[5];
    }

    // Update is called once per frame
    void Update()
    {
        //if (dice.diceValue > 0)
       // {
        //    img.sprite = imageSides[dice.diceValue];
        //}
    }
}
