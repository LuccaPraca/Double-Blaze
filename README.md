<div id="top"></div>


<br />
<div align="center">
  <a style="text-decoration: none;" href="https://blaze.com/r/KOGDR9">
    <img src="https://imgur.com/tnBL4BP.png" alt="Logo" width="auto" height="80">
  </a>

  <h2 align="center">Blaze Double</h2>
</div>

## 🤖 Bot Blaze DOuble Com Sala de Sinais

Esse programa e uma ferramenta para automatizar suas análises e envio de mensagem em grupos/canal do telegram , com ele você adicionará os padrões que você irá seguir e a forma no qual o robô irá analisar e de forma automática pela API da blaze e enviará de forma automatica mensagens para seu grupo/canal os sinais escolhidos.

# 💻requisitos💻 #
-visual Studio 2022 community

-.net 6.0

-conhecimento básico em c# para alterar as estratégias

## 🚀instruções de uso🚀 ##
 # uso com as estratégias já programadas #
Para executar o programa basta baixar o projeto e subistituir as seguintes variaveis: 
botClient = new TelegramBotClient("TELEGRAM_BOT_TOKEN_AQUI"); , e subistituir "TELEGRAM_BOT_TOKEN_AQUI" pelo token do bot do telegram criado no BotFather.



long chatId = "ID_SALA_TELEGRAM"; , e subistituir "ID_SALA_TELEGRAM" pelo id do grupo/canal do telegram obitido encaminhando uma mensagem do grupo para o bot JsonDumpBot no privado.

 # uso com estrategias personalizadas #
Para personalizar estrategias nesse bot e uma maneira muito simples. e so voce ir na classe "EstrategiaManager" e instanciar uma nova <br />
Estrategias estrategiasum = new Estrategias() { color = 1, colorProteger = 0, comparacaoUm = 1, comparacaoDois = 2, comparacaoTres = 1, comparacaoQuatro = 2 };<br />
<br />
color = cor que voce quer jogar;<br />
color proteger = CorDeProteção(sempre colocar Branco);<br />
comparacaoUm = ultima cor que saiu na blaze;<br />
comparacaoDois = penultima cor que saiu na blaze;<br />
comparacaoTres = antepenultima cor que saiu na blaze;<br />
comparacaoQuarto = quartaultima cor que saiu na blaze;<br />
<br />
<br />
<br />
apos fazer isso e so adiciona a estrategia na lista e ser feliz : <br />
<br />
Listadeestrategias.Add(estrategiasum);<br />




# contato # 

telegram: [@Luccapraca](https://web.telegram.org/k/#@Luccapraca).

  

  
  
  ### License

Copyright © 2023, [Lucca Peixoto Praça](https://github.com/LuccaPraca).
