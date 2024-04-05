using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SoftFlix_API.Models;

namespace SoftFlix_API.Data
{
    public class DbInitializer
    {
        public DbInitializer(ApplicationDbContext? context, RoleManager<SoftFlixRole>? roleManager, UserManager<SoftFlixUser> userManager)
        {

            SoftFlixRole identityRole;
            Restriction restriction;
            SoftFlixUser user;
            Plan plan = null;
            Category? category = null;
            if (context != null)
            {
                context.Database.Migrate();


                if (roleManager != null)
                {
                    if (roleManager.Roles.Count() == 0)
                    {
                        identityRole = new SoftFlixRole("Administrator");
                        roleManager.CreateAsync(identityRole).Wait();
                        identityRole = new SoftFlixRole("Admin");
                        roleManager.CreateAsync(identityRole).Wait();
                        identityRole = new SoftFlixRole("Company Admin");
                        roleManager.CreateAsync(identityRole).Wait();
                        identityRole = new SoftFlixRole("Manager");
                        roleManager.CreateAsync(identityRole).Wait();
                    }
                }

                if (context.Categories.Count() == 0)
                {
                    category = new Category();
                    category.Name = "Bilim-Kurgu";
                    context.Categories.Add(category);
                    category = new Category();
                    category.Name = "Aksiyon";
                    context.Categories.Add(category);
                    category = new Category();
                    category.Name = "Gerilim";
                    context.Categories.Add(category);
                    category = new Category();
                    category.Name = "Korku";
                    context.Categories.Add(category);
                    category = new Category();
                    category.Name = "Komedi";
                    context.Categories.Add(category);
                    category = new Category();
                    category.Name = "Romantik";
                    context.Categories.Add(category);
                    category = new Category();
                    category.Name = "Animasyon";
                    context.Categories.Add(category);
                    category = new Category();
                    category.Name = "Fantastik";
                    context.Categories.Add(category);
                    context.SaveChanges();
                }

                if (context.Plans.Count() == 0)
                {
                    plan = new Plan();
                    plan.Name = "Çocuk";
                    plan.Price = 50;
                    plan.Resolution = "1080";
                    context.Plans.Add(plan);
                    plan = new Plan();
                    plan.Name = "Yetişkin";
                    plan.Price = 100;
                    plan.Resolution = "1040";
                    context.Plans.Add(plan);
                    plan = new Plan();
                    plan.Name = "Pro";
                    plan.Price = 200;
                    plan.Resolution = "3080";
                    context.Plans.Add(plan);
                    context.SaveChanges();
                }

                if (!context.Restrictions.Any())
                {
                    restriction = new Restriction();
                    restriction.Name = "Genel Izleyici";
                    restriction.Id = 0;
                    context.Restrictions.Add(restriction);
                    restriction = new Restriction();
                    restriction.Name = "7";
                    restriction.Id = 7;
                    context.Restrictions.Add(restriction);
                    restriction = new Restriction();
                    restriction.Name = "13";
                    restriction.Id = 13;
                    context.Restrictions.Add(restriction);
                    restriction = new Restriction();
                    restriction.Name = "18";
                    restriction.Id = 18;
                    context.Restrictions.Add(restriction);
                    restriction = new Restriction();
                    restriction.Name = "Korku ve Şiddet";
                    restriction.Id = 19;
                    context.Restrictions.Add(restriction);
                    restriction = new Restriction();
                    restriction.Name = "Olumsuz Örnek";
                    restriction.Id = 20;
                    context.Restrictions.Add(restriction);
                    context.SaveChanges();

                }


                if (userManager != null)
                {
                    if (userManager.Users.Count() == 0)
                    {

                        user = new SoftFlixUser();
                        user.UserName = "Administrator";
                        user.Name = "Administrator";
                        user.Email = "abc@def.com";
                        user.PhoneNumber = "1112223344";
                        user.Passive = false;
                        userManager.CreateAsync(user, "Admin123!").Wait();
                        userManager.AddToRoleAsync(user, "Administrator").Wait();

                    }
                }
            }
        }
    }
}

