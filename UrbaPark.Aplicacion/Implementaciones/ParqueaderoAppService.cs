using UrbaPark.Aplicacion.Abstracciones;
using UrbaPark.Aplicacion.DTO;
using UrbaPark.Dominio.Modelo.Abstracciones;
using UrbaPark.Dominio.Modelo.Entidades;

namespace UrbaPark.Aplicacion.Implementaciones;

public class ParqueaderoAppService : IParqueaderoAppService
{
    private readonly IParqueaderoRepositorio _parqueaderoRepositorio;

    public ParqueaderoAppService(IParqueaderoRepositorio parqueaderoRepositorio)
    {
        _parqueaderoRepositorio = parqueaderoRepositorio;
    }

    public async Task<IEnumerable<ParqueaderoDto>> GetAllParqueaderosAsync()
    {
        return await GetFilteredParqueaderosAsync(new ParqueaderoFilterDto());
    }

    public async Task<IEnumerable<ParqueaderoDto>> GetFilteredParqueaderosAsync(ParqueaderoFilterDto filter)
    {
        var parqueaderos = await _parqueaderoRepositorio.GetAllAsync(p =>
            (!filter.IdParqueadero.HasValue || p.IdParqueadero == filter.IdParqueadero.Value) &&
            (string.IsNullOrEmpty(filter.Nombre) || p.Nombre.Contains(filter.Nombre)) &&
            (string.IsNullOrEmpty(filter.Direccion) || p.Direccion.Contains(filter.Direccion)) &&
            (!filter.EstaEliminado.HasValue || p.EstaEliminado == filter.EstaEliminado.Value)
        );

        return parqueaderos.Select(p => new ParqueaderoDto
        {
            IdParqueadero = p.IdParqueadero,
            Nombre = p.Nombre,
            Direccion = p.Direccion,
            Estado = !p.EstaEliminado,
            FechaCreacion = p.FechaCreacion,
            FechaModificacion = p.FechaModificacion
        });
    }

    public async Task<ParqueaderoDto?> GetParqueaderoByIdAsync(int id)
    {
        var parqueadero = await _parqueaderoRepositorio.GetByIdAsync(id);
        if (parqueadero == null || parqueadero.EstaEliminado) return null;

        return new ParqueaderoDto
        {
            IdParqueadero = parqueadero.IdParqueadero,
            Nombre = parqueadero.Nombre,
            Direccion = parqueadero.Direccion,
            Estado = !parqueadero.EstaEliminado,
            FechaCreacion = parqueadero.FechaCreacion,
            FechaModificacion = parqueadero.FechaModificacion
        };
    }

    public async Task<ParqueaderoDto> CreateParqueaderoAsync(CreateParqueaderoDto parqueaderoDto)
    {
        var parqueadero = new Parqueadero
        {
            Nombre = parqueaderoDto.Nombre,
            Direccion = parqueaderoDto.Direccion,
            EstaEliminado = false, // Default to not deleted on creation
            FechaCreacion = DateTime.Now,
            FechaModificacion = DateTime.Now
        };

        await _parqueaderoRepositorio.AddAsync(parqueadero);

        return new ParqueaderoDto
        {
            IdParqueadero = parqueadero.IdParqueadero,
            Nombre = parqueadero.Nombre,
            Direccion = parqueadero.Direccion,
            Estado = !parqueadero.EstaEliminado,
            FechaCreacion = parqueadero.FechaCreacion,
            FechaModificacion = parqueadero.FechaModificacion
        };
    }

    public async Task<ParqueaderoDto> UpdateParqueaderoAsync(int id, UpdateParqueaderoDto parqueaderoDto)
    {
        var parqueadero = await _parqueaderoRepositorio.GetByIdAsync(id);
        if (parqueadero == null) throw new KeyNotFoundException("Parqueadero no encontrado.");

        parqueadero.Nombre = parqueaderoDto.Nombre ?? parqueadero.Nombre;
        parqueadero.Direccion = parqueaderoDto.Direccion ?? parqueadero.Direccion;
        parqueadero.FechaModificacion = DateTime.Now;

        await _parqueaderoRepositorio.UpdateAsync(parqueadero);

        return new ParqueaderoDto
        {
            IdParqueadero = parqueadero.IdParqueadero,
            Nombre = parqueadero.Nombre,
            Direccion = parqueadero.Direccion,
            Estado = !parqueadero.EstaEliminado,
            FechaCreacion = parqueadero.FechaCreacion,
            FechaModificacion = parqueadero.FechaModificacion
        };
    }

    public async Task DeleteParqueaderoAsync(int id)
    {
        var parqueadero = await _parqueaderoRepositorio.GetByIdAsync(id);
        if (parqueadero == null) throw new KeyNotFoundException("Parqueadero no encontrado.");

        parqueadero.EstaEliminado = true;
        await _parqueaderoRepositorio.UpdateAsync(parqueadero);
    }

    
}
