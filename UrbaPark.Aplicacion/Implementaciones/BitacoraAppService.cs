using UrbaPark.Aplicacion.Abstracciones;
using UrbaPark.Aplicacion.DTO;
using UrbaPark.Dominio.Modelo.Abstracciones;
using UrbaPark.Dominio.Modelo.Entidades;
using UrbaPark.Dominio.Servicio.Abstracciones;
using Microsoft.AspNetCore.Http;

namespace UrbaPark.Aplicacion.Implementaciones;

public class BitacoraAppService : IBitacoraAppService
{
    private readonly IBitacoraRepositorio _bitacoraRepositorio;
    private readonly IFileStorageService _fileStorageService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMantenimientoRepositorio _mantenimientoRepositorio;
    private readonly IInfo_EncaRepositorio _infoEncaRepositorio;

    public BitacoraAppService(IBitacoraRepositorio bitacoraRepositorio, IFileStorageService fileStorageService, IHttpContextAccessor httpContextAccessor, IMantenimientoRepositorio mantenimientoRepositorio, IInfo_EncaRepositorio infoEncaRepositorio)
    {
        _bitacoraRepositorio = bitacoraRepositorio;
        _fileStorageService = fileStorageService;
        _httpContextAccessor = httpContextAccessor;
        _mantenimientoRepositorio = mantenimientoRepositorio;
        _infoEncaRepositorio = infoEncaRepositorio;
    }

    private string? GetAbsoluteUrl(string? relativeUrl)
    {
        if (string.IsNullOrEmpty(relativeUrl)) return null;
        var request = _httpContextAccessor.HttpContext?.Request;
        if (request == null) return relativeUrl; // Fallback if HttpContext is not available
        return $"{request.Scheme}://{request.Host}{relativeUrl}";
    }

    public async Task<IEnumerable<BitacoraDto>> GetAllBitacorasAsync()
    {
        return await GetFilteredBitacorasAsync(new BitacoraFilterDto());
    }

    public async Task<IEnumerable<BitacoraDto>> GetFilteredBitacorasAsync(BitacoraFilterDto filter)
    {
        var bitacoras = await _bitacoraRepositorio.GetAllAsync(b =>
            (!filter.IdBitacora.HasValue || b.IdBitacora == filter.IdBitacora.Value) &&
            (!filter.IdMantenimiento.HasValue || b.IdMantenimiento == filter.IdMantenimiento.Value) &&
            (!filter.FechaDesde.HasValue || b.FechaHora >= filter.FechaDesde.Value) &&
            (!filter.FechaHasta.HasValue || b.FechaHora <= filter.FechaHasta.Value) &&
            (string.IsNullOrEmpty(filter.Descripcion) || b.Descripcion.Contains(filter.Descripcion)) &&
            (!filter.EstaEliminado.HasValue || b.EstaEliminado == filter.EstaEliminado.Value)
        );

        return bitacoras.Select(b => new BitacoraDto
        {
            IdBitacora = b.IdBitacora,
            IdMantenimiento = b.IdMantenimiento,
            FechaHora = b.FechaHora,
            Descripcion = b.Descripcion,
            ImagenUrl = GetAbsoluteUrl(b.ImagenUrl),
            EstaEliminado = b.EstaEliminado,
            FechaCreacion = b.FechaCreacion,
            FechaModificacion = b.FechaModificacion
        });
    }

    public async Task<BitacoraDto?> GetBitacoraByIdAsync(int id)
    {
        var bitacora = await _bitacoraRepositorio.GetByIdAsync(id);
        if (bitacora == null || bitacora.EstaEliminado) return null;

        return new BitacoraDto
        {
            IdBitacora = bitacora.IdBitacora,
            IdMantenimiento = bitacora.IdMantenimiento,
            FechaHora = bitacora.FechaHora,
            Descripcion = bitacora.Descripcion,
            ImagenUrl = GetAbsoluteUrl(bitacora.ImagenUrl),
            EstaEliminado = bitacora.EstaEliminado,
            FechaCreacion = bitacora.FechaCreacion,
            FechaModificacion = bitacora.FechaModificacion
        };
    }

    public async Task<BitacoraDto> CreateBitacoraAsync(CreateBitacoraDto bitacoraDto, IFormFile? imageFile)
    {
        string? imageUrl = null;
        if (imageFile != null)
        {
            var allowedExtensions = new[] { ".png", ".jpg", ".jpeg", ".svg" };
            var fileExtension = Path.GetExtension(imageFile.FileName).ToLowerInvariant();
            if (!allowedExtensions.Contains(fileExtension))
            {
                throw new InvalidOperationException("Tipo de archivo de imagen no permitido. Solo se permiten PNG, JPG, JPEG, SVG.");
            }

            using (var memoryStream = new MemoryStream())
            {
                await imageFile.CopyToAsync(memoryStream);
                var imageData = memoryStream.ToArray();
                imageUrl = await _fileStorageService.SaveFileAsync(imageData, imageFile.FileName, "images");
            }
        }

        var bitacora = new Bitacora
        {
            IdMantenimiento = bitacoraDto.IdMantenimiento,
            FechaHora = bitacoraDto.FechaHora,
            Descripcion = bitacoraDto.Descripcion,
            ImagenUrl = imageUrl,
            EstaEliminado = false,
            FechaCreacion = DateTime.Now,
            FechaModificacion = DateTime.Now
        };

        await _bitacoraRepositorio.AddAsync(bitacora);

        return new BitacoraDto
        {
            IdBitacora = bitacora.IdBitacora,
            IdMantenimiento = bitacora.IdMantenimiento,
            FechaHora = bitacora.FechaHora,
            Descripcion = bitacora.Descripcion,
            ImagenUrl = GetAbsoluteUrl(bitacora.ImagenUrl),
            EstaEliminado = bitacora.EstaEliminado,
            FechaCreacion = bitacora.FechaCreacion,
            FechaModificacion = bitacora.FechaModificacion
        };
    }

    public async Task<BitacoraDto> UpdateBitacoraAsync(int id,UpdateBitacoraDto bitacoraDto, IFormFile? imageFile)
    {
        var bitacora = await _bitacoraRepositorio.GetByIdAsync(id);
        if (bitacora == null) throw new KeyNotFoundException("Bitácora no encontrada.");

        bitacora.IdMantenimiento = bitacoraDto.IdMantenimiento ?? bitacora.IdMantenimiento;
        bitacora.FechaHora = bitacoraDto.FechaHora ?? bitacora.FechaHora;
        bitacora.Descripcion = bitacoraDto.Descripcion ?? bitacora.Descripcion;
        bitacora.FechaModificacion = DateTime.Now;

        if (imageFile != null)
        {
            var allowedExtensions = new[] { ".png", ".jpg", ".jpeg", ".svg" };
            var fileExtension = Path.GetExtension(imageFile.FileName).ToLowerInvariant();
            if (!allowedExtensions.Contains(fileExtension))
            {
                throw new InvalidOperationException("Tipo de archivo de imagen no permitido. Solo se permiten PNG, JPG, JPEG, SVG.");
            }

            // Delete old file if exists
            if (!string.IsNullOrEmpty(bitacora.ImagenUrl))
            {
                _fileStorageService.DeleteFile(bitacora.ImagenUrl, "images");
            }
            using (var memoryStream = new MemoryStream())
            {
                await imageFile.CopyToAsync(memoryStream);
                var imageData = memoryStream.ToArray();
                bitacora.ImagenUrl = await _fileStorageService.SaveFileAsync(imageData, imageFile.FileName, "images");
            }
        }
        

        await _bitacoraRepositorio.UpdateAsync(bitacora);

        return new BitacoraDto
        {
            IdBitacora = bitacora.IdBitacora,
            IdMantenimiento = bitacora.IdMantenimiento,
            FechaHora = bitacora.FechaHora,
            Descripcion = bitacora.Descripcion,
            ImagenUrl = GetAbsoluteUrl(bitacora.ImagenUrl),
            EstaEliminado = bitacora.EstaEliminado,
            FechaCreacion = bitacora.FechaCreacion,
            FechaModificacion = bitacora.FechaModificacion
        };
    }

    public async Task DeleteBitacoraAsync(int id)
    {
        var bitacora = await _bitacoraRepositorio.GetByIdAsync(id);
        if (bitacora == null) throw new KeyNotFoundException("Bitácora no encontrada.");

        bitacora.EstaEliminado = true;
        await _bitacoraRepositorio.UpdateAsync(bitacora);
    }
}