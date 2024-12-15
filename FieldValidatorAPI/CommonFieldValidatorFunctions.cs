using FieldValidatorAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FieldValidatorAPI
{
    public delegate bool RequiredValidDel(string fieldVal);
    public delegate bool StringLengthValidDel(string fieldVal, int min, int max);
    public delegate bool DateValidDel(string fieldVal, out DateTime date_time);
    public delegate bool PatternMatchDel(string fieldVal, string pattern);
    public delegate bool CompareFieldsValidDel(string fieldVal, string FieldValidCompare);

    public class CommonFieldValidatorFunctions
    {
        private static RequiredValidDel _requiredValidDel = null;
        private static StringLengthValidDel _stringLengthValidDel = null;
        private static DateValidDel _dateValidDel = null;
        private static PatternMatchDel _patternMatchDel = null;
        private static CompareFieldsValidDel _compareFieldsValidDel = null;

        public static RequiredValidDel RequiredFieldValidDel
        {
            get
            {
                if(_requiredValidDel == null)
                    _requiredValidDel = new RequiredValidDel(RequiredFieldValid);
                    return _requiredValidDel;
            }
        }

        public static StringLengthValidDel stringLengthValid_Del
        {
            get
            {
                if(_stringLengthValidDel == null)
                
                    _stringLengthValidDel = new StringLengthValidDel(StringFieldLengthValid);
                    return _stringLengthValidDel;
            }
        }

        public static DateValidDel DateValid_Del
        {
            get
            {
                if (_dateValidDel == null)
                    _dateValidDel = new DateValidDel(DateFieldValidDel);
                    return _dateValidDel;
            }
        }

        public static PatternMatchDel PatternMatch_Del
        {
            get
            {
                if( _patternMatchDel == null)
                
                    _patternMatchDel = new PatternMatchDel(FieldPatternValid);
                     return _patternMatchDel;
            }
        }

        public static CompareFieldsValidDel CompareFieldsValid_Del
        {
            get
            {
                if (_compareFieldsValidDel == null)
                    _compareFieldsValidDel = new CompareFieldsValidDel(FieldComparisionValid);
                    return _compareFieldsValidDel;
            }
        }

        private static bool RequiredFieldValid(string FieldVal)
        {
            if (!string.IsNullOrEmpty(FieldVal))
            {
                return true;
            }
            return false;
        }

        private static bool StringFieldLengthValid(string fieldVal, int min, int max)
        {
            if (fieldVal.Length > max && fieldVal.Length < min)
            {
                return false;
            }

            else
            { return true; }

        }
        private static bool DateFieldValidDel(string fieldVal, out DateTime date_time)
        {
            if (DateTime.TryParse(fieldVal, out date_time))
            {
                return true;
            }

            else
            {
                return false;
            }
        }
        private static bool FieldPatternValid(string fieldVal, string regularExpressionPattern)
        {
            Regex regex = new Regex(regularExpressionPattern);
            if (regex.IsMatch(fieldVal))
            {
                return true;
            }
            return false;
        }

        private static bool FieldComparisionValid(string field1, string field2)
        {
            if (field1 == field2)
            {
                return true;
            }
            return false;
        }
    }
}