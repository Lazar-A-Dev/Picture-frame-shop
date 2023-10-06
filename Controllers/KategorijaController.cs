using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Models
{
    [ApiController]
    [Route("[controller]")]
    public class KategorijaController : ControllerBase
    {
        public Context _context;

        public KategorijaController(Context context)
        {
            _context = context;
        }

        [Route("VratiSveKat")]
        [HttpGet]
        public async Task<ActionResult> VratiSveKat(){
            try{
                var kat = await _context.Kategorije.ToListAsync();
                return Ok(kat);
            }
            catch(Exception e){
                return BadRequest(e.Message);
            }
        }

        [Route("NapraviKat")]
        [HttpPost]
        public async Task<ActionResult> NapraviKat(Kategorija kat){
            try{
                _context.Kategorije.Add(kat);
                await _context.SaveChangesAsync();
                return Ok("Uspesno napravljena kategorija");
            }
            catch(Exception e){
                return BadRequest(e.Message);
            }
        }

        [Route("DodajKatSliku")]
        [HttpPut]
        public async Task<ActionResult> DodajKatSliku(int idKat, int idSlika){
            try{
                var kat = await _context.Kategorije.Include(s => s.Slike).FirstOrDefaultAsync(k => k.ID == idKat);
                if(kat == null)
                return BadRequest("Kategorija sa ovim id-jem ne postoji");

                var slika = await _context.Slike.FindAsync(idSlika);
                if(slika == null)
                return BadRequest("Slika sa ovim id-jem ne postoji");


                kat.Slike.Add(slika);
                slika.Kategorija = kat;
                await _context.SaveChangesAsync();
                return Ok("Uspesno je slika dodata kategoriji");
            }
            catch(Exception e){
                return BadRequest(e.Message);
            }
        }
    }
}