using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AccountManager : MonoBehaviour
{
    [SerializeField] private GameObject LoginPageUI;
    [SerializeField] private GameObject LobbyUI;

    private TMP_InputField[] inputFields;
    private TMP_InputField userNameField;
    private TMP_InputField passwordNameField;

    private Button[] buttons;
    private Button createAccountButton;
    private Button signInButton;
    private Button switchBetweenButton;

    private TMP_Text[] titleText;
    private TMP_Text createAccountText;
    private TMP_Text signInText;

    private const string newAccountSignifier = "0";
    private const string returningAccountSignifier = "1";

    private void Start()
    {
        inputFields = FindObjectsOfType<TMP_InputField>(true);
        buttons = FindObjectsOfType<Button>(true);
        titleText = FindObjectsOfType<TMP_Text>(true);

        foreach (TMP_InputField field in inputFields)
        {
            if (field.name == "InputField_AccountName")
            {
                userNameField = field;
            }
            else if (field.name == "InputField_Password")
            {
                passwordNameField = field;
            }
        }

        foreach (Button button in buttons)
        {
            if (button.name == "Button_CreateAccount")
            {
                createAccountButton = button;
            }
            else if (button.name == "Button_SignIn")
            {
                signInButton = button;
            }
            else if (button.name == "Button_SwitchBetween")
            {
                switchBetweenButton = button;
            }
        }

        foreach (TMP_Text text in titleText)
        {
            if (text.name == "Text_CreateAccount")
            {
                createAccountText = text;
            }
            else if (text.name == "Text_SignIn")
            {
                signInText = text;
            }
        }

        createAccountButton.onClick.AddListener(RegisterAccountInformation);
        signInButton.onClick.AddListener(SignInWithAccountInformation);
        switchBetweenButton.onClick.AddListener(SwitchUIState);
    }

    private void SwitchUIState()
    {
        if (createAccountText.IsActive())
        {
            createAccountText.gameObject.SetActive(false);
            createAccountButton.gameObject.SetActive(false);

            signInText.gameObject.SetActive(true);
            signInButton.gameObject.SetActive(true);

            switchBetweenButton.gameObject.GetComponentInChildren<TMP_Text>().text = "Create New Account";
        }
        else
        {
            createAccountText.gameObject.SetActive(true);
            createAccountButton.gameObject.SetActive(true);

            signInText.gameObject.SetActive(false);
            signInButton.gameObject.SetActive(false);

            switchBetweenButton.gameObject.GetComponentInChildren<TMP_Text>().text = "Sign In Instead";
        }
    }

    private void RegisterAccountInformation()
    {
        NetworkClient.Instance.SendMessageToServer(newAccountSignifier + "," + userNameField.text + "," + passwordNameField.text);
    }

    private void SignInWithAccountInformation()
    {
        NetworkClient.Instance.SendMessageToServer(returningAccountSignifier + "," + userNameField.text + "," + passwordNameField.text);
    }

    private void Update()
    {
        switch (StateManager.Instance.state)
        { 
            case StateManager.GameState.LOGINPAGE:
                if (!LoginPageUI.activeSelf)
                {
                    LoginPageUI.SetActive(true);
                }
                break;
            case StateManager.GameState.LOBBY:
                LoginPageUI.SetActive(false);
                LobbyUI.SetActive(true);
                break;
        }
    }
}