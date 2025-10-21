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
    private readonly UserEstateObjectRepository _userEstateObjectRepository;

    public EstateObjectService(
        FileService fileService,
        EstateObjectRepository estateObjectRepository,
        UserEstateObjectRepository userEstateObjectRepository
    ) {
        this._fileService = fileService;
        this._estateObjectRepository = estateObjectRepository;
        this._userEstateObjectRepository = userEstateObjectRepository;
    }

    public async Task<HttpStatusCode> Create(Guid userId, EstateObjectObject estateObjectObject)
    {
        Guid estateObjectId = Guid.NewGuid();
        HttpStatusCode status = await _fileService.CreateFiles(estateObjectObject.Files, estateObjectId);

        if (status != HttpStatusCode.OK)
            return status;

        await _userEstateObjectRepository.Add(userId, estateObjectId);

        EstateObjectEntity estateObject = new EstateObjectEntity
        {
            Id = estateObjectId,
            Address = estateObjectObject.Address,
            Name = estateObjectObject.Name,
            Description = estateObjectObject.Description
        };

        await _estateObjectRepository.Create(estateObject);
        return HttpStatusCode.Created;
    }

    public async Task<List<EstateObjectCard>> GetAll(Guid userId)
    {
        List<Guid> estateObjectIds = await _userEstateObjectRepository.GetEstateObjectIdsByUserId(userId);
        List<EstateObjectCard> cards = await _estateObjectRepository.GetAll(estateObjectIds);

        foreach (EstateObjectCard card in cards)
            card.ImagePath = await _fileService.GetThumbnail(card.Id) ?? string.Empty;

        return cards;
    }

    public async Task<EstateObjectReturn?> Get(Guid Id)
    {
        EstateObjectEntity? estateObject = await _estateObjectRepository.Get(Id);

        if (estateObject == null)
            return null;

        List<string> images = await _fileService.GetAllFiles(Id);

        return new EstateObjectReturn {
            Images = images,
            Name = estateObject.Name,
            Address = estateObject.Address,
            Description = estateObject.Description
        };
    }
}
