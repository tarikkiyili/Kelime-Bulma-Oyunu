using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System;

public class InputManager : MonoBehaviour
{

    public static InputManager instance;

    [Header(" Elements ")]
    [SerializeField] private WordContainer[] WordContainers;
    [SerializeField] private Button tryButton;
    [SerializeField] private keyboardColorizer keyboardColorizer;

    [Header(" Settings ")]
    private int currentWordContainerIndex;
    private bool canAddletter = true;
    private bool shouldReset;

    [Header(" Events ")]
    public static Action onLetterAdded;
    public static Action onLetterRemoved;

    private void Awake(){
        if(instance == null)
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
       Initialize();

       KeyboardKey.onKeyPressed += KeyPressedCallback;
       GameManager.onGameStateChanged += GameStateChangedCallback;
    }

    private void onDestroy(){

        KeyboardKey.onKeyPressed -= KeyPressedCallback;
        GameManager.onGameStateChanged -= GameStateChangedCallback;
    }

        private void GameStateChangedCallback(GameState gameState){

        switch(gameState){

            case GameState.Game:

            if(shouldReset)
            Initialize();
            break;

            case GameState.LevelComplete:
            shouldReset = true;
            break;

            case GameState.Gameover:
            shouldReset = true;
            break;
            }
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void Initialize(){

        currentWordContainerIndex = 0;
        canAddletter = true;
        
        DisableTryButton();

        for (int i = 0; i < WordContainers.Length; i++)
        {
            WordContainers[i].Initialize();
        }

        shouldReset = false;
    }

    private void KeyPressedCallback(char letter)
    {
        if(!canAddletter)
        return;

        WordContainers[currentWordContainerIndex].Add(letter);

                if(WordContainers[currentWordContainerIndex].IsComplete())
        {
            canAddletter = false;
            EnableTryButton();
        }

        onLetterAdded?.Invoke();
    }

    public void CheckWord()
    {

        string wordToCheck = WordContainers[currentWordContainerIndex].GetWord();
        string secretWord = WordManager.instance.GetSecretWord();

        WordContainers[currentWordContainerIndex].Colorize(secretWord);
        keyboardColorizer.Colorize(secretWord, wordToCheck);

        if(wordToCheck == secretWord)
            SetLevelComplete();
        
        else
        {
        
        currentWordContainerIndex++;
        DisableTryButton();

        if(currentWordContainerIndex >= WordContainers.Length){
            DataManager.instance.ResetScore();
            GameManager.instance.SetGameState(GameState.Gameover);
        }
        else{
        canAddletter = true;
        }
        }
    }

    private void SetLevelComplete(){

        UpdateData();
        GameManager.instance.SetGameState(GameState.LevelComplete);
    }

    private void UpdateData(){
        int scoreToAdd = 10 - currentWordContainerIndex;

        DataManager.instance.IncreseScore(scoreToAdd);
        DataManager.instance.AddCoins(scoreToAdd * 3 / 2);
    }

    public void backspacePressedCallback(){

        bool removedLetter = WordContainers[currentWordContainerIndex].RemoveLetter();

        if(removedLetter)
        DisableTryButton();

        canAddletter = true;

        onLetterRemoved?.Invoke();
    }

    private void EnableTryButton(){
        tryButton.interactable = true;
    }

    private void DisableTryButton(){
        tryButton.interactable = false;
    }

    public WordContainer GetcurrentWordContainer(){
        return WordContainers[currentWordContainerIndex];
    }
}
