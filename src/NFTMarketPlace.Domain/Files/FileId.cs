using NFTMarketPlace.Domain.SeedWork;

namespace NFTMarketPlace.Domain.Files;

public class FileId : StronglyTypedId<FileId>
{
    public FileId(Guid value) : base(value)
    {
    }
}
