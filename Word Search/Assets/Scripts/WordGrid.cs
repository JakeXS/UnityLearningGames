using System.Collections;
using System.Collections.Generic;
using Mono.CompilerServices.SymbolWriter;
using UnityEngine;

public class WordGrid : MonoBehaviour
{
    public GameData gameData;
    public GameObject gridSquarePref;
    public AlphabetData alphabetData;

    public float squareOffset = 0.0f;
    public float topPosition = 0.0f;

    private List<GameObject> squareList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        SpawnGridSquares();
        SetSquaresPositions();
    }

    private void SetSquaresPositions()
    {
        var squareRect = squareList[0].GetComponent<SpriteRenderer>().sprite.rect;
        var squareTransform = squareList[0].GetComponent<Transform>();
        var offset = new Vector2
        {
            x = (squareRect.width * squareTransform.localScale.x + squareOffset) * 0.01f,
            y = (squareRect.height * squareTransform.localScale.y + squareOffset) * 0.01f
        };
        var startPosition = GetFirstSquarePosition();
        int columnNumber = 0;
        int rowNumber = 0;
        foreach (var square in squareList)
        {
            if (rowNumber + 1 > gameData.selectedBoard.row)
            {
                columnNumber ++;
                rowNumber = 0;
            }

            var positionX = startPosition.x + offset.x * columnNumber;
            var positionY = startPosition.y - offset.y * rowNumber;
            square.GetComponent<Transform>().position = new Vector2(positionX, positionY);
            rowNumber++;
        }
    }

    private Vector2 GetFirstSquarePosition()
    {
        var startPosition = new Vector2(0f,transform.position.y);
        var squareRect = squareList[0].GetComponent<SpriteRenderer>().sprite.rect;
        var squareTransform = squareList[0].GetComponent<Transform>();
        var squareSize = new Vector2(0f, 0f);

        squareSize.x = squareRect.width * squareTransform.localScale.x;
        squareSize.y = squareRect.height * squareTransform.localScale.y;

        var midWidthPosition = (((gameData.selectedBoard.col - 1 )* squareSize.x) / 2) * 0.01f;
        var midWidthHeight = (((gameData.selectedBoard.row - 1) * squareSize.y) / 2) * 0.01f;

        startPosition.x = (midWidthPosition != 0) ? midWidthPosition * -1 :midWidthPosition;
        startPosition.y += midWidthHeight;

        return startPosition;
    }
    private void SpawnGridSquares()
    {
        if (gameData != null)
        {
            var squareScale = GetSquareState(new Vector3(1.5f, 1.5f, 0.1f));

            foreach (var square in gameData.selectedBoard.Board)
            {
                foreach (var squareletter in square.Row)
                {
                    var normalLetter = alphabetData.alphabetNormal.Find(data => data.letter == squareletter);
                    var selectedLetter = alphabetData.alphabetHighlighted.Find(data => data.letter == squareletter);
                    var correctLetter = alphabetData.alphabetWrong.Find(data => data.letter == squareletter);

                    if (normalLetter.image == null || selectedLetter.image == null)
                    {
                        Debug.Log("Error:Missing letters" + squareletter);
#if UNITY_EDITOR
                        if (UnityEditor.EditorApplication.isPlaying)
                        {
                            UnityEditor.EditorApplication.isPlaying = false;
                        }
#endif
                    }
                    else
                    {
                        squareList.Add(Instantiate(gridSquarePref));
                        squareList[squareList.Count - 1].GetComponent<GridSquare>().SetSprite(normalLetter, selectedLetter, correctLetter);
                        squareList[squareList.Count - 1].transform.SetParent(this.transform);
                        squareList[squareList.Count - 1].GetComponent<Transform>().position = new Vector3(0f, 0f, 0f);
                        squareList[squareList.Count - 1].transform.localScale = squareScale;
                    }


                }
            }
        }
    }

    private Vector3 GetSquareState(Vector3 defaultScale)
    {
        var finalScale = defaultScale;
        var adjustment = 0.01f;
        while (shouldScaleDown(finalScale))
        {
            finalScale.x -= adjustment;
            finalScale.y -= adjustment;

            if (finalScale.x <= 0 || finalScale.y <= 0)
            {
                finalScale.x = adjustment;
                finalScale.y = adjustment;
                return finalScale;
            }
        }
        return finalScale;
    }

    private bool shouldScaleDown(Vector3 targetScale)
    {
        var squareRect = gridSquarePref.GetComponent<SpriteRenderer>().sprite.rect;
        var squareSize = new Vector2(0f, 0f);
        var startPosition = new Vector2(0f, 0f);
        squareSize.x = (squareRect.width * targetScale.x) + squareOffset;
        squareSize.y = (squareRect.height * targetScale.y) + squareOffset;

        var midWidthPosition = ((gameData.selectedBoard.col * squareSize.x) / 2) * 0.01f;
        var midHeightPosition = ((gameData.selectedBoard.row * squareSize.y) / 2) * 0.01f;

        startPosition.x = (midWidthPosition != 0f) ? midWidthPosition * -1 : midWidthPosition;
        startPosition.y = midHeightPosition;

        return startPosition.x < GetHalfScreenWidth() * -1 || startPosition.y > topPosition;
    }

    private float GetHalfScreenWidth()
    {
        float height = Camera.main.orthographicSize * 2;
        float width = (1.7f * height) * Screen.width / Screen.height;

        return width / 2;
    }
}