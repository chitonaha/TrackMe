using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace TrackMe.Support.Import
{
    /// <summary>
    /// Imports items from an Text file allowing custom transformation from each Text row into objects. 
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity to import.</typeparam>
    public abstract class TextDataImporter<TEntity> : DataImporter<TEntity>
    {
        private StreamReader _document = null;
        private string _textFilePath;
        private string _separator = ",";

        /// <summary>
        /// Initializes a new instance of the TextDataImporter class.
        /// </summary>
        /// <param name="textFilePath">The text file path to use during the import.</param>
        public TextDataImporter(string textFilePath)
        {
            this._textFilePath = textFilePath;
        }

        /// <summary>
        /// Initializes a new instance of the TextDataImporter class.
        /// </summary>
        /// <param name="textFilePath">The text file path to use during the import.</param>
        /// <param name="separator">The text file separator.</param>
        public TextDataImporter(string textFilePath, string separator)
        {
            this._textFilePath = textFilePath;
            this._separator = separator;
        }

        public override void OpenDocument()
        {
            _document = new StreamReader(_textFilePath);
        }

        public override object[] GetRow()
        {
            string[] separators = new string[] { _separator };

            string row = _document.ReadLine();
            string[] result = (row == null) ? new string[0] : row.Split(separators, StringSplitOptions.None);

            return result;
        }
    }
}
