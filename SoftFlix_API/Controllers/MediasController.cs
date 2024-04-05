using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoftFlix_API.Data;
using SoftFlix_API.Models;

namespace SoftFlix_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MediasController : ControllerBase
    {
        //public struct MediaPostModel
        //{
        //    public Media Media { get; set; }
        //    public List<short> CatIds { get; set; }
        //}
        private readonly ApplicationDbContext _context;

        public MediasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Medias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Media>>> GetMedias()
        {
          if (_context.Medias == null)
          {
              return NotFound();
          }
            return await _context.Medias.ToListAsync();
        }

        // GET: api/Medias/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Media>> GetMedia(int id)
        {
          if (_context.Medias == null)
          {
              return NotFound();
          }
            var media = await _context.Medias.FindAsync(id);

            if (media == null)
            {
                return NotFound();
            }

            return media;
        }

        // PUT: api/Medias/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        //[Authorize(Roles = "ContentAdmin")]
        public void PutCategory(Media media)
        {
            _context.Medias.Update(media);

            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
            }
        }

        // POST: api/Meidas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<Media> PostMedia(Media model/*, int dirId, int starId*/) // Categoryleri çoklu parametle olarak ekleyemediğimiz için ek bir viewmodel(MediaPostModel) oluşturduk.
        {
           // Media media = model.Media;
            //List<short> catIds = model.CatIds;

            //MediaDirector mediaDirector = new MediaDirector();
            //MediaStar mediaStar = new MediaStar();
            _context.Medias.Add(model);
            _context.SaveChanges();

            //foreach(short catId in catIds)
            //{
            //    MediaCategory mediaCategory = new MediaCategory();

            //    mediaCategory.CategoryId = catId;
            //    mediaCategory.MediaId = media.Id;
            //    _context.MediaCategories.Add(mediaCategory);

            //}

            //mediaDirector.DirectorId =dirId;
            //mediaDirector.MediaId = media.Id;


            //mediaStar.StarId = starId;
            //mediaStar.MediaId = media.Id;


            //_context.MediaDirectors.Add(mediaDirector);
            //_context.MediaStars.Add(mediaStar);
            _context.SaveChanges();

            return Ok();
        }

        // DELETE: api/Medias/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedia(int id)
        {
            if (_context.Medias == null)
            {
                return NotFound();
            }
            var media = await _context.Medias.FindAsync(id);
            if (media == null)
            {
                return NotFound();
            }

            _context.Medias.Remove(media);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MediaExists(int id)
        {
            return (_context.Medias?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
