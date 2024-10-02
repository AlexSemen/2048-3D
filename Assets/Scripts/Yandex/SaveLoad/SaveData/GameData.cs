using System.Collections.Generic;
using System.Linq;

public class GameData
{
    public int Points;
    public int Coins;
    public bool IsFace;
    public ShapeType ShapeType;
    public List<int> BlockValues;
    public bool IsLimitMove;
    public List<int> LimitingMovementsValues;
    public bool IsAudio;

    public void SetData(PlayerData playerData, FaceData faceData, LimitingData limitingData, bool isAudio)
    {
        BlockValues = null;
        LimitingMovementsValues = null;

        Coins = playerData.Coins;
        IsFace = faceData.IsFace;
        IsLimitMove = limitingData.IsLimitMove;
        IsAudio = isAudio;

        if (IsFace) 
        {
            Points = playerData.Points;
            ShapeType = faceData.ShapeType;
            BlockValues = faceData.BlockValues.ToList();

            if (IsLimitMove)
            {
                LimitingMovementsValues = limitingData.LimitingMovementsValues.ToList();
            }
        }
    }
}
