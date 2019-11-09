using System.Reflection;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class CardDisplay : MonoBehaviour
{
    public object[] positiveCards;
    public object[] negativeCards;
    public object[] neutralCards;

    public int id;
    public Card card;
    
    public Image symbolImage;
    public Button button;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;
    public new SpriteRenderer renderer;

    private UnityAction _myAction;
    private PlayerSettings gameManager;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<PlayerSettings>();

        positiveCards = Resources.LoadAll("Cards/Positive");
        negativeCards = Resources.LoadAll("Cards/Negative");
        neutralCards = Resources.LoadAll("Cards/Neutral");
    }

    private void OnEnable()
    {
        Debug.Log("ID: " + id);
        if(gameManager.cardType == 0) 
            card = positiveCards[UnityEngine.Random.Range(0, positiveCards.Length)] as Card;
        if(gameManager.cardType == 1)
            card = negativeCards[UnityEngine.Random.Range(0, negativeCards.Length)] as Card;
        if(gameManager.cardType == 2)
            card = neutralCards[UnityEngine.Random.Range(0, neutralCards.Length)] as Card;

        _myAction = () => { InvokeAction(card.methodName); };

        symbolImage.sprite = card.symbol;
        nameText.text = card.name;
        descriptionText.text = card.description;
        renderer.material = card.material;
        button.onClick.AddListener(_myAction);
        button.onClick.AddListener(null);
    }

    private void InvokeAction(string action)
    {
        GameObject gameManager = GameObject.Find("GameManager");
        UpgradeSettings us = gameManager.GetComponent<UpgradeSettings>();

        string methodName = GetMethodName(action);
        MethodInfo method = us.GetType().GetMethod(methodName);
        object[] functionParameters = GetMethodParameters(action, method.GetParameters());

        method.Invoke(us, functionParameters);
        Debug.Log("Button: " + button.transform.parent.parent);
        //PlayerSettings.instance.upgradeTime = false;
    }

    private string GetMethodName(string eventData)
    {
        string eventName = eventData.Substring(0, eventData.IndexOf("("));
        return eventName;
    }

    private object[] GetMethodParameters(string eventData, ParameterInfo[] parameterInfos)
    {
        object[] parameters = new object[parameterInfos.Length];

        string parametersText = eventData.Substring(eventData.IndexOf("(")).Replace("(", "").Replace(")", "");

        for(int i = 0; i < parameterInfos.Length; i++)
        {
            int nextParameterTextLength = parametersText.Contains(",") ? parametersText.IndexOf(",") : parametersText.Length;
            string nextParameter = parametersText.Substring(0, nextParameterTextLength);

            parameters[i] = Convert.ChangeType(nextParameter, parameterInfos[i].ParameterType);

            parametersText = parametersText.Substring(nextParameterTextLength);
        }

        return parameters;
    }
}
//Mati#6862 <--- Great guy