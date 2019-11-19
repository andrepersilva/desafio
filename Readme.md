# Desafio Criacao do Projeto para Administracao de Ponto de Venda

## Projeto Desenvolvido em .Net Core 3.0 com Docker

No projeto foi utilizado .net Core 3.0, com banco de dados NoSQL  MongoDB 4.2, para tratamento do GeoJson, nativo desse banco.

Foi construido o DockerCompose , onde  foi feita a carga e a criacao do indices necessarios usando bash. 

Os Parametros abaixo sao necessarios validacao para execucao da aplicacao. 

    environment:  
      - MONGO_HOST=mongo
      - MONGO_PORT=27017
      - MONGODB_DATABASE=dbpdv
    volumes:
      - c:/mongocarga:/mongocarga

O Volume foi feito com base no windows para outros sistemas operacionais mudar o caminho. 

O Arquivo pdv.json devera ser copiado previamente para a pasta mongocarga, foram feitas algumas alteracoes para facilitar a carga
dentro do mongo usando o mongo import.

Alguns metodos nao foram implementados devido a falta de definicao do escopo.

Segue alguns exemplos de chamadas: 

https://localhost:44353/Pdv/GetByLocation http Post 
body 
{
   "longitude" : -43.36556, 
   "latitude" : -22.99669
}

https://localhost:44353/Pdv/getbyid/12 http Get

https://localhost:44353/pdv/InsertPdv/ http Post

  "id": "100",
          "tradingName": "Adega Osasco234322",
          "ownerName": "Ze da Ambev 24234234",
          "document": "02.453.416/000170",
          "coverageArea": {
             "type": "MultiPolygon",
             "coordinates": [
                [
                   [
                      [
                         -43.36556,
                         -22.99669
                      ],
                      [
                         -43.36539,
                         -23.01928
                      ],
                      [
                         -43.26583,
                         -23.01802
                      ],
                      [
                         -43.25724,
                         -23.00649
                      ],
                      [
                         -43.23355,
                         -23.00127
                      ],
                      [
                         -43.2381,
                         -22.99716
                      ],
                      [
                         -43.23866,
                         -22.99649
                      ],
                      [
                         -43.24063,
                         -22.99756
                      ],
                      [
                         -43.24634,
                         -22.99736
                      ],
                      [
                         -43.24677,
                         -22.99606
                      ],
                      [
                         -43.24067,
                         -22.99381
                      ],
                      [
                         -43.24886,
                         -22.99121
                      ],
                      [
                         -43.25617,
                         -22.99456
                      ],
                      [
                         -43.25625,
                         -22.99203
                      ],
                      [
                         -43.25346,
                         -22.99065
                      ],
                      [
                         -43.29599,
                         -22.98283
                      ],
                      [
                         -43.3262,
                         -22.96481
                      ],
                      [
                         -43.33427,
                         -22.96402
                      ],
                      [
                         -43.33616,
                         -22.96829
                      ],
                      [
                         -43.342,
                         -22.98157
                      ],
                      [
                         -43.34817,
                         -22.97967
                      ],
                      [
                         -43.35142,
                         -22.98062
                      ],
                      [
                         -43.3573,
                         -22.98084
                      ],
                      [
                         -43.36522,
                         -22.98032
                      ],
                      [
                         -43.36696,
                         -22.98422
                      ],
                      [
                         -43.36717,
                         -22.98855
                      ],
                      [
                         -43.36636,
                         -22.99351
                      ],
                      [
                         -43.36556,
                         -22.99669
                      ]
                   ]
                ]
             ]
          },
          "address": {
             "type": "Point",
             "coordinates": [
                -43.297337,
                -23.013538
             ]
          }
       }

	   Obrigado, 

	   Andre
