using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{

    public static DataManager instance;

    [Header(" Data ")]
    private int coins;
    private int score;
    private int bestScore;


    [Header(" Events ")]
    public static Action onCoinsUpdated;

    private void Awake(){
        if (instance == null)
        instance = this;
        else
        Destroy(gameObject);

        LoadData();
    }
    // Start is called before the first frame update
    void Start()
    {  
    }

    // Update is called once per frame
    void Update()
    {  
    }

    public void AddCoins(int amount){
        coins += amount;
        SaveData();

        onCoinsUpdated?.Invoke();
    }

    public void RemoveCoins(int amount){
        coins -= amount;
        coins = Math.Max(coins, 0);
        SaveData();

        onCoinsUpdated?.Invoke();
    }

    public void IncreseScore(int amount){
        score += amount;

        if(score > bestScore)
        bestScore = score;

        SaveData();
    }

    public void ResetScore(){
        score = 0;
        SaveData();
    }

    public int GetCoins(){
        return coins;
    }

        public int GetScore(){
        return score;
    }

        public int GetBestScore(){
        return bestScore;
    }

    private void LoadData(){
        coins = PlayerPrefs.GetInt("coins", 150);
        score = PlayerPrefs.GetInt("score");
        bestScore = PlayerPrefs.GetInt("bestScore");
    }
        private void SaveData(){
        PlayerPrefs.SetInt("coins", coins);
        PlayerPrefs.SetInt("score", score);
        PlayerPrefs.SetInt("bestScore", bestScore);
    }
}
