using System.Net;
using DmsDb.Repository;
using DmsDb.Entity;
using DmsService.Object;

namespace DmsService.Service;

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

    public async Task<HttpStatusCode> Create(Guid userId, EstateObjectObject estateObjectObject)
    {
        Guid estateObjectId = Guid.NewGuid();
        // HttpStatusCode status = await _fileService.CreateFiles(estateObjectObject.Files, estateObjectId);
        //
        // if (status != HttpStatusCode.OK)
        //     return status;

        EstateObjectEntity estateObject = new EstateObjectEntity
        {
            Id = estateObjectId,
            Address = estateObjectObject.Address,
            Name = estateObjectObject.Name,
            Description = estateObjectObject.Description
        };

        await _estateObjectRepository.Create(userId, estateObject);
        return HttpStatusCode.Created;

        // await _userEstateObjectRepository.Add(userId, estateObjectId);
        //
        // EstateObjectEntity estateObject = new EstateObjectEntity
        // {
        //     Id = estateObjectId,
        //     Address = estateObjectObject.Address,
        //     Name = estateObjectObject.Name,
        //     Description = estateObjectObject.Description
        // };
        //
        // await _estateObjectRepository.Create(estateObject);
        // return HttpStatusCode.Created;
    }

    public async Task<List<EstateObjectCard>> GetAllOfUser(Guid userId)
    {
        // List<Guid> estateObjectIds = await _userEstateObjectRepository.GetAllWithUser(userId);
        // List<EstateObjectEntity> entities = await _estateObjectRepository.GetByListOfIds(estateObjectIds);
        List<EstateObjectEntity> estateObjects = await _estateObjectRepository.GetByUserId(userId);
        Console.WriteLine($"[AMOUNT OF IMAGES RETURED] {estateObjects[0].Images.Count}");
        List<EstateObjectCard> cards = [];

        foreach (EstateObjectEntity entity in estateObjects) {
            List<EstateObjectImageEntity> images = entity.Images;

            EstateObjectCard card = new EstateObjectCard {
                Id = entity.Id,
                ImagePath = images.Count < 1 ? null : images[0].Path,
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

        return new EstateObjectReturn {
            Images = estateObject.Images.ConvertAll(i => i.Path),
            Name = estateObject.Name,
            Address = estateObject.Address,
            Description = estateObject.Description
        };
    }
}
