using Newtonsoft.Json;
using OpenQA.Selenium.DevTools.V108.Debugger;
using OpenQA.Selenium.DevTools.V108.Network;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading.Tasks;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot;

using Telegram.Bot.Types;
using Telegram.Bot.Args;
using static ApiConsumer;
using System.Runtime.ConstrainedExecution;
using Telegram.Bot.Types.Enums;
using System.Net;

public static class ApiConsumer
{

    public class Data
    {
        public string id { get; set; }
        public DateTime created_at { get; set; }
        public int color { get; set; }
        public int roll { get; set; }
        public string server_seed { get; set; }
    }

    public class Resultado
    {
        public string estrategia { get; set; }
        public int corSelecionada { get; set; }
        public string id { get; set; }
        public int colorProteger { get; set; }
    }

    public class Estrategias
    {
        public int color;
        public int colorProteger;
        //igual site da blase <<<<< de \\\/// de cima pra baixo 
        //cores de comparacao
        public int comparacaoUm;
        public int comparacaoDois;
        public int comparacaoTres;
        public int comparacaoQuatro;
    }


    public static class EstrategiaManager
    {
        public static List<Estrategias> GetListaDeEstrategias()
        {
            List<Estrategias> Listadeestrategias = new List<Estrategias>();

            Estrategias estrategiasum = new Estrategias() { color = 1, colorProteger = 0, comparacaoUm = 1, comparacaoDois = 2, comparacaoTres = 1, comparacaoQuatro = 2 };
            Estrategias estrategiadois = new Estrategias() { color = 2, colorProteger = 0, comparacaoUm = 2, comparacaoDois = 1, comparacaoTres = 2, comparacaoQuatro = 1 };
            Estrategias estrategiastres = new Estrategias() { color = 2, colorProteger = 0, comparacaoUm = 2, comparacaoDois = 1, comparacaoTres = 2, comparacaoQuatro = 2 };
            Estrategias estrategiasquatro = new Estrategias() { color = 1, colorProteger = 0, comparacaoUm = 1, comparacaoDois = 2, comparacaoTres = 1, comparacaoQuatro = 1 };
            Estrategias estrategiascinco = new Estrategias() { color = 1, colorProteger = 0, comparacaoUm = 1, comparacaoDois = 1, comparacaoTres = 2, comparacaoQuatro = 2 };
            Estrategias estrategiasseis = new Estrategias() { color = 2, colorProteger = 0, comparacaoUm = 2, comparacaoDois = 2, comparacaoTres = 1, comparacaoQuatro = 1 };
            Estrategias estrategiassete = new Estrategias() { color = 2, colorProteger = 0, comparacaoUm = 1, comparacaoDois = 2, comparacaoTres = 2, comparacaoQuatro = 1 };
            Estrategias estrategiasoito = new Estrategias() { color = 1, colorProteger = 0, comparacaoUm = 2, comparacaoDois = 1, comparacaoTres = 1, comparacaoQuatro = 2 };

            Listadeestrategias.Add(estrategiasum);
            Listadeestrategias.Add(estrategiadois);
            Listadeestrategias.Add(estrategiastres);
            Listadeestrategias.Add(estrategiasquatro);
            Listadeestrategias.Add(estrategiascinco);
            Listadeestrategias.Add(estrategiasseis);
            Listadeestrategias.Add(estrategiassete);
            Listadeestrategias.Add(estrategiasoito);


            return Listadeestrategias;
        }
    }

    public static float numeroDeWins;
    public static float numeroDeLoss;
    public static float numeroTotalDeEntrada;


    private static ITelegramBotClient botClient;

    private const string ApiUrl = "https://api-v2.blaze.com/roulette_games/recent";


    static async Task Main(string[] args)
    {

        botClient = new TelegramBotClient("TELEGRAM_BOT_TOKEN_AQUI");

        await Start();

    }

    public static async Task Start()
    {


        Console.WriteLine("Iniciou o Programa");

        try
        {
            await CheckEstrategias(EstrategiaManager.GetListaDeEstrategias());
        }
        catch
        {
            await CheckEstrategias(EstrategiaManager.GetListaDeEstrategias());
        }
    }

    public static async Task<List<Data>> RestornaUltimosResultados()
    {
        
        Console.WriteLine("Rodou a analise");
        var handler = new HttpClientHandler()
        {
            Proxy = new WebProxy("84.32.32.21:59100"),
            UseProxy = true
        };

        handler.Proxy.Credentials = new NetworkCredential("lpeixotopraca", "opEe4h5hGm");

        var httpClient = new HttpClient();// adicinar "handler" dentro do parentese para ativar o proxy
        var response = await httpClient.GetAsync(ApiUrl);
        response.EnsureSuccessStatusCode();
        var responseBody = await response.Content.ReadAsStringAsync();
        var dataList = System.Text.Json.JsonSerializer.Deserialize<List<Data>>(responseBody);
        
        return dataList;
    }

    public static async Task CheckEstrategias(List<Estrategias> ListEstrategias)
    {
        while (true)
        {
            Thread.Sleep(1500);
            var dataList = await RestornaUltimosResultados();

            for (int i = 0; i < ListEstrategias.Count; i++)
            {
                

                if (ListEstrategias[i].comparacaoUm == dataList[0].color && ListEstrategias[i].comparacaoDois == dataList[1].color && ListEstrategias[i].comparacaoTres == dataList[2].color && ListEstrategias[i].comparacaoQuatro == dataList[3].color)
                {
                    // executa se a estrategia for igual a cor escrita
                    if (ListEstrategias[i].color == 0)
                    {
                        Console.Write(ListEstrategias[i].comparacaoUm.ToString() + " " + ListEstrategias[i].comparacaoDois.ToString() + " " + ListEstrategias[i].comparacaoTres.ToString() + " " + ListEstrategias[i].comparacaoQuatro.ToString());

                        //chamar funcao entrar no brnaco
                        await EntrarNoBrancoAsync(dataList[0].id, ListEstrategias[i].color, ListEstrategias[i].colorProteger) ;
                        return;
                    }
                    else if (ListEstrategias[i].color == 1)
                    {

                        if (ListEstrategias[i].colorProteger == 0)
                        {
                            Console.Write(ListEstrategias[i].comparacaoUm.ToString() + " " + ListEstrategias[i].comparacaoDois.ToString() + " " + ListEstrategias[i].comparacaoTres.ToString() + " " + ListEstrategias[i].comparacaoQuatro.ToString());

                            //chamar funcao entrar no vermelho e proteger no branco
                            await EntrarNoVermelhoComProtecaoAsync(dataList[0].id, ListEstrategias[i].color, ListEstrategias[i].colorProteger);
                            return;
                        }
                        else if (ListEstrategias[i].colorProteger != 0)
                        {

                            Console.Write(ListEstrategias[i].comparacaoUm.ToString() + " " + ListEstrategias[i].comparacaoDois.ToString() + " " + ListEstrategias[i].comparacaoTres.ToString() + " " + ListEstrategias[i].comparacaoQuatro.ToString());

                            //chamar funcao entrar no vermelho
                            await EntrarNoVermelhoAsync(dataList[0].id, ListEstrategias[i].color, ListEstrategias[i].colorProteger);
                            return;
                        }


                    }
                    else if (ListEstrategias[i].color == 2)
                    {
                        if (ListEstrategias[i].colorProteger == 0)
                        {

                            Console.Write(ListEstrategias[i].comparacaoUm.ToString() + " " + ListEstrategias[i].comparacaoDois.ToString() + " " + ListEstrategias[i].comparacaoTres.ToString() + " " + ListEstrategias[i].comparacaoQuatro.ToString());

                            //mandou proteger no BRANCO e entrar no PRETO
                            await EntrarNoPretoComProtecaoAsync(dataList[0].id, ListEstrategias[i].color, ListEstrategias[i].colorProteger);
                            return;
                        }
                        else if (ListEstrategias[i].colorProteger != 0)
                        {

                            Console.Write(ListEstrategias[i].comparacaoUm.ToString() + " " + ListEstrategias[i].comparacaoDois.ToString() + " " + ListEstrategias[i].comparacaoTres.ToString() + " " + ListEstrategias[i].comparacaoQuatro.ToString());

                            //mandou entrar no Preto sem proteçao
                            await EntrarNoPretoAsync(dataList[0].id, ListEstrategias[i].color, ListEstrategias[i].colorProteger);
                            return;
                        }

                    }
                    else
                    {
                        Console.Write(ListEstrategias[i].comparacaoUm.ToString() + " " + ListEstrategias[i].comparacaoDois.ToString() + " " + ListEstrategias[i].comparacaoTres.ToString() + " " + ListEstrategias[i].comparacaoQuatro.ToString());
                        Console.WriteLine("Erro na hora de mandar entrar!");
                        // repete o codigo
                        continue;
                    }

                }
                else
                {
                    //return new Resultado { estrategia = "ERRO", corSelecionada = ListEstrategias[i].color, id = dataList[0].id, colorProteger = 0 };
                    Console.WriteLine("seguiu");
                    continue;
                }

            }
        }
    }

    public static async Task EntrarNoBrancoAsync(string idUltimaRodada, int corSelecionada, int corProtecao)
    {
        Console.WriteLine("ENTRAR NO BRANCO NA PROXIMA RODADA");

        string nomeDaCor = "";
        string nomeDaCorProtecao = "";
        switch (corSelecionada)
        {
            case 0:
                nomeDaCor = "BRANCO";
                break;
            case 1:
                nomeDaCor = "VERMELHO";
                break;
            case 2:
                nomeDaCor = "PRETO";
                break;
            default:
                nomeDaCor = "cor inválida";
                break;
        }

        switch (corProtecao)
        {
            case 0:
                nomeDaCorProtecao = "BRANCO";
                break;
            case 1:
                nomeDaCorProtecao = "VERMELHO";
                break;
            case 2:
                nomeDaCorProtecao = "PRETO";
                break;
            default:
                nomeDaCorProtecao = "cor inválida";
                break;
        }


        //CONFIGURAR A MENSAGEM DE ENTRADA NO BRANCO
        EnviarMensagem(@"<b>🚨ENTRADA CONFIRMADA🚨</b>"+
                            "\n"+
                            "\n"+
                            "\n📊<b>ENTRAR NA COR</b>:⚪" + nomeDaCor+"⚪"+
                            "\n"+
                            "\n"+
                            "\n👩‍💻<a href='http://bit.ly/blazelinkEMG' rel='nofollow'>ABRIR JOGO</a>👈👈👈 " +
                            "\n" +
                            "\n➡ <a href='http://bit.ly/3kSqs24' rel='nofollow' >️ CLIQUE AQUI</a> E ABRA SUA CONTA!");
        await ChecagemGreen(idUltimaRodada, corSelecionada, corProtecao);
        return;

    }

    public static async Task EntrarNoVermelhoAsync(string idUltimaRodada, int corSelecionada, int corProtecao)
    {
        Console.WriteLine("ENTRAR NO VERELHO NA PROXIMA RODADA!");
        string nomeDaCor = "";
        string nomeDaCorProtecao = "";
        switch (corSelecionada)
        {
            case 0:
                nomeDaCor = "BRANCO";
                break;
            case 1:
                nomeDaCor = "VERMELHO";
                break;
            case 2:
                nomeDaCor = "PRETO";
                break;
            default:
                nomeDaCor = "cor inválida";
                break;
        }

        switch (corProtecao)
        {
            case 0:
                nomeDaCorProtecao = "BRANCO";
                break;
            case 1:
                nomeDaCorProtecao = "VERMELHO";
                break;
            case 2:
                nomeDaCorProtecao = "PRETO";
                break;
            default:
                nomeDaCorProtecao = "cor inválida";
                break;
        }



        EnviarMensagem(@"<b>🚨ENTRADA CONFIRMADA🚨</b>"+
                            "\n" +
                            "\n" +
                            "\n📊<b>ENTRAR NA COR</b>:🔴" + nomeDaCor +"🔴" +
                            "\n💰<b>Opcional</b>: cobertura no "+nomeDaCorProtecao+"⚪" +
                            "\n" +
                            "\n" +
                            "\n👩‍💻<a href='http://bit.ly/blazelinkEMG' rel='nofollow'>ABRIR JOGO</a>👈👈👈 " +
                            "\n" +
                            "\n➡ <a href='http://bit.ly/3kSqs24' rel='nofollow' >️ CLIQUE AQUI</a> E ABRA SUA CONTA!");
        await ChecagemGreen(idUltimaRodada, corSelecionada, corProtecao);
        return;
    }

    public static async Task EntrarNoPretoAsync(string idUltimaRodada, int corSelecionada, int corProtecao)
    {
        Console.WriteLine("ENTRAR NO PRETO NA PROXIMA RODADA!");
        string nomeDaCor = "";
        string nomeDaCorProtecao = "";
        switch (corSelecionada)
        {
            case 0:
                nomeDaCor = "BRANCO";
                break;
            case 1:
                nomeDaCor = "VERMELHO";
                break;
            case 2:
                nomeDaCor = "PRETO";
                break;
            default:
                nomeDaCor = "cor inválida";
                break;
        }

        switch (corProtecao)
        {
            case 0:
                nomeDaCorProtecao = "BRANCO";
                break;
            case 1:
                nomeDaCorProtecao = "VERMELHO";
                break;
            case 2:
                nomeDaCorProtecao = "PRETO";
                break;
            default:
                nomeDaCorProtecao = "cor inválida";
                break;
        }



        EnviarMensagem(@"<b>🚨ENTRADA CONFIRMADA🚨</b>"+
                            "\n" +
                            "\n" +
                            "\n📊<b>ENTRAR NA COR</b>:⚫"+nomeDaCor+"⚫"+
                            "\n💰<b>Opcional</b>: cobertura no "+nomeDaCorProtecao+"⚪"+
                            "\n" +
                            "\n" +
                            "\n👩‍💻<a href='http://bit.ly/blazelinkEMG' rel='nofollow'>ABRIR JOGO</a>👈👈👈 " +
                            "\n" +
                            "\n➡ <a href='http://bit.ly/3kSqs24' rel='nofollow' >️ CLIQUE AQUI</a> E ABRA SUA CONTA!");
        await ChecagemGreen(idUltimaRodada, corSelecionada, corProtecao);
        return;
    }

    public static async Task EntrarNoVermelhoComProtecaoAsync(string idUltimaRodada, int corSelecionada, int corProtecao)
    {
        Console.WriteLine("ENTRAR NO VERELHO COM PROTECAO NO BRANCO NA PROXIMA RODADA!");
        string nomeDaCor = "";
        string nomeDaCorProtecao = "";
        switch (corSelecionada)
        {
            case 0:
                nomeDaCor = "BRANCO";
                break;
            case 1:
                nomeDaCor = "VERMELHO";
                break;
            case 2:
                nomeDaCor = "PRETO";
                break;
            default:
                nomeDaCor = "cor inválida";
                break;
        }

        switch (corProtecao)
        {
            case 0:
                nomeDaCorProtecao = "BRANCO";
                break;
            case 1:
                nomeDaCorProtecao = "VERMELHO";
                break;
            case 2:
                nomeDaCorProtecao = "PRETO";
                break;
            default:
                nomeDaCorProtecao = "cor inválida";
                break;
        }



        EnviarMensagem(@"<b>🚨ENTRADA CONFIRMADA🚨</b>"+
                            "\n" +
                            "\n" +
                            "\n📊<b>ENTRAR NA COR</b>:🔴" + nomeDaCor + "🔴" +
                            "\n💰<b>Opcional</b>: cobertura no "+nomeDaCorProtecao+"⚪" +
                            "\n" +
                            "\n" +
                            "\n👩‍💻<a href='http://bit.ly/blazelinkEMG' rel='nofollow'>ABRIR JOGO</a>👈👈👈 " +
                            "\n" +
                            "\n➡ <a href='http://bit.ly/3kSqs24' rel='nofollow' >️ CLIQUE AQUI</a> E ABRA SUA CONTA!");
        await ChecagemGreen(idUltimaRodada, corSelecionada, corProtecao);
        return;
    }

    public static async Task EntrarNoPretoComProtecaoAsync(string idUltimaRodada, int corSelecionada, int corProtecao)
    {
        Console.WriteLine("ENTRAR NO PRETO COM PROTECAO NO BRANCO NA PROXIMA RODADA!");
        string nomeDaCor = "";
        string nomeDaCorProtecao = "";
        switch (corSelecionada)
        {
            case 0:
                nomeDaCor = "BRANCO";
                break;
            case 1:
                nomeDaCor = "VERMELHO";
                break;
            case 2:
                nomeDaCor = "PRETO";
                break;
            default:
                nomeDaCor = "cor inválida";
                break;
        }

        switch (corProtecao)
        {
            case 0:
                nomeDaCorProtecao = "BRANCO";
                break;
            case 1:
                nomeDaCorProtecao = "VERMELHO";
                break;
            case 2:
                nomeDaCorProtecao = "PRETO";
                break;
            default:
                nomeDaCorProtecao = "cor inválida";
                break;
        }



        EnviarMensagem(@"<b>🚨ENTRADA CONFIRMADA🚨</b>"+
                            "\n"+
                            "\n"+
                            "\n📊<b>ENTRAR NA COR</b>:⚫"+nomeDaCor+"⚫" +
                            "\n💰<b>Opcional</b>: cobertura no " + nomeDaCorProtecao + "⚪" +
                            "\n" +
                            "\n" +
                            "\n👩‍💻<a href='http://bit.ly/blazelinkEMG' rel='nofollow'>ABRIR JOGO</a>👈👈👈 " +
                            "\n" +
                            "\n➡ <a href='http://bit.ly/3kSqs24' rel='nofollow' >️ CLIQUE AQUI</a> E ABRA SUA CONTA!");
        await ChecagemGreen(idUltimaRodada, corSelecionada, corProtecao);
        return;
    }


    public static async Task ChecagemGreen(string idUltimaRodada, int corSelecionada, int corProtecao)
    {
        Console.WriteLine("ChecagemGreen");
        while (true)
        {
            Thread.Sleep(1500);
            var dataList = await RestornaUltimosResultados();

            //cheka se o id da partida e diferente
            if(dataList[0].id != idUltimaRodada)
            {
                
                if (dataList[0].color == corSelecionada)
                {
                    //deu green
                    await CheckerGreenAonde(corSelecionada);
                    return;
                }
                else if (dataList[0].color == corProtecao)
                {
                    //deu green na protrecao 
                    await CheckerGreenProtecao(corProtecao);
                    return;
                }
                else
                {
                    if(corSelecionada != 0)
                    {
                        //             REEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEED
                        // errou mais entrou em martin gale
                        await MartiGale1(dataList[0].id,corSelecionada,corProtecao);
                        return;
                    }
                    else
                    {
                        //deu red no branco

                        await ChecagemDeReentrada(dataList[0].id);
                        return;
                    }
                    
                }
            }
            else
            {
                continue;  
            }
        }
    }

    public static async Task ChecagemDeReentrada(string idUltimaRodada)
    {
        Console.WriteLine("ChecagemDeReentrada");
        while (true)
        {
            Thread.Sleep(1500);
            var dataList = await RestornaUltimosResultados();

            if (dataList[0].id == idUltimaRodada)
            {
                await Start();
                return;
            }
            else
            {
                continue;
            }

        }
    }

    public static async Task CheckerGreenAonde(int corSelecionada)
    {
        Console.WriteLine("CheckerGreenAonde");

        if(corSelecionada == 0)
        {
            //win no branco 
            numeroDeWins += 1;
            numeroTotalDeEntrada += 1;
            EnviarMensagem("✅⚪️✅");
            EnviarMensagem("📊 RELATÓRIO 📊" +
                           "\nTotal de entradas: " + numeroTotalDeEntrada +
                           "\nTotal de win: " + numeroDeWins +
                           "\nTotal de loss: " + numeroDeLoss +
                           "\nTaxa de acertividade: " + RetornaPorcetagem() + "%"
                           ) ;
            await Start();
            return;
        }
        else if (corSelecionada == 1)
        {
            //win no vermelho
            numeroDeWins += 1;
            numeroTotalDeEntrada += 1;
            EnviarMensagem("✅✅✅");
            EnviarMensagem("📊 RELATÓRIO 📊" +
                           "\nTotal de entradas: " + numeroTotalDeEntrada +
                           "\nTotal de win: " + numeroDeWins +
                           "\nTotal de loss: " + numeroDeLoss +
                           "\nTaxa de acertividade: " + RetornaPorcetagem() + "%"
                           );
            await Start();
            return;
        }
        else if(corSelecionada == 2)
        {
            // win no preto
            numeroDeWins += 1;
            numeroTotalDeEntrada += 1;
            EnviarMensagem("✅✅✅");
            EnviarMensagem("📊 RELATÓRIO 📊" +
                           "\nTotal de entradas: " + numeroTotalDeEntrada +
                           "\nTotal de win: " + numeroDeWins +
                           "\nTotal de loss: " + numeroDeLoss +
                           "\nTaxa de acertividade: " + RetornaPorcetagem() + "%"
                           );
            await Start();
            return;
        }
        else
        {
            //       ERRO
            Console.WriteLine("ERROR");
        }
        

    }

    public static async Task CheckerGreenProtecao(int corProtecao)
    {
        Console.WriteLine("CheckerGreenProtecao");

        if (corProtecao == 0)
        {
            //win no branco 
            numeroDeWins += 1;
            numeroTotalDeEntrada += 1;
            EnviarMensagem("✅⚪️✅");
            EnviarMensagem("📊 RELATÓRIO 📊" +
                           "\nTotal de entradas: " + numeroTotalDeEntrada +
                           "\nTotal de win: " + numeroDeWins +
                           "\nTotal de loss: " + numeroDeLoss +
                           "\nTaxa de acertividade: " + RetornaPorcetagem() + "%"
                           );
            await Start();
            return;
        }
        else if (corProtecao == 1)
        {
            //win no vermelho
            numeroDeWins += 1;
            numeroTotalDeEntrada += 1;
            EnviarMensagem("✅⚪️✅");
            EnviarMensagem("📊 RELATÓRIO 📊" +
                           "\nTotal de entradas: " + numeroTotalDeEntrada +
                           "\nTotal de win: " + numeroDeWins +
                           "\nTotal de loss: " + numeroDeLoss +
                           "\nTaxa de acertividade: " + RetornaPorcetagem() + "%"
                           );
            await Start();
            return;
        }
        else if (corProtecao == 2)
        {
            //win no preto
            numeroDeWins += 1;
            numeroTotalDeEntrada += 1;
            EnviarMensagem("✅⚪️✅");
            EnviarMensagem("📊 RELATÓRIO 📊" +
                           "\nTotal de entradas: " + numeroTotalDeEntrada +
                           "\nTotal de win: " + numeroDeWins +
                           "\nTotal de loss: " + numeroDeLoss +
                           "\nTaxa de acertividade: " + RetornaPorcetagem() + "%"
                           );
            await Start();
            return;
        }

    }

    public static async Task MartiGale1(string idUltimaRodada, int corSelecionada, int corProtecao)
    {
        Console.WriteLine("entrou martingale1");
        string nomeDaCor = "";
        string nomeDaCorProtecao = "";

        switch (corSelecionada)
        {
            case 0:
                nomeDaCor = "branco";
                break;
            case 1:
                nomeDaCor = "vermelho";
                break;
            case 2:
                nomeDaCor = "preto";
                break;
            default:
                nomeDaCor = "cor inválida";
                break;
        }

        switch (corProtecao)
        {
            case 0:
                nomeDaCorProtecao = "branco";
                break;
            case 1:
                nomeDaCorProtecao = "vermelho";
                break;
            case 2:
                nomeDaCorProtecao = "preto";
                break;
            default:
                nomeDaCorProtecao = "cor inválida";
                break;
        }


        //enviar uma mensagem para o usuario para dar entrada na rodada
        EnviarMensagem("🤞 Façam a 1º proteção 🤞");
        var dataList = await RestornaUltimosResultados();

        await ChecagerMGUm(dataList[0].id, corSelecionada, corProtecao);
        return;

    }

    public static async Task ChecagerMGUm(string idRodada, int CorSelecionada, int CorProtecao)
    {
        Console.WriteLine("Entrou na checagem do MG1");
        while (true)
        {
            Thread.Sleep(1500);
            var dataList = await RestornaUltimosResultados();

            if (dataList[0].id != idRodada)
            {
                if(dataList[0].color == CorSelecionada)
                {
                    // deu green no MG1
                    await CheckGreenMGUm(CorSelecionada);
                    return;

                }
                else if(dataList[0].color == CorProtecao)
                {
                    // deu green no branco no MG1


                    await CheckMGUmProtecao(CorSelecionada);
                    return;
                }
                else
                {
                    //deu red CHAMA MG2
                    await MartiGale2(dataList[0].id, CorSelecionada, CorProtecao);
                    return;
                }
            }
            else
            {
                continue;
            }


        }
    }

    public static async Task CheckGreenMGUm(int corSelecionada)
    {
        Console.WriteLine("CheckGreenMGUm");

        if (corSelecionada == 0)
        {
            //win no branco COM MG1
            numeroDeWins += 1;
            numeroTotalDeEntrada += 1;
            EnviarMensagem("✅⚪️✅");
            EnviarMensagem("📊 RELATÓRIO 📊" +
                           "\nTotal de entradas: " + numeroTotalDeEntrada +
                           "\nTotal de win: " + numeroDeWins +
                           "\nTotal de loss: " + numeroDeLoss +
                           "\nTaxa de acertividade: " + RetornaPorcetagem() + "%"
                           );
            await Start();
            return;
        }
        else if (corSelecionada == 1)
        {
            //win no vermelho COM MG1
            numeroDeWins += 1;
            numeroTotalDeEntrada += 1;
            EnviarMensagem("✅✅✅");
            EnviarMensagem("📊 RELATÓRIO 📊" +
                           "\nTotal de entradas: " + numeroTotalDeEntrada +
                           "\nTotal de win: " + numeroDeWins +
                           "\nTotal de loss: " + numeroDeLoss +
                           "\nTaxa de acertividade: " + RetornaPorcetagem() + "%"
                           );
            await Start();
            return;
        }
        else if (corSelecionada == 2)
        {
            //win no preto COM MG1
            numeroDeWins += 1;
            numeroTotalDeEntrada += 1;
            EnviarMensagem("✅✅✅");
            EnviarMensagem("📊 RELATÓRIO 📊" +
                           "\nTotal de entradas: " + numeroTotalDeEntrada +
                           "\nTotal de win: " + numeroDeWins +
                           "\nTotal de loss: " + numeroDeLoss +
                           "\nTaxa de acertividade: " + RetornaPorcetagem() + "%"
                           );
            await Start();
            return;
        }

    }

    public static async Task CheckMGUmProtecao(int corProtecao)
    {
        Console.WriteLine("CheckGreenMGUm");

        if (corProtecao == 0)
        {
            //win no branco 
            numeroDeWins += 1;
            numeroTotalDeEntrada += 1;
            EnviarMensagem("✅⚪️✅");
            EnviarMensagem("📊 RELATÓRIO 📊" +
                           "\nTotal de entradas: " + numeroTotalDeEntrada +
                           "\nTotal de win: " + numeroDeWins +
                           "\nTotal de loss: " + numeroDeLoss +
                           "\nTaxa de acertividade: " + RetornaPorcetagem() + "%"
                           );
            await Start();
            return;
        }
        else if (corProtecao == 1)
        {
            //win no vermelho
            numeroDeWins += 1;
            numeroTotalDeEntrada += 1;
            EnviarMensagem("✅⚪️✅");
            EnviarMensagem("📊 RELATÓRIO 📊" +
                           "\nTotal de entradas: " + numeroTotalDeEntrada +
                           "\nTotal de win: " + numeroDeWins +
                           "\nTotal de loss: " + numeroDeLoss +
                           "\nTaxa de acertividade: " + RetornaPorcetagem() + "%"
                           );
            await Start();
            return;
        }
        else if (corProtecao == 2)
        {
            //win no preto
            numeroDeWins += 1;
            numeroTotalDeEntrada += 1;
            EnviarMensagem("✅⚪️✅");
            EnviarMensagem("📊 RELATÓRIO 📊" +
                           "\nTotal de entradas: " + numeroTotalDeEntrada +
                           "\nTotal de win: " + numeroDeWins +
                           "\nTotal de loss: " + numeroDeLoss +
                           "\nTaxa de acertividade: " + RetornaPorcetagem() + "%"
                           );
            await Start();
            return;
        }

    }

    public static async Task MartiGale2(string idUltimaRodada, int corSelecionada, int corProtecao)
    {
        Console.WriteLine("entrou martingale2");
        string nomeDaCor = "";
        string nomeDaCorProtecao = "";

        switch (corSelecionada)
        {
            case 0:
                nomeDaCor = "branco";
                break;
            case 1:
                nomeDaCor = "vermelho";
                break;
            case 2:
                nomeDaCor = "preto";
                break;
            default:
                nomeDaCor = "cor inválida";
                break;
        }

        switch (corProtecao)
        {
            case 0:
                nomeDaCorProtecao = "branco";
                break;
            case 1:
                nomeDaCorProtecao = "vermelho";
                break;
            case 2:
                nomeDaCorProtecao = "preto";
                break;
            default:
                nomeDaCorProtecao = "cor inválida";
                break;
        }


        //enviar uma mensagem para o usuario para dar entrada na rodada
        EnviarMensagem("🤞 Façam a 2º proteção 🤞");
        var dataList = await RestornaUltimosResultados();

        await ChecagerMGDois(dataList[0].id, corSelecionada, corProtecao);
        return;

    }
    public static async Task ChecagerMGDois(string idRodada, int CorSelecionada, int CorProtecao)
    {
        Console.WriteLine("Entrou na checagem do MG2");
        while (true)
        {
            Thread.Sleep(1500);
            var dataList = await RestornaUltimosResultados();

            if (dataList[0].id != idRodada)
            {
                if (dataList[0].color == CorSelecionada)
                {
                    // deu green no MG2
                    await CheckGreenMDois(CorSelecionada);
                    return;

                }
                else if (dataList[0].color == CorProtecao)
                {
                    // deu green no branco no MG1


                    await CheckMGDoisProtecao(CorProtecao);
                    return;
                }
                else
                {
                    numeroTotalDeEntrada += 1;
                    numeroDeLoss += 1;
                    EnviarMensagem("💔💔💔");
                    EnviarMensagem("📊 RELATÓRIO 📊" +
                                   "\nTotal de entradas: " + numeroTotalDeEntrada +
                                   "\nTotal de win: " + numeroDeWins +
                                   "\nTotal de loss: " + numeroDeLoss +
                                   "\nTaxa de acertividade: " + RetornaPorcetagem() + "%"
                                   );
                    await Start();
                    return;
                }
            }
            else
            {
                continue;
            }


        }
    }

    public static async Task CheckGreenMDois(int corSelecionada)
    {
        Console.WriteLine("CheckGreenMGDois");

        if (corSelecionada == 0)
        {
            //win no branco COM MG1
            numeroDeWins += 1;
            numeroTotalDeEntrada += 1;
            EnviarMensagem("✅⚪️✅");
            EnviarMensagem("📊 RELATÓRIO 📊" +
                           "\nTotal de entradas: " + numeroTotalDeEntrada +
                           "\nTotal de win: " + numeroDeWins +
                           "\nTotal de loss: " + numeroDeLoss +
                           "\nTaxa de acertividade: " + RetornaPorcetagem() + "%"
                           );
            await Start();
            return;
        }
        else if (corSelecionada == 1)
        {
            //win no vermelho COM MG1
            numeroDeWins += 1;
            numeroTotalDeEntrada += 1;
            EnviarMensagem("✅✅✅");
            EnviarMensagem("📊 RELATÓRIO 📊" +
                           "\nTotal de entradas: " + numeroTotalDeEntrada +
                           "\nTotal de win: " + numeroDeWins +
                           "\nTotal de loss: " + numeroDeLoss +
                           "\nTaxa de acertividade: " + RetornaPorcetagem() + "%"
                           );
            await Start();
            return;
        }
        else if (corSelecionada == 2)
        {
            //win no preto COM MG1
            numeroDeWins += 1;
            numeroTotalDeEntrada += 1;
            EnviarMensagem("✅✅✅");
            EnviarMensagem("📊 RELATÓRIO 📊" +
                           "\nTotal de entradas: " + numeroTotalDeEntrada +
                           "\nTotal de win: " + numeroDeWins +
                           "\nTotal de loss: " + numeroDeLoss +
                           "\nTaxa de acertividade: " + RetornaPorcetagem() + "%"
                           );
            await Start();
            return;
        }

    }

    public static async Task CheckMGDoisProtecao(int corSelecionada)
    {
        Console.WriteLine("CheckGreenMGDois");

        if (corSelecionada == 0)
        {
            //win no branco COM MG1
            numeroDeWins += 1;
            numeroTotalDeEntrada += 1;
            EnviarMensagem("✅⚪️✅");
            EnviarMensagem("📊 RELATÓRIO 📊" +
                           "\nTotal de entradas: " + numeroTotalDeEntrada +
                           "\nTotal de win: " + numeroDeWins +
                           "\nTotal de loss: " + numeroDeLoss +
                           "\nTaxa de acertividade: " + RetornaPorcetagem() + "%"
                           );
            await Start();
            return;
        }
        else if (corSelecionada == 1)
        {
            //win no vermelho COM MG1
            numeroDeWins += 1;
            numeroTotalDeEntrada += 1;
            EnviarMensagem("✅⚪️✅");
            EnviarMensagem("📊 RELATÓRIO 📊" +
                           "\nTotal de entradas: " + numeroTotalDeEntrada +
                           "\nTotal de win: " + numeroDeWins +
                           "\nTotal de loss: " + numeroDeLoss +
                           "\nTaxa de acertividade: " + RetornaPorcetagem() + "%"
                           );
            await Start();
            return;
        }
        else if (corSelecionada == 2)
        {
            //win no preto COM MG1
            numeroDeWins += 1;
            numeroTotalDeEntrada += 1;
            EnviarMensagem("✅⚪️✅");
            EnviarMensagem("📊 RELATÓRIO 📊" +
                           "\nTotal de entradas: " + numeroTotalDeEntrada +
                           "\nTotal de win: " + numeroDeWins +
                           "\nTotal de loss: " + numeroDeLoss +
                           "\nTaxa de acertividade: " + RetornaPorcetagem() + "%"
                           );
            await Start();
            return;
        }

    }

    public static string RetornaPorcetagem()
    {
        float resultado = numeroDeWins / numeroTotalDeEntrada * 100;
        Console.WriteLine("o resultado da porcentagem e" + resultado.ToString() + " e fomatado e : " + resultado.ToString("F2"));
        return resultado.ToString("F2");




    }




    public static async void EnviarMensagem(string mensagem, string caminhoFoto = null)
    {
        long chatId = "ID_SALA_TELEGRAM"; // Substitua pelo ID do grupo em que deseja enviar a mensagem


        if (caminhoFoto == null)
        {
            await botClient.SendTextMessageAsync(chatId, mensagem, ParseMode.Html, disableWebPagePreview: true);
        }
        else
        {
            InputOnlineFile foto = new InputOnlineFile(caminhoFoto);
            await botClient.SendPhotoAsync(chatId, foto, caption: mensagem, ParseMode.Html);
        }
    }





}




