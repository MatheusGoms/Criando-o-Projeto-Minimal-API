Criação da Minimal API:
   - Usar o .NET CLI para criar um novo projeto de Minimal API.
   - Definir os endpoints para registro, consulta e gerenciamento de veículos.
Incorporação de administradores e autenticação JWT:
   - Implementar autenticação e autorização usando JWT (JSON Web Tokens).
   - Definir regras para que apenas administradores possam acessar/alterar determinados recursos.
Uso do Swagger:
   - Configurar o Swagger para documentação e teste interativo dos endpoints da API.
   - Explorar os recursos do Swagger UI para testar as rotas e visualizar os modelos de dados.
Implementação de testes:
   - Criar testes automatizados para os endpoints da API, garantindo que tudo funcione como esperado.
   - Cobrir cenários de sucesso e falha, incluindo autenticação e autorização.


dotnet new webapi -n VeiculosApi --no-https
cd VeiculosApi
