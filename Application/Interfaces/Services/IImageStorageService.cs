using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IImageStorageService
    {
        /// <summary>
        /// Sube un archivo y devuelve la URL relativa o absoluta.
        /// </summary>
        Task<string> UploadAsync(Stream content, string originalFileName, string? subfolder = null, CancellationToken ct = default);

        /// <summary>
        /// Elimina un archivo físico dado su URL relativa.
        /// </summary>
        Task<bool> DeleteAsync(string relativeUrl, CancellationToken ct = default);
    }
}
