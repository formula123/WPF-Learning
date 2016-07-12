using System;
using System.Collections.Generic;

namespace ContactManager.Model
{
    public static class States
    {
        private static readonly List<string> _names;
        static States()
        {
            _names = new List<string>(50);

            _names.Add("Alababa");
            _names.Add("Alaska");
            _names.Add("Arizona");
            _names.Add("zzl");
            _names.Add("juan");
            _names.Add("ruby");
        }

        public static IList<string> GetNames()
        {
            return _names;
        }
    }
}
