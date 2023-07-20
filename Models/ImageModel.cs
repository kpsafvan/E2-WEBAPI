using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WEBAPI_E2.Models
{
    public class ImageModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public long ProductId { get; set; }
        public byte[] ImageData { get; set; }
        [JsonIgnore]
        public virtual ProductModel? Product { get; set; }
    }
}
