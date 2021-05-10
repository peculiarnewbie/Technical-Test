using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeConversion : MonoBehaviour
{
    [SerializeField] private InputField timeInput;
    [SerializeField] Text timeConverted;
    [SerializeField] GameObject errorText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

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



    }

    private bool CheckInputValidity(string inputString)
    {
        bool validInput = true;

        if (inputString.Length < 9)
            return !validInput;

        //Debug.Log(inputString.Length);

        for(int i = 0; i<inputString.Length; i++)
        {
            bool tempValidCheck = false;

            switch (i)
            {
                case 0:
                    if (inputString[i] == 48 || inputString[i] == 49)
                        tempValidCheck = true;
                    break;
                case 1:
                    if (inputString[0] == 48)
                        tempValidCheck = true;
                    else if(inputString[i] > 47 && inputString[i] < 51)
                        tempValidCheck = true;
                    break;
                case 3:
                case 4:
                case 6:
                case 7:
                    if (inputString[i] > 47 && inputString[i] < 58)
                        tempValidCheck = true;
                    break;
                case 2:
                case 5:
                    if (inputString[i] == 58)
                        tempValidCheck = true;
                    break;
                case 8:
                    if (inputString[i] == 65 || inputString[i] == 80)
                        tempValidCheck = true;
                    break;
                case 9:
                    if (inputString[i] == 77)
                        tempValidCheck = true;
                    break;
                default:
                    break;
            }

            //Debug.Log(inputString[i] + " " + tempValidCheck);

            if (!tempValidCheck)
            {
                validInput = false;
                break;
            }

        }

        return validInput;
    }
}
