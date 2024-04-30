using NFTMarketPlace.Domain.Files;
using NFTMarketPlace.Domain.SeedWork;

namespace NFTMarketPlace.Domain.Collections;

public sealed class Collection : AggregateRoot<CollectionId>
{
    public Collection(CollectionId collectionId, string name, bool isActive, FileId fileId)
    {
        Id = collectionId;
        Name = name;
        IsActive = isActive;
        FileId = fileId;
        Created = DateTime.Now;
    }

    private Collection() { }

    public string Name { get; private set; }
    public DateTime Created { get; private set; }
    public bool IsActive { get; private set; }
    public FileId FileId { get; private set; }
    public Files.File Cover { get; private set; }

    public static Collection CreateNew(Guid id, string name, bool isActive, FileId fileId)
    {
        var collectionId = new CollectionId(id);
        return new Collection(collectionId, name, isActive, fileId);
    }

}
