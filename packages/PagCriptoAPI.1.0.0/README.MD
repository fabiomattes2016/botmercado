# ![Logo](https://raw.githubusercontent.com/Romulo-Meirelles/PagCriptoAPI/main/Pictures/pagcripto_Logo.png) 
PagCriptoAPI - (.NetFramework | .NetStandard | .NetCore)

[![NuGet version (PagCriptoAPI)](https://img.shields.io/nuget/v/PagCriptoAPI.svg?style=flat-square)](https://www.nuget.org/packages/PagCriptoAPI/)

PAGCRIPTO API NÃO OFICIAL (UNOFFICIAL). "Não Possuímos nenhum vínculo com a empresa PAGCRIPTO."

(API .V2) PAGCRIPTO com essa API você faz consultas, balanço, cotação, saques, pagamentos, trades, transações, abre ordens, fechar ordens, compra a mercado, vender a limite e outras mais na plataforma do Pagcripto Confira!

- Características
- Faça pagamentos.
- Veja os Book de ofertas.
- Veja Tickes. (Candles)
- Veja seu balanço.
- Verifique suas ordens.
- Abra ordens.
- Encerre ordens ativas.
- Veja o histórico de suas transações.
- Faça saques.
- Gerencie seus ativos e dinheiro.
- Fácil de usar.

#DIVIRTA-SE!

##  ALGUNS EXEMPLOS DE COMO USAR.

## Visual Basic

```vb
   Shared ReadOnly TokenAPI As String = "ls86apl6az5toeauatalnlihfzsren3hofprn3q2" '<-- SEU TOKEN
 
   Using Client = PagCriptoClient.Create(New ApiConfig With {.TokenAPI = TokenAPI})
            Try

                '<--------------------------------------------- PUBLIC ----------------------------------------------->
                Dim Order = Await Client.Orders("BTC")
                Console.WriteLine("Currency: " & Order.ToString)

                Dim Ticker = Await Client.Ticker("BTCBRL")
                Console.WriteLine(Ticker.ToString)

                Dim Tickers = Await Client.Tickers()
                Console.WriteLine(Tickers.ToString)

                Dim Trades = Await Client.Trades("BTCBRL")
                Console.WriteLine(Trades.ToString)

                Dim Pairs = Await Client.Pairs
                Console.WriteLine(Pairs.ToString)

                '<--------------------------------------------- TRADER ----------------------------------------------->
                Dim Balance = Await Client.Balance()
                For i = 0 To Balance.Count - 1
                    Console.WriteLine("Currency: " & Balance(i).Currency & vbCrLf &
                                      "Current: " & Balance(i).Current & vbCrLf &
                                      "Orders: " & Balance(i).Orders & vbCrLf &
                                      "Whitdrwal Fee: " & Balance(i).Withdraw_Fee & vbCrLf &
                                      "Trade Fee: " & Balance(i).Trade_Fee & vbCrLf & vbCrLf)
                Next

                Dim GetBookOrders = Await Client.GetBookOrders("BTCBRL")
                For i = 0 To GetBookOrders.Orders.BTCBRL.Buy.Count - 1
                    Console.WriteLine("Currency: BTCBRL" & vbCrLf &
                                      "Id: " & GetBookOrders.Orders.BTCBRL.Buy(i).Cotation & vbCrLf &
                                      "Quantidade: " & GetBookOrders.Orders.BTCBRL.Buy(i).Quantity & vbCrLf &
                                      "Cotação: " & GetBookOrders.Orders.BTCBRL.Buy(i).Cotation & vbCrLf &
                                      "Status: " & GetBookOrders.Orders.BTCBRL.Buy(i).Status & vbCrLf & vbCrLf)
                Next



                Dim GetAggregateOrders = Await Client.GetAggregateOrders(Page:=0, Per_Page:=0)
                Console.WriteLine("Current Page: " & GetAggregateOrders.Pagination.Current_Page & vbCrLf &
                                 "Total Page: " & GetAggregateOrders.Pagination.Total_Page & vbCrLf &
                                 "Per Page: " & GetAggregateOrders.Pagination.Per_Page & vbCrLf &
                                 "Total Records: " & GetAggregateOrders.Pagination.Total_Records & vbCrLf & vbCrLf)

                For i = 0 To GetAggregateOrders.History.Count - 1
                    Console.WriteLine("Currency: BTCBRL" & vbCrLf &
                                      "Symbol: " & GetAggregateOrders.History(i).Symbol & vbCrLf &
                                      "Id: " & GetAggregateOrders.History(i).Id & vbCrLf &
                                      "Price: " & GetAggregateOrders.History(i).Price & vbCrLf &
                                      "Total: " & GetAggregateOrders.History(i).Total & vbCrLf &
                                      "Ordered: " & GetAggregateOrders.History(i).Ordered & vbCrLf &
                                      "Quantity: " & GetAggregateOrders.History(i).Quantity & vbCrLf &
                                      "Order: " & GetAggregateOrders.History(i).Order & vbCrLf &
                                      "Created Date: " & GetAggregateOrders.History(i).Created_Date & vbCrLf &
                                      "Updated Date: " & GetAggregateOrders.History(i).Updated_Date & vbCrLf &
                                      "Status: " & GetAggregateOrders.History(i).Status & vbCrLf & vbCrLf)
                Next


                Dim StatusOrder = Await Client.StatusOrder(IdOrder:="5d9e200fcd51a", Par:="BTCBRL")
                Console.WriteLine("Pair: " & StatusOrder.Pair & vbCrLf &
                    "Id: " & StatusOrder.Id & vbCrLf &
                    "Status: " & StatusOrder.Status & vbCrLf &
                    "Preço Total: " & StatusOrder.Preco_Total & vbCrLf &
                    "Quantidade Executada: " & StatusOrder.Quantidade_Executada & vbCrLf &
                    "Quantidade Total: " & StatusOrder.Quantidade_Total & vbCrLf &
                    "Tipo: " & StatusOrder.Tipo & vbCrLf &
                    "Created Date: " & StatusOrder.Create_Date & vbCrLf &
                    "Updated Date: " & StatusOrder.Update_Date & vbCrLf & vbCr)



                Dim History = Await Client.History(Page:=0, Par:="BTCBRL")
                Console.WriteLine("Current Page: " & History.Pagination.Current_Page & vbCrLf &
                                 "Total Page: " & History.Pagination.Total_Page & vbCrLf &
                                 "Per Page: " & History.Pagination.Per_Page & vbCrLf &
                                 "Total Records: " & History.Pagination.Total_Records & vbCrLf & vbCrLf)

                For i = 0 To History.History.Count - 1
                    Console.WriteLine("Pair: " & History.History(i).Pair & vbCrLf &
                                      "Quantity: " & History.History(i).Quantity & vbCrLf &
                                      "Price: " & History.History(i).Price & vbCrLf &
                                      "Total: " & History.History(i).Total & vbCrLf &
                                      "Order: " & History.History(i).Order & vbCrLf &
                                      "Tax: " & History.History(i).Tax & vbCrLf &
                                      "Date: " & History.History(i).Data & vbCrLf & vbCrLf)
                Next

                '<--------------------------------------------- GATEWAY ----------------------------------------------->

                Dim CreatePayment = Await Client.CreatePayment(Currency:="LTC", Amount:=20.0, Description:="ID#20", CallBack:="https://www.seu-ecommerce.com.br/callback.php")
                Console.WriteLine(CreatePayment.ToString())

                Dim CheckPayment = Await Client.CheckPayment(Payment_Request_Id:="6049144925d4f6ecd3f986")
                Console.WriteLine(CheckPayment.ToString())

                Dim CancelPayment = Await Client.CancelPayment(Payment_Request_Id:="6049144925d4f6ecd3f986")
                Console.WriteLine(CancelPayment.ToString())

                Dim GetListPayment = Await Client.GetListPayment()
                For i = 0 To GetListPayment.Count - 1
                    Console.WriteLine("Currency: " & GetListPayment(i).Currency & vbCrLf &
                                      "payment_request: " & GetListPayment(i).Payment_Request & vbCrLf &
                                      "created-at: " & GetListPayment(i).Created_At & vbCrLf &
                                      "amount: " & GetListPayment(i).Amount & vbCrLf &
                                      "received_amount: " & GetListPayment(i).Received_Amount & vbCrLf &
                                      "confirmations: " & GetListPayment(i).Confirmations & vbCrLf &
                                      "description: " & GetListPayment(i).Description & vbCrLf &
                                      "status: " & GetListPayment(i).Status & vbCrLf & vbCrLf)
                Next


                '<--------------------------------------------- OTC ----------------------------------------------->

                Dim GetTicker_OTC = Await Client.Ticker_OTC("BTCBRL")
                Console.WriteLine(GetTicker_OTC.ToString())

                Dim RFQ_OTC = Await Client.RFQ_OTC("BTCBRL", "buy", 0.001)
                Console.WriteLine(RFQ_OTC.ToString())

                Dim Trade_OTC = Await Client.Trade_OTC("f63fc00c-bddc-403c-bda5-43f1dd57f674", 0.001)
                Console.WriteLine(Trade_OTC.ToString())

                Dim History_OTC = Await Client.History_OTC(Page:=0)
                Console.WriteLine(History_OTC.ToString())

            Catch ex As Exception
                Console.WriteLine(ex.Message)
            End Try



        End Using
```

## C# CSharp

```csharp
Using;
Client = PagCriptoClient.Create(new ApiConfig(), With {okenAPI=TokenAPI);
{
    // <--------------------------------------------- PUBLIC ----------------------------------------------->
    object Order = Await;
    Client.Orders("BTC");
    Console.WriteLine(("Currency: " + Order.ToString));
    object Ticker = Await;
    Client.Ticker("BTCBRL");
    Console.WriteLine(Ticker.ToString);
    object Tickers = Await;
    Client.Tickers();
    Console.WriteLine(Tickers.ToString);
    object Trades = Await;
    Client.Trades("BTCBRL");
    Console.WriteLine(Trades.ToString);
    object Pairs = Await;
    Client.Pairs;
    Console.WriteLine(Pairs.ToString);
    }
```

## Links

- [Homepage](https://pagcripto.com.br/?aff=4398)
- [NuGet Package](https://www.nuget.org/packages/PagCriptoAPI/)
- [Github Project](https://github.com/Romulo-Meirelles/PagCriptoAPI)
- [License](https://github.com/Romulo-Meirelles/PagCripto/blob/main/LICENSE)
- [Telegram](https://t.me/Romulo_Meirelles)
- [WhatsApp](https://wa.me/message/KWIS3BYO6K24N1)
- [UpWork](https://www.upwork.com/freelancers/~01fcbc5039ac5766b4)

