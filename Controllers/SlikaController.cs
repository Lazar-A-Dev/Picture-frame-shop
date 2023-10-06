using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Models
{
    [ApiController]
    [Route("[controller]")]
    public class SlikaController : ControllerBase
    {
        public Context _context;

        public SlikaController(Context context)
        {
            _context = context;
        }

        [Route("VratiSveSlike")]
        [HttpGet]
        public async Task<ActionResult> VratiSveSlike()
        {
            try
            {
                var slika = await _context.Slike.ToListAsync();
                return Ok(slika);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("VratiJednuSlike/{idSlike}")]
        [HttpGet]
        public async Task<ActionResult> VratiJednuSlike(int idSlike)
        {
            try
            {
                var slika = await _context.Slike.FindAsync(idSlike);
                if (slika == null)
                    return BadRequest("Slika sa ovim id-jem ne postoji");
                return Ok(slika);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("VratiTrazeneSlike/{idBoje}/{idKat}/{idMat}/{cenaOd}/{cenaDo}")]
        [HttpGet]
        public async Task<ActionResult> VratiTrazeneSlike(int idBoje, int idKat, int idMat, int cenaOd, int cenaDo)
        {
            try
            {
                var boja = await _context.Kategorije.FindAsync(idBoje);
                if (boja == null)
                    return BadRequest("Boja sa ovim id-jem ne postoji");

                var kat = await _context.Kategorije.FindAsync(idKat);
                if (kat == null)
                    return BadRequest("Kategorija sa ovim id-jem ne postoji");

                var mat = await _context.Materijali.FindAsync(idMat);
                if (mat == null)
                    return BadRequest("Materijal sa ovim id-jem ne postoji");


                var slike = await _context.Slike
            .Where(s => s.Boja.ID == idBoje && s.Materijal.ID == idMat && s.Kategorija.ID == idKat && s.Cena >= cenaOd && s.Cena <= cenaDo && s.Kolicina > 0)
            .Select(s => new
            {
                s.ID,
                s.NazivSlike,
                s.VelicnaSlike,
                s.Kolicina,
                s.Cena,
                s.Sifra,
                BojaNaziv = s.Boja.NazivBoje, // Ako želite da uključite naziv boje u rezultat
                MaterijalNaziv = s.Materijal.NazivMaterijala, // Ako želite da uključite naziv materijala u rezultat
                KategorijaNaziv = s.Kategorija.NazivKategorije // Ako želite da uključite naziv kategorije u rezultat
            })
            .ToListAsync();

            return Ok(slike);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("NapraviSliku")]
        [HttpPost]
        public async Task<ActionResult> NapraviSliku(Slika slika)
        {
            try
            {
                _context.Slike.Add(slika);
                await _context.SaveChangesAsync();
                return Ok("Uspesno napravljena slika");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("PoruciSliku/{idSlike}")]
        [HttpPut]
        public async Task<ActionResult> PoruciSliku(int idSlike)
        {
            try
            {
                var slika = await _context.Slike.FindAsync(idSlike);
                if (slika == null)
                    return BadRequest("Slika sa ovim id-jem ne postoji");

                if (slika.Kolicina - 1 > 0)
                    slika.Kolicina--;
                await _context.SaveChangesAsync();
                return Ok("Uspesno je kupljena slika");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}