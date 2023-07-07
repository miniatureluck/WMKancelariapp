using System.ComponentModel.DataAnnotations.Schema;

namespace WMKancelariapp.Models
{
    public class Entity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
    }
}
