using NFTMarketPlace.Domain.SeedWork;

namespace NFTMarketPlace.Domain.Files;

public sealed class File : AggregateRoot<FileId>
{
    public string FileName { get; private set; }
    public byte[] Content { get; private set; }
    public byte Size { get; private set; }
    public string ContentType { get; private set; }
    public string Extension { get; private set; }

    public static File CreateNew(string fileName, byte[] content, byte size, string contentType, string extension)
    {

        if (string.IsNullOrEmpty(fileName))
            throw new BusinessRuleException("file name is empty.");
        if (content == null || content.Length <= 0)
            throw new BusinessRuleException("file content is empty.");
        if (size <= 0)
            throw new BusinessRuleException("file size is invalid");
        if (string.IsNullOrEmpty(contentType))
            throw new BusinessRuleException("file contentType is empty.");
        if (string.IsNullOrEmpty(extension))
            throw new BusinessRuleException("file extension is empty.");

        var fileId = new FileId(Guid.NewGuid());
        return new File(
            fileId,
            fileName,
            content,
            size,
            contentType,
            extension);
    }

    public static File CreateFileForContent(FileId id, byte[] content, string contentType, string extension)
    {
        return new File(id, content, contentType, extension);
    }
    private File(FileId fileId, string fileName, byte[] content, byte size, string contentType, string extension)
    {
        Id = fileId;
        FileName = fileName;
        Content = content;
        Size = size;
        ContentType = contentType;
        Extension = extension;
    }
    private File(FileId id, byte[] content, string contentType, string extension)
    {
        Id = id;
        Content = content;
        ContentType = contentType;
        Extension = extension;
    }

    //Empty constructor for EF 
    private File() { }
}
