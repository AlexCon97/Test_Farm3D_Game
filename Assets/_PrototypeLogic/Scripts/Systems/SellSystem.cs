using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SellSystem
{
    public static void Sell(IBuyable whatSell, float customerMoney){
        if(whatSell.Price>=customerMoney){
            Debug.Log("You got it");
        }

    }
}
