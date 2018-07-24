using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PegePlayer.Common.Utils
{
    public class PegBoardFileSource : IPegBoardSource
    {
        public int Columns { get; private set; }
        public int Rows { get; private set; }
        public int Goal { get; private set; }
        public IEnumerable<Peg> MissingPegs { get; private set; }

        public static PegBoardFileSource ParseFile(string path)
        {
            if (!File.Exists(path))
            {
                throw new ArgumentException("File not found");
            }

            using (var file = File.OpenRead(path))
            {
                using (var streamReader = new StreamReader(file, Encoding.UTF8))
                {
                    var size = ParseFromString(streamReader.ReadLine());
                    return new PegBoardFileSource
                    {
                        Columns = size.Column,
                        Rows = size.Row,
                        Goal = ParseInt(streamReader.ReadLine()),
                        MissingPegs = ParseMissingPegs(streamReader)
                    };
                }
            }            
        }

        private static IEnumerable<Peg> ParseMissingPegs(StreamReader streamReader)
        {
            var totalMisingPegs = ParseInt(streamReader.ReadLine());
            var missingPegs = new List<Peg>(totalMisingPegs);
            for (var missingPegIdx = 0; missingPegIdx < totalMisingPegs; missingPegIdx++)
            {
                missingPegs.Add(ParseFromString(streamReader.ReadLine()));
            }

            return missingPegs;
        }

        private static int ParseInt(string line)
        {
            if (int.TryParse(line, out var goal))
            {
                return goal;
            }

            throw new ArgumentException("Invalid goal line");
        }

        private static Peg ParseFromString(string line)
        {
            var size = line.Split(',').Select(part => part.Trim());
            if (size.Count() == 2 &&
                int.TryParse(size.ElementAt(1), out var columns) &&
                int.TryParse(size.ElementAt(0), out var rows))
            {
                return Peg.Create(rows, columns);
            }

            throw new ArgumentException("invalid board size line");
        }
    }
}
