using UrbaPark.Aplicacion.Abstracciones;
using UrbaPark.Aplicacion.DTO;
using UrbaPark.Dominio.Modelo.Abstracciones;
using UrbaPark.Dominio.Modelo.Entidades;

namespace UrbaPark.Aplicacion.Implementaciones;

public class BitacoraAppService : IBitacoraAppService
{
    private readonly IBitacoraRepositorio _bitacoraRepositorio;

    public BitacoraAppService(IBitacoraRepositorio bitacoraRepositorio)
    {
        _bitacoraRepositorio = bitacoraRepositorio;
    }

    public async Task<IEnumerable<BitacoraDto>> GetAllBitacorasAsync()
    {
        return await GetFilteredBitacorasAsync(new BitacoraFilterDto());
    }

    public async Task<IEnumerable<BitacoraDto>> GetFilteredBitacorasAsync(BitacoraFilterDto filter)
    {
        var bitacoras = await _bitacoraRepositorio.GetAllAsync(b =>
            (!filter.IdBitacora.HasValue || b.IdBitacora == filter.IdBitacora.Value) &&
            (!filter.IdInforme.HasValue || b.IdInforme == filter.IdInforme.Value) &&
            (!filter.IdMantenimiento.HasValue || b.IdMantenimiento == filter.IdMantenimiento.Value) &&
            (!filter.FechaDesde.HasValue || b.FechaHora >= filter.FechaDesde.Value) &&
            (!filter.FechaHasta.HasValue || b.FechaHora <= filter.FechaHasta.Value) &&
            (string.IsNullOrEmpty(filter.Descripcion) || b.Descripcion.Contains(filter.Descripcion)) &&
            (string.IsNullOrEmpty(filter.ImagenUrl) || b.ImagenUrl.Contains(filter.ImagenUrl))
        );

        return bitacoras.Select(b => new BitacoraDto
        {
            IdBitacora = b.IdBitacora,
            IdInforme = b.IdInforme,
            IdMantenimiento = b.IdMantenimiento,
            FechaHora = b.FechaHora,
            Descripcion = b.Descripcion,
            ImagenUrl = b.ImagenUrl
        });
    }

    public async Task<BitacoraDto?> GetBitacoraByIdAsync(int id)
    {
        var bitacora = await _bitacoraRepositorio.GetByIdAsync(id);
        if (bitacora == null) return null;

        return new BitacoraDto
        {
            IdBitacora = bitacora.IdBitacora,
            IdInforme = bitacora.IdInforme,
            IdMantenimiento = bitacora.IdMantenimiento,
            FechaHora = bitacora.FechaHora,
            Descripcion = bitacora.Descripcion,
            ImagenUrl = bitacora.ImagenUrl
        };
    }

    public async Task<BitacoraDto> CreateBitacoraAsync(CreateBitacoraDto bitacoraDto)
    {
        var bitacora = new Bitacora
        {
            IdInforme = bitacoraDto.IdInforme,
            IdMantenimiento = bitacoraDto.IdMantenimiento,
            FechaHora = bitacoraDto.FechaHora,
            Descripcion = bitacoraDto.Descripcion,
            ImagenUrl = bitacoraDto.ImagenUrl
        };

        await _bitacoraRepositorio.AddAsync(bitacora);

        return new BitacoraDto
        {
            IdBitacora = bitacora.IdBitacora,
            IdInforme = bitacora.IdInforme,
            IdMantenimiento = bitacora.IdMantenimiento,
            FechaHora = bitacora.FechaHora,
            Descripcion = bitacora.Descripcion,
            ImagenUrl = bitacora.ImagenUrl
        };
    }

    public async Task UpdateBitacoraAsync(UpdateBitacoraDto bitacoraDto)
    {
        var bitacora = await _bitacoraRepositorio.GetByIdAsync(bitacoraDto.IdBitacora);
        if (bitacora == null) throw new KeyNotFoundException("Bitácora no encontrada.");

        bitacora.IdInforme = bitacoraDto.IdInforme ?? bitacora.IdInforme;
        bitacora.IdMantenimiento = bitacoraDto.IdMantenimiento ?? bitacora.IdMantenimiento;
        bitacora.FechaHora = bitacoraDto.FechaHora ?? bitacora.FechaHora;
        bitacora.Descripcion = bitacoraDto.Descripcion ?? bitacora.Descripcion;
        bitacora.ImagenUrl = bitacoraDto.ImagenUrl ?? bitacora.ImagenUrl;

        await _bitacoraRepositorio.UpdateAsync(bitacora);
    }

    public async Task DeleteBitacoraAsync(int id)
    {
        await _bitacoraRepositorio.DeleteAsync(id);
    }
}
