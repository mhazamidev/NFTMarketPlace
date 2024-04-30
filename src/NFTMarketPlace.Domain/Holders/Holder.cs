using NFTMarketPlace.Domain.Files;
using NFTMarketPlace.Domain.SeedWork;

namespace NFTMarketPlace.Domain.Holders;

public sealed class Holder : AggregateRoot<HolderId>
{
    public string? Email { get; private set; }
    public string? Firsname { get; private set; }
    public string? Lastname { get; private set; }
    public string? Country { get; private set; }
    public string? Nickname { get; private set; }
    public DateTime? Created { get; private set; }
    public DateTime? BirthDate { get; private set; }
    public bool Blocked { get; private set; } = false;
    public FileId FileId { get; private set; }
    public Files.File Avatar { get; private set; }

    private Holder() { }

    private Holder(HolderId holderId, string? email, string? firstname, string? lastname, string? country, string? nickname, DateTime? birthDate, bool blocked, FileId fileId)
    {
        Id = holderId;
        Email = email;
        Lastname = lastname;
        Country = country;
        Nickname = nickname;
        BirthDate = birthDate;
        Blocked = blocked;
        FileId = fileId;
        Created = DateTime.Now;
    }

    public static Holder CreateNew(Guid id, string? email, string? firstname, string? lastname, string? country, string? nickname, DateTime? birthDate, bool blocked, FileId fileId)
    {
        var holderId = new HolderId(id);
        return new Holder(holderId, email, firstname, lastname, country, nickname, birthDate, blocked, fileId);
    }
}
