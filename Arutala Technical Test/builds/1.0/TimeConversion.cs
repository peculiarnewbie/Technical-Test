using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeConversion : MonoBehaviour
{
    [SerializeField] InputField timeInput;
    [SerializeField] Text timeConverted;
    [SerializeField] GameObject errorText;

    public void ConvertTime()
    {
        string inputString = timeInput.text;

        if (CheckInputValidity(inputString))
        {
            errorText.SetActive(false);
        }
        else
        {
            errorText.SetActive(true);
            return;
        }

        char[] charTemp = inputString.ToCharArray();
        int hourTemp = (int)char.GetNumericValue(charTemp[0]) * 10 + (int)char.GetNumericValue(charTemp[1]);

        if (charTemp[8] == 'A' || charTemp[8] == 'a')
        {
            if (hourTemp == 12)
                hourTemp = 0;
        }
        else
        {
            if (hourTemp != 12)
                hourTemp += 12;
        }

        int tens = hourTemp / 10;
        charTemp[0] = (char)('0' + tens);
        charTemp[1] = (char)('0' + hourTemp - tens * 10);
       
        string newString = new string(charTemp,  0, 8);
        timeConverted.text = newString;

    }

    private bool CheckInputValidity(string inputString)
    {
        if (inputString.Length != 10)
            return false;

        //Debug.Log(inputString.Length);

        for(int i = 0; i < 10; i++)
        {
            bool tempValidCheck = false;
            char currentCharacter = inputString[i];

            switch (i)
            {
                case 0:
                    if (currentCharacter == '0' || currentCharacter == '1')
                        tempValidCheck = true;
                    break;
                case 1:
                    if (inputString[0] == '0')
                    {
                        if (currentCharacter != '0')
                            tempValidCheck = true;
                    } 
                    else if(currentCharacter >= '0' && currentCharacter <= '2')
                        tempValidCheck = true;
                    break;
                case 3:
                case 4:
                case 6:
                case 7:
                    if (currentCharacter >= '0' && currentCharacter <= '9')
                        tempValidCheck = true;
                    break;
                case 2:
                case 5:
                    if (currentCharacter == ':')
                        tempValidCheck = true;
                    break;
                case 8:
                    if (currentCharacter == 'P' || currentCharacter == 'p' || currentCharacter == 'A' || currentCharacter == 'a')
                        tempValidCheck = true;
                    break;
                case 9:
                    if (currentCharacter == 'M' || currentCharacter == 'm')
                        tempValidCheck = true;
                    break;
                default:
                    break;
            }

            //Debug.Log(inputString[i] + " " + tempValidCheck);

            if (!tempValidCheck)
            {
                return false;
            }

        }

        return true;
    }
}
