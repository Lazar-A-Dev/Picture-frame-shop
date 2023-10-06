using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Models
{
    [ApiController]
    [Route("[controller]")]
    public class MaterijalController : ControllerBase
    {
        public Context _context;

        public MaterijalController(Context context)
        {
            _context = context;
        }

        [Route("VratiSveMat")]
        [HttpGet]
        public async Task<ActionResult> VratiSveMat(){
            try{
                var mat = await _context.Materijali.ToListAsync();
                return Ok(mat);
            }
            catch(Exception e){
                return BadRequest(e.Message);
            }
        }

        [Route("NapraviMat")]
        [HttpPost]
        public async Task<ActionResult> NapraviMat(Materijal mat){
            try{
                _context.Materijali.Add(mat);
                await _context.SaveChangesAsync();
                return Ok("Uspesno napravljen materijal");
            }
            catch(Exception e){
                return BadRequest(e.Message);
            }
        }

        [Route("DodajMatSliku")]
        [HttpPut]
        public async Task<ActionResult> DodajMatSliku(int idMat, int idSlika){
            try{
                var mat = await _context.Materijali.Include(s => s.Slike).FirstOrDefaultAsync(m => m.ID == idMat);
                if(mat == null)
                return BadRequest("Materijal sa ovim id-jem ne postoji");

                var slika = await _context.Slike.FindAsync(idSlika);
                if(slika == null)
                return BadRequest("Slika sa ovim id-jem ne postoji");


                mat.Slike.Add(slika);
                slika.Materijal = mat;
                await _context.SaveChangesAsync();
                return Ok("Uspesno je slika dodata materijal");
            }
            catch(Exception e){
                return BadRequest(e.Message);
            }
        }
    }
}