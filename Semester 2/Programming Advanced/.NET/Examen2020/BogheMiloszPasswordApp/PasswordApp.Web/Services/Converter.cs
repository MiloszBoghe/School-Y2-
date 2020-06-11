using System;
using PasswordApp.Data;
using PasswordApp.Web.Models;
using PasswordApp.Web.Services.Contracts;

namespace PasswordApp.Web.Services
{
    public class Converter : IConverter
    {
        /// <summary>
        /// Converts an object to TTarget
        /// </summary>
        /// <typeparam name="TTarget">The target type the source object is converted to.</typeparam>
        /// <param name="source">The object that must be converted.</param>
        /// <returns>The converted object.</returns>
        public TTarget ConvertTo<TTarget>(object source) where TTarget: class
        {
            if (source is Entry entry)
            {
                if (typeof(TTarget) == typeof(EntryListItemViewModel))
                {
                    return ConvertEntryToListItemViewModel(entry) as TTarget;
                }

                if (typeof(TTarget) == typeof(EntryEditViewModel))
                {
                    return ConvertEntryToEditViewModel(entry) as TTarget;
                }
            }
            throw new InvalidOperationException("Conversion not supported");
        }

        private EntryListItemViewModel ConvertEntryToListItemViewModel(Entry entry)
        {
            return new EntryListItemViewModel
            {
                Id = entry.Id,
                Title = entry.Title,
                UserName = entry.UserName
            };
        }

        private EntryEditViewModel ConvertEntryToEditViewModel(Entry entry)
        {
            return new EntryEditViewModel
            {
                Password = entry.Password,
                Url = entry.Url
            };
        }
    }
}