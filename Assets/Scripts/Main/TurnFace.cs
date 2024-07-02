public class TurnFace
{
    public Cell[,] TurnRight(Cell[,] cells, int cellEdge)
    {
        Cell[,] newArray = new Cell[cellEdge, cellEdge];

        for (int i = cellEdge - 1; i >= 0; i--)
        {
            for (int j = 0; j < cellEdge; j++)
            {
                newArray[j, cellEdge - (i + 1)] = cells[i, j];
            }
        }

        return newArray;
    }

    public Cell[,] TurnLeft(Cell[,] cells, int cellEdge)
    {
        Cell[,] newArray = new Cell[cellEdge, cellEdge];

        for (int i = cellEdge - 1; i >= 0; i--)
        {
            for (int j = 0; j < cellEdge; j++)
            {
                newArray[i, j] = cells[j, cellEdge - (i + 1)];
            }
        }

        return newArray;
    }

    public Cell[,] Invert(Cell[,] cells, int cellEdge)
    {
        Cell[,] newArray = new Cell[cellEdge, cellEdge];

        for (int i = 0; i < cellEdge; i++)
        {
            for (int j = 0; j < cellEdge; j++)
            {
                newArray[i, j] = cells[cellEdge - (i + 1), cellEdge - (j + 1)];
            }
        }

        return newArray;
    }
}
