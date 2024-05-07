const Collection = artifacts.require("Collection");
const MarketPlace = artifacts.require("MarketPlace");


module.exports = function (deployer) {
    deployer.deploy(Collection, 1000000000000000);
    deployer.deploy(MarketPlace, 2, "0x3125499F37719eb8828e5bdd919fbDfF46e6a016");
};
