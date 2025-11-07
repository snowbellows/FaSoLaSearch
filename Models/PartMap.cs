using CsvHelper.Configuration;

namespace FaSoLaSearch.Models
{
    public sealed class PartMap : ClassMap<Part>
    {
        public PartMap()
        {
            Map(m => m.SongNumber).Name("SongNumber");
            Map(m => m.SongName).Name("SongName");
            Map(m => m.Name).Name("Name");
            Map(m => m.First).Name("First");
            Map(m => m.Second).Name("Second");
            Map(m => m.Third).Name("Third");
            Map(m => m.Fourth).Name("Fourth");
            Map(m => m.Fifth).Name("Fifth");
            Map(m => m.Sixth).Name("Sixth");
            Map(m => m.Seventh).Name("Seventh");
            Map(m => m.Eighth).Name("Eighth");
        }
    }
}
