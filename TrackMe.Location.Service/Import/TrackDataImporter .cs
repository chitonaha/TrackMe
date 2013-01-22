using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TrackMe.Support.Import;
using TrackMe.Model;
using System.IO;

namespace TrackMe.Location.Service.Import
{
    public class TrackDataImporter : TextDataImporter<Track>
    {
        private TrackDataTransformer _transformer;

        public TrackDataImporter(string textFilePath): base(textFilePath)
        {
            _transformer = new TrackDataTransformer();
        }

        public TrackDataImporter(string textFilePath, string separator): base(textFilePath, separator)
        {
            _transformer = new TrackDataTransformer();
        }

        public override ImportedItemTransformer<Track> Transformer
        {
            get { return _transformer; }
        }

        public override bool IsDataRow
        {
            get { return RowIndex > -1; }
        }
    }
}
