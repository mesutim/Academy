﻿using Academy.Model.Models.IdentityModels;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Academy.Model.ViewModels
{
    public class UserForAdminViewModel
    {
        public List<UserInfoForAdminViewModel> Users { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
    }

    public class DeleteUsersForAdminViewModel
    {
        public List<UserInfoForAdminViewModel> Users { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        //public int ShowCount { get; set; }

    }

    public class UserInfoForAdminViewModel
    {
        [Key]
        public int UserId { get; set; }


        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کارکتر باشد")]
        public string UserName { get; set; }


        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کارکتر باشد")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمی باشد")]
        public string Email { get; set; }


        [Display(Name = "وضعیت")]
        public bool IsActive { get; set; }


        [Display(Name = "تاریخ ثبت نام")]
        public DateTime RegisterDate { get; set; }
    }

    public class DeletedUserInformationViewModel
    {
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public DateTime RegisterDate { get; set; }
        //public int? Wallet { get; set; }
    }

    public class CreateUserViewModel
    {
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string UserName { get; set; }
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمی باشد")]
        public string Email { get; set; }
        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Password { get; set; }

        public IFormFile UserAvatar { get; set; }
        // public List<int> UserRoles { get; set; }

    }

    public class EditUserViewModel
    {
        public int UserId { get; set; }
        public string? UserName { get; set; }

        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمی باشد")]
        public string Email { get; set; }

        [Display(Name = "کلمه عبور")]
        public string? Password { get; set; }

        public IFormFile? UserAvatar { get; set; }

        public List<int>? UserRoles { get; set; }

        public string AvatarName { get; set; }
    }

}
