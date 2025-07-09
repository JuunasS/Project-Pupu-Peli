using UnityEngine;

public class CoinPurse : MonoBehaviour
{
    public int currentMoney;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddMoney(int money)
    {
        this.currentMoney += money;
    }

    public bool RemoveMoney(int money)
    {
        if(this.currentMoney - money < 0) { Debug.Log("Insufficient money!"); return false;}
        this.currentMoney -= money;

        return true;
    }
}
