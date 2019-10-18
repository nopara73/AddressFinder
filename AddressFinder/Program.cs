using NBitcoin;
using System;

namespace AddressFinder
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var extPubKey = ExtPubKey.Parse(args[0]);
            var address = BitcoinAddress.Create(args[1], Network.Main);
            var listAddresses = bool.Parse(args[2]);

            Console.WriteLine($"ExtPubKey:\t{extPubKey.GetWif(Network.Main)}");
            Console.WriteLine($"Address:\t{address}");

            var numberOfKeysToSearch = 1_000_000;
            for (int i = 0; i < numberOfKeysToSearch; i++)
            {
                var receivePath = new KeyPath($"0/{i}");
                var changePath = new KeyPath($"1/{i}");
                BitcoinWitPubKeyAddress recAddr = extPubKey.Derive(receivePath).PubKey.GetSegwitAddress(Network.Main);
                BitcoinWitPubKeyAddress changeAddr = extPubKey.Derive(changePath).PubKey.GetSegwitAddress(Network.Main);

                if (recAddr == address)
                {
                    Console.WriteLine($"Provided address FOUND on RECEIVE path: {receivePath}.");
                    return;
                }

                if (changeAddr == address)
                {
                    Console.WriteLine($"Provided address FOUND on CHANGE path: {changePath}.");
                    return;
                }

                if (listAddresses)
                {
                    Console.WriteLine($"Rec:\t{recAddr}");
                    Console.WriteLine($"Change:\t{changeAddr}");
                }
            }

            Console.WriteLine($"Searched {2 * numberOfKeysToSearch} keys. Provided address is NOT FOUND.");
        }
    }
}
