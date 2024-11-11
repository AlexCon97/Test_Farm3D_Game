using UnityEngine;

public static class BuySystem
{
    public static void Buy(ref float myMoney, IBuyable item)
    {
        if(myMoney>=item.Price){
            myMoney-=item.Price;
            return;
        }
        Debug.Log("Not enough money");
    }
}
