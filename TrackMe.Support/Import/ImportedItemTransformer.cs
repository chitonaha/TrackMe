using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrackMe.Support.Import
{
    /// <summary>
    /// Allows transforming the result of the database import into entities.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public abstract class ImportedItemTransformer<TEntity>
    {
        public object[] Header { get; set; }

        /// <summary>
        /// Transforms a single item represented by the <paramref name="itemValues"/> array into a new entity.
        /// </summary>
        /// <param name="itemValues">The values for the current item.</param>
        /// <returns>A new entity for the given values.</returns>
        public abstract TEntity TransformItem(object[] itemValues);

        public virtual void SetHeader(object[] header)
        {
            Header = header;
        }


        /// <summary>
        /// Determines whether the specified item values represents a valid item.
        /// </summary>
        /// <param name="itemValues">The item values which represents an element.</param>
        /// <returns>
        /// 	<c>true</c> if the element is valid ; otherwise, <c>false</c>.
        /// </returns>
        public virtual bool IsValidItem(object[] itemValues)
        {
            return true;
        }

        /// <summary>
        /// Gets the value specified by <paramref name="index"/> casted to the given type.
        /// </summary>
        /// <typeparam name="TValue">The type of the value to do the cast.</typeparam>
        /// <param name="itemValues">The item values.</param>
        /// <param name="index">The index to obtain.</param>
        /// <returns></returns>
        public TValue GetValue<TValue>(object[] itemValues, int index)
        {
            try
            {
                return (TValue)itemValues[index];
            }
            catch (InvalidCastException)
            {
                throw new Exception(String.Format("The item at index {0} was not a valid {1} instance. Item value: {2}. Item type: {3}", index, typeof(TValue), itemValues[index], itemValues[index].GetType()));
            }
        }

        /// <summary>
        /// Checks if the value is blank
        /// </summary>
        /// <param name="value">The value to check</param>
        /// <returns>True if the value is blank</returns>
        public bool IsBlankValue(object value)
        {
            return value == DBNull.Value || value == null || (value is string && String.IsNullOrEmpty(((string)value).Trim()));
        }


        /// <summary>
        /// Checks if the <paramref name="itemValues"/> array is a blank line.
        /// </summary>
        /// <param name="itemValues">Elements of the current line.</param>
        /// <returns>True if the elements are a blank line, else returns false.</returns>
        public bool IsBlankLine(object[] itemValues)
        {
            int blankLines = 0;
            for (int i = 0; i < itemValues.Length; i++)
            {
                if (this.IsBlankValue(itemValues[i]))
                {
                    ++blankLines;
                }
            }
            return blankLines == itemValues.Length;
        }
    }
}
