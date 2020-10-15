# rabbitmq-net-core

  - Projeto desenvolvido em C# .net core 3.1
  - Foi usado o RabbitMQ para desenvolver a mensageria 
  - O projeto foi sepadaro em dois serviços console, onde um envia as mensagens para a fila(Send) e o outro recebe as mensagens(Receive)
  

# Intalando o RappiMQ em um conteiner docker no Linux(ubuntu 20.04)
### Pré requisito
 - Ter o [Docker](https://docs.docker.com/engine/install/ubuntu/) instalado 
### Instruções

1) Crie o diretório abaixo para persistir os dados fora do conteiner.
```sh
mkdir -p /docker/rabbitmq/data
```
2) Baixe a imagem Docker do RabbitMQ.
```sh
VERSAO=3-management
export VERSAO
docker pull rabbitmq:$VERSAO
```
Use o comando abaixo para listar as imagens obtidas.
```sh
docker images
```
3) Execute o conteiner do RabbitMQ.
```sh
docker run -d --name rabbitmq \
 -p 5672:5672 \
 -p 15672:15672 \
 --restart=always \
 --hostname rabbitmq-master \
 -v /docker/rabbitmq/data:/var/lib/rabbitmq \
 rabbitmq:$VERSAO
```
4) Acesse o RabbitMQ na URL http://localhost:15672. O login padrão é guest e a senha é guest.
5) Se quiser parar o conteiner, é só executar o comando abaixo.
```sh
docker stop rabbitmq
```
6) Para iniciá-lo novamente, execute o comando abaixo.
```sh
docker start rabbitmq
```
referência: http://blog.aeciopires.com/instalando-o-rabbitmq-via-docker/
