using System.ComponentModel.DataAnnotations;

namespace Stockery.Model
{
    public class Bond
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "⚠️ The bond must have a name.")]
        [MaxLength(16)]
        public string Name { get; set; }
    }
}
