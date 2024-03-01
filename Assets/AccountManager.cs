using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AccountManager : MonoBehaviour
{
    private TMP_InputField[] inputFields;
    private TMP_InputField newUserNameField;
    private TMP_InputField newPasswordNameField;

    private Button[] buttons;
    private Button createAccountButton;
    private Button SwitchToLoginButton;

    private const string newAccountSignifier = "0";

    private void Start()
    {
        inputFields = FindObjectsOfType<TMP_InputField>(true);
        buttons = FindObjectsOfType<Button>(true);

        foreach (TMP_InputField field in inputFields)
        {
            if (field.name == "InputField_AccountName")
            {
                newUserNameField = field;
            }
            else if (field.name == "InputField_Password")
            {
                newPasswordNameField = field;
            }
        }

        foreach (Button button in buttons)
        {
            if (button.name == "Button_CreateAccount")
            {
                createAccountButton = button;
            }
            else if (button.name == "Button_SwitchToLogin")
            {
                SwitchToLoginButton = button;
            }
        }

        createAccountButton.onClick.AddListener(RegisterAccountInformation);
    }

    private void RegisterAccountInformation()
    {
        NetworkClient.Instance.SendMessageToServer(newAccountSignifier + "," + newUserNameField.text + "," + newPasswordNameField.text);
    }
}