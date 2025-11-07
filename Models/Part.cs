using System.Xml;

namespace FaSoLaSearch.Models
{
    public class Part
    {
        public int PartId { get; set; }
        public required int SongNumber { get; set; }
        public required string SongName { get; set; }
        public required string Name { get; set; }
        public required char First { get; set; }
        public required char Second { get; set; }
        public required char Third { get; set; }
        public required char Fourth { get; set; }
        public required char Fifth { get; set; }
        public required char Sixth { get; set; }
        public required char Seventh { get; set; }
        public required char Eighth { get; set; }
    }
}
