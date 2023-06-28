# ![Logo](https://raw.githubusercontent.com/Romulo-Meirelles/MercadoBitcoinAPI/main/Pictures/MercadoBitcoin_LogoNew.png) 
MercadoBitcoinAPI - (.NetFramework | .NetStandard | .NetCore)

[![NuGet version (MercadoBitcoinAPI)](https://img.shields.io/nuget/v/MercadoBitcoinAPI.svg?style=flat-square)](https://www.nuget.org/packages/MercadoBitcoinAPI/)

MERCADOBITCOIN API NÃO OFICIAL (UNOFFICIAL). "Não Possuímos nenhum vínculo com a empresa MERCADOBITCOIN."

(API .V4) MercadoBitcoin com essa API você faz consultas, balanço, cotação, saques, pagamentos, trades, transações, abrir ordens, fechar ordens, compra a mercado, vender a limite e outras mais na plataforma do MercadoBitcoin Confira!

- Características
- Faça pagamentos.
- Veja os Book de ofertas.
- Veja Tickes. (Candles)
- Veja seu balanço.
- Verifique suas ordens.
- Abra ordens.
- Encerre ordens ativas.
- Compre Tokens
- Compre Precatórios
- Veja o histórico de suas transações.
- Faça saques.
- Gerencie seus ativos e dinheiro.
- Fácil de usar.

#DIVIRTA-SE!

##  ALGUNS EXEMPLOS DE COMO USAR.

## Visual Basic

```vb
  Using Client = MercadoBitcoinClient.Create(New ApiConfig With {.TAPI_ID = "Sua ID", .TAPI_SECRET = "Sua SecretKey"})
            Try

                '--------------- API DE DADOS ----------------

                Dim H = Await Client.GetFeesFromAsset(Asset:="BTC")
                Console.WriteLine(H.ToString)

                Dim GOB = Await Client.GetOrderBook(SymbolCurrency:="BTC-BRL", Limit:=10)
                Console.WriteLine(GOB.ToString)

                Dim LT = Await Client.ListTrades(SymbolCurrency:="BTC-BRL")
                Console.WriteLine(LT.ToString)

                Dim LC = Await Client.ListCandles(SymbolCurrency:="BTC-BRL", Resolution:="1h", [To]:=Date.Now, From:=Date.Now.AddDays(-1))
                Console.WriteLine(LC.ToString)

                Dim LS = Await Client.ListSymbol(SymbolCurrency:="BTC-BRL")
                Console.WriteLine(LS.ToString)

                Dim LTS = Await Client.ListTickers(SymbolCurrency:="BTC-BRL,LTC-BRL")
                Console.WriteLine(LTS.ToString)

                '-------------- API TRADER ACCOUNT --------------
                Dim AccountID = Await Client.ListAccount()
                Console.WriteLine(AccountID.Root.Item(0).Identificacao) 'Result: (xfyh33gfbqeg1svo4on04lakkoc4ab82yhqad4h40wizxfennt3lt9qi5end)

                Dim LA = Await Client.ListAccount()
                Console.WriteLine(LA.ToString)

                Dim LB = Await Client.ListBalance(AccountID:="xfyh33gfbqeg1svo4on04lakkoc4ab82yhqad4h40wizxfennt3lt9qi5end")
                Console.WriteLine(LB.ToString)

                Dim LP = Await Client.ListPositions(AccountID:="xfyh33gfbqeg1svo4on04lakkoc4ab82yhqad4h40wizxfennt3lt9qi5end", SymbolCurrency:="BTC-BRL")
                Console.WriteLine(LP.ToString)

                '--------------API TRADER WALLET ----------------

                Dim LD = Await Client.ListDeposits(AccountID:="xfyh33gfbqeg1svo4on04lakkoc4ab82yhqad4h40wizxfennt3lt9qi5end", SymbolCurrency:="BTC", From:=Date.Now.AddYears(-3), [To]:=Date.Now)
                Console.WriteLine(LD.ToString)

                Dim WC = Await Client.WithdrawCoin(AccountID:="xfyh33gfbqeg1svo4on04lakkoc4ab82yhqad4h40wizxfennt3lt9qi5end", SymbolCurrency:="BTC", AccountRef:="1", Address:="687fadsARQWE65sf4qFSDFQWsf654qwsASDF", Description:="description example", DestinationTag:="string", NetWork:="stellar", Quantity:="2.5", Fee:="2")
                Console.WriteLine(WC.ToString)

                Dim GW = Await Client.GetWithdrawCoin(AccountID:="xfyh33gfbqeg1svo4on04lakkoc4ab82yhqad4h40wizxfennt3lt9qi5end", SymbolCurrency:="BTC", WithdrawID:="1")
                Console.WriteLine(GW.ToString)

                '-------------- API TRADER ---------------------

                Dim LO = Await Client.ListOrders(AccountID:="xfyh33gfbqeg1svo4on04lakkoc4ab82yhqad4h40wizxfennt3lt9qi5end", SymbolCurrency:="BTC-BRL")
                Console.WriteLine(LO.ToString)

                Dim PO = Await Client.PlaceOrders(AccountID:="xfyh33gfbqeg1svo4on04lakkoc4ab82yhqad4h40wizxfennt3lt9qi5end", SymbolCurrency:="BTC-BRL", Async:=True, Cost:=100, ExternalId:="134872873", LimitPrice:=9997, Quantity:="0.001", Side:="buy", StopPrice:=1000, Type:="limit")
                Console.WriteLine(PO.ToString)

                Dim CCO = Await Client.CancelOrder(AccountID:="xfyh33gfbqeg1svo4on04lakkoc4ab82yhqad4h40wizxfennt3lt9qi5end", SymbolCurrency:="BTC-BRL", OrderID:="123546")
                Console.WriteLine(CCO.ToString)

                Dim CO = Await Client.CancelOrder(AccountID:="xfyh33gfbqeg1svo4on04lakkoc4ab82yhqad4h40wizxfennt3lt9qi5end", SymbolCurrency:="BTC-BRL", OrderID:="123546")
                Console.WriteLine(CO.ToString)

                Dim GO = Await Client.GetOrder(AccountID:="xfyh33gfbqeg1svo4on04lakkoc4ab82yhqad4h40wizxfennt3lt9qi5end", SymbolCurrency:="BTC-BRL", OrderID:="123546")
                Console.WriteLine(GO.ToString)

                Dim CC = Await Client.CancellAllOpenOrder(AccountID:="xfyh33gfbqeg1svo4on04lakkoc4ab82yhqad4h40wizxfennt3lt9qi5end", HasExecutions:=False, SymbolCurrency:="BTC-BRL")
                Console.WriteLine(CC.ToString)

                Dim LAO = Await Client.ListAllOrders(AccountID:="xfyh33gfbqeg1svo4on04lakkoc4ab82yhqad4h40wizxfennt3lt9qi5end", HasExecutions:=False, SymbolCurrency:="BTC-BRL", Status:="filled", Size:="1")
                Console.WriteLine(LAO.ToString)
            Catch ex As Exception
                Console.WriteLine(ex.Message)
            End Try
        End Using
```

## C# CSharp

```csharp
{
    using (var Client = MercadoBitcoinClient.Create(new ApiConfig() { TAPI_ID = "Sua ID", TAPI_SECRET = "Sua SecretKey" }))
    {
        try
        {

            // --------------- API DE DADOS ----------------

            var H = Await Client.GetFeesFromAsset(Asset:="BTC");
            Console.WriteLine(H.ToString);

            var GOB = Await Client.GetOrderBook(SymbolCurrency:="BTC-BRL", Limit:=10);
            Console.WriteLine(GOB.ToString);

            var LT = Await Client.ListTrades(SymbolCurrency:="BTC-BRL");
            Console.WriteLine(LT.ToString);

            var LC = Await Client.ListCandles(SymbolCurrency:="BTC-BRL", Resolution:="1h", [To]:=Date.Now, From:=Date.Now.AddDays(-1));
            Console.WriteLine(LC.ToString);

            var LS = Await Client.ListSymbol(SymbolCurrency:="BTC-BRL");
            Console.WriteLine(LS.ToString);

            var LTS = Await Client.ListTickers(SymbolCurrency:="BTC-BRL,LTC-BRL");
            Console.WriteLine(LTS.ToString);

            // -------------- API TRADER ACCOUNT --------------
            var AccountID = Await Client.ListAccount();
            Console.WriteLine(AccountID.Root.Item(0).Identificacao); // Result: (xfyh33gfbqeg1svo4on04lakkoc4ab82yhqad4h40wizxfennt3lt9qi5end)

            var LA = Await Client.ListAccount();
            Console.WriteLine(LA.ToString);

            var LB = Await Client.ListBalance(AccountID:="xfyh33gfbqeg1svo4on04lakkoc4ab82yhqad4h40wizxfennt3lt9qi5end");
            Console.WriteLine(LB.ToString);

            var LP = Await Client.ListPositions(AccountID:="xfyh33gfbqeg1svo4on04lakkoc4ab82yhqad4h40wizxfennt3lt9qi5end", SymbolCurrency:="BTC-BRL");
            Console.WriteLine(LP.ToString);

            // --------------API TRADER WALLET ----------------

            var LD = Await Client.ListDeposits(AccountID:="xfyh33gfbqeg1svo4on04lakkoc4ab82yhqad4h40wizxfennt3lt9qi5end", SymbolCurrency:="BTC", From:=Date.Now.AddYears(-3), [To]:=Date.Now);
            Console.WriteLine(LD.ToString);

            var WC = Await Client.WithdrawCoin(AccountID:="xfyh33gfbqeg1svo4on04lakkoc4ab82yhqad4h40wizxfennt3lt9qi5end", SymbolCurrency:="BTC", AccountRef:="1", Address:="687fadsARQWE65sf4qFSDFQWsf654qwsASDF", Description:="description example", DestinationTag:="string", NetWork:="stellar", Quantity:="2.5", Fee:="2");
            Console.WriteLine(WC.ToString);

            var GW = Await Client.GetWithdrawCoin(AccountID:="xfyh33gfbqeg1svo4on04lakkoc4ab82yhqad4h40wizxfennt3lt9qi5end", SymbolCurrency:="BTC", WithdrawID:="1");
            Console.WriteLine(GW.ToString);

            // -------------- API TRADER ---------------------

            var LO = Await Client.ListOrders(AccountID:="xfyh33gfbqeg1svo4on04lakkoc4ab82yhqad4h40wizxfennt3lt9qi5end", SymbolCurrency:="BTC-BRL");
            Console.WriteLine(LO.ToString);

            var PO = Await Client.PlaceOrders(AccountID:="xfyh33gfbqeg1svo4on04lakkoc4ab82yhqad4h40wizxfennt3lt9qi5end", SymbolCurrency:="BTC-BRL", Async:=True, Cost:=100, ExternalId:="134872873", LimitPrice:=9997, Quantity:="0.001", Side:="buy", StopPrice:=1000, Type:="limit");
            Console.WriteLine(PO.ToString);

            var CCO = Await Client.CancelOrder(AccountID:="xfyh33gfbqeg1svo4on04lakkoc4ab82yhqad4h40wizxfennt3lt9qi5end", SymbolCurrency:="BTC-BRL", OrderID:="123546");
            Console.WriteLine(CCO.ToString);

            var CO = Await Client.CancelOrder(AccountID:="xfyh33gfbqeg1svo4on04lakkoc4ab82yhqad4h40wizxfennt3lt9qi5end", SymbolCurrency:="BTC-BRL", OrderID:="123546");
            Console.WriteLine(CO.ToString);

            var GO = Await Client.GetOrder(AccountID:="xfyh33gfbqeg1svo4on04lakkoc4ab82yhqad4h40wizxfennt3lt9qi5end", SymbolCurrency:="BTC-BRL", OrderID:="123546");
            Console.WriteLine(GO.ToString);

            var CC = Await Client.CancellAllOpenOrder(AccountID:="xfyh33gfbqeg1svo4on04lakkoc4ab82yhqad4h40wizxfennt3lt9qi5end", HasExecutions:=False, SymbolCurrency:="BTC-BRL");
            Console.WriteLine(CC.ToString);

            var LAO = Await Client.ListAllOrders(AccountID:="xfyh33gfbqeg1svo4on04lakkoc4ab82yhqad4h40wizxfennt3lt9qi5end", HasExecutions:=False, SymbolCurrency:="BTC-BRL", Status:="filled", Size:="1");
            Console.WriteLine(LAO.ToString);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}


```

## Links

- [Homepage](https://api.mercadobitcoin.net/api/v4/docs)
- [NuGet Package](https://www.nuget.org/packages/MercadoBitcoinAPI/)
- [Github Project](https://github.com/Romulo-Meirelles/MercadoBitcoinAPI)
- [License](https://github.com/Romulo-Meirelles/MercadoBitcoinAPI/blob/main/LICENSE)
- [Telegram](https://t.me/Romulo_Meirelles)
- [WhatsApp](https://wa.me/message/KWIS3BYO6K24N1)
- [UpWork](https://www.upwork.com/freelancers/~01fcbc5039ac5766b4)

