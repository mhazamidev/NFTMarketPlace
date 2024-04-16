// SPDX-License-Identifier: MIT
pragma solidity >=0.4.16 <0.9.0;

import "@openzeppelin/contracts/token/ERC721/extensions/ERC721URIStorage.sol";
import "@openzeppelin/contracts/utils/Counters.sol";

contract Collection is ERC721URIStorage {
    using Counters for Counters.Counter;
    Counters.Counter private _tokenIds;
    uint internal immutable mintFee;

    event NFTMinted(address indexed minter, uint tokenId);

    constructor(uint _minttFee) ERC721("My NFT", "MNF") {
        mintFee = _minttFee;
    }

    modifier CheckMintFee() {
        require(msg.value >= mintFee, "You have to pay mint fee");
        _;
    }

    function mint(
        string memory tokenURI
    ) external payable CheckMintFee returns (uint) {
        _tokenIds.increment();
        uint tokenId = _tokenIds.current();

        _safeMint(msg.sender, tokenId);
        _setTokenURI(tokenId, tokenURI);
        emit NFTMinted(msg.sender, tokenId);
        return tokenId;
    }

    function getMintFee() public view returns (uint) {
        return mintFee;
    }

    function getTokenCounter() public view returns (uint) {
        return _tokenIds.current();
    }

    function approveNFT(address _market, uint _tokenId) external {
        _approve(_market, _tokenId);
    }
}
