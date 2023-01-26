using System;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class DigitUnitTestScene : MonoBehaviour
{
    [SerializeField] private InputField inputField;
    [SerializeField] private Text outputText;
    [SerializeField] private Button executeButton;

    private void Start()
    {
        this.executeButton.OnClickAsObservable().Subscribe(_ => {
            this.convertToNumberWithDigitUnit(BigInteger.Parse(this.inputField.text));
        }).AddTo(this);

        this.ObserveEveryValueChanged(x => x.inputField.text).Skip(1).Subscribe(inputText => {
            if (inputText == string.Empty)
                inputText = "0";
                
            this.convertToNumberWithDigitUnit(BigInteger.Parse(inputText));
        }).AddTo(this);
    }

    private void convertToNumberWithDigitUnit(BigInteger value)
    {            
        var firstThreeDigit = value.ToString("E2").Substring(0, 4);
        var numberOfDigit = (int)Math.Truncate(BigInteger.Log10(value));
        var (quotient, remainder) = DigitUnit.GetDevideByThree(numberOfDigit);

        this.outputText.text = value.ToNumberWithDigitUnit() + "\n" +
                                "First 3Digit: " + firstThreeDigit + "\n" +
                                "Digit: " + (int)Math.Truncate(BigInteger.Log10(value)) + "\n" +
                                "Digit/3: " + quotient + "\n" +
                                "Remainder: " + remainder + "\n";
    }
}
