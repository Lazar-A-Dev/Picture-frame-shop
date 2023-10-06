using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Models
{
    [ApiController]
    [Route("[controller]")]
    public class ProdavnicaController : ControllerBase
    {
        public Context _context;

        public ProdavnicaController(Context context)
        {
            _context = context;
        }

        [Route("VratiSveProdavnice")]
        [HttpGet]
        public async Task<ActionResult> VratiSveProdavnice(){
            try{
                var pro = await _context.Prodavnice.Include(b => b.Boje).Include(m => m.Materijali).Include(k => k.Kategorije).ToListAsync();
                return Ok(pro);
            }
            catch(Exception e){
                return BadRequest(e.Message);
            }
        }

        [Route("NapraviProdavnicu")]
        [HttpPost]
        public async Task<ActionResult> NapraviProdavnicu(Prodavnica pro){
            try{
                _context.Prodavnice.Add(pro);
                await _context.SaveChangesAsync();
                return Ok("Uspesno napravljena prodavnica");
            }
            catch(Exception e){
                return BadRequest(e.Message);
            }
        }

        [Route("DodajProdavniciBoju")]
        [HttpPut]
        public async Task<ActionResult> DodajProdavniciBoju(int idPro, int idBoja){
            try{
                var pro = await _context.Prodavnice.Include(b => b.Boje).FirstOrDefaultAsync(p => p.ID == idPro);
                if(pro == null)
                return BadRequest("Prodavnica sa ovim id-jem ne postoji");

                var boja = await _context.Boje.FindAsync(idBoja);
                if(boja == null)
                return BadRequest("Boja sa ovim id-jem ne postoji");


                pro.Boje.Add(boja);
                await _context.SaveChangesAsync();
                return Ok("Uspesno je boja dodata prodavnici");
            }
            catch(Exception e){
                return BadRequest(e.Message);
            }
        }

        [Route("DodajProdavniciKat")]
        [HttpPut]
        public async Task<ActionResult> DodajProdavniciKat(int idPro, int idKat){
            try{
                var pro = await _context.Prodavnice.Include(k => k.Kategorije).FirstOrDefaultAsync(p => p.ID == idPro);
                if(pro == null)
                return BadRequest("Prodavnica sa ovim id-jem ne postoji");

                var kat = await _context.Kategorije.FindAsync(idKat);
                if(kat == null)
                return BadRequest("Kategorija sa ovim id-jem ne postoji");


                pro.Kategorije.Add(kat);
                await _context.SaveChangesAsync();
                return Ok("Uspesno je kategorija dodata prodavnici");
            }
            catch(Exception e){
                return BadRequest(e.Message);
            }
        }

        [Route("DodajProdavniciMat")]
        [HttpPut]
        public async Task<ActionResult> DodajProdavniciMat(int idPro, int idMat){
            try{
                var pro = await _context.Prodavnice.Include(m => m.Materijali).FirstOrDefaultAsync(p => p.ID == idPro);
                if(pro == null)
                return BadRequest("Prodavnica sa ovim id-jem ne postoji");

                var mat = await _context.Materijali.FindAsync(idMat);
                if(mat == null)
                return BadRequest("Materijal sa ovim id-jem ne postoji");


                pro.Materijali.Add(mat);
                await _context.SaveChangesAsync();
                return Ok("Uspesno je materijal dodat prodavnici");
            }
            catch(Exception e){
                return BadRequest(e.Message);
            }
        }
    }
}