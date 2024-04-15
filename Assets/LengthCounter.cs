using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LengthCounter : MonoBehaviour
{

    public TextMeshProUGUI text;

    public Color32 victoryColor = new Color32(0, 255, 0, 255);
    public Color32 defaultColor = new Color32(255, 255, 255, 255);

    public void UpdateLength(int length){
        text.text = "length  " + IntToRoman(length);

        if(length > 6){
            text.color = victoryColor;
        }
        else
        {
            text.color = defaultColor;
        }
    }

    public string IntToRoman(int num) {
    string romanResult = string.Empty;
    string[] romanLetters = {
        "M",
        "CM",
        "D",
        "CD",
        "C",
        "XC",
        "L",
        "XL",
        "X",
        "IX",
        "V",
        "IV",
        "I"
    };
    int[] numbers = {
        1000,
        900,
        500,
        400,
        100,
        90,
        50,
        40,
        10,
        9,
        5,
        4,
        1
    };
    int i = 0;
    while (num != 0) {
        if (num >= numbers[i]) {
            num -= numbers[i];
            romanResult += romanLetters[i];
        } else {
            i++;
        }
    }
    return romanResult;
}
}
