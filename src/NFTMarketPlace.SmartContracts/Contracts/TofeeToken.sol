// SPDX-License-Identifier: MIT
pragma solidity >=0.4.16 <0.9.0;

import "@openzeppelin/contracts/token/ERC20/ERC20.sol";

contract TafiToken is ERC20 {
    constructor(uint _initialSupply) ERC20("Tofee", "TOF") {
        _mint(msg.sender, _initialSupply * 10 ** decimals());
    }
}
