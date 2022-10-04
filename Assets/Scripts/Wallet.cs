using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    public uint usualMoney{ 
        get => usualMoney;
        set { 
            if (usualMoney - value >= 0) 
                usualMoney = value;
        } 
    }

    public int premiumMoney { 
        get => premiumMoney; 
        set {
            if (premiumMoney - value >= 0)
                premiumMoney = value;
        }
    }

    
}
