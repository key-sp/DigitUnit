using System;
using System.Linq;
using System.Collections.Generic;
using System.Numerics;

public static class DigitUnit
{
    /// <summary>
    /// List of Units such as k, m, t, aa~zz.
    /// </summary>
    private static IReadOnlyList<string> digitSymbolList;

    /// <summary>
    /// Returns a unit of "3 significant digits + kmbt, aa-zz" from the given number.
    /// The maximum value is 999zz, and if it is more than that, it will appear as ∞.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string ToNumberWithDigitUnit(this double value)
    {
        // get 3 significant digits.
        var firstThreeDigit = value.ToString("E2").Substring(0, 4);

        // get number of digits.
        var numberOfDigit = (int)Math.Truncate(Math.Log10(value));

        // Increment units by three digits.
        var (quotient, remainder) = GetDevideByThree(numberOfDigit);

        // get unit from number of digits.
        var unit = GetDigitUnit(quotient);

        if (unit == "∞")
            return "∞";

        var numberWithUnit = (Convert.ToDouble(firstThreeDigit) * Math.Pow(10, remainder)).ToString() + unit;

        return numberWithUnit;
    }

    /// <summary>
    /// BigInteger overload.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string ToNumberWithDigitUnit(this BigInteger value)
    {
        // get 3 significant digits.
        var firstThreeDigit = value.ToString("E2").Substring(0, 4);

        // get number of digits.
        var numberOfDigit = (int)Math.Truncate(BigInteger.Log10(value));

        // Increment units by three digits.
        var (quotient, remainder) = GetDevideByThree(numberOfDigit);

        // get unit from number of digits.
        var unit = GetDigitUnit(quotient);

        if (unit == "∞")
            return "∞";

        var numberWithUnit = (Convert.ToDouble(firstThreeDigit) * Math.Pow(10, remainder)).ToString() + unit;

        return numberWithUnit;
    }

    /// <summary>
    /// Get the quotient and remainder after dividing by 3.
    /// </summary>
    /// <param name="digits"></param>
    /// <returns></returns>
    public static (int, int) GetDevideByThree(double digits)
    {
        var quotient = (int)digits / 3;
        var remainder = (int)digits % 3;
        return (quotient, remainder);
    }

    /// <summary>
    /// Return units from number of digits.
    /// </summary>
    /// <param name="numberOfDigits"></param>
    /// <returns></returns>
    public static string GetDigitUnit(int numberOfDigits)
    {
        if (DigitUnit.digitSymbolList == null)
        {
            string digitSymbol = " kmbt";
            string atoz = " abcdefghijklmnopqrstuvwxyz";
            var atozList = atoz.SelectMany(y1 => atoz.Select(y2 => (y1, y2)))
                .Select(x => x.y1.ToString() + x.y2.ToString())
                .Select(x => x.Replace(" ", ""))
                .Where(x => x != "")
                .ToList();
            var digitSymbolList = digitSymbol.Select(x => x.ToString()).ToList();
            digitSymbolList.AddRange(atozList);
            DigitUnit.digitSymbolList = digitSymbolList;
        }
        if (numberOfDigits >= digitSymbolList.Count)
            return "∞";

        return digitSymbolList[numberOfDigits];
    }
}