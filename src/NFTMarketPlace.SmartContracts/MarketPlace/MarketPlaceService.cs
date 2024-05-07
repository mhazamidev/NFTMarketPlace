using System.Numerics;
using Nethereum.Web3;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Contracts.ContractHandlers;
using NFTMarketPlace.SmartContracts.MarketPlace.ContractDefinition;

namespace NFTMarketPlace.SmartContracts.MarketPlace
{
    public partial class MarketPlaceService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Web3 web3, MarketPlaceDeployment marketPlaceDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<MarketPlaceDeployment>().SendRequestAndWaitForReceiptAsync(marketPlaceDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Web3 web3, MarketPlaceDeployment marketPlaceDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<MarketPlaceDeployment>().SendRequestAsync(marketPlaceDeployment);
        }

        public static async Task<MarketPlaceService> DeployContractAndGetServiceAsync(Web3 web3, MarketPlaceDeployment marketPlaceDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, marketPlaceDeployment, cancellationTokenSource);
            return new MarketPlaceService(web3, receipt.ContractAddress);
        }

        protected Web3 Web3 { get; }

        public ContractHandler ContractHandler { get; }

        public MarketPlaceService(Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public Task<BigInteger> AssetCountQueryAsync(AssetCountFunction assetCountFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<AssetCountFunction, BigInteger>(assetCountFunction, blockParameter);
        }

        
        public Task<BigInteger> AssetCountQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<AssetCountFunction, BigInteger>(null, blockParameter);
        }

        public Task<BigInteger> FeePercentQueryAsync(FeePercentFunction feePercentFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<FeePercentFunction, BigInteger>(feePercentFunction, blockParameter);
        }

        
        public Task<BigInteger> FeePercentQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<FeePercentFunction, BigInteger>(null, blockParameter);
        }

        public Task<ItemsOutputDTO> ItemsQueryAsync(ItemsFunction itemsFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<ItemsFunction, ItemsOutputDTO>(itemsFunction, blockParameter);
        }

        public Task<ItemsOutputDTO> ItemsQueryAsync(BigInteger returnValue1, BlockParameter blockParameter = null)
        {
            var itemsFunction = new ItemsFunction();
                itemsFunction.ReturnValue1 = returnValue1;
            
            return ContractHandler.QueryDeserializingToObjectAsync<ItemsFunction, ItemsOutputDTO>(itemsFunction, blockParameter);
        }

        public Task<BigInteger> LastIndexQueryAsync(LastIndexFunction lastIndexFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<LastIndexFunction, BigInteger>(lastIndexFunction, blockParameter);
        }

        
        public Task<BigInteger> LastIndexQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<LastIndexFunction, BigInteger>(null, blockParameter);
        }

        public Task<string> ListRequestAsync(ListFunction listFunction)
        {
             return ContractHandler.SendRequestAsync(listFunction);
        }

        public Task<TransactionReceipt> ListRequestAndWaitForReceiptAsync(ListFunction listFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(listFunction, cancellationToken);
        }

        public Task<string> ListRequestAsync(string nft, BigInteger tokenId, BigInteger price)
        {
            var listFunction = new ListFunction();
                listFunction.Nft = nft;
                listFunction.TokenId = tokenId;
                listFunction.Price = price;
            
             return ContractHandler.SendRequestAsync(listFunction);
        }

        public Task<TransactionReceipt> ListRequestAndWaitForReceiptAsync(string nft, BigInteger tokenId, BigInteger price, CancellationTokenSource cancellationToken = null)
        {
            var listFunction = new ListFunction();
                listFunction.Nft = nft;
                listFunction.TokenId = tokenId;
                listFunction.Price = price;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(listFunction, cancellationToken);
        }

        public Task<string> PurchaseRequestAsync(PurchaseFunction purchaseFunction)
        {
             return ContractHandler.SendRequestAsync(purchaseFunction);
        }

        public Task<TransactionReceipt> PurchaseRequestAndWaitForReceiptAsync(PurchaseFunction purchaseFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(purchaseFunction, cancellationToken);
        }

        public Task<string> PurchaseRequestAsync(BigInteger assetIndex)
        {
            var purchaseFunction = new PurchaseFunction();
                purchaseFunction.AssetIndex = assetIndex;
            
             return ContractHandler.SendRequestAsync(purchaseFunction);
        }

        public Task<TransactionReceipt> PurchaseRequestAndWaitForReceiptAsync(BigInteger assetIndex, CancellationTokenSource cancellationToken = null)
        {
            var purchaseFunction = new PurchaseFunction();
                purchaseFunction.AssetIndex = assetIndex;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(purchaseFunction, cancellationToken);
        }

        public Task<string> EditAssetPriceRequestAsync(EditAssetPriceFunction editAssetPriceFunction)
        {
             return ContractHandler.SendRequestAsync(editAssetPriceFunction);
        }

        public Task<TransactionReceipt> EditAssetPriceRequestAndWaitForReceiptAsync(EditAssetPriceFunction editAssetPriceFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(editAssetPriceFunction, cancellationToken);
        }

        public Task<string> EditAssetPriceRequestAsync(BigInteger assetIndex, BigInteger newPrice)
        {
            var editAssetPriceFunction = new EditAssetPriceFunction();
                editAssetPriceFunction.AssetIndex = assetIndex;
                editAssetPriceFunction.NewPrice = newPrice;
            
             return ContractHandler.SendRequestAsync(editAssetPriceFunction);
        }

        public Task<TransactionReceipt> EditAssetPriceRequestAndWaitForReceiptAsync(BigInteger assetIndex, BigInteger newPrice, CancellationTokenSource cancellationToken = null)
        {
            var editAssetPriceFunction = new EditAssetPriceFunction();
                editAssetPriceFunction.AssetIndex = assetIndex;
                editAssetPriceFunction.NewPrice = newPrice;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(editAssetPriceFunction, cancellationToken);
        }

        public Task<string> UnListRequestAsync(UnListFunction unListFunction)
        {
             return ContractHandler.SendRequestAsync(unListFunction);
        }

        public Task<TransactionReceipt> UnListRequestAndWaitForReceiptAsync(UnListFunction unListFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(unListFunction, cancellationToken);
        }

        public Task<string> UnListRequestAsync(BigInteger assetIndex)
        {
            var unListFunction = new UnListFunction();
                unListFunction.AssetIndex = assetIndex;
            
             return ContractHandler.SendRequestAsync(unListFunction);
        }

        public Task<TransactionReceipt> UnListRequestAndWaitForReceiptAsync(BigInteger assetIndex, CancellationTokenSource cancellationToken = null)
        {
            var unListFunction = new UnListFunction();
                unListFunction.AssetIndex = assetIndex;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(unListFunction, cancellationToken);
        }

        public Task<BigInteger> GetTotalPriceQueryAsync(GetTotalPriceFunction getTotalPriceFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetTotalPriceFunction, BigInteger>(getTotalPriceFunction, blockParameter);
        }

        
        public Task<BigInteger> GetTotalPriceQueryAsync(BigInteger assetIndex, BlockParameter blockParameter = null)
        {
            var getTotalPriceFunction = new GetTotalPriceFunction();
                getTotalPriceFunction.AssetIndex = assetIndex;
            
            return ContractHandler.QueryAsync<GetTotalPriceFunction, BigInteger>(getTotalPriceFunction, blockParameter);
        }
    }
}
