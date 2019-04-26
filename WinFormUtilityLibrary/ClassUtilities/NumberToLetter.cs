using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinFormUtilityLibrary.ClassUtilities
{
    /// <summary>
    /// Conversion class from number to letters
    /// </summary>
    public class NumberToLetter
    {
        /// <summary>
        /// Converts to euro
        /// </summary>
        /// <param name="value">Value to convert</param>
        /// <returns>A string corresponding to the value</returns>
        /// <remarks>format xxx euro and yyy cent(s)</remarks>
        public static string ConvertirEuro(decimal value)
        {
            return Convertir(value, 2, " euro", " cent(s)", lang == Language.FRENCH ? " et " : " and ");
        }

        static Dictionary<int, string> tabeng;


        static Dictionary<int, string> tabfr;


        /// <summary>
        /// Language used during conversion
        /// </summary>
        public static Language lang = Language.ENGLISH;


        /// <summary>
        /// Converts value to letter
        /// </summary>
        /// <param name="value">value to convert</param>
        /// <param name="numberOfDecimals">Number of decimals to conserve</param>
        /// <param name="wholeUnit">Unit given to the whole of the value</param>
        /// <param name="decimalUnit">Unit given to the decimal of the value</param>
        /// <param name="seperator">le séparateur entre les parties</param>
        /// <returns>A string corresponding to the value</returns>
        public static string Convertir(decimal value,
          int numberOfDecimals = 0,
          string wholeUnit = "XAF",
          string decimalUnit = "",
          string seperator = ",")
        {
            value = Math.Round(value, numberOfDecimals);

            int val = (int)Math.Floor((double)value);
            string ret = Convertir(val) + wholeUnit;

            value = value - val;
            value = value * (int)(Math.Pow(10, numberOfDecimals));
            val = (int)Math.Floor((double)value);
            if (val > 0)
                ret += seperator + Convertir(val) + decimalUnit;

            return ret;
        }

        /// <summary>
        /// Converts a whole number to letter
        /// </summary>
        /// <param name="number">Number to convert</param>
        /// <returns>A string corresponding to the value</returns>
        public static string Convertir(int number)
        {
            StringBuilder lettre = new StringBuilder();
            int centaine, dizaine, unite, reste, y;
            reste = Math.Abs(number);

            for (int i = 1000000000; i >= 1; i /= 1000)
            {
                y = reste / i;
                if (y != 0)
                {
                    centaine = y / 100;
                    dizaine = (y - centaine * 100) / 10;
                    unite = y - (centaine * 100) - (dizaine * 10);
                    switch (centaine)
                    {
                        case 0:
                            break;
                        case 1:
                            if (lang == Language.ENGLISH)
                            {
                                lettre.Append("one ");
                            }
                            lettre.Append(ConvertSimple(centaine * 100));
                            lettre.Append(" ");
                            break;
                        default:
                            lettre.Append(ConvertSimple(centaine));
                            lettre.Append(" ");
                            lettre.Append(ConvertSimple(100));
                            if ((dizaine == 0) && (unite == 0)) lettre.Append("s ");
                            else lettre.Append(" ");
                            break;
                    }

                    switch (dizaine)
                    {
                        case 0:
                            //if (unite != 1 || (unite == 1 && i != 1000))
                            if (lang == Language.ENGLISH && centaine > 0 && unite > 0) lettre.Append("and ");
                            //if (lang == "A" && centaine == 0 && unite > 0 & nombre>1000) lettre.Append("and ");
                            if (unite != 0)
                            {
                                //if (lang == "A" && centaine > 0 && unite >0) lettre.Append("and ");
                                lettre.Append(ConvertSimple(unite) + " ");
                                if (unite != 0) lettre.Append(" ");
                            }
                            //if (unite == 1)
                            //{
                            //    //if (lang == "A" && centaine > 0) lettre.Append("and ");
                            //    lettre.Append(Convert(unite) + " ");
                            //}
                            break;
                        case 1:
                            if (lang == Language.ENGLISH && centaine > 0) lettre.Append("and ");
                            lettre.Append(ConvertSimple(dizaine * 10 + unite));
                            lettre.Append(" ");
                            break;
                        case 7:
                            goto case 1;
                        case 9:
                            goto case 1;
                        default:
                            if (lang == Language.ENGLISH && centaine > 0) lettre.Append("and ");
                            lettre.Append(ConvertSimple(dizaine * 10));
                            if (unite == 1 && dizaine != 8)
                            {
                                if (lang == Language.FRENCH)
                                    lettre.Append("-et-");
                                else
                                    lettre.Append(" and ");
                            }
                            else lettre.Append(" ");
                            lettre.Append(ConvertSimple(unite));
                            lettre.Append(" ");
                            break;

                    }
                    switch (i)
                    {
                        case 1000000000:
                            if (y > 1) lettre.Append(setBillions("s "));
                            else lettre.Append(setBillions(" "));
                            break;
                        case 1000000:
                            if (y > 1) lettre.Append("millions ");
                            else lettre.Append("million ");
                            break;
                        case 1000:
                            if (lang == Language.ENGLISH) lettre.Append("thousand ");
                            if (lang == Language.FRENCH) lettre.Append("mille ");
                            break;
                    }
                }
                reste -= y * i;
            } // end for
            if (lettre.Length == 0) return "zero";

            return lettre.ToString().Trim();
        }


        private static string setBillions(string suffix)
        {
            if (lang == Language.ENGLISH) return "Billion" + suffix;
            else return "milliard" + suffix;
        }

        private static void InitTab()
        {
            if (tabeng != null) return;
            tabeng = new Dictionary<int, string>();
            tabeng.Add(0, "");
            tabeng.Add(1, "one");
            tabeng.Add(2, "two");
            tabeng.Add(3, "three");
            tabeng.Add(4, "four");
            tabeng.Add(5, "five");
            tabeng.Add(6, "six");
            tabeng.Add(7, "seven");
            tabeng.Add(8, "eight");
            tabeng.Add(9, "nine");
            tabeng.Add(10, "ten");
            tabeng.Add(11, "eleven");
            tabeng.Add(12, "twelve");
            tabeng.Add(13, "thirteen");
            tabeng.Add(14, "fourteen");
            tabeng.Add(15, "fifteen");
            tabeng.Add(16, "sixteen");
            tabeng.Add(17, "seventeen");
            tabeng.Add(18, "eighteen");
            tabeng.Add(19, "nineteen");
            tabeng.Add(20, "twenty");
            tabeng.Add(30, "thirty");
            tabeng.Add(40, "fourty");
            tabeng.Add(50, "fifty");
            tabeng.Add(60, "sixty");
            tabeng.Add(70, "seventy");
            tabeng.Add(71, "seventy-one");
            tabeng.Add(72, "seventy-two");
            tabeng.Add(73, "seventy-three");
            tabeng.Add(74, "seventy-four");
            tabeng.Add(75, "seventy-five");
            tabeng.Add(76, "seventy-six");
            tabeng.Add(77, "seventy-seven");
            tabeng.Add(78, "seventy-eight");
            tabeng.Add(79, "seventy-nine");
            tabeng.Add(80, "eighty");
            tabeng.Add(90, "ninety");
            tabeng.Add(91, "ninety-one");
            tabeng.Add(92, "ninety-two");
            tabeng.Add(93, "ninety-three");
            tabeng.Add(94, "ninety-four");
            tabeng.Add(95, "ninety-five");
            tabeng.Add(96, "ninety-six");
            tabeng.Add(97, "ninety-seven");
            tabeng.Add(98, "ninety-eight");
            tabeng.Add(99, "ninety-nine");
            tabeng.Add(100, "hundred");
            tabeng.Add(1000, "thousand");

            tabfr = new Dictionary<int, string>();
            tabfr.Add(0, "");
            tabfr.Add(1, "un");
            tabfr.Add(2, "deux");
            tabfr.Add(3, "trois");
            tabfr.Add(4, "quatre");
            tabfr.Add(5, "cinq");
            tabfr.Add(6, "six");
            tabfr.Add(7, "sept");
            tabfr.Add(8, "huit");
            tabfr.Add(9, "neuf");
            tabfr.Add(10, "dix");
            tabfr.Add(11, "onze");
            tabfr.Add(12, "douze");
            tabfr.Add(13, "treize");
            tabfr.Add(14, "quatorze");
            tabfr.Add(15, "quinze");
            tabfr.Add(16, "seize");
            tabfr.Add(17, "dix-sept");
            tabfr.Add(18, "dix-huit");
            tabfr.Add(19, "dix-neuf");
            tabfr.Add(20, "vingt");
            tabfr.Add(30, "trente");
            tabfr.Add(40, "quarante");
            tabfr.Add(50, "cinquante");
            tabfr.Add(60, "soixante");
            tabfr.Add(70, "soixante-dix");
            tabfr.Add(71, "soixante-onze");
            tabfr.Add(72, "soixante-douze");
            tabfr.Add(73, "soixante-treize");
            tabfr.Add(74, "soixante-quatorze");
            tabfr.Add(75, "soixante-quinze");
            tabfr.Add(76, "soixante-seize");
            tabfr.Add(77, "soixante-dix-sept");
            tabfr.Add(78, "soixante-dix-huit");
            tabfr.Add(79, "soixante-dix-neuf");
            tabfr.Add(80, "quatre-vingt");
            tabfr.Add(90, "quatre-vingt-dix");
            tabfr.Add(91, "quatre-vingt-onze");
            tabfr.Add(92, "quatre-vingt-douze");
            tabfr.Add(93, "quatre-vingt-treize");
            tabfr.Add(94, "quatre-vingt-quatorze");
            tabfr.Add(95, "quatre-vingt-quinze");
            tabfr.Add(96, "quatre-vingt-seize");
            tabfr.Add(97, "quatre-vingt-dix-sept");
            tabfr.Add(98, "quatre-vingt-dix-huit");
            tabfr.Add(99, "quatre-vingt-dix-neuf");
            tabfr.Add(100, "cent");
            tabfr.Add(1000, "mille");
        }

        private static string ConvertSimple(int nb)
        {
            InitTab();
            if (lang == Language.ENGLISH)
            {
                return tabeng[nb];
            }
            else
            {
                return tabfr[nb];
            }
        }
    }
}
