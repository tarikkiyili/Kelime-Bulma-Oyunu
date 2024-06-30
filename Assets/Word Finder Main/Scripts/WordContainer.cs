using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordContainer : MonoBehaviour
{

    [Header(" Elements ")]
    private LetterContainer[] letterContainers;

    [Header(" Settings ")]
    private int currentLetterIndex;

    private void Awake(){

        letterContainers = GetComponentsInChildren<LetterContainer>();
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Initialize(){

        currentLetterIndex = 0;

        for (int i = 0; i < letterContainers.Length; i++)
        {
            letterContainers[i].Initialize();
        }
    }

    public void Add(char letter){
        letterContainers[currentLetterIndex].SetLetter(letter);
        currentLetterIndex++;
    }

    public void AddAsHint(int letterIndex, char letter){
        letterContainers[letterIndex].SetLetter(letter, true);
    }

    public bool RemoveLetter(){
        if(currentLetterIndex <= 0)
        return false;

        currentLetterIndex--;
        letterContainers[currentLetterIndex].Initialize();
        return true;
    }

    public string GetWord(){
        string word = "";

        for (int i = 0; i < letterContainers.Length; i++)
        {
            word += letterContainers[i].GetLetter().ToString();
        }

        return word;
    }

    public void Colorize(string secretWord){

        List<char> chars = new List<char>(secretWord.ToCharArray());


        for (int i = 0; i < letterContainers.Length; i++)
        {
            char letterToCkeck = letterContainers[i].GetLetter();

            if(letterToCkeck == secretWord[i]){
                letterContainers[i].SetValid();
            }

            else if(secretWord.Contains(letterToCkeck)){
                letterContainers[i].SetPotential();
            }
            
            else{
                letterContainers[i].SetInvalid();
            }
        }
    }

    public bool IsComplete(){
        return currentLetterIndex >= 5;
    }
}
