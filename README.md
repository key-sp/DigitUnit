# DigitUnit

DigitUnit gives the given number a unit (=digiunit) according to the number of digits and returns it.
It is a unit such as aa, ab... zz, which is common in management simulation games and tycoon games.

Numbers up to 2198 digits can be represented using this digitunit using BigInteger
There is also an overload using double , but it can only represent a smaller number of digits.

![DigitUnit](https://user-images.githubusercontent.com/33142993/214645250-920d6165-0132-4331-8887-7a2e2d6f4edf.gif)

The list of digitunit is generated with the code below.
```
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
```

If the number of digits is 2199 or more, it will be displayed as "∞" as shown in the image below.

![DigitUnit2](https://user-images.githubusercontent.com/33142993/214645607-d221ae44-a435-4dd7-a71d-1846a964ff33.gif)
