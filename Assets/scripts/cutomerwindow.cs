using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerWindow : MonoBehaviour, IInteractable
{
    [Header("Player")]
    [SerializeField] private PlayerInventory playerInventory;

    [Header("Order")]
    [SerializeField] private int minimumIngredients = 2;
    [SerializeField] private int maximumIngredients = 3;

    [Header("Ingredient Pools")]
    [SerializeField] private ObjectPool vegetablePool;
    [SerializeField] private ObjectPool cheesePool;
    [SerializeField] private ObjectPool meatPool;

    [Header("Order UI")]
    [SerializeField] private GameObject orderUI;
    [SerializeField] private TMPro.TMP_Text orderText;
    [Header("Score Popup")]
    [SerializeField] private GameObject scorePopup;
    [SerializeField] private TMPro.TMP_Text scorePopupText;
    [SerializeField] private float scorePopupDuration = 2f;
    [Header("Score")]
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private GameManager gameManager;
    private List<IngredientType> order =
        new List<IngredientType>();

    private List<IngredientType> originalOrder =
        new List<IngredientType>();

    private float orderStartTime;

    private bool isWaitingForNewOrder;


    public void CreateOrder()
    {
        order.Clear();
        originalOrder.Clear();

        int orderSize = Random.Range(
            minimumIngredients,
            maximumIngredients + 1
        );

        for (int i = 0; i < orderSize; i++)
        {
            int randomIngredient = Random.Range(0, 3);

            IngredientType ingredient =
                (IngredientType)randomIngredient;

            order.Add(ingredient);
            originalOrder.Add(ingredient);
        }

 
        orderStartTime = Time.time;

        isWaitingForNewOrder = false;

        UpdateOrderUI();

        Debug.Log(
            gameObject.name +
            " Order: " +
            GetOrderText()
        );
    }


    private void Update()
    {
        if (!gameManager.IsGameRunning)
            return;

       
        if (isWaitingForNewOrder)
            return;

        if (order.Count == 0)
            return;

        UpdateOrderUI();
    }


    private void UpdateOrderUI()
    {
        if (orderText == null)
            return;

        if (order.Count == 0)
        {
            orderText.text = "ORDER COMPLETE";
            return;
        }

        int elapsedSeconds =
            Mathf.FloorToInt(Time.time - orderStartTime);

        string text = "ORDER\n";

        for (int i = 0; i < order.Count; i++)
        {
            text += order[i].ToString();

            if (i < order.Count - 1)
            {
                text += "\n";
            }
        }

        text += "\n\nTIME: " + elapsedSeconds + "s";

        orderText.text = text;
    }


    public void Interact()
    {
        if (!gameManager.IsGameRunning)
            return;

        if (!playerInventory.IsHolding)
        {
            Debug.Log("You are not holding an ingredient.");
            return;
        }

        GameObject ingredient =
            playerInventory.GetHeldIngredient();

        Ingredient ingredientData =
            ingredient.GetComponent<Ingredient>();

        if (ingredientData == null)
        {
            Debug.Log("Invalid ingredient.");
            return;
        }

        IngredientType deliveredIngredient =
            ingredientData.Type;

        if ((deliveredIngredient == IngredientType.Vegetable ||
             deliveredIngredient == IngredientType.Meat) &&
            !ingredientData.IsPrepared)
        {
            Debug.Log(
                deliveredIngredient +
                " is not prepared yet."
            );

            return;
        }

        
        int orderIndex =
            order.IndexOf(deliveredIngredient);

        if (orderIndex == -1)
        {
            Debug.Log(
                "Customer does not need " +
                deliveredIngredient
            );

            return;
        }

        order.RemoveAt(orderIndex);

        
        UpdateOrderUI();

       
        playerInventory.ClearHand();

        ReturnIngredientToPool(ingredient);

        Debug.Log(
            "Delivered " +
            deliveredIngredient +
            " to " +
            gameObject.name
        );

      
        if (order.Count == 0)
        {
            CompleteOrder();
        }
        else
        {
            Debug.Log(
                "Remaining order: " +
                GetOrderText()
            );
        }
    }


    private void ReturnIngredientToPool(GameObject ingredient)
    {
        Ingredient ingredientData =
            ingredient.GetComponent<Ingredient>();

        switch (ingredientData.Type)
        {
            case IngredientType.Vegetable:
                vegetablePool.Return(ingredient);
                break;

            case IngredientType.Cheese:
                cheesePool.Return(ingredient);
                break;

            case IngredientType.Meat:
                meatPool.Return(ingredient);
                break;
        }
    }


    private void CompleteOrder()
    { 
        int elapsedSeconds =
            Mathf.FloorToInt(Time.time - orderStartTime);
         
        int ingredientScore = 0;

        for (int i = 0; i < originalOrder.Count; i++)
        {
            switch (originalOrder[i])
            {
                case IngredientType.Vegetable:
                    ingredientScore += 20;
                    break;

                case IngredientType.Cheese:
                    ingredientScore += 10;
                    break;

                case IngredientType.Meat:
                    ingredientScore += 30;
                    break;
            }
        }
         
        int finalScore =
            ingredientScore - elapsedSeconds;

        if (scoreManager != null)
        {
            scoreManager.AddScore(finalScore);

        }
        StartCoroutine(ShowScorePopup(finalScore));
        Debug.Log(
            gameObject.name +
            " Order Complete! " +
            "Ingredient Score: " +
            ingredientScore +
            " | Time: " +
            elapsedSeconds +
            " | Final Score: " +
            finalScore
        );

        UpdateOrderUI();

        StartCoroutine(NewOrderAfterDelay());
    }


    private IEnumerator NewOrderAfterDelay()
    {
        isWaitingForNewOrder = true;

        yield return new WaitForSeconds(5f);

        CreateOrder();
    }


    private string GetOrderText()
    {
        string result = "";

        for (int i = 0; i < order.Count; i++)
        {
            result += order[i].ToString();

            if (i < order.Count - 1)
            {
                result += " + ";
            }
        }

        return result;
    }
    private IEnumerator ShowScorePopup(int amount)
    {
        if (scorePopup == null || scorePopupText == null)
            yield break;

        if (amount >= 0)
        {
            scorePopupText.text = "+" + amount;
        }
        else
        {
            scorePopupText.text = amount.ToString();
        }

        scorePopup.SetActive(true);

        yield return new WaitForSeconds(scorePopupDuration);

        scorePopup.SetActive(false);
    }
}