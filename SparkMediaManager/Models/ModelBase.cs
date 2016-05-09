// Developed by: Gabriel Duarte
// 
// Created at: 30/04/2016 16:03

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using GalaSoft.MvvmLight;

namespace SparkMediaManager.Models
{
    public class ModelBase : ObservableObject, INotifyDataErrorInfo
    {
        private void ValidarPropriedade(string propertyName, object value)
        {
            ModelBase objectToValidate = this;
            var results = new List<ValidationResult>();
            bool isValid = Validator.TryValidateProperty(
                                                         value,
                                                         new ValidationContext(objectToValidate, null, null)
                                                         {
                                                             MemberName = propertyName
                                                         },
                                                         results);

            if (isValid)
            {
                RemoveErrorsForProperty(propertyName);
            }
            else
            {
                AddErrorsForProperty(propertyName, results);
            }

            OnErrorsChanged(propertyName);
        }

        public virtual bool ValidarObjeto()
        {
            ModelBase objectToValidate = this;
            _errors.Clear();
            Type objectType = objectToValidate.GetType();
            PropertyInfo[] properties = objectType.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (property.GetCustomAttributes(typeof(ValidationAttribute), true).Any())
                {
                    object value = property.GetValue(objectToValidate, null);
                    ValidarPropriedade(property.Name, value);
                }
            }

            return !HasErrors;
        }

        #region Implementation of INotifyDataErrorInfo

        private readonly IDictionary<string, IList<string>> _errors = new Dictionary<string, IList<string>>();

        public IEnumerable GetErrors(string propertyName)
        {
            if (!_errors.ContainsKey(propertyName))
            {
                yield break;
            }

            IList<string> propertyErrors = _errors[propertyName];
            foreach (string propertyError in propertyErrors)
            {
                yield return propertyError;
            }
        }

        private void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        private void AddErrorsForProperty(string propertyName, IEnumerable<ValidationResult> validationResults)
        {
            RemoveErrorsForProperty(propertyName);
            _errors.Add(propertyName, validationResults.Select(vr => vr.ErrorMessage).ToList());
        }

        private void RemoveErrorsForProperty(string propertyName)
        {
            if (_errors.ContainsKey(propertyName))
            {
                _errors.Remove(propertyName);
            }
        }

        public bool HasErrors => _errors.Count > 0;

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        #endregion
    }
}
