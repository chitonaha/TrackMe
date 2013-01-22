using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrackMe.Support.Import
{
    /// <summary>
    /// Generic structure to perform import of simple objects from a <see cref="DbConnection"/> into entities.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public abstract class DataImporter<TEntity>
    {
        private int _rowIndex;

        /// <summary>
        /// Imports a list of entities from the current connection.
        /// </summary>
        /// <returns>List of imported entities.</returns>
        public IList<TEntity> Import()
        {
            IList<TEntity> importedItems = new List<TEntity>();
            object[] values = new string[0];
            try
            {
                String errors = String.Empty;
                ImportedItemTransformer<TEntity> transformer = this.Transformer;

                this.OpenDocument();

                _rowIndex = 1;
                values = this.GetRow();

                while (values.Length > 0)
                {
                    if (IsDataRow)
                    {
                        if (this.IsValidItem(transformer, values))
                            importedItems.Add(transformer.TransformItem(values));
                    }

                    _rowIndex++;
                    values = this.GetRow();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return importedItems;
        }

        /// <summary>
        /// Get the number of the current imported row.
        /// </summary>
        protected int RowIndex
        {
            get { return this._rowIndex; }
        }

        /// <summary>
        /// Check if an imported value is valid.
        /// </summary>
        /// <param name="transformer">The transformer to use during the import process.</param>
        /// <param name="values">The import values.</param>
        protected virtual Boolean IsValidItem(ImportedItemTransformer<TEntity> transformer, object[] values)
        {
            return transformer.IsValidItem(values);
        }

        /// <summary>
        /// Gets the service which will transform every resulting row into objects.
        /// </summary>
        /// <value>The transformer to use during the import process.</value>
        public abstract ImportedItemTransformer<TEntity> Transformer { get; }

        #region Metodos Nuevos

        public abstract void OpenDocument();

        public abstract object[] GetRow();

        public abstract bool IsDataRow { get; }

        #endregion
    }
}
