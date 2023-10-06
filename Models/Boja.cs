using System.ComponentModel.DataAnnotations;

namespace Models{
    public class Boja{
        [Key]
        public int ID{get; set;}
        public string? NazivBoje{get; set;}
        public List<Slika>? Slike{get; set;}
    }
}