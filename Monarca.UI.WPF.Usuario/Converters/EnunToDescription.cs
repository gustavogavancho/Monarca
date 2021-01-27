﻿using System;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Windows.Data;

namespace Monarca.UI.WPF.Usuario.Converters
{
    public class EnunToDescription : IValueConverter
    {
        public string GetEnumDescription(Enum enumObj)
        {
            if (enumObj == null)
            {
                return string.Empty;
            }
            FieldInfo fieldInfo = enumObj.GetType().GetField(enumObj.ToString());

            object[] attribArray = fieldInfo?.GetCustomAttributes(false);

            if (attribArray?.Length == 0)
            {
                return enumObj.ToString();
            }
            else
            {
                DescriptionAttribute attrib = attribArray?[0] as DescriptionAttribute;
                return attrib?.Description;
            }
        }

        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Enum)
            {
                Enum myEnum = (Enum)value;
                if (myEnum == null)
                {
                    return null;
                }
                string description = GetEnumDescription(myEnum);
                if (!string.IsNullOrEmpty(description))
                {
                    return description;
                }
                return myEnum.ToString();
            }
            return null;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return string.Empty;
        }
    }
}
