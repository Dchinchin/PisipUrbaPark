using UrbaPark.Aplicacion.DTO;
using Microsoft.AspNetCore.Http;

namespace UrbaPark.Aplicacion.Abstracciones;

public interface IBitacoraAppService
{
    Task<IEnumerable<BitacoraDto>> GetAllBitacorasAsync();
    Task<IEnumerable<BitacoraDto>> GetFilteredBitacorasAsync(BitacoraFilterDto filter);
    Task<BitacoraDto?> GetBitacoraByIdAsync(int id);
    Task<BitacoraDto> CreateBitacoraAsync(CreateBitacoraDto bitacoraDto, IFormFile? imageFile);
    Task<BitacoraDto> UpdateBitacoraAsync(int id, UpdateBitacoraDto bitacoraDto, IFormFile? imageFile);
    Task DeleteBitacoraAsync(int id);
}
