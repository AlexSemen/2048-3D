namespace Yandex.SaveLoad.SaveData
{
    public class PlayerData
    {
        public int Points { get; private set; }
        public int Coins { get; private set; }

        public void SetData(int points, int coins)
        {
            Points = points;
            Coins = coins;
        }
    }
}
