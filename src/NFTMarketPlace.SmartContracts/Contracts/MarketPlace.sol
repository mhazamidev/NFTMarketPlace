// SPDX-License-Identifier: MIT
pragma solidity >=0.4.16 <0.9.0;
import "@openzeppelin/contracts/token/ERC721/IERC721.sol";
import "@openzeppelin/contracts/security/ReentrancyGuard.sol";

contract MarketPlace is ReentrancyGuard {
    address payable immutable feeAccount;
    uint public immutable feePercent;
    uint public assetCount;
    uint public lastIndex;

    struct Item {
        uint itemIndex;
        IERC721 nft;
        uint tokenId;
        uint price;
        address payable seller;
        bool sold;
    }

    mapping(uint => Item) public items;

    event Listed(
        uint assetIndex,
        address indexed nft,
        uint tokenId,
        uint price,
        address indexed seller
    );

    event Bought(
        uint assetIndex,
        address indexed nft,
        uint tokenId,
        uint price,
        address indexed seller,
        address indexed buyer
    );

    constructor(uint _feePercent, address _feeAccount) {
        feePercent = _feePercent;
        feeAccount = payable(_feeAccount);
    }

    function list(
        IERC721 _nft,
        uint _tokenId,
        uint _price
    ) external nonReentrant {
        require(_price > 0, "Invalid Price");

        assetCount++;
        lastIndex++;
        _nft.transferFrom(msg.sender, address(this), _tokenId);

        items[lastIndex] = Item(
            lastIndex,
            _nft,
            _tokenId,
            _price,
            payable(msg.sender),
            false
        );

        emit Listed(lastIndex, address(_nft), _tokenId, _price, msg.sender);
    }

    function purchase(uint _assetIndex) external payable nonReentrant {
        Item storage item = items[_assetIndex];
        uint _totalPrice = getTotalPrice(_assetIndex);

        require(
            _assetIndex > 0 && _assetIndex <= lastIndex,
            "asset doesn't exist"
        );
        require(msg.value >= _totalPrice, "not enough eth to cover item price");
        require(!item.sold, "item already sold");

        item.nft.transferFrom(address(this), msg.sender, item.tokenId);
        item.seller.transfer(item.price);
        feeAccount.transfer(_totalPrice - item.price);

        emit Bought(
            _assetIndex,
            address(item.nft),
            item.tokenId,
            item.price,
            item.seller,
            msg.sender
        );
    }

    function editAssetPrice(uint _assetIndex, uint _newPrice) external {
        items[_assetIndex].price = _newPrice;
    }

    function unList(uint _assetIndex) external {
        items[_assetIndex].nft.transferFrom(
            address(this),
            msg.sender,
            items[_assetIndex].tokenId
        );
        delete items[_assetIndex];
        assetCount--;
    }

    function getTotalPrice(uint _assetIndex) public view returns (uint) {
        return (items[_assetIndex].price * (100 + feePercent)) / 100;
    }
}
