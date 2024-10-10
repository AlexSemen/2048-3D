namespace Main
{
    public class Cell
    {
        private Block _block = null;
        private bool _isTargetCell = false;
        private bool _isBoom = false;

        public Block Block => _block;
        public bool IsTargetCell => _isTargetCell;
        public bool IsBoom => _isBoom;

        public void SetBlock(Block block)
        {
            _block = block;
        }

        public void SetTargetCell(bool value)
        {
            _isTargetCell = value;
        }

        public void ThrowStatusBlock()
        {
            if (_block != null)
            {
                _block.ThrowStatus();
            }
        }

        public void SetBoom(bool isBoom)
        {
            _isBoom = isBoom;
        }
    }
}
