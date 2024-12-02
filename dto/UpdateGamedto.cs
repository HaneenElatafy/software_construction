using System.ComponentModel.DataAnnotations;

namespace system.dto;

public record class UpdateGamedto
([Required][StringLength(50)]string Name,
 [Required][StringLength(20)]string Genre,
 [Range(1,100)]decimal Price,
 DateOnly ReleaseDate);
