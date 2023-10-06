using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Models
{
    [ApiController]
    [Route("[controller]")]
    public class BojaController : ControllerBase
    {
        public Context _context;

        public BojaController(Context context)
        {
            _context = context;
        }

        [Route("VratiSveBoje")]
        [HttpGet]
        public async Task<ActionResult> VratiSveBoje(){
            try{
                var boja = await _context.Boje.ToListAsync();
                return Ok(boja);
            }
            catch(Exception e){
                return BadRequest(e.Message);
            }
        }

        [Route("NapraviBoju")]
        [HttpPost]
        public async Task<ActionResult> NapraviBoju(Boja boja){
            try{
                _context.Boje.Add(boja);
                await _context.SaveChangesAsync();
                return Ok("Uspesno napravljena boja");
            }
            catch(Exception e){
                return BadRequest(e.Message);
            }
        }

        [Route("DodajBojiSliku")]
        [HttpPut]
        public async Task<ActionResult> DodajBojiSliku(int idBoja, int idSlika){
            try{
                var boja = await _context.Boje.Include(s => s.Slike).FirstOrDefaultAsync(b => b.ID == idBoja);
                if(boja == null)
                return BadRequest("Boja sa ovim id-jem ne postoji");

                var slika = await _context.Slike.FindAsync(idSlika);
                if(slika == null)
                return BadRequest("Slika sa ovim id-jem ne postoji");


                boja.Slike.Add(slika);
                slika.Boja = boja;
                await _context.SaveChangesAsync();
                return Ok("Uspesno je slika dodata boji");
            }
            catch(Exception e){
                return BadRequest(e.Message);
            }
        }
    }
}