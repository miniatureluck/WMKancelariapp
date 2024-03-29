﻿using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WMKancelariapp.Models.ViewModels
{
    public class DeadlineDtoViewModel
    {
        public string DeadlineId { get; set; }
        
        [DisplayName("Opis")]
        public string Description { get; set; }

        [DisplayName("Sprawa")]
        public Case Case { get; set; }

        [DisplayName("Deadline")]
        public DateTime Date { get; set; } = DateTime.Now.Date;

        [DisplayName("Wykonawca")]
        public User User { get; set; }

        [DisplayName("Status")]
        public bool IsCompleted { get; set; } = false;


        public List<SelectListItem> UsersSelectList = new();
        public List<SelectListItem> CasesSelectList = new();

    }
}
