using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Obuka_Vojnih_Pasa.Models;
using Obuka_Vojnih_Pasa.Models.Domain;
using Obuka_Vojnih_Pasa.Models.Extensions;
using Obuka_Vojnih_Pasa.Services;
using Obuka_Vojnih_Pasa.ViewModels;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;


namespace Obuka_Vojnih_Pasa.Controllers
{
    [AllowAnonymous]
    public class AdministrationController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IInstruktorService service;
        private readonly IObukaService serviceObuka;
        private readonly ObukaPasaContext context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ILogger<AdministrationController> logger;
        private readonly IConfiguration configuration;
        public AdministrationController(IInstruktorService service, ObukaPasaContext context, IObukaService serviceObuka, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ILogger<AdministrationController> logger, IConfiguration configuration)
        {
            this.roleManager = roleManager;
            this.service = service;
            this.serviceObuka = serviceObuka;
            this.context = context;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
            this.configuration = configuration;
        }




        [HttpGet]

        public IActionResult Register()
        {
            CinoviDropDownList();
            ObukeDropDownList();
            return View();
        }








        [HttpPost]

        public async Task<IActionResult> Register(RegistrationViewModel model)
        {

            if (ModelState.IsValid)
            {
                var user = new Instruktor
                {
                    Email = model.Email,
                    ImePrezime = model.ImePrezime,
                    Cin = model.Cin,
                    UserName = model.Username,
                    ObukaId = model.ObukaId


                };
                ObukeDropDownList(model.ObukaId);
                CinoviDropDownList(model.Cin);
                var result = await userManager.CreateAsync(user, model.Password);


                if (result.Succeeded)
                {
                    /*
                     * sve radi ok
                     * 
                    var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                    var confirmationLink = Url.Action("ConfirmEmail", "Account",
                        new { userId = user.Id, token = token }, Request.Scheme);
               
                    MailMessage mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress(configuration["Email:Email"]);
                    //user.Email
                    mailMessage.To.Add("katarinasimic1997@outlook.com");
                    mailMessage.Body = $"Kliknite na link kako biste aktivirali nalog {confirmationLink}";
                    mailMessage.Subject = "Confirmation link";
                    SmtpClient client = new SmtpClient("smtp-mail.outlook.com");
                    client.Port = 587;
                    client.UseDefaultCredentials = false;
                    client.EnableSsl = true;
                    var credential = new NetworkCredential
                    {
                        UserName = configuration["Email:Email"],
                        Password = configuration["Email:Password"]
                    };
                    client.Credentials = credential;
                    client.Send(mailMessage);
                        logger.Log(LogLevel.Warning, confirmationLink);
                    return RedirectToAction("GetAllUsers", new { message = $"Link za verifikaciju naloga korisnika {user.UserName} je poslat na email adresu {user.Email}" });
                    */
                    try
                    {
                        var resultRole = await userManager.AddToRoleAsync(user, "Instruktor");

                        if (!resultRole.Succeeded)
                        {
                            ModelState.AddModelError("", "Neuspešno dodavanje uloga korisniku");
                            return View(model);
                        }
     
                     return  RedirectToAction("GetAllUsers", new { message = $"Registracija uspešno izvršena." });

                    }catch(Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }

                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            CinoviDropDownList();
            ObukeDropDownList();
            return View(model);
        }
        private void ObukeDropDownList(object izabranaObuka = null)
        {
            var obukeQuery = from o in serviceObuka.GetAll().AsQueryable().AsNoTracking()
                             orderby o.Naziv
                             select o;



            ViewBag.ObukaId = new SelectList(obukeQuery, "Id", "Naziv", izabranaObuka);
        }



        [HttpGet]

        public IActionResult CreateRole()
        {
            return View();
        }



        [HttpPost]
        public async Task<JsonResult> CreateRoleAsync([FromBody] Object rvm)
        {
            string json = rvm.ToString();
            RoleViewModel uloga = Newtonsoft.Json.JsonConvert.DeserializeObject<RoleViewModel>(json);

            int i = 0;
            try
            {
                IdentityRole identityRole = new IdentityRole()
                {
                    Name = uloga.RoleName
                };
                IdentityResult result = await roleManager.CreateAsync(identityRole);
                i++;
                return Json(i);
            }
            catch (Exception)
            {
                return Json(i);
            }

        }
        [HttpGet]

        public IActionResult GetAllRoles()
        {
            var roles = roleManager.Roles;

            return View(roles);

        }

        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            if (role == null)
            {
                ViewBag.Message = $"Uloga nije pronađena!";
                return View("PageNotFound");
            }
            var model = new EditRoleViewModel
            {
                Id = role.Id,
                RoleName = role.Name


            };

            foreach (var user in userManager.Users)
            {
                var currentUser = user;

                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    model.Users.Add(user.UserName);
                }
            }
            return View(model);
        }



        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            var role = await roleManager.FindByIdAsync(model.Id);

            if (role == null)
            {
                ViewBag.Message = $"Uloga nije pronađena!";
                return View("PageNotFound");
            }
            else
            {
                role.Name = model.RoleName;


                var result = await roleManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("GetAllRoles");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditUsersInRole(string roleId)
        {
            ViewBag.roleId = roleId;

            var role = await roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                ViewBag.Message = $"Uloga nije pronađena!";
                return View("PageNotFound");
            }

            var model = new List<UserRoleViewModel>();

            foreach (var user in userManager.Users)
            {
                var userRoleViewModel = new UserRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    Email = user.Email
                };

                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    userRoleViewModel.IsSelected = true;
                }
                else
                {
                    userRoleViewModel.IsSelected = false;
                }

                model.Add(userRoleViewModel);
            }

            return View(model);
        }



        [HttpPost]
        public async Task<IActionResult> EditUsersInRole(List<UserRoleViewModel> model, string roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                ViewBag.Message = $"Uloga nije pronađena!";
                return View("PageNotFound");
            }

            for (int i = 0; i < model.Count; i++)
            {
                var user = await userManager.FindByIdAsync(model[i].UserId);

                IdentityResult result = null;

                if (model[i].IsSelected && !(await userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!model[i].IsSelected && await userManager.IsInRoleAsync(user, role.Name))
                {
                    result = await userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }

                if (result.Succeeded)
                {
                    if (i < (model.Count - 1))
                        continue;
                    else
                        return RedirectToAction("EditRole", new { Id = roleId });
                }
            }

            return RedirectToAction("EditRole", new { Id = roleId });
        }



        [HttpGet]
        public IActionResult GetAllUsers(string message)
        {
            if (message == null) message = string.Empty;
            ViewBag.message = message;
            var users = userManager.Users;
            return View(users);
        }



        [HttpGet]

        public async Task<IActionResult> EditUser(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            if (user == null)
            {
                ViewBag.Message = $"Korisnik nije pronađen!";
                return View("PageNotFound");
            }


            var userClaims = await userManager.GetClaimsAsync(user);

            var userRoles = await userManager.GetRolesAsync(user);

            var model = new EditUserViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Claims = userClaims.Select(c => c.Type + " : " + c.Value).ToList(),
                Roles = userRoles,
                Email = user.Email
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserViewModel model)
        {
            var user = await userManager.FindByIdAsync(model.Id);

            if (user == null)
            {
                ViewBag.Message = $"Korisnik nije pronađen!";
                return View("PageNotFound");
            }
            else
            {

                user.UserName = model.UserName;
                user.Email = model.Email;

                var result = await userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("GetAllUsers");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(model);
            }
        }

        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                ViewBag.Message = $"Korisnik nije pronađen!";
                return View("PageNotFound");
            }
            var result = await userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("GetAllUsers");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View("GetAllUsers");
        }

        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            if (role == null)
            {
                ViewBag.Message = $"Uloga nije pronađena!";
                return View("PageNotFound");
            }

            var result = await roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                return RedirectToAction("GetAllRoles");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return RedirectToAction("GetAllRoles");
        }


        [HttpGet]
        public async Task<IActionResult> ManageUserRoles(string userId)
        {
            ViewBag.userId = userId;

            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                ViewBag.Message = $"Korisnik nije pronađen!";
                return View("PageNotFound");
            }

            var model = new List<UserRolesViewModel>();

            foreach (var role in roleManager.Roles)
            {
                var userRolesViewModel = new UserRolesViewModel
                {
                    RoleId = role.Id,
                    RoleName = role.Name
                };

                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    userRolesViewModel.IsSelected = true;
                }
                else
                {
                    userRolesViewModel.IsSelected = false;
                }

                model.Add(userRolesViewModel);
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult>
    ManageUserRoles(List<UserRolesViewModel> model, string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                ViewBag.Message = $"Korisnik nije pronađen!";
                return View("PageNotFound");
            }

            var roles = await userManager.GetRolesAsync(user);
            var result = await userManager.RemoveFromRolesAsync(user, roles);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Neuspešno uklanjanje uloge korisnika.");
                return View(model);
            }

            result = await userManager.AddToRolesAsync(user,
                model.Where(x => x.IsSelected).Select(y => y.RoleName));

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Neuspešno dodavanje uloga korisniku");
                return View(model);
            }

            return RedirectToAction("EditUser", new { Id = userId });
        }





        [HttpGet]
        public async Task<IActionResult> ManageUserClaims(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                ViewBag.Message = $"Korisnik nije pronađen!";
                return View("PageNotFound");
            }

            var existingUserClaims = await userManager.GetClaimsAsync(user);

            var model = new UserClaimsViewModel
            {
                UserId = userId
            };


            foreach (Claim claim in ClaimsStore.AllClaims)
            {
                UserClaim userClaim = new UserClaim
                {
                    ClaimType = claim.Type
                };


                if (existingUserClaims.Any(c => c.Type == claim.Type && c.Value == "true"))
                {
                    userClaim.IsSelected = true;
                }

                model.Claims.Add(userClaim);
            }

            return View(model);

        }

        [HttpPost]
        public async Task<IActionResult> ManageUserClaims(UserClaimsViewModel model)
        {
            var user = await userManager.FindByIdAsync(model.UserId);

            if (user == null)
            {
                ViewBag.Message = $"Korisnik nije pronađen!";
                return View("PageNotFound");
            }


            var claims = await userManager.GetClaimsAsync(user);
            var result = await userManager.RemoveClaimsAsync(user, claims);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Nije moguće ukloniti dozvolu za korisnika.");
                return View(model);
            }


            result = await userManager.AddClaimsAsync(user,
                model.Claims.Select(c => new Claim(c.ClaimType, c.IsSelected ? "true" : "false")));

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Nije moguće dodeliti dozvolu korisniku.");
                return View(model);
            }

            return RedirectToAction("EditUser", new { Id = model.UserId });

        }


        private void CinoviDropDownList(string cin = null)
        {
            List<string> cinovi = new List<string> { "Razvodnik", "Desetar", "Mlađi vodnik", "Vodnik", "Vodnik I klase", "Stariji vodnik", "Stariji vodnik I klase", "Zastavik" };
            ViewBag.Cinovi = new SelectList(cinovi.AsQueryable().AsNoTracking(), cin);
        }


    }
}
