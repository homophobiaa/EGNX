using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGNValidation
{
    public class Validation : IEGNValidator
    {
        public string[] Generate(DateTime birthDate, string city, bool isMale)
        {
            List<string> validEGNs = new List<string>();

            int year = birthDate.Year;
            int month = birthDate.Month;
            int day = birthDate.Day;

            if (year >= 1800 && year < 1900)
                month += 20;
            else if (year >= 2000 && year < 2100)
                month += 40;

            string datePart = birthDate.ToString("yy") + month.ToString("D2") + day.ToString("D2");

            int startCode = 0;
            int endCode = 0;
            switch (city)
            {
                case "Blagoevgrad":
                    startCode = 0;
                    endCode = 43;
                    break;
                case "Burgas":
                    startCode = 44;
                    endCode = 93;
                    break;
                case "Varna":
                    startCode = 94;
                    endCode = 139;
                    break;
                case "Veliko Turnovo":
                    startCode = 140;
                    endCode = 169;
                    break;
                case "Vidin":
                    startCode = 170;
                    endCode = 183;
                    break;
                case "Vraca":
                    startCode = 184;
                    endCode = 217;
                    break;
                case "Gabrovo":
                    startCode = 218;
                    endCode = 233;
                    break;
                case "Kurdjali":
                    startCode = 234;
                    endCode = 281;
                    break;
                case "Kustendil":
                    startCode = 282;
                    endCode = 301;
                    break;
                case "Lovech":
                    startCode = 302;
                    endCode = 319;
                    break;
                case "Montana":
                    startCode = 320;
                    endCode = 341;
                    break;
                case "Pazardjik":
                    startCode = 342;
                    endCode = 377;
                    break;
                case "Pernik":
                    startCode = 378;
                    endCode = 395;
                    break;
                case "Pleven":
                    startCode = 396;
                    endCode = 435;
                    break;
                case "Plovdiv":
                    startCode = 436;
                    endCode = 501;
                    break;
                case "Razgrad":
                    startCode = 502;
                    endCode = 527;
                    break;
                case "Ruse":
                    startCode = 528;
                    endCode = 555;
                    break;
                case "Silistra":
                    startCode = 556;
                    endCode = 575;
                    break;
                case "Sliven":
                    startCode = 576;
                    endCode = 601;
                    break;
                case "Smolqn":
                    startCode = 602;
                    endCode = 623;
                    break;
                case "Sofia - grad":
                    startCode = 624;
                    endCode = 721;
                    break;
                case "Sofia - oblast":
                    startCode = 722;
                    endCode = 751;
                    break;
                case "Stara Zagora":
                    startCode = 752;
                    endCode = 789;
                    break;
                case "Dobrich":
                    startCode = 790;
                    endCode = 821;
                    break;
                case "Turgovishte":
                    startCode = 822;
                    endCode = 843;
                    break;
                case "Haskovo":
                    startCode = 844;
                    endCode = 871;
                    break;
                case "Shumen":
                    startCode = 872;
                    endCode = 903;
                    break;
                case "Qmbol":
                    startCode = 904;
                    endCode = 925;
                    break;
                default:
                    startCode = 926;
                    endCode = 999;
                    break;
            }

            for (int code = startCode; code <= endCode; code++)
            {
                if (isMale && code % 2 != 0) continue;
                if (!isMale && code % 2 != 1) continue;

                string region = "";

                if (code < 10)
                {
                    region = "00" + code;
                }
                else if (code < 100)
                {
                    region = "0" + code;
                }
                else
                {
                    region = code.ToString();
                }
                string partialEgn = datePart + region;

                for (int controlDigit = 0; controlDigit <= 9; controlDigit++)
                {
                    string egn = partialEgn + controlDigit;

                    if (Validate(egn))
                    {
                        validEGNs.Add(egn);
                        break;
                    }
                }
            }

            return validEGNs.ToArray();
        }

        public bool Validate(string egn)
        {
            if (egn.Length != 10)
            {
                return false;
            }
            char[] chars = egn.ToCharArray();
            int lastNumber =
                (int.Parse(chars[0].ToString()) * 2 +
                int.Parse(chars[1].ToString()) * 4 +
                int.Parse(chars[2].ToString()) * 8 +
                int.Parse(chars[3].ToString()) * 5 +
                int.Parse(chars[4].ToString()) * 10 +
                int.Parse(chars[5].ToString()) * 9 +
                int.Parse(chars[6].ToString()) * 7 +
                int.Parse(chars[7].ToString()) * 3 +
                int.Parse(chars[8].ToString()) * 6)%11;

            if (lastNumber == int.Parse(chars[9].ToString()))
            {
                return true;
            }
            else { return false;}
        }
    }
}
