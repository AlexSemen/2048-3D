namespace Main
{
    public class Block
    {
        private int _meaning;
        private bool _isCanCombined = true;

        public int Meaning => _meaning;
        public bool IsCanCombined => _isCanCombined;

        public Block(int meaning)
        {
            _meaning = meaning;
        }

        public void SetMeaning(int meaning)
        {
            _meaning = meaning;
            _isCanCombined = false;
        }

        public void ThrowStatus()
        {
            _isCanCombined = true;
        }
    }
}
