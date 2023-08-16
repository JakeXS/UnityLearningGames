using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using Random = UnityEngine.Random;


[CustomEditor(typeof(BoardData),false)]
[CanEditMultipleObjects]
[System.Serializable]
public class BoardDataDrawer : Editor
{
  private BoardData gameDataInstance => target as BoardData;
  private ReorderableList dataList;

  private void OnEnable()
  {
    InitializeRecordableList(ref dataList, "SearchWords", "searching words");
  }

  public override void OnInspectorGUI()
  {
      serializedObject.Update();
      DrawInputFields();
      EditorGUILayout.Space();
      ConvertToUpperButton();

      if (gameDataInstance.Board != null && gameDataInstance.col > 0 && gameDataInstance.row > 0)
      {
          DrawBoardTable();
      }

      GUILayout.BeginHorizontal();

      ClearBoardButton();
      FillWithRandomButton();

      GUILayout.EndHorizontal();

      EditorGUILayout.Space();
      dataList.DoLayoutList();
      serializedObject.ApplyModifiedProperties();

      if (GUI.changed)
      {
          EditorUtility.SetDirty(gameDataInstance);
      }
  }

  private void DrawInputFields()
  {
      var colTemp = gameDataInstance.col;
      var rowTemp = gameDataInstance.row;
      gameDataInstance.col = EditorGUILayout.IntField("Columns",gameDataInstance.col);
      gameDataInstance.row = EditorGUILayout.IntField("Rows", gameDataInstance.row);

      if (gameDataInstance.col != colTemp || gameDataInstance.row != rowTemp 
          && gameDataInstance.col > 0 && gameDataInstance.row > 0)
      {
          gameDataInstance.CreateNewBoard();
      }
    }

  private void DrawBoardTable()
  {
      var tableStyle = new GUIStyle("box");
      tableStyle.padding = new RectOffset(10, 10, 10, 10);
      tableStyle.margin.left = 32;

      var headerColStyle = new GUIStyle();
      headerColStyle.fixedWidth = 35;

      var colStyle = new GUIStyle();
      colStyle.fixedWidth = 50;

      var rowStyle = new GUIStyle();
      rowStyle.fixedHeight = 25;
      rowStyle.fixedWidth = 40;
      rowStyle.alignment = TextAnchor.MiddleCenter;

      var textFieldStyle = new GUIStyle();
      textFieldStyle.normal.background = Texture2D.grayTexture;
      textFieldStyle.normal.textColor = Color.white;
      textFieldStyle.fontStyle = FontStyle.Bold;
      textFieldStyle.alignment = TextAnchor.MiddleCenter;

      EditorGUILayout.BeginHorizontal(tableStyle);
      for (int x = 0; x < gameDataInstance.col; x++)
      {
          EditorGUILayout.BeginVertical(x == -1 ? headerColStyle : colStyle);
          for (int y = 0; y < gameDataInstance.row; y++)
          {
              if (x >= 0 && y >= 0)
              {
                  EditorGUILayout.BeginHorizontal(rowStyle);
                  var character = (string)EditorGUILayout.TextArea(gameDataInstance.Board[x].Row[y],textFieldStyle);
                  if (gameDataInstance.Board[x].Row[y].Length>1)
                  {
                      character = gameDataInstance.Board[x].Row[y].Substring(0,1);
                  }
                  gameDataInstance.Board[x].Row[y] = character;
                  EditorGUILayout.EndHorizontal();
              }
          }
          EditorGUILayout.EndVertical();
      }
      EditorGUILayout.EndHorizontal();
  }

  private void InitializeRecordableList(ref ReorderableList list, string propertyName, string listLabel)
  {
      list = new ReorderableList(serializedObject, serializedObject.FindProperty(propertyName), true, true, true, true);
      list.drawHeaderCallback = (Rect rect) =>
      {
          EditorGUI.LabelField(rect, listLabel);
      };
      var l = list;

      list.drawElementCallback = (rect, index, active, focused) =>
      {
          var element = l.serializedProperty.GetArrayElementAtIndex(index);
          rect.y += 2;

          EditorGUI.PropertyField(
              new Rect(rect.x, rect.y, EditorGUIUtility.labelWidth, EditorGUIUtility.singleLineHeight),
              element.FindPropertyRelative("word"), GUIContent.none);
      };
  }

  private void ConvertToUpperButton()
  {
      if (GUILayout.Button("To Upper"))
      {
          for (int i = 0; i < gameDataInstance.col; i++)
          {
              for (int j = 0; j < gameDataInstance.row; j++)
              {
                  var ErrorCounter = Regex.Matches(gameDataInstance.Board[i].Row[j], @"[a-z]").Count;
                  if (ErrorCounter>0)
                  {
                        gameDataInstance.Board[i].Row[j] = gameDataInstance.Board[i].Row[j].ToUpper();
                  }
              }
          }

          foreach (var searchWord in gameDataInstance.SearchWords)
          {
              var ErrorCounter = Regex.Matches(searchWord.word, @"[a-z]").Count;
              if (ErrorCounter > 0)
              {
                  searchWord.word = searchWord.word.ToUpper();
              }
          }
      }
  }

  private void ClearBoardButton()
  {
      if (GUILayout.Button("Clear Board"))
      {
          for (int i = 0; i < gameDataInstance.col; i++)
          {
              for (int j = 0; j < gameDataInstance.row; j++)
              {
                    gameDataInstance.Board[i].Row[j] = String.Empty;
              }
          }
      }
  }

  private void FillWithRandomButton()
  {
      if (GUILayout.Button("Random Letters"))
      {
          for (int i = 0; i < gameDataInstance.col; i++)
          {
              for (int j = 0; j < gameDataInstance.row; j++)
              {
                  int errorCounter = Regex.Matches(gameDataInstance.Board[i].Row[j], @"[a-zA-z]").Count;
                  string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                  int index = Random.Range(0, letters.Length);
                  if (errorCounter == 0)
                  {
                        gameDataInstance.Board[i].Row[j] = letters[index].ToString();
                  }
              }
          }
      }
  }
}
