using System.ComponentModel.DataAnnotations;

namespace Models{
    public class Prodavnica{
        [Key]
        public int ID{get; set;}
        public List<Boja>? Boje{get; set;}
        public List<Materijal>? Materijali{get; set;}
        public List<Kategorija>? Kategorije{get; set;}
    }
}