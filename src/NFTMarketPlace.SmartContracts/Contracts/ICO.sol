// SPDX-License-Identifier: MIT
pragma solidity >=0.4.16 <0.9.0;

import "@openzeppelin/contracts/token/ERC20/IERC20.sol";

contract ICO {
    address public admin;
    IERC20 public token;
    uint public tokenPrice = 10 ** 15 wei;
    uint public airDropAmount = 100 * 1e18;
    uint public totalReleasedAirdrop;
    uint public maxAirdropAmount = 1_000_000 * 1e18;
    uint public icoEndTime;
    uint public holdersCount;

    mapping(address => uint) public airdrops;
    mapping(address => uint) public holders;
    mapping(address => bool) public isInList;

    event Buy(address indexed buyer_address, uint indexed amount);
    event Airdrop(address indexed Receiver_address, uint indexed amount);

    constructor(address _token, address _admin) {
        admin = _admin;
        token = IERC20(_token);
    }

    modifier onlyAdmin() {
        require(msg.sender == admin, "only admin can call this function");
        _;
    }

    modifier isActive() {
        require(
            icoEndTime > 0 && block.timestamp < icoEndTime,
            "ICO have been ended"
        );
        _;
    }

    function activate(uint duration) external onlyAdmin {
        require(duration > 0, "duration must be greater than 0");
        icoEndTime = block.timestamp + duration;
    }

    function inactivate() external onlyAdmin isActive {
        icoEndTime = 0;
    }

    function depositTokens(uint _amount) public onlyAdmin {
        /*
            call from UI
            msg.sender = admin
            token.approve(ico,amount);
        */
        token.transferFrom(admin, address(this), _amount);
    }

    function withdrawTokens(uint _amount) public {
        require(
            _amount <= balanceOfToken(address(this)),
            "the amount is higher than the balance"
        );
        token.transfer(admin, _amount);
    }

    function balanceOfETH(address _account) public view returns (uint) {
        return _account.balance;
    }

    function balanceOfToken(address _account) public view returns (uint) {
        return token.balanceOf(_account);
    }

    function getICOAddress() public view returns (address) {
        return address(this);
    }

    function getTokenAddress() public view returns (address) {
        return address(token);
    }

    function updateAdmin(address _newAdmin) external onlyAdmin {
        admin = _newAdmin;
    }

    function withdrawETH(uint _amount) public {
        require(
            _amount <= balanceOfETH(address(this)),
            "the amount is higher than the balance"
        );
        payable(admin).transfer(_amount);
    }

    function airdrop(address _receiver) external isActive {
        require(airdrops[_receiver] == 0, "airdrop: already done!");
        require(
            totalReleasedAirdrop + airDropAmount <= maxAirdropAmount,
            "airdrop: all airdrop tokens were released!"
        );
        require(
            balanceOfToken(address(this)) >= airDropAmount,
            "airdrop: No enough tokens for airdrop!"
        );

        token.transfer(_receiver, airDropAmount);
        airdrops[_receiver] = airDropAmount;
        totalReleasedAirdrop += airDropAmount;
        emit Airdrop(_receiver, airDropAmount);
        if (!isInList[_receiver]) {
            isInList[_receiver] = true;
            holdersCount += 1;
        }
        holders[_receiver] += airDropAmount;
    }

    function purchase(uint _amount) external payable {
        require(
            msg.value == ((_amount / 1e18) * tokenPrice),
            "purchase: not correct value!"
        );

        token.transfer(msg.sender, _amount);

        if (!isInList[msg.sender]) {
            isInList[msg.sender] = true;
            holdersCount += 1;
        }
        holders[msg.sender] += _amount;
        emit Buy(msg.sender, _amount);
    }
}
