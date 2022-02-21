using System.ComponentModel.DataAnnotations;
using Bongo.Models.ModelValidations;

namespace Bongo.Models.Models.ViewModels;

public class StudyRoomBookingBase
{
    [Required]
    [StringLength(50)]
    public string? FirstName { get; set; }

    [Required]
    [StringLength(50)]
    public string? LastName { get; set; }

    [Required]
    [StringLength(100)]
    [EmailAddress]
    public string? Email { get; set; }

    [DataType(DataType.Date)]
    [DateInFuture]
    public DateTime Date { get; set; }
}