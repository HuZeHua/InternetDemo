﻿using System;
using ResourceMetadata.Model;
using ResourceMetadata.Data.Configurations;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using Microsoft.AspNet.Identity;

namespace ResourceMetadata.Data
{
    public class ResourceManagerEntities : IdentityDbContext<ApplicationUser>
    {
        public ResourceManagerEntities()
            : base("ResourceManagerEntities")
        {

        }

        public DbSet<Resource> Resources { get; set; }
        public DbSet<ResourceActivity> Activities { get; set; }
        public DbSet<Location> Locations { get; set; }

        //public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new LocationConfiguration());
            modelBuilder.Configurations.Add(new ResourceConfiguration());
            modelBuilder.Configurations.Add(new ResourceActivityConfiguration());

            //Configurations Auto generated tables for IdentityDbContext.
            modelBuilder.Configurations.Add(new IdentityUserRoleConfiguration());
            modelBuilder.Configurations.Add(new IdentityUserLoginConfiguration());
        }
    }

    public class ResourceManagerDbInitializer : DropCreateDatabaseIfModelChanges<ResourceManagerEntities>
    {
        protected override void Seed(ResourceManagerEntities context)
        {
            try
            {
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                userManager.UserValidator = new UserValidator<ApplicationUser>(userManager)
                {
                    AllowOnlyAlphanumericUserNames = false
                };
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

                if (!roleManager.RoleExists("Admin"))
                {
                    roleManager.Create(new IdentityRole("Admin"));
                }

                if (!roleManager.RoleExists("Member"))
                {
                    roleManager.Create(new IdentityRole("Member"));
                }

                var user = new ApplicationUser();
                user.FirstName = "Admin";
                user.LastName = "Shiju";
                user.Email = "admin@shijuvar.com";
                user.UserName = "admin@shijuvar.com";

                var userResult = userManager.Create(user, "Shijuvar");

                if (userResult.Succeeded)
                {
                    //var user = userManager.FindByName("admin@marlabs.com");
                    userManager.AddToRole<ApplicationUser, string>(user.Id, "Admin");
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            //context.Users.Add(new ApplicationUser { Email = "abc@yahoo.com", Password = "Marlabs" });
            //context.SaveChanges();           
        }
    }

}
