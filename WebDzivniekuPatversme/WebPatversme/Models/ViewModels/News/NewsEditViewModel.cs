﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using WebDzivniekuPatversme.Validation;
using WebDzivniekuPatversme.Models.StaticModels;

namespace WebDzivniekuPatversme.Models.ViewModels.News
{
    public class NewsEditViewModel
    {
        [Key]
        public string Id { set; get; }

        [StringLength(13000, ErrorMessage = ValidationErrorMessages.StringLength)]
        [Required(ErrorMessage = ValidationErrorMessages.RequiredM)]
        [Display(Name = "Teksts")]
        public string Text { set; get; }

        public string ImagePath { set; get; }

        [NotMapped]
        [DataType(DataType.Upload)]
        [MaxFileSizeValidation(6 * 1024 * 1024, ErrorMessage = ValidationErrorMessages.MaxFileSize)]
        [ExtensionValidation(new string[] { ".jpg", ".png", ".jpeg", ".gif", ".tif" }, ErrorMessage = ValidationErrorMessages.FileType)]
        [Display(Name = "Attēls")]
        public IFormFile Image { set; get; }

        [NewsTitleValidation(true, ErrorMessage = ValidationErrorMessages.AlreadyExists)]
        [StringLength(100, ErrorMessage = ValidationErrorMessages.StringLength)]
        [Required(ErrorMessage = ValidationErrorMessages.RequiredM)]
        [Display(Name = "Tituls")]
        public string Title { set; get; }

        [Display(Name = "Autors")]
        public string UsersName { set; get; }
    }
}