using System.Net;
using DmsDb.Repository;
using DmsDb.Entity;
using DmsDb.Object;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DmsDb.Service;

public class EstateObjectService
{
    private readonly FileOptions _fileOptions;
    private readonly FileService _fileService;
    private readonly EstateObjectRepository _estateObjectRepository;

    public EstateObjectService(
        FileOptions fileOptions,
        FileService fileService,
        EstateObjectRepository estateObjectRepository
    ) {
        this._fileOptions = fileOptions;
        this._fileService = fileService;
        this._estateObjectRepository = estateObjectRepository;
    }

    public async Task<HttpStatusCode> Create(EstateObjectObject estateObjectObject)
    {
        foreach (IFormFile file in estateObjectObject.Files)
        {
            if (file.Length > _fileOptions.MaxFileSizeBytes)
                return HttpStatusCode.RequestEntityTooLarge;

            if (!_fileOptions.IsAllowed(file))
                return HttpStatusCode.UnsupportedMediaType;

            _fileService.CreateFile(file);
        }

        return HttpStatusCode.OK;

        EstateObjectEntity estateObject = new EstateObjectEntity
        {
            Id = Guid.NewGuid(),
            Address = estateObjectObject.Address,
            Name = estateObjectObject.Name,
            Description = estateObjectObject.Description
        };

        await _estateObjectRepository.Create(estateObject);
        return HttpStatusCode.Created;
    }
}
