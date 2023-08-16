using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSquare : MonoBehaviour
{

    public int SquareIndex { get; set; }

    private AlphabetData.LetterData _normaLetterData;
    private AlphabetData.LetterData _selectedData;
    private AlphabetData.LetterData _correctData;

    private SpriteRenderer displayedImage;
    // Start is called before the first frame update
    void Start()
    {
        displayedImage = GetComponent<SpriteRenderer>();
    }

    public void SetSprite(AlphabetData.LetterData normaLetterData, AlphabetData.LetterData selectedLetterData, AlphabetData.LetterData correctLetterData)
    {
        _normaLetterData = normaLetterData;
        _selectedData = selectedLetterData;
        _correctData = correctLetterData;

        GetComponent<SpriteRenderer>().sprite = _normaLetterData.image;
    }
}
