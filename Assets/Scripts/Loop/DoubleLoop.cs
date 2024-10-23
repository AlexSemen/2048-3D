using System.Collections.Generic;

namespace Loop 
{
    public static class DoubleLoop
    {
        public static List<ValueIJ> GetValues(SettingsLoop SettingsI, SettingsLoop SettingsJ = null, bool isReverse = false)
        {
            List<ValueIJ> _valueIJs = new List<ValueIJ>();

            if (SettingsJ == null)
            {
                SettingsJ = SettingsI;
            }

            for (int i = SettingsI.Value; i != SettingsI.EndValue; i += SettingsI.Iterator)
            {
                for (int j = SettingsJ.Value; j != SettingsJ.EndValue; j += SettingsJ.Iterator)
                {
                    if (isReverse == false)
                    {
                        _valueIJs.Add(new ValueIJ(i, j));
                    }
                    else
                    {
                        _valueIJs.Add(new ValueIJ(j, i));
                    }
                }
            }

            return _valueIJs;
        }
    }
}
