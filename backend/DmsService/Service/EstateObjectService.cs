using System.Net;
using DmsDb.Repository;
using DmsDb.Entity;
using DmsService.Object;

namespace DmsService.Service;

public class EstateObjectService
{
    private readonly FileService _fileService;
    private readonly EstateObjectRepository _estateObjectRepository;
    // private readonly UserEstateObjectRepository _userEstateObjectRepository;

    public EstateObjectService(
        FileService fileService,
        EstateObjectRepository estateObjectRepository
        // UserEstateObjectRepository userEstateObjectRepository
    ) {
        this._fileService = fileService;
        this._estateObjectRepository = estateObjectRepository;
        // this._userEstateObjectRepository = userEstateObjectRepository;
    }

    public async Task<HttpStatusCode> Create(Guid userId, EstateObjectObject estateObjectObject)
    {
        Guid estateObjectId = Guid.NewGuid();
        HttpStatusCode status = await _fileService.CreateFiles(estateObjectObject.Files, estateObjectId);

        if (status != HttpStatusCode.OK)
            return status;

        // await _userEstateObjectRepository.Add(userId, estateObjectId);

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

    public async Task<List<EstateObjectCard>> GetAllOfUser(Guid userId)
    {
        // List<Guid> estateObjectIds = await _userEstateObjectRepository.GetAllWithUser(userId);
        // List<EstateObjectEntity> entities = await _estateObjectRepository.GetByListOfIds(estateObjectIds);
        List<EstateObjectEntity> estateObjects = await _estateObjectRepository.GetByUserId(userId);
        List<EstateObjectCard> cards = [];

        foreach (EstateObjectEntity entity in estateObjects) {
            EstateObjectCard card = new EstateObjectCard {
                Id = entity.Id,
                ImagePath = await _fileService.GetFirstOfEntity(entity.Id) ?? string.Empty,
                Name = entity.Name,
                Address = entity.Address
            };

            cards.Add(card);
        }

        return cards;
    }

    public async Task<EstateObjectReturn?> GetById(Guid estateObjectId)
    {
        EstateObjectEntity? estateObject = await _estateObjectRepository.GetById(estateObjectId);

        if (estateObject == null)
            return null;

        List<string> images = await _fileService.GetAllOfEntity(estateObjectId);

        return new EstateObjectReturn {
            Images = images,
            Name = estateObject.Name,
            Address = estateObject.Address,
            Description = estateObject.Description
        };
    }
}
