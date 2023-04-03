## Conversor de Queries escritas no Clarion para SQL ##

Quando trabalhei na empresa [Softbus Consultoria e Informática Ltda](https://www.softbus.net.br/), utilizavamos uma linguagem de programação chamada **Clarion** e por uma questão de perfomance, boa parte das Queries eram escritas diretamente no código, concatenando as strings com as variáveis a serem consultadas.

Muitas vezes prcisavamos copiar essas queries para rodar no banco, para realizar alguma verificação ou manutenção, mas tinhamos que perder algum tempo limpando a query e substituindo os valores das variáveis. 

Para facilitar esse trabalho, criei esse programa desktop, em WPF, que permitia copiar a query como estava no código e o programa reconhecia quais campos eram variaveis e adicionava inputs para informar os valores, após isso se obitinha a query pronta para rodar diretamente no banco.
