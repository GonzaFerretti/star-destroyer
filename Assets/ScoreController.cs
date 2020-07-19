using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour {
    private int digitno;
    public Sprite[] numberspr;
    private SpriteRenderer sr;
    private void Start()
    {
        digitno = gameObject.name[5] - 48 ;
        sr = GetComponent<SpriteRenderer>();
        global.score = 0;
    }
    public void UpdateScores()
    {
        int maxDigits = FindObjectsOfType<ScoreController>().Length + 1;
        string score = Mathf.RoundToInt(global.score).ToString();
        int digito = (digitno < maxDigits - score.Length)
                     ? 0
                     : score[-maxDigits + score.Length + digitno] - 48;
        Sprite numSprite = SpriteById(digito);
        sr.sprite = numSprite;
        //sr.sprite = SpriteById(Mathf.RoundToInt(global.score).ToString()[Mathf.Clamp(digitno - 1, 0, global.score.ToString().Length - 1)] - 48);
    }
    public void ResetScores()
    {
        sr.sprite = SpriteById(0);
    }
    public Sprite SpriteById(int id)
    {
        return numberspr[id];
    }
}
