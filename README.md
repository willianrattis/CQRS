# CQRS 

#### Sobre o padrão arquitetural CQRS 

É um padrão muito utilizado em micro serviços que necessitam de uma alta demanda no consumo de dados, onde o foco princípal é **separar** os meios de **leitura** e **escrita** de dados. Alterações de dados são realizado via **Commands** e Leitura de dados via **Queries**. 
Tendo como uma das suas principais características uma banco de leitura e outro para escrita.

Desafios
- Seu principal desafio é manter os dois bancos de dados atualizados pois promove inconsistencia eventual, ex: quando possuímos um banco de **leitura** e outro de **escrita** com os mesmo dados, porém os dados não são consitidos extamente no mesmo momento.

Qualquer aplicação que necessite ler de uma base de dados e escrever em outra tem grande aptidão para trabalhar com CQRS. 
- Exemplo: Um Redis em Cache para ler e um banco de dados MySQL para escrever.

Commands e Queries 
- Commands representam uma intenção de mudança no estado de uma entidade. São muito expressivos e representam uma única intenção do negócio, ex: **AumentarSalarioFuncionarioCommand**.
- Queries são a forma de obter os dados de um banco ou origem de dados para atender as necessidadesda aplicação.
