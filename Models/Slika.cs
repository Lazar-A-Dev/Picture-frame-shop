using System.ComponentModel.DataAnnotations;

namespace Models{
    public class Slika{
        [Key]
        public int ID{get; set;}
        public int Sifra{get; set;}
        public string? NazivSlike{get; set;}
        public string? VelicnaSlike{get; set;}
        public int Kolicina{get; set;}
        public int Cena{get; set;}
        public Boja? Boja{get; set;}
        public Materijal? Materijal{get; set;}
        public Kategorija? Kategorija{get; set;}
    }
}