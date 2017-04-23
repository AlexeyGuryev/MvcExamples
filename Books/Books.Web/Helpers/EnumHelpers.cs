using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Books.Web.Helpers
{
    public static class EnumHelpers
    {
        public static IEnumerable<SelectListItem> GetItems(this Type enumType, int? selectedValue)
        {
            if (!typeof(Enum).IsAssignableFrom(enumType)) {
                throw new ArgumentException("Type is not enum");
            }

            var names = Enum.GetNames(enumType);
            var values = Enum.GetValues(enumType).Cast<int>();

            var items = names.Zip(values, (name, value) => {
                return new SelectListItem { Text = GetName(enumType, name), Value = value.ToString(), Selected = (value == selectedValue) };
            });
            return items;
        }

        public static string GetItemName(this Type enumType, int? selectedValue)
        {
            var name = Enum.GetName(enumType, selectedValue);
            if (!string.IsNullOrEmpty(name)) {
                return GetName(enumType, name);
            }
            return name;
        }

        private static string GetName(Type enumType, string name)
        {
            var result = name;
            var attribute = enumType
                .GetField(name)
                .GetCustomAttributes(inherit: false)
                .OfType<DisplayAttribute>()
                .FirstOrDefault();

            if (attribute != null)
            {
                result = attribute.GetName();
            }
            return result;
        }
    }
}