using NFTMarketPlace.Domain.Holders;
using NFTMarketPlace.Domain.SeedWork;
using System.ComponentModel;

namespace NFTMarketPlace.Domain.Wallets;

public sealed class Wallet : AggregateRoot<WalletId>
{
    public string Address { get; private set; }
    public WalletType WalletType { get; private set; }
    public long ChainID { get; private set; }
    public string Name { get; private set; }
    public HolderId HolderId { get; private set; }
    public Holder Holder { get; private set; }

    public static Wallet CreateNew(Guid id, string address, WalletType walletType, long chainId, string name, HolderId holderId)
    {
        var walletId = new WalletId(id);
        return new Wallet(walletId, address, walletType, chainId, name, holderId);
    }


    private Wallet(WalletId walletId, string address, WalletType walletType, long chainId, string name, HolderId holderId)
    {
        Id = walletId;
        Address = address;
        WalletType = walletType;
        ChainID = chainId;
        Name = name;
        HolderId = holderId;
    }

    private Wallet() { }
}

public enum WalletType : byte
{
    [Description("MetaMask")]
    MetaMask = 101
}
