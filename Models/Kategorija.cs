using System.ComponentModel.DataAnnotations;

namespace Models{
    public class Kategorija{
        [Key]
        public int ID{get; set;}
        public string? NazivKategorije{get; set;}
        public List<Slika>? Slike{get; set;}
    }
}