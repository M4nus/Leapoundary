using System.Reflection;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class CardDisplay : MonoBehaviour
{
    
    public Card card;
    
    public Image symbolImage;
    public Button button;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;
    public new Image renderer;

    private UnityAction _myAction;
    private PlayerSettings gameManager;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<PlayerSettings>();
    }

    private void OnEnable()
    {
        RandomizeCards();

        _myAction = () => { InvokeAction(card.methodName); };

        symbolImage.sprite = card.symbol;
        titleText.text = card.name;
        descriptionText.text = card.description;
        renderer.material = card.material;

        StartCoroutine(Dissolve());
        button.interactable = true;
        button.onClick.AddListener(_myAction);
        button.onClick.AddListener(null);
    }

    private void OnDisable()
    {
        gameManager.ResetCardList();
    }

    private void InvokeAction(string action)
    {
        GameObject gameManager = GameObject.Find("GameManager");
        UpgradeSettings us = gameManager.GetComponent<UpgradeSettings>();

        string methodName = GetMethodName(action);
        MethodInfo method = us.GetType().GetMethod(methodName);
        object[] functionParameters = GetMethodParameters(action, method.GetParameters());

        method.Invoke(us, functionParameters);
        PlayerSettings.instance.upgradeTime = false;
        button.interactable = false;
        StartCoroutine(Solve());
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

    public void RandomizeCards()
    {
        if(gameManager.cardType == 0)
        {
            int index = UnityEngine.Random.Range(0, gameManager.positiveCards.Count);
            card = gameManager.positiveCards[index] as Card;
            gameManager.positiveCards.Remove(gameManager.positiveCards[index]);
        }
        if(gameManager.cardType == 1)
        {
            int index = UnityEngine.Random.Range(0, gameManager.negativeCards.Count);
            card = gameManager.negativeCards[index] as Card;
            gameManager.negativeCards.Remove(gameManager.negativeCards[index]);
        }
        if(gameManager.cardType == 2)
        {
            int index = UnityEngine.Random.Range(0, gameManager.neutralCards.Count);
            card = gameManager.neutralCards[index] as Card;
            gameManager.neutralCards.Remove(gameManager.neutralCards[index]);
        }
    }

    public IEnumerator Dissolve()
    {
        float alpha = renderer.material.GetFloat("Vector1_43A1A6D");
        while(alpha <= 0.5f)
        {
            renderer.material.SetFloat("Vector1_43A1A6D", alpha);
            titleText.alpha = alpha + 1.1f;
            descriptionText.alpha = alpha + 1.1f;
            symbolImage.color = new Vector4(renderer.color.r, renderer.color.g, renderer.color.b, alpha + 1.1f);
            alpha += Time.deltaTime * 2;
            yield return null;
        }
    }

    public IEnumerator Solve()
    {
        float alpha = renderer.material.GetFloat("Vector1_43A1A6D");
        while(alpha >= -1.1f)
        {
            renderer.material.SetFloat("Vector1_43A1A6D", alpha);
            if((alpha + 0.6f) > 0f)
            {
                titleText.alpha = alpha + 0.6f;
                descriptionText.alpha = alpha + 0.6f;
                symbolImage.color = new Vector4(renderer.color.r, renderer.color.g, renderer.color.b, alpha + 0.6f);
            }
            alpha -= Time.deltaTime * 2;
            yield return null;
        }
        PlayerSettings.instance.cardsDissolved = true;
    }
}
//Mati#6862 <--- Great guy