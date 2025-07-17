namespace UrbaPark.Dominio.Servicio.Abstracciones
{
    public interface IFileStorageService
    {
        Task<string> SaveFileAsync(byte[] fileData, string fileName, string folderName);
        void DeleteFile(string filePath, string folderName);
    }
}