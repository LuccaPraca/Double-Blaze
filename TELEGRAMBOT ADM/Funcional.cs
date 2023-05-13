using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using static ApiConsumer;

public static class ApiConsumer
{
    public static bool desligar = false;

    static async Task Main(string[] args)
    {







        Console.WriteLine("gerando lista!!");

        Console.WriteLine("INICIANDO BOT!!");

        // Faça algo com os dados recebidos


        if (!desligar)
        {
            await Iniciar();
        }




    }


    public static async Task Iniciar()
    {
        Console.WriteLine("Iniciou bot");


        Resultado resultado = await GetDataAsync(Listadeestrategias());
        TimeSpan retryInterval = TimeSpan.FromSeconds(1);
        while (resultado.estrategia == "ERRO")
        {

            await Task.Delay(retryInterval);
            resultado = await GetDataAsync(Listadeestrategias());
        }

        CompararDados(resultado.estrategia, resultado.corSelecionada, resultado.id, resultado.colorProteger);
    }



    public static List<Estrategias> Listadeestrategias()
    {
        List<Estrategias> Listadeestrategias = new List<Estrategias>();

        Estrategias estrategiasum = new Estrategias() { color = 1, colorProteger = 0, comparacaoUm = 2, comparacaoDois = 2, comparacaoTres = 1, comparacaoQuatro = 2 };
        Estrategias estrategiadois = new Estrategias() { color = 2, colorProteger = 0, comparacaoUm = 1, comparacaoDois = 2, comparacaoTres = 1, comparacaoQuatro = 2 };
        Estrategias estrategiastres = new Estrategias() { color = 2, colorProteger = 0, comparacaoUm = 2, comparacaoDois = 2, comparacaoTres = 1, comparacaoQuatro = 2 };
        Estrategias estrategiasquatro = new Estrategias() { color = 1, colorProteger = 0, comparacaoUm = 1, comparacaoDois = 1, comparacaoTres = 2, comparacaoQuatro = 1 };
        Estrategias estrategiascinco = new Estrategias() { color = 2, colorProteger = 0, comparacaoUm = 1, comparacaoDois = 1, comparacaoTres = 2, comparacaoQuatro = 1 };
        Estrategias estrategiasseis = new Estrategias() { color = 1, colorProteger = 2, comparacaoUm = 1, comparacaoDois = 1, comparacaoTres = 2, comparacaoQuatro = 2 };
        Estrategias estrategiassete = new Estrategias() { color = 1, colorProteger = 2, comparacaoUm = 2, comparacaoDois = 0, comparacaoTres = 1, comparacaoQuatro = 1 };
        Estrategias estrategiasoito = new Estrategias() { color = 1, colorProteger = 2, comparacaoUm = 2, comparacaoDois = 0, comparacaoTres = 2, comparacaoQuatro = 1 };
        Estrategias estrategiasnove = new Estrategias() { color = 1, colorProteger = 2, comparacaoUm = 1, comparacaoDois = 2, comparacaoTres = 1, comparacaoQuatro = 1 };
        Estrategias estrategiasdez = new Estrategias() { color = 1, colorProteger = 2, comparacaoUm = 2, comparacaoDois = 1, comparacaoTres = 0, comparacaoQuatro = 2 };
        Estrategias estrategiasonze = new Estrategias() { color = 1, colorProteger = 2, comparacaoUm = 2, comparacaoDois = 2, comparacaoTres = 1, comparacaoQuatro = 2 };



        Listadeestrategias.Add(estrategiasum);
        Listadeestrategias.Add(estrategiadois);
        Listadeestrategias.Add(estrategiastres);
        Listadeestrategias.Add(estrategiasquatro);
        Listadeestrategias.Add(estrategiascinco);
        Listadeestrategias.Add(estrategiasseis);
        Listadeestrategias.Add(estrategiassete);
        Listadeestrategias.Add(estrategiasoito);
        Listadeestrategias.Add(estrategiasnove);
        Listadeestrategias.Add(estrategiasdez);
        Listadeestrategias.Add(estrategiasonze);

        return Listadeestrategias;
    }


    private const string ApiUrl = "https://api-v2.blaze.com/roulette_games/recent";
    public class Resultado
    {
        public string estrategia { get; set; }
        public int corSelecionada { get; set; }
        public string id { get; set; }
        public int colorProteger { get; set; }
    }

    public static async Task<Resultado> GetDataAsync(List<Estrategias> ListEstrategias)
    {
        Console.WriteLine("Rodou a analise");
        var httpClient = new HttpClient();
        var response = await httpClient.GetAsync(ApiUrl);
        response.EnsureSuccessStatusCode();
        var responseBody = await response.Content.ReadAsStringAsync();
        var dataList = System.Text.Json.JsonSerializer.Deserialize<List<Data>>(responseBody);

        for (int i = 0; i < 5; i++)
        {
            var currentColor = dataList[i].color;

            for (int j = 0; j < ListEstrategias.Count; j++)
            {
                Console.WriteLine("estrategia: " + ListEstrategias[j].comparacaoUm + " " + ListEstrategias[j].comparacaoDois + " " + ListEstrategias[j].comparacaoTres + " " + ListEstrategias[j].comparacaoQuatro);
                Console.WriteLine("ultima rodada: " + dataList[0].color + " " + dataList[1].color + " " + dataList[2].color + " " + dataList[3].color);
                Console.WriteLine("entrou no forech");
                if (ListEstrategias[j].comparacaoUm == dataList[0].color && ListEstrategias[j].comparacaoDois == dataList[1].color && ListEstrategias[j].comparacaoTres == dataList[2].color && ListEstrategias[j].comparacaoQuatro == dataList[3].color)
                {
                    // executa se a estrategia for igual a cor escrita
                    if (ListEstrategias[j].color == 0)
                    {
                        Console.Write(ListEstrategias[j].comparacaoUm.ToString() + " " + ListEstrategias[j].comparacaoDois.ToString() + " " + ListEstrategias[j].comparacaoTres.ToString() + " " + ListEstrategias[j].comparacaoQuatro.ToString());
                        CompararDados("ENTRAR NO BRANCO", ListEstrategias[j].color, dataList[0].id, 0);
                        return new Resultado { estrategia = "ENTRAR NO BRANCO", corSelecionada = ListEstrategias[j].color, id = dataList[0].id, colorProteger = ListEstrategias[j].colorProteger };
                    }
                    else if (ListEstrategias[j].color == 1)
                    {

                        if (ListEstrategias[j].colorProteger == 0)
                        {
                            //mandou proteger no BRANCO
                            Console.Write(ListEstrategias[j].comparacaoUm.ToString() + " " + ListEstrategias[j].comparacaoDois.ToString() + " " + ListEstrategias[j].comparacaoTres.ToString() + " " + ListEstrategias[j].comparacaoQuatro.ToString());
                            CompararDados("ENTRAR NO VERMELHO PROTEGER NO BRANCO", ListEstrategias[j].color, dataList[0].id, ListEstrategias[j].colorProteger);
                            return new Resultado { estrategia = "ENTRAR NO VERMELHO PROTEGER NO BRANCO", corSelecionada = ListEstrategias[j].color, id = dataList[0].id, colorProteger = ListEstrategias[j].colorProteger };
                        }
                        else if (ListEstrategias[j].colorProteger != 0)
                        {
                            //mandou entrar no vermelho
                            Console.Write(ListEstrategias[j].comparacaoUm.ToString() + " " + ListEstrategias[j].comparacaoDois.ToString() + " " + ListEstrategias[j].comparacaoTres.ToString() + " " + ListEstrategias[j].comparacaoQuatro.ToString());
                            CompararDados("ENTRAR NO VERMELHO", ListEstrategias[j].color, dataList[0].id, 0);
                            return new Resultado { estrategia = "ENTRAR NO VERMELHO", corSelecionada = ListEstrategias[j].color, id = dataList[0].id, colorProteger = ListEstrategias[j].colorProteger };
                        }


                    }
                    else if (ListEstrategias[j].color == 2)
                    {
                        if (ListEstrategias[j].colorProteger == 0)
                        {
                            //mandou proteger no BRANCO e entrar no PRETO
                            Console.Write(ListEstrategias[j].comparacaoUm.ToString() + " " + ListEstrategias[j].comparacaoDois.ToString() + " " + ListEstrategias[j].comparacaoTres.ToString() + " " + ListEstrategias[j].comparacaoQuatro.ToString());
                            CompararDados("ENTRAR NO PRETO PROTEGER NO BRANCO", ListEstrategias[j].color, dataList[0].id, ListEstrategias[j].colorProteger);
                            return new Resultado { estrategia = "ENTRAR NO PRETO PROTEGER NO BRANCO", corSelecionada = ListEstrategias[j].color, id = dataList[0].id, colorProteger = ListEstrategias[j].colorProteger };
                        }
                        else if (ListEstrategias[j].colorProteger != 0)
                        {
                            //mandou entrar no Preto sem proteçao
                            Console.Write(ListEstrategias[j].comparacaoUm.ToString() + " " + ListEstrategias[j].comparacaoDois.ToString() + " " + ListEstrategias[j].comparacaoTres.ToString() + " " + ListEstrategias[j].comparacaoQuatro.ToString());
                            CompararDados("ENTRAR NO PRETO", ListEstrategias[j].color, dataList[0].id, 0);
                            return new Resultado { estrategia = "ENTRAR NO PRETO", corSelecionada = ListEstrategias[j].color, id = dataList[0].id, colorProteger = ListEstrategias[j].colorProteger };
                        }

                    }
                    else
                    {
                        Console.Write(ListEstrategias[j].comparacaoUm.ToString() + " " + ListEstrategias[j].comparacaoDois.ToString() + " " + ListEstrategias[j].comparacaoTres.ToString() + " " + ListEstrategias[j].comparacaoQuatro.ToString());
                        Console.WriteLine("Erro na hora de mandar entrar!");
                        return new Resultado { estrategia = "ERRO", corSelecionada = ListEstrategias[j].color, id = dataList[0].id, colorProteger = 0 };
                    }

                }
                else
                {
                    return new Resultado { estrategia = "ERRO", corSelecionada = ListEstrategias[j].color, id = dataList[0].id, colorProteger = 0 };
                }
            }
        }

        return new Resultado { estrategia = "ERRO", corSelecionada = 1, id = dataList[0].id, colorProteger = 0 };












    }


    public static async void CompararDados(string estrategia, int corSelecionada, string id, int corProtegida)
    {
        HttpClient httpClient = new HttpClient();
        var response = httpClient.GetAsync("https://api-v2.blaze.com/roulette_games/recent").Result;

        if (response.IsSuccessStatusCode)
        {
            var jsonString = response.Content.ReadAsStringAsync().Result;
            var data = JsonConvert.DeserializeObject<dynamic>(jsonString);

            for (int i = 0; i < 1; i++)
            {
                if (estrategia == "ENTRAR NO BRANCO")
                {
                    // entrar no branco
                    Console.WriteLine("Entrar no branco!");
                    // funcao para ver se a rodada acabou3
                    TimeSpan retryInterval = TimeSpan.FromSeconds(1);

                    int resultado = await VerificarId(id);

                    while (resultado == 2)
                    {
                        await Task.Delay(retryInterval);
                        resultado = await VerificarId(id);
                    }

                    // funcao que chama a conferencia de resultado 

                    int resultadoConferido = await CompareColor(corSelecionada);

                    if (resultadoConferido == 1)
                    {
                        //caso der green
                        //mensagem de green no branco
                        Console.WriteLine("DEU GREEEEEEN NO BRANCO!!!!");
                        await Iniciar();
                    }
                    else
                    {
                        //deu red
                        //mensagem de red
                        Console.WriteLine("RED,CONTABILIZADO!");
                        await Iniciar();

                    }




                }
                else if (estrategia == "ENTRAR NO VERMELHO PROTEGER NO BRANCO")
                {
                    //entrar no vermelho protegendo no branco
                    Console.WriteLine("Entrar no vermelho e proteger no branco!");
                    // funcao para ver se a rodada acabou
                    TimeSpan retryInterval = TimeSpan.FromSeconds(1);

                    int resultado = await VerificarId(id);

                    while (resultado == 2)
                    {
                        await Task.Delay(retryInterval);
                        resultado = await VerificarId(id);
                    }

                    // funcao que chama a conferencia de resultado 

                    int resultadoConferido = await CompareColorProteger(corSelecionada, corProtegida);

                    if (resultadoConferido == 1)
                    {
                        //caso der green

                        await Iniciar();
                    }
                    else
                    {
                        //deu red
                        //mensagem de red
                        Console.WriteLine("RED,CONTABILIZADO!");
                        await Iniciar();

                    }
                }
                else if (estrategia == "ENTRAR NO VERMELHO")
                {
                    //entrar no vermelho
                    Console.WriteLine("Entrar no vermelho!");
                    // funcao para ver se a rodada acabou
                    TimeSpan retryInterval = TimeSpan.FromSeconds(1);

                    int resultado = await VerificarId(id);

                    while (resultado == 2)
                    {
                        await Task.Delay(retryInterval);
                        resultado = await VerificarId(id);
                    }

                    // funcao que chama a conferencia de resultado 

                    int resultadoConferido = await CompareColor(corSelecionada);

                    if (resultadoConferido == 1)
                    {
                        //caso der green

                        await Iniciar();
                    }
                    else
                    {
                        //deu red
                        //mensagem de red
                        Console.WriteLine("RED,CONTABILIZADO!");
                        await Iniciar();

                    }
                }
                else if (estrategia == "ENTRAR NO PRETO PROTEGER NO BRACNO")
                {
                    //entrar no preto protegendo no branco
                    Console.WriteLine("Entrar no preto e rpoteger no branco!");
                    // funcao para ver se a rodada acabou
                    TimeSpan retryInterval = TimeSpan.FromSeconds(1);
                    int resultado = await VerificarId(id);

                    while (resultado == 2)
                    {
                        await Task.Delay(retryInterval);
                        resultado = await VerificarId(id);
                    }

                    // funcao que chama a conferencia de resultado 

                    int resultadoConferido = await CompareColorProteger(corSelecionada, corProtegida);

                    if (resultadoConferido == 1)
                    {
                        //caso der green
                        //mensagem de green no 
                        Console.WriteLine("GREEN NO PRETO");
                        await Iniciar();
                    }
                    else
                    {
                        //deu red
                        //mensagem de red
                        Console.WriteLine("RED,CONTABILIZADO!");
                        await Iniciar();

                    }
                }
                else if (estrategia == "ENTRAR NO PRETO")
                {
                    //entrar no preto
                    Console.WriteLine("Entrar no preto!");
                    // funcao para ver se a rodada acabou
                    TimeSpan retryInterval = TimeSpan.FromSeconds(1);

                    int resultado = await VerificarId(id);

                    while (resultado == 2)
                    {
                        await Task.Delay(retryInterval);
                        resultado = await VerificarId(id);
                    }

                    // funcao que chama a conferencia de resultado 

                    int resultadoConferido = await CompareColor(corSelecionada);

                    if (resultadoConferido == 1)
                    {
                        //caso der green
                        //mensagem de green no preto
                        Console.WriteLine("GREEN NO PRETO");
                        await Iniciar();
                    }
                    else
                    {
                        //deu red
                        //mensagem de red
                        Console.WriteLine("RED,CONTABILIZADO!");
                        await Iniciar();

                    }
                }

            }
            Console.WriteLine("Errou!");
            await Iniciar();
        }
        else
        {
            Console.WriteLine("Não foi possível obter os dados da API.");
        }
    }

    public static async Task<int> VerificarId(string id)
    {
        var apiUrl = "https://api-v2.blaze.com/roulette_games/recent";
        var httpClient = new HttpClient();
        var response = await httpClient.GetAsync(apiUrl);
        response.EnsureSuccessStatusCode();
        var responseBody = await response.Content.ReadAsStringAsync();
        var dataList = System.Text.Json.JsonSerializer.Deserialize<List<Data>>(responseBody);

        if (dataList[0].id != id)
        {
            // retorna 1 se o id for diferente  
            return 1;
        }
        else
        {
            // retorna 2 se o id for igual
            return 2;
        }
    }

    public class RecentGame
    {
        public string id { get; set; }
        public DateTime created_at { get; set; }
        public int color { get; set; }
        public int roll { get; set; }
        public string server_seed { get; set; }
    }


    public static async Task<int> CompareColor(int color)
    {
        var httpClient = new HttpClient();
        var response = await httpClient.GetAsync(ApiUrl);
        response.EnsureSuccessStatusCode();
        var responseBody = await response.Content.ReadAsStringAsync();
        var dataList = System.Text.Json.JsonSerializer.Deserialize<List<Data>>(responseBody);

        // Último resultado da roleta é o primeiro item da lista
        var lastResult = dataList[0];

        if (lastResult.color == color)
        {
            return 1;
        }
        else
        {
            return 2;
        }
    }


    public static async Task<int> CompareColorProteger(int color, int colordois)
    {
        var httpClient = new HttpClient();
        var response = await httpClient.GetAsync(ApiUrl);
        response.EnsureSuccessStatusCode();
        var responseBody = await response.Content.ReadAsStringAsync();
        var dataList = System.Text.Json.JsonSerializer.Deserialize<List<Data>>(responseBody);

        // Último resultado da roleta é o primeiro item da lista
        var lastResult = dataList[0];

        if (lastResult.color == color)
        {
            return 1;
        }
        else if (lastResult.color == colordois)
        {
            return 1;
        }
        else
        {
            return 2;
        }
    }





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

public class Data
{
    public string id { get; set; }
    public DateTime created_at { get; set; }
    public int color { get; set; }
    public int roll { get; set; }
    public string server_seed { get; set; }
}










