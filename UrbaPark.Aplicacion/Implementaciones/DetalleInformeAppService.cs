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
            (!filter.IdDetalleInforme.HasValue || d.id_detInfo == filter.IdDetalleInforme.Value) &&
            (string.IsNullOrEmpty(filter.Descripcion) || d.descripcion.Contains(filter.Descripcion))
        );

        return detalles.Select(d => new DetalleInformeDto
        {
            IdDetInfo = d.id_detInfo,
            IdInforme = d.id_informe,
            Descripcion = d.descripcion,
            ArchivoUrl = GetAbsoluteUrl(d.archivo_url)
        });
    }

    public async Task<DetalleInformeDto?> GetDetalleInformeByIdAsync(int id)
    {
        var detalle = await _detalleInformeRepositorio.GetByIdAsync(id);
        if (detalle == null) return null;

        return new DetalleInformeDto
        {
            IdDetInfo = detalle.id_detInfo,
            IdInforme = detalle.id_informe,
            Descripcion = detalle.descripcion,
            ArchivoUrl = GetAbsoluteUrl(detalle.archivo_url)
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
            id_informe = null,
            descripcion = detalleInformeDto.Descripcion,
            archivo_url = fileUrl
        };

        await _detalleInformeRepositorio.AddAsync(detalle);

        return new DetalleInformeDto
        {
            IdDetInfo = detalle.id_detInfo,
            IdInforme = null,
            Descripcion = detalle.descripcion,
            ArchivoUrl = GetAbsoluteUrl(detalle.archivo_url)
        };
    }

    public async Task<DetalleInformeDto> UpdateDetalleInformeAsync(int id, UpdateDetalleInformeDto detalleInformeDto, IFormFile? archivoFile)
    {
        var detalle = await _detalleInformeRepositorio.GetByIdAsync(id);
        if (detalle == null) throw new KeyNotFoundException("Detalle de Informe no encontrado.");

        detalle.descripcion = detalleInformeDto.Descripcion ?? detalle.descripcion;

        if (archivoFile != null)
        {
            var allowedExtensions = new[] { ".pdf", ".docx" };
            var fileExtension = Path.GetExtension(archivoFile.FileName).ToLowerInvariant();
            if (!allowedExtensions.Contains(fileExtension))
            {
                throw new InvalidOperationException("Tipo de archivo no permitido. Solo se permiten PDF y DOCX.");
            }

            // Delete old file if exists
            if (!string.IsNullOrEmpty(detalle.archivo_url))
            {
                _fileStorageService.DeleteFile(detalle.archivo_url, "files");
            }
            using (var memoryStream = new MemoryStream())
            {
                await archivoFile.CopyToAsync(memoryStream);
                var archivoData = memoryStream.ToArray();
                detalle.archivo_url = await _fileStorageService.SaveFileAsync(archivoData, archivoFile.FileName, "files");
            }
        }
        

        await _detalleInformeRepositorio.UpdateAsync(detalle);

        return new DetalleInformeDto
        {
            IdDetInfo = detalle.id_detInfo,
            IdInforme = detalle.id_informe,
            Descripcion = detalle.descripcion,
            ArchivoUrl = GetAbsoluteUrl(detalle.archivo_url)
        };
    }

    public async Task DeleteDetalleInformeAsync(int id)
    {
        var detalle = await _detalleInformeRepositorio.GetByIdAsync(id);
        if (detalle == null) throw new KeyNotFoundException("Detalle de Informe no encontrado.");

        if (!string.IsNullOrEmpty(detalle.archivo_url))
        {
            _fileStorageService.DeleteFile(detalle.archivo_url, "files");
        }

        await _detalleInformeRepositorio.DeleteAsync(id);
    }
}