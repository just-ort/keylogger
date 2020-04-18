using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace lab2
{
    public static class Translator
    {
        private static Dictionary<Keys, string> dictionary = new Dictionary<Keys, string>
        {
            {Keys.Q, "Й"}, {Keys.W, "Ц"}, {Keys.R, "К"}, {Keys.T, "Е"}, {Keys.Y, "Н"}, {Keys.U, "Е"}, {Keys.E, "У"},
            {Keys.O, "Щ"}, {Keys.P, "З"}, {Keys.I, "Ш"}, {Keys.OemOpenBrackets, "Х"}, {Keys.Oem6, "Ъ"}, {Keys.Oem1, "Ж"},
            {Keys.Oem7, "Э"}, {Keys.Oemcomma, "Б"}, {Keys.OemPeriod, "."}, {Keys.A, "Ф"}, {Keys.S, "Ы"}, {Keys.D, "В"}, 
            {Keys.F, "А"}, {Keys.G, "П"}, {Keys.H, "Р"}, {Keys.J, "О"}, {Keys.K, "Л"}, {Keys.L, "Д"}, {Keys.Z, "Я"}, 
            {Keys.X, "Ч"}, {Keys.C, "С"}, {Keys.V, "М"}, {Keys.B, "И"}, {Keys.N, "Т"}, {Keys.M, "Ь"}
        };

        public static string Translate(Keys key)
        {
            if (dictionary.ContainsKey(key))
            {
                var symbol = dictionary.Single(d => d.Key == key).Value;
                return symbol;
            }

            else
            {
                return key.ToString();
            }
        }
        
    }
}