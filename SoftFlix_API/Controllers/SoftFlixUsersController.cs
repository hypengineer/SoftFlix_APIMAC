using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoftFlix_API.Data;
using SoftFlix_API.Models;
using SoftFlix_API.Models.ViewModels;

namespace SoftFlix_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SoftFlixUsersController : ControllerBase
    {
        

        private readonly SignInManager<SoftFlixUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public SoftFlixUsersController(SignInManager<SoftFlixUser> signInManager, ApplicationDbContext context)
        {
            _signInManager = signInManager;
            _context = context;
        }

        // GET: api/SoftFlixUsers
        [HttpGet]
        //[Authorize(Roles = "Administrator")]
        public ActionResult<List<SoftFlixUser>> GetUsers(bool includePassive = true)
        {
            IQueryable<SoftFlixUser> users = _signInManager.UserManager.Users;

            if (includePassive == false)
            {
                users = users.Where(u => u.Passive == false);
            }
            return users.AsNoTracking().ToList();
        }

        // GET: api/SoftFlixUsers/5
        [HttpGet("{id}")]
        //[Authorize]
        public ActionResult<SoftFlixUser> GetSoftFlixUser(long id)
        {
            SoftFlixUser? SoftFlixUser = null;

            if (User.IsInRole("Administrator") == false)
            {
                if (User.FindFirstValue(ClaimTypes.NameIdentifier) != id.ToString())
                {
                    return Unauthorized();
                }
            }
            SoftFlixUser = _signInManager.UserManager.Users.Where(u => u.Id == id).AsNoTracking().FirstOrDefault();

            if (SoftFlixUser == null)
            {
                return NotFound();
            }

            return SoftFlixUser;
        }

        // PUT: api/SoftFlixUsers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        //[Authorize]
        public ActionResult PutSoftFlixUser(SoftFlixUser SoftFlixUser)
        {
            SoftFlixUser? user = null;

            if (User.IsInRole("CustomerRepresentative") == false)
            {
                if (User.FindFirstValue(ClaimTypes.NameIdentifier) != SoftFlixUser.Id.ToString())
                {
                    return Unauthorized();
                }
            }
            user = _signInManager.UserManager.Users.Where(u => u.Id == SoftFlixUser.Id).FirstOrDefault();

            if (user == null)
            {
                return NotFound();
            }
            user.PhoneNumber = SoftFlixUser.PhoneNumber;
            user.BirthDate = SoftFlixUser.BirthDate;
            user.Email = SoftFlixUser.Email;
            user.Name = SoftFlixUser.Name;
            _signInManager.UserManager.UpdateAsync(user).Wait();
            return Ok();
        }

        // POST: api/SoftFlixUsers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<string> PostSoftFlixUser(SoftFlixUser SoftFlixUser)
        {
            //if (User.Identity!.IsAuthenticated == true)
            //{
            //    return BadRequest();
            //}
            IdentityResult identityResult = _signInManager.UserManager.CreateAsync(SoftFlixUser, SoftFlixUser.Password).Result;

            if (identityResult != IdentityResult.Success)
            {
                return identityResult.Errors.FirstOrDefault()!.Description;
            }
            return Ok();
        }

        // DELETE: api/SoftFlixUsers/5
        [HttpDelete("{id}")]
        //[Authorize]
        public ActionResult DeleteSoftFlixUser(long id)
        {
            SoftFlixUser? user = null;

            if (User.IsInRole("Administrator") == false)
            {
                if (User.FindFirstValue(ClaimTypes.NameIdentifier) != id.ToString())
                {
                    return Unauthorized();
                }
            }
            user = _signInManager.UserManager.Users.Where(u => u.Id == id).FirstOrDefault();

            if (user == null)
            {
                return NotFound();
            }
            user.Passive = true;
            _signInManager.UserManager.UpdateAsync(user).Wait();
            return Ok();
        }

        [HttpPost("LogIn")]
        public ActionResult<List<Media>?> LogIn(LogInVM logInModel)
        {
            Microsoft.AspNetCore.Identity.SignInResult signInResult;
            SoftFlixUser applicationUser = _signInManager.UserManager.FindByNameAsync(logInModel.UserName).Result;
            List<Media>? medias = null;
            List<UserFavorite> userFavorites;
            IGrouping<short, MediaCategory>? mediaCategories;
            IQueryable<Media> mediaQuery;
            IQueryable<int> userWatcheds;

            if (applicationUser == null)
            {
                return NotFound();
            }
            //if (_context.UserPlans.Where(u => u.UserId == applicationUser.Id && u.EndDate >= DateTime.Today).Any() == false)
            //{
            //    applicationUser.Passive = true;
            //    _signInManager.UserManager.UpdateAsync(applicationUser).Wait();
            //}
            //if (applicationUser.Passive == true)
            //{
            //    return Content("Passive");
            //}
            signInResult = _signInManager.PasswordSignInAsync(applicationUser, logInModel.Password, false, false).Result;
            if (signInResult.Succeeded == true)
            {
                
                //Kullanıcının favori olarak işaretlediği mediaları ve kategorilerini alıyoruz.
                userFavorites = _context.UserFavorites.Where(u => u.UserId == applicationUser.Id).Include(u => u.Media).Include(u => u.Media!.MediaCategories).ToList();
                //userFavorites içindeki media kategorilerini ayıklıyoruz (SelectMany)
                //Bunları kategori id'lerine göre grupluyoruz (GroupBy)
                //Her grupta kaç adet item olduğuna bakıp (m.Count())
                //Çoktan aza doğru sıralıyoruz (OrderByDescending)
                //En üstteki, yani en çok item'a sahip grubu seçiyoruz (FirstOrDefault)
                mediaCategories = userFavorites.SelectMany(u => u.Media!.MediaCategories!).GroupBy(m => m.CategoryId).OrderByDescending(m => m.Count()).FirstOrDefault();
                if (mediaCategories != null)
                {
                    //Kullabıcının izlediği episode'lardan media'ya ulaşıp, sadece media id'lerini alıyoruz (Select)
                    //Tekrar eden media id'leri eliyoruz (Distinct)
                    userWatcheds = _context.UserWatcheds.Where(u => u.UserId == applicationUser.Id).Include(u => u.Episode).Select(u => u.Episode!.MediaId).Distinct();
                    //Öneri yapmak için mediaCategories.Key'i yani kullanıcının en çok favorilediği kategori id'sini kullanıyoruz
                    //Media listesini çekerken sadece bu kategoriye ait mediaların MediaCategories listesini dolduruyoruz (Include(m => m.MediaCategories!.Where(mc => mc.CategoryId == mediaCategories.Key)))
                    //Diğer mediaların MediaCategories listeleri boş kalıyor
                    //Sonrasında MediaCategories'i boş olmayan media'ları filtreliyoruz (m.MediaCategories!.Count > 0)
                    //Ayrıca bu kategoriye giren fakat kullanıcının izlemiş olduklarını da dışarıda bırakıyoruz (userWatcheds.Contains(m.Id) == false)
                    mediaQuery = _context.Medias.Include(m => m.MediaCategories!.Where(mc => mc.CategoryId == mediaCategories.Key)).Where(m => m.MediaCategories!.Count > 0 && userWatcheds.Contains(m.Id) == false);
                    if (applicationUser.Restriction != null)
                    {
                        //TO DO
                        //Son olarak, kullanıcı bir restrictiona sahipse seçilen media içerisinden bunları da çıkarmamız gerekiyor.
                        mediaQuery = mediaQuery.Include(m => m.MediaRestrictions!.Where(r => r.RestrictionId <= applicationUser.Restriction));
                    }
                    medias = mediaQuery.ToList();
                }
                //Populate medias
            }
            return medias;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            SoftFlixUser user = new SoftFlixUser();
            user.Name = registerVM.Name;
            user.UserName = registerVM.UserName;
            user.Email = registerVM.EmailAddress;
            user.PhoneNumber = registerVM.PhoneNumber;
            user.BirthDate = registerVM.BirthDate;
            user.Passive = true;

            IdentityResult identityResult = _signInManager.UserManager.CreateAsync(user, registerVM.Password).Result;
            await _signInManager.UserManager.AddToRoleAsync(user, "Manager");
            return Ok();
        }
    }
}
