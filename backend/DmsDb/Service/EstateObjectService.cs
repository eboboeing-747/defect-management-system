using System.Net;
using DmsDb.Repository;
using DmsDb.Entity;
using DmsDb.Object;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DmsDb.Service;

public class EstateObjectService
{
    private readonly FileService _fileService;
    private readonly EstateObjectRepository _estateObjectRepository;

    public EstateObjectService(
        FileService fileService,
        EstateObjectRepository estateObjectRepository
    ) {
        this._fileService = fileService;
        this._estateObjectRepository = estateObjectRepository;
    }

    public async Task<HttpStatusCode> Create(EstateObjectObject estateObjectObject)
    {
        Guid EstateObjectId = Guid.NewGuid();
        HttpStatusCode status = await _fileService.CreateFiles(estateObjectObject.Files, EstateObjectId);

        if (status != HttpStatusCode.OK)
            return status;

        EstateObjectEntity estateObject = new EstateObjectEntity
        {
            Id = EstateObjectId,
            Address = estateObjectObject.Address,
            Name = estateObjectObject.Name,
            Description = estateObjectObject.Description
        };

        await _estateObjectRepository.Create(estateObject);
        return HttpStatusCode.Created;
    }

    public async Task<List<EstateObjectCard>> GetAll()
    {
        List<EstateObjectCard> cards = await _estateObjectRepository.GetAll();

        foreach (EstateObjectCard card in cards)
            card.ImagePath = await _fileService.GetThumbnail(card.Id) ?? string.Empty;

        return cards;
    }
}
