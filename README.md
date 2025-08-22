Como usar
Rode a aplicação:
   No terminal, na pasta do projeto, execute:
   bash    dotnet run    
Acesse o Swagger UI:
   Navegue até http://localhost:5000/swagger (ou a porta exibida no terminal).
Faça login:
   Use o endpoint /login com:
   json    {      "nome": "admin",      "senha": "123456"    }    
   O retorno será um token JWT.
Use o token:
   Nos endpoints protegidos (como POST para /veiculos), clique em "Authorize" no Swagger e cole o token retornado como:
       Bearer {seu_token}    
