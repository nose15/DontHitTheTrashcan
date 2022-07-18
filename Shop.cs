using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] ScoreManager scoreManager;

    public int price = 0;
    public int ammount = 0;


    private bool isAvailable(int balance, int price)
    {
        if (price <= balance) return true;
        else return false;
    }

    private void transactionGems(int price, int ammount)
    {
        scoreManager.gems -= price;
        scoreManager.reviveTokens += ammount;
    }

    private void transactionCoins(int price, int ammount)
    {
        scoreManager.coins -= price;
        scoreManager.fuelCanisters += ammount;
    }

    public void RevivesForGemsOffer1()
    {
        if (isAvailable(scoreManager.gems, 500))
        {
            transactionGems(500, 1);
        }
    }

    public void RevivesForGemsOffer2()
    {
        if (isAvailable(scoreManager.gems, 2250))
        {
            transactionGems(2250, 5);
        }
    }

    public void RevivesForGemsOffer3()
    {
        if (isAvailable(scoreManager.gems, 8000))
        {
            transactionGems(8000, 20);
        }
    }

    public void RevivesForGemsOffer4()
    {
        if (isAvailable(scoreManager.gems, 17500))
        {
            transactionGems(17500, 50);
        }
    }

    public void FuelForCoinsOffer1()
    {
        if (isAvailable(scoreManager.coins, 10000))
        {
            transactionCoins(10000, 1);
        }
    }

    public void FuelForCoinsOffer2()
    {
        if (isAvailable(scoreManager.coins, 45000))
        {
            transactionCoins(45000, 5);
        }
    }

    public void FuelForCoinsOffer3()
    {
        if (isAvailable(scoreManager.coins, 160000))
        {
            transactionCoins(160000, 20);
        }
    }

    public void FuelForCoinsOffer4()
    {
        if (isAvailable(scoreManager.coins, 300000))
        {
            transactionCoins(300000, 50);
        }
    }






}
