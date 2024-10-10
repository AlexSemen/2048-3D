using Loop;

namespace Main 
{
    public class TurnFace
    {
        public Cell[,] TurnRight(Cell[,] cells, int cellEdge)
        {
            Cell[,] newArray = new Cell[cellEdge, cellEdge];

            foreach (ValueIJ valueIJ in DoubleLoop.GetValues(new SettingsLoop(0, cellEdge - 1, false),
                new SettingsLoop(cellEdge - 1)))
            {
                newArray[valueIJ.J, cellEdge - (valueIJ.I + 1)] = cells[valueIJ.I, valueIJ.J];
            }

            return newArray;
        }

        public Cell[,] TurnLeft(Cell[,] cells, int cellEdge)
        {
            Cell[,] newArray = new Cell[cellEdge, cellEdge];

            foreach (ValueIJ valueIJ in DoubleLoop.GetValues(new SettingsLoop(0, cellEdge - 1, false),
                new SettingsLoop(cellEdge - 1)))
            {
                newArray[valueIJ.I, valueIJ.J] = cells[valueIJ.J, cellEdge - (valueIJ.I + 1)];
            }

            return newArray;
        }

        public Cell[,] Invert(Cell[,] cells, int cellEdge)
        {
            Cell[,] newArray = new Cell[cellEdge, cellEdge];

            foreach (ValueIJ valueIJ in DoubleLoop.GetValues(new SettingsLoop(cellEdge - 1)))
            {
                newArray[valueIJ.I, valueIJ.J] = cells[cellEdge - (valueIJ.I + 1), cellEdge - (valueIJ.J + 1)];
            }

            return newArray;
        }
    }
} 
