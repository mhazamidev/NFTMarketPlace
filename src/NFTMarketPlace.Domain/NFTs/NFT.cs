using NFTMarketPlace.Domain.Collections;
using NFTMarketPlace.Domain.Files;
using NFTMarketPlace.Domain.Holders;
using NFTMarketPlace.Domain.SeedWork;

namespace NFTMarketPlace.Domain.NFTs;

public sealed class NFT : AggregateRoot<NFTId>
{
    public string URI { get; private set; }
    public string Name { get; private set; }
    public string Address { get; private set; }
    public DateTime Created { get; private set; }
    public bool ForSell { get; private set; }
    public HolderId OwnerId { get; private set; }
    public Holder Owner { get; private set; }
    public CollectionId CollectionId { get; private set; }
    public Collection Collection { get; private set; }
    public FileId FileId { get; private set; }
    public Files.File File { get; private set; }


    public static NFT CreateNew(Guid id, string name, string uRI, string address, HolderId ownerId, CollectionId collectionId, FileId fileId)
    {
        var nftId = new NFTId(id);
        return new NFT(nftId, name, uRI, address, ownerId, collectionId, fileId);
    }

    private NFT(NFTId nftId, string name, string uRI, string address, HolderId ownerId, CollectionId collectionId, FileId fileId)
    {
        Id = nftId;
        Name = name;
        URI = uRI;
        Address = address;
        OwnerId = ownerId;
        CollectionId = collectionId;
        FileId = fileId;
        Created = DateTime.Now;
        ForSell = false;
    }

    private NFT() { }

}
