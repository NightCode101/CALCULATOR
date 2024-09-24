using System;
using System.Collections.Generic;
using Microsoft.Maui.Controls;

namespace Calculator
{
    public partial class MainPage : ContentPage
    {
        double firstNumber = 0;
        double secondNumber = 0;
        string operatorSelected = "";
        bool isOperatorClicked = false;
        bool isNewEntry = false;
        List<string> history = new List<string>();  // To store the calculation history

        public MainPage()
        {
            InitializeComponent();
        }

        void OnDigit(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            if (Display.Text == "0" || isOperatorClicked || isNewEntry)
            {
                Display.Text = button.Text;
                isOperatorClicked = false;
                isNewEntry = false;
            }
            else
            {
                Display.Text += button.Text;
            }
        }

        void OnOperator(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            // Handle "+/-" operation immediately
            if (button.Text == "+/-")
            {
                double currentValue = Convert.ToDouble(Display.Text);
                currentValue *= -1; // Negate the current value
                Display.Text = currentValue.ToString();
                return; // Exit early to avoid changing firstNumber and operatorSelected
            }

            if (isOperatorClicked && operatorSelected == "-")
            {
                // If the current operator is "-" and the user clicks "-" again,
                // append the "-" sign for the next negative number.
                Display.Text += button.Text;
                return; // Exit early
            }

            if (!isOperatorClicked)
            {
                firstNumber = Convert.ToDouble(Display.Text);
                operatorSelected = button.Text;
                isOperatorClicked = true;
                isNewEntry = true;
            }
        }

        void OnEquals(object sender, EventArgs e)
        {
            if (!isOperatorClicked)
            {
                secondNumber = Convert.ToDouble(Display.Text);

                double result = 0;
                string operation = $"{firstNumber} {operatorSelected} {secondNumber} = ";

                switch (operatorSelected)
                {
                    case "+":
                        result = firstNumber + secondNumber;
                        break;
                    case "-":
                        result = firstNumber - secondNumber;
                        break;
                    case "×":
                        result = firstNumber * secondNumber;
                        break;
                    case "÷":
                        if (secondNumber != 0)
                        {
                            result = firstNumber / secondNumber;
                        }
                        else
                        {
                            Display.Text = "Error";
                            return;
                        }
                        break;
                    case "%":
                        result = (firstNumber * secondNumber) / 100;
                        break;
                }

                if (operatorSelected != "%")
                {
                    Display.Text = result.ToString();
                }

                // Add result to history and maintain only last 50 entries
                operation += result.ToString();
                history.Add(operation);
                if (history.Count > 50)
                {
                    history.RemoveAt(0);
                }

                isOperatorClicked = false;
                isNewEntry = true;
            }
        }




        void OnClear(object sender, EventArgs e)
        {
            firstNumber = 0;
            secondNumber = 0;
            operatorSelected = "";
            isOperatorClicked = false;
            isNewEntry = false;
            Display.Text = "0";
        }

        void OnDecimal(object sender, EventArgs e)
        {
            if (!Display.Text.Contains("."))
            {
                Display.Text += ".";
            }
        }
    }
}
