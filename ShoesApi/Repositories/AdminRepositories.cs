﻿using AutoMapper;
using log4net;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShoesApi.DbContextFile;
using ShoesApi.DbContextFile.DBFiles;
using ShoesApi.Interfaces;
using ShoesApi.Models;
using ShoesApi.Models.ProductModel;

namespace ShoesApi.Repositories
{
    public class AdminRepositories : IAdmin
    {
        private readonly IMapper _mapper;


        private UserManager<AppUser> userManager;
        private SignInManager<AppUser> signInManager;
        private static readonly ILog log = LogManager.GetLogger(typeof(UserRepositories));

        private IUserValidator<AppUser> userValidator;
        // userValidator validate email and user name
        private IPasswordValidator<AppUser> passwordValidator;

        private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment _webHostEnvironment; // image

        public AdminRepositories(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
            IUserValidator<AppUser> userValidator, IPasswordValidator<AppUser> passwordValidator,
            ApplicationDbContext context, IWebHostEnvironment webHostEnvironment, IServiceProvider serviceProvider)
        {
            _mapper = serviceProvider.GetRequiredService<IMapper>();

            this.userValidator = userValidator;
            this.passwordValidator = passwordValidator;

            this.userManager = userManager;
            this.signInManager = signInManager;

            this.context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<List<AspUsersTable>> AdminTables(string Uid)
        {
            try
            {
                AppUser current_user = await userManager.FindByIdAsync(Uid);
                if (current_user != null)
                {
                    var roles = await userManager.GetRolesAsync(current_user);
                    // 'roles' will contain the roles assigned to the user
                    if (roles.Contains("Admin"))
                    {
                        var users = userManager.Users;
                        List<AspUsersTable> adminIndices = new List<AspUsersTable>();
                        
                        foreach (var user in users)
                        {
                            AspUsersTable data = _mapper.Map<AspUsersTable>(user);
                            //AdminIndex data = new AdminIndex()
                            //{
                            //    Id = user.Id,
                            //    Name = user.UserName,
                            //    Email = user.Email,
                            //    Number = "",
                            //    ImageUrl = user.imageUrl
                            //};
                            adminIndices.Add(data);
                        }
                        return adminIndices;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.InnerException != null ? string.Format("Inner Exception: {0} --- Exception: {1}", ex.InnerException.Message, ex.Message) : ex.Message, ex);
            }
            return new List<AspUsersTable>();
        }

        public async Task<AppUser> Edit(string Id)
        {
            try
            {
                AppUser user = await userManager.FindByIdAsync(Id);

                if (user != null)
                {
                    return user;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.InnerException != null ? string.Format("Inner Exception: {0} --- Exception: {1}", ex.InnerException.Message, ex.Message) : ex.Message, ex);
            }
            return new AppUser();
        }

        public async Task<bool> Delete(string Id)
        {
            try
            {
                AppUser user = await userManager.FindByIdAsync(Id);
                if (user != null)
                {
                    IdentityResult result = await userManager.DeleteAsync(user);
                    if (result.Succeeded)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.InnerException != null ? string.Format("Inner Exception: {0} --- Exception: {1}", ex.InnerException.Message, ex.Message) : ex.Message, ex);
            }
            return false;
        }

        public async Task<IActionResult> SaveEdits(AppUser appUser)
        {
            try
            {
                if (appUser != null)
                {
                    AppUser user = await userManager.FindByIdAsync(appUser.Id);

                    if (!string.IsNullOrEmpty(appUser.Email))
                    {
                        IdentityResult validEmail = null;
                        validEmail = await userValidator.ValidateAsync(userManager, user);
                        if (validEmail.Succeeded)
                        {
                            user.State = appUser.State; user.Email = appUser.Email;
                            user.imageUrl = appUser.imageUrl; user.UserName = appUser.UserName;
                            user.PhoneNumber = appUser.PhoneNumber; user.UserSurname = appUser.UserSurname;
                            user.Country = appUser.Country; user.Zip_Code = appUser.Zip_Code;

                            IdentityResult result = await userManager.UpdateAsync(user);
                            if (result.Succeeded)
                            {
                                return new StatusCodeResult(200);
                            }
                        }
                        else
                        {
                            return new StatusCodeResult(401);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.InnerException != null ? string.Format("Inner Exception: {0} --- Exception: {1}", ex.InnerException.Message, ex.Message) : ex.Message, ex);
            }
            return new StatusCodeResult(500);
        }

    }
}
