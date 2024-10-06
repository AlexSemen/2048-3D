using System.Collections.Generic;

public static class DoubleLoop
{
    public static List<ValueIJ> GetValues(SettingsLoop SettingsI, SettingsLoop SettingsJ = null)
    {
        List<ValueIJ> _valueIJs = new List<ValueIJ>();

        if (SettingsJ != null)
        {
            SettingsJ = SettingsI;
        }

        for (int i = SettingsI.Value; i != SettingsI.EndValue; i += SettingsI.Iterator)
        {
            for (int j = SettingsJ.Value; j != SettingsJ.EndValue; j += SettingsJ.Iterator)
            {
                _valueIJs.Add(new ValueIJ(i, j));
            }
        }
    
        return _valueIJs;
    }
}
