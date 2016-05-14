// Developed by: Gabriel Duarte
// 
// Created at: 10/05/2016 01:39

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls;
using MahApps.Metro.SimpleChildWindow;

namespace SparkMediaManager.Helpers
{
    public static class Extensions
    {
        public static void AplicarVisualAtributosDataContext(this Window objWindow)
        {
            foreach (TextBox oTextBox in objWindow.FindChildren<TextBox>())
            {
                AplicarVisualTextBox(oTextBox, objWindow.DataContext);
            }
        }

        public static void AplicarVisualAtributosDataContext(this ChildWindow objWindow)
        {
            foreach (Control oControl in objWindow.FindChildren<Control>())
            {
                if (oControl is TextBox)
                {
                    AplicarVisualTextBox((TextBox) oControl, objWindow.DataContext);
                }
                else if (oControl is Button)
                {
                    AplicarVisualButton((Button) oControl, objWindow.DataContext);
                }
                else if (oControl is NumericUpDown)
                {
                    AplicarVisualNumericUpDown((NumericUpDown) oControl, objWindow.DataContext);
                }
            }
        }

        private static void AplicarVisualNumericUpDown(NumericUpDown objNumericUpDown, object objDataContext)
        {
            string strPropriedade = objNumericUpDown.GetBindingExpression(NumericUpDown.ValueProperty)?.ParentBinding.Path.Path;

            IEnumerable<CustomAttributeData> lstAtributos = objDataContext.GetType().GetProperties().First(x => x.Name == strPropriedade).CustomAttributes;

            foreach (CustomAttributeData objAtributo in lstAtributos)
            {
                switch (objAtributo.AttributeType.Name)
                {
                    case nameof(RangeAttribute):
                        objNumericUpDown.Minimum = (int) objAtributo.ConstructorArguments.First(x => x.ArgumentType == typeof(int)).Value;
                        objNumericUpDown.Maximum = (int) objAtributo.ConstructorArguments.Where(x => x.ArgumentType == typeof(int)).Skip(1).First().Value;
                        break;
                }
            }
        }

        private static void AplicarVisualButton(Button objButton, object objDataContext)
        {
            if (objButton.IsDefault)
            {
                objButton.Style = Application.Current.FindResource("ButtonDefault") as Style;
            }
        }

        private static void AplicarVisualTextBox(TextBox objTextBox, object objDataContext)
        {
            string strPropriedade = objTextBox.GetBindingExpression(TextBox.TextProperty)?.ParentBinding.Path.Path;

            if (string.IsNullOrWhiteSpace(strPropriedade))
            {
                return;
            }

            IEnumerable<CustomAttributeData> lstAtributos = objDataContext.GetType().GetProperties().First(x => x.Name == strPropriedade).CustomAttributes;

            foreach (CustomAttributeData objAtributo in lstAtributos)
            {
                switch (objAtributo.AttributeType.Name)
                {
                    case nameof(RequiredAttribute):
                        objTextBox.Style = Application.Current.FindResource("TextboxRequired") as Style;
                        break;
                    case nameof(MaxLengthAttribute):
                        objTextBox.MaxLength = (int) objAtributo.ConstructorArguments.First(x => x.ArgumentType == typeof(int)).Value;
                        break;
                }
            }
        }
    }
}
