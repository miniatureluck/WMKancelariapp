namespace WMKancelariapp.Models.ViewModels
{
    public class DeadlineIndexViewModel
    {
        public IEnumerable<DeadlineDtoViewModel>? DeadlineDtos { get; set; }

        public DateTime DateRangeFrom { get; set; }
        public DateTime DateRangeTo { get; set; }
    }
}
