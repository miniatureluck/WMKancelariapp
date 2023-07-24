﻿using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WMKancelariapp.Models.ViewModels
{
    public class UserTaskDtoViewModel
    {
        public string UserTaskId { get; set; }
        [DisplayName("Opiekun")]
        public User User { get; set; }
        [DisplayName("Rozpoczęto")]
        public DateTime? StartTime { get; set; }
        [DisplayName("Zakończono")]
        public DateTime? EndTime { get; set; }
        [DisplayName("Czas")]
        public TimeSpan? Duration { get; set; }
        [DisplayName("Opis")]
        public string? Description { get; set; }

        [DisplayName("Klient")]
        public Client? Client { get; set; }
        [DisplayName("Sprawa")]
        public Case? Case { get; set; }
        [DisplayName("Kategoria")]
        public TaskType TaskType { get; set; }
        [DisplayName("Stawki")]
        public HourlyPrice? HourlyPrice { get; set; }


        [DisplayName("Kategoria")]
        public List<SelectListItem>? TaskTypeSelectList { get; set; } = new List<SelectListItem>();
    }
}
