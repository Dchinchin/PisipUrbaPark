using UrbaPark.Aplicacion.DTO;

namespace UrbaPark.Aplicacion.Abstracciones;

public interface IBitacoraAppService
{
    Task<IEnumerable<BitacoraDto>> GetAllBitacorasAsync();
    Task<IEnumerable<BitacoraDto>> GetFilteredBitacorasAsync(BitacoraFilterDto filter);
    Task<BitacoraDto?> GetBitacoraByIdAsync(int id);
    Task<BitacoraDto> CreateBitacoraAsync(CreateBitacoraDto bitacoraDto);
    Task UpdateBitacoraAsync(UpdateBitacoraDto bitacoraDto);
    Task DeleteBitacoraAsync(int id);
}
