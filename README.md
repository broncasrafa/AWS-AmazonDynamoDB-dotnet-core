# AWS-AmazonDynamoDB-dotnet-core
Developing .NET 7 Application with DynamoDB on AWS. It's a practice that learn how to create an ASP.NET Core web API application that interacts with Amazon Web Services (AWS) DynamoDB.


![Csharp](https://img.shields.io/badge/csharp-019733?&style=for-the-badge&logo=csharp&logoColor=white)
![.NET7](https://img.shields.io/badge/.NET7-512BD4?logo=.net&logoColor=ffffff&style=for-the-badge)
![Visual Studio](https://img.shields.io/badge/VisualStudio-6C33AF?logo=visual%20studio&style=for-the-badge)
![AWS](https://img.shields.io/badge/AWS-%23FF9900.svg?style=for-the-badge&logo=amazon-aws&logoColor=white)
![AmazonDynamoDB](https://img.shields.io/badge/Amazon%20DynamoDB-4053D6?style=for-the-badge&logo=Amazon%20DynamoDB&logoColor=white)
[![LinkedIn](https://img.shields.io/badge/linkedin-%230077B5.svg?style=for-the-badge&logo=linkedin&logoColor=white)](https://www.linkedin.com/in/rafael-francisco-44750522/)



## Features
Working with Items in DynamoDB Using the Object Persistence Model
Working with Items in DynamoDB Using the Document Model
Working with Items in DynamoDB Using the Low Level Model

- Object Persistence Model - é um wrapper em torno do modelo de baixo nivel (Low Level Model). Podemos armazenar, carregar e consultar o DynamoDB com esse modelo, é o mais simples de desenvolver. Esse modelo nos permite mapear nossas classes do lado do cliente para a nossa tabela do DynamoDB. Cada instancia do objeto é então mapeada para um item na tabela correspondente. Embora possamos fazer a maioria das coisas com esse modelo, um dos grandes recursos ausentes é a capacidade de criar, atualizar e excluir tabelas do DynamoDB.

- Document Model: é um wrapper em torno do modelo de baixo nivel (Low Level Model). As classes primarias do Document Model são as tabelas e a classe de documento. Usamos a classe Table para Put,Get e Delete itens e também podemos usar os metodos de Scan e Query dentro desse modelo. Assim como o Object Persistence Model, não podemos criar, atualizar e excluir tabelas no DynamoDB.

- Low Level Model: é o terceiro modelo fornecido para interagir com o DynamoDB dentro da aplicação .NET. Este modelo nos fornece todos os recursos oferecidos pelo DynamoDB, incluindo a habilidade de criar, atualizar e deletar tables.