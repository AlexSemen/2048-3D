using System;

namespace Loop
{
    public class SettingsLoop
    {
        private const int NullValue = 0;

        private int _value;
        private int _endValue;
        private int _iterator;

        public int Value => _value;
        public int EndValue => _endValue;
        public int Iterator => _iterator;

        public SettingsLoop(int endValue, int startValue = NullValue, bool increasing = true)
        {
            if ((startValue < endValue && increasing == false) || (startValue > endValue && increasing == true))
            {
                throw new ArgumentOutOfRangeException();
            }

            _value = startValue;
            _endValue = endValue;


            if (increasing == true)
            {
                _iterator = 1;
                _endValue += 1;
            }
            else
            {
                _iterator = -1;
                _endValue -= 1;
            }
        }
    }
}
