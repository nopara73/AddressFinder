# AddressFinder

Find if an address can be derived from specific extpub (bech32 only currently.)

```sh
dotnet run -- {extpubkey} {addressToFind} {true/false}
```

- arg1: extpubkey
- arg2: bech32 address
- arg3: If the derived addresses should be written out to the console or not. Set it to false if performance is the goal.
