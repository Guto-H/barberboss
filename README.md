## Sobre o projeto
API desenvolvida em .NET 8 adota príncipios ***Domain-Driven Design (DDD)*** e ***SOLID*** para oferecer solução estruturada e eficaz no gerenciamento de uma barbearia. Possuindo como objetivo principal o gerenciamento do faturamento da barbearia a API oferece ao seu usuario as funcionalidades de registrar um faturamento, onde o mesmo poderá detalhar informações como título, descrição, data e hora, valor recebido e tipo de recebimento pelo serviço prestado com dados sendo armazenados de forma segura em um banco de dados ***MySQL***.

A arquitetura da API está totalmente baseada em ***REST***, utilizando métodos ***HTTP*** padrão focando em uma comunicação simples porem eficiente com seu usuário. Pensando em colaborar com futuros desenvolvedores que possam utilizar a API, a mesma foi complementada com uma ***documentação Swagger***, proporcionando uma interface gráfica interativa, permitindo de maneira facilitada uma melhor exploração e realização de testes com os endpoints.

### Features

- **Domain-Driven Design (DDD)**: Estrutura modular que facilita o entendimento e a manutenção do domínio da aplicação.

- **Testes de Unidade**: Testes abrangentes com FluentAssertions para garantir a funcionalidade e a qualidade.

- **Geração de Relátorios**: Capacidade de exportar relatórios detalhados para PDF e Excel, oferecendo uma análise visual e eficaz dos faturamentos.

- **RESTful API com Documentação Swagger**: Interface documentada que facilita a integração e o teste por parte dos desenvolvedores.

### Pacotes NuGet
Nesta API foi utilizado pacotes NuGet

- **AutoMapper**: Responsável pelo mapeamento entre objetos de domínio e requisição/resposta, permitindo um código mais enxuto e automático sem necessidade de repetição.

- **Bogus**: Atua na produção de testes de unidade, auxiliando no envio de dados randomicos para as diversas situações, com isso mantemos o código em seu devido funcionamento.

- **FluentAssertions**: Utilizado nos testes de unidades, permitindo verificações legíveis, auxiliando na escrita de testes claros e compreensíveis.

- **FluentValidation**: Usado para implementar regras de validação de maneira simples e intuitiva nas classes de requisições, mantendo um código limpo.

- **EntityFramework**: Simplifica as interações com o banco de dados atuando como ORM (Object-Relational Mapper), permitindo a utilização de objetos .NET para manipular dados diretamente, sem a necessidade de lidar com consultas SQL.

### Construido com

![Windows-Badge]
![.Net-Badge]
![MySQL]
![Visual-Studio]
![Swagger-Badge]


## Requisitos
* Visual Studio versão 2024+ ou Visual Studio Code

* Windows 10+ ou Linux/MacOS com [.NET SDK][net-sdk-link] instalado

* MySQL Server

### Instalação
1. Clone o repositório:
 ```sh
    git clone https://github.com/Guto-H/barberboss.git
 ```
 
2. Preencha as informações do banco de dados no arquivo `appsettings.Development.json`

3. Execute a API e faça seus testes.



<!-- LINKS -->

[net-sdk-link]: https://dotnet.microsoft.com/en-us/download/dotnet/8.0

<!-- BADGES -->
[Windows-Badge]: https://img.shields.io/badge/Windows-blue?style=for-the-badge&logo=windows

[.Net-Badge]: https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white

[MySQL]: https://img.shields.io/badge/MySQL-005C84?style=for-the-badge&logo=mysql&logoColor=white

[Visual-Studio]: https://img.shields.io/badge/Visual_Studio-5C2D91?style=for-the-badge&logo=visual%20studio&logoColor=white

[Swagger-Badge]: https://img.shields.io/badge/SWAGGER-darkgreen?style=for-the-badge&logo=swagger

