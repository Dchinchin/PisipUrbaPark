using UrbaPark.Aplicacion.Abstracciones;
using UrbaPark.Aplicacion.DTO;
using UrbaPark.Dominio.Modelo.Abstracciones;
using UrbaPark.Dominio.Modelo.Entidades;
using UrbaPark.Dominio.Servicio.Abstracciones;
using Microsoft.AspNetCore.Http;

namespace UrbaPark.Aplicacion.Implementaciones;

public class DetalleInformeAppService : IDetalleInformeAppService
{
    private readonly IDet_InfoEncaRepositorio _detalleInformeRepositorio;
    private readonly IFileStorageService _fileStorageService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public DetalleInformeAppService(IDet_InfoEncaRepositorio detalleInformeRepositorio, IFileStorageService fileStorageService, IHttpContextAccessor httpContextAccessor)
    {
        _detalleInformeRepositorio = detalleInformeRepositorio;
        _fileStorageService = fileStorageService;
        _httpContextAccessor = httpContextAccessor;
    }

    private string? GetAbsoluteUrl(string? relativeUrl)
    {
        if (string.IsNullOrEmpty(relativeUrl)) return null;
        var request = _httpContextAccessor.HttpContext?.Request;
        if (request == null) return relativeUrl; // Fallback if HttpContext is not available
        return $"{request.Scheme}://{request.Host}{relativeUrl}";
    }

    public async Task<IEnumerable<DetalleInformeDto>> GetAllDetallesInformeAsync()
    {
        return await GetFilteredDetallesInformeAsync(new DetalleInformeFilterDto());
    }

    public async Task<IEnumerable<DetalleInformeDto>> GetFilteredDetallesInformeAsync(DetalleInformeFilterDto filter)
    {
        var detalles = await _detalleInformeRepositorio.GetAllAsync(d =>
            (!filter.IdDetalleInforme.HasValue || d.IdDetInfo == filter.IdDetalleInforme.Value) &&
            (string.IsNullOrEmpty(filter.Descripcion) || d.Descripcion.Contains(filter.Descripcion)) &&
            (!filter.EstaEliminado.HasValue || d.EstaEliminado == filter.EstaEliminado.Value)
        );

        return detalles.Select(d => new DetalleInformeDto
        {
            IdDetInfo = d.IdDetInfo,
            IdInforme = d.IdInforme,
            Descripcion = d.Descripcion,
            ArchivoUrl = GetAbsoluteUrl(d.ArchivoUrl),
            FechaCreacion = d.FechaCreacion,
            FechaModificacion = d.FechaModificacion,
            Estado = d.EstaEliminado ? "Inactivo" : "Activo"
        });
    }

    public async Task<DetalleInformeDto?> GetDetalleInformeByIdAsync(int id)
    {
        var detalle = await _detalleInformeRepositorio.GetByIdAsync(id);
        if (detalle == null || detalle.EstaEliminado) return null;

        return new DetalleInformeDto
        {
            IdDetInfo = detalle.IdDetInfo,
            IdInforme = detalle.IdInforme,
            Descripcion = detalle.Descripcion,
            ArchivoUrl = GetAbsoluteUrl(detalle.ArchivoUrl),
            FechaCreacion = detalle.FechaCreacion,
            FechaModificacion = detalle.FechaModificacion
        };
    }

    public async Task<DetalleInformeDto> CreateDetalleInformeAsync(CreateDetalleInformeDto detalleInformeDto, IFormFile? archivoFile)
    {
        string? fileUrl = null;
        if (archivoFile != null)
        {
            var allowedExtensions = new[] { ".pdf", ".docx" };
            var fileExtension = Path.GetExtension(archivoFile.FileName).ToLowerInvariant();
            if (!allowedExtensions.Contains(fileExtension))
            {
                throw new InvalidOperationException("Tipo de archivo no permitido. Solo se permiten PDF y DOCX.");
            }

            using (var memoryStream = new MemoryStream())
            {
                await archivoFile.CopyToAsync(memoryStream);
                var archivoData = memoryStream.ToArray();
                fileUrl = await _fileStorageService.SaveFileAsync(archivoData, archivoFile.FileName, "files");
            }
        }

        var detalle = new Detalle_Informe
        {
            IdInforme = detalleInformeDto.IdInforme,
            Descripcion = detalleInformeDto.Descripcion,
            ArchivoUrl = fileUrl,
            FechaCreacion = DateTime.Now,
            FechaModificacion = DateTime.Now
        };

        await _detalleInformeRepositorio.AddAsync(detalle);

        return new DetalleInformeDto
        {
            IdDetInfo = detalle.IdDetInfo,
            IdInforme = detalle.IdInforme,
            Descripcion = detalle.Descripcion,
            ArchivoUrl = GetAbsoluteUrl(detalle.ArchivoUrl),
            FechaCreacion = detalle.FechaCreacion,
            FechaModificacion = detalle.FechaModificacion
        };
    }

    public async Task<DetalleInformeDto> UpdateDetalleInformeAsync(int id, UpdateDetalleInformeDto detalleInformeDto, IFormFile? archivoFile)
    {
        var detalle = await _detalleInformeRepositorio.GetByIdAsync(id);
        if (detalle == null) throw new KeyNotFoundException("Detalle de Informe no encontrado.");

        detalle.Descripcion = detalleInformeDto.Descripcion ?? detalle.Descripcion;
        detalle.FechaModificacion = DateTime.Now;

        if (archivoFile != null)
        {
            var allowedExtensions = new[] { ".pdf", ".docx" };
            var fileExtension = Path.GetExtension(archivoFile.FileName).ToLowerInvariant();
            if (!allowedExtensions.Contains(fileExtension))
            {
                throw new InvalidOperationException("Tipo de archivo no permitido. Solo se permiten PDF y DOCX.");
            }

            // Delete old file if exists
            if (!string.IsNullOrEmpty(detalle.ArchivoUrl))
            {
                _fileStorageService.DeleteFile(detalle.ArchivoUrl, "files");
            }
            using (var memoryStream = new MemoryStream())
            {
                await archivoFile.CopyToAsync(memoryStream);
                var archivoData = memoryStream.ToArray();
                detalle.ArchivoUrl = await _fileStorageService.SaveFileAsync(archivoData, archivoFile.FileName, "files");
            }
        }
        

        await _detalleInformeRepositorio.UpdateAsync(detalle);

        return new DetalleInformeDto
        {
            IdDetInfo = detalle.IdDetInfo,
            IdInforme = detalle.IdInforme,
            Descripcion = detalle.Descripcion,
            ArchivoUrl = GetAbsoluteUrl(detalle.ArchivoUrl)
        };
    }

    public async Task DeleteDetalleInformeAsync(int id)
    {
        var detalle = await _detalleInformeRepositorio.GetByIdAsync(id);
        if (detalle == null) throw new KeyNotFoundException("Detalle de Informe no encontrado.");

        detalle.EstaEliminado = true;
        await _detalleInformeRepositorio.UpdateAsync(detalle);
    }
}