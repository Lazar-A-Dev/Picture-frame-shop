using System.ComponentModel.DataAnnotations;

namespace Models{
    public class Materijal{
        [Key]
        public int ID{get; set;}
        public string? NazivMaterijala{get; set;}
        public List<Slika>? Slike{get; set;}
    }
}