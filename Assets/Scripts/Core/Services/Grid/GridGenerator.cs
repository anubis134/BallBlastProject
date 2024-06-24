using System;
using System.Collections.Generic;
using Core.Services.Grid;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
   public event Action<List<GridElement>> OnGridWasGenerated;  

   public List<GridElement> GridElements => _gridElements;

   [SerializeField] private GridSettings gridSettings;
   [SerializeField] private GridElement gridElementPrefab;
   [SerializeField] private Vector3 cellSize;

   [SerializeField]
   private List<GridElement> _gridElements = new ();

   private void Awake()
   {
      Generate();
   }

   public void Generate()
   {
      try
      {
         for (int i = gridSettings.Columns; i > 0; i--)
         {
            for (int j = gridSettings.Rows; j > 0; j--)
            {
               GridElement gridElement = Instantiate(gridElementPrefab);

               gridElement.X = j;
               gridElement.Y = i;
            
               gridElement.transform.position = new Vector3(
                  j + gridSettings.XGridOffset,
                  i + gridSettings.YGridOffset,
                  0f);

               gridElement.name = $"{j} : {i}";

               if (j != 0 || i != 0)
               {
                  gridElement.transform.position += new Vector3(
                     gridSettings.XElementOffset * j,
                     gridSettings.YElementOffset * i,
                     0f);
               }

               gridElement.transform.localScale = cellSize;

               _gridElements.Add(gridElement);
            }
         }

         OnGridWasGenerated?.Invoke(_gridElements);
      }
      catch (Exception e)
      {
         Console.WriteLine(e);
         throw;
      }
   }

   public void ResetGrid()
   {
      GridElement[] gridElements = _gridElements.ToArray();
      
      foreach (var element in gridElements)
      {
         GridElement gridElement = element;
         
         _gridElements.Remove(element);
         
         DestroyImmediate(gridElement.gameObject,false);
      }
   }

   private void OnDrawGizmos()
   {
      if(_gridElements.Count == 0) return;
      
      foreach (var element in _gridElements)
      {
         Gizmos.DrawWireCube(element.transform.position, cellSize);
      }
   }
}
